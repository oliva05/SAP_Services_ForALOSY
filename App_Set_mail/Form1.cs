using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;
using App_Set_mail.Classes;
using System.Collections;
using Devart.Data.PostgreSql;
using App_Set_mail.Classes.Model;
using System.Net;
using System.Threading;
using System.Security.Cryptography;

namespace App_Set_mail
{
    public partial class Form1 : Form
    {
        int Encendido = 1;
        DataOperations dp = new DataOperations();
        dsReport dsReport = new dsReport();
        string HTML;
        string Creador;
        string fecha;
        string proveedor;
        string Aprovador;
        string fechaCreacion;
        string total;
        string comentario;
        string endHTMl;
        bool enviarC = false;

        public enum StatusConexionSAP
        {
            Desconectado = 0,
            Conectado = 1 
        }

        StatusConexionSAP EstadoConexionActual;

        SqlConnection ConnectionTicketsAMS;
        DataOperations DataOperationGlobal;
        public Form1()
        {

            InitializeComponent();
            DataOperationGlobal = new DataOperations();
            ConnectionTicketsAMS = new SqlConnection(dp.ConnectionStringTicketsAMS_Web);
            EstadoConexionActual = StatusConexionSAP.Desconectado;
            PlayServices();
        }
        
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Encendido = 0;
            //time_sinc_reloj_empleados.Enabled = false;
            lblEstado.Text = "Apagado.";
        }
        public void load_orden(int DocEntry)
        {
            string query = @"sp_obtener_documento_Draft";
            SqlConnection cn = new SqlConnection(dp.ConnectionSAP_OnlySELECT);
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Docentry", DocEntry);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dsReport.encabezado.Clear();
                da.Fill(dsReport.encabezado);

                query = @"sp_obtener_documento_draft_d";
                cmd = new SqlCommand(query, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Docentry", DocEntry);
                da = new SqlDataAdapter(cmd);
                dsReport.detalle.Clear();
                da.Fill(dsReport.detalle);
                cn.Close();
            }
            catch (Exception EX)
            {
                SetErrorGrid("SAP Function load_orden(). Error: " + EX.Message, null);
            }

        }

        public void SetErrorGrid(string error_message, string type_)
        {
            if (string.IsNullOrEmpty(type_))
                type_ = "Error";
            dsReport.exceptionsRow row1 = dsReport1.exceptions.NewexceptionsRow();
            row1.descripcion = error_message;
            row1.fecha = dp.Now();
            row1.tipo = type_;

            dsReport1.exceptions.AddexceptionsRow(row1);
            dsReport1.AcceptChanges();
        }

        private void cmd_stop_Click(object sender, EventArgs e)
        {
            timerLotesPT_SAP.Enabled = false;
            timerLotesPT_SAP.Stop();
            lblEstado.Text = "Apagado";
            Encendido = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayServices();
        }

        void PlayServices()
        {
            timerSubirOrdenesCompra.Enabled =
            timerLotesPT_SAP.Enabled = true;

            timerLotesPT_SAP.Start();
            timerSubirOrdenesCompra.Start();
            lblEstado.Text = "Encendido";
            Encendido = 1;
        }

        
        EmailSendParams parametrosEmail = new EmailSendParams();
      
        private class MailTicketAbierto {
            public int Id { get; set; }
            public int HorasTranscurridas { get; set; }
            public string Correo { get; set; }
            public string Type { get; set; }
            public string Estado { get; set; }
            public string Responsable { get; set; }
            public string Categoria { get; set; }
            public string Asignado { get; set; }
            public string Descripcion { get; set; }
            public DateTime Fecha { get; set; }
        }

        private void EnviarCorreo(EmailSendParams emailSendParams)
        {
            DataOperations dp = new DataOperations();

            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress(dp.Conf_Mail_FromAddress, dp.Conf_Mail_DisplayName);

            foreach (var item in emailSendParams.Destinatarios)
            {
                message.To.Add(item);
            }

            foreach (var item in emailSendParams.Copias)
            {
                message.CC.Add(item);
            }

            //message.To.Add("reuceda05@hotmail.com");
            message.Subject = emailSendParams.Subject;
            message.Body = emailSendParams.Body;
            //message.Body = "<p>Estimado(a) " + nombre + ", reciba un cordial saludo,</p> <p>Se adjunta el archivo de horas trabajas que usted solicitó</p> ";
            message.IsBodyHtml = true;

            smtp.EnableSsl = true;
            smtp.Port = dp.Conf_Mail_port;
            smtp.Host = dp.Conf_Mail_SMTP;
            //smtp.UseDefaultCredentials = true;
            smtp.Credentials = new NetworkCredential(dp.Conf_Mail_UserName, dp.Conf_Mail_Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(message);

        }

        public class ItemTarima
        {
            public int idTarima;
            public string ItemCode;
            public decimal cantidadUnidades;
            public decimal pesoKg;
            public string CodBodega;// 'BG008'as whs,
            public int Lote_producto_termiado;
            public int IdTurno;
            public DateTime FechaVencimiento;
            public DateTime FechaProduccion;
            public string BarcodeTarima;
        }

        private void timerLotesPT_SAP_Tick(object sender, EventArgs e)
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_cant_tarimas_pendientes_subir_a_SAP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                int ContadorTarimasPendientes = 0;
                if (dr.Read())
                {
                    ContadorTarimasPendientes = dr.GetInt32(0);
                }
                dr.Close();

                ArrayList ListaTarimas = new ArrayList();
                if (ContadorTarimasPendientes > 0)
                {

                    if (EstadoConexionActual == StatusConexionSAP.Desconectado)
                    {
                        bool guardado = false;
                        SqlCommand cmd2 = new SqlCommand("sp_get_tarimas_pendientes_subir_a_SAP", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            ItemTarima Tarima = new ItemTarima();
                            Tarima.idTarima = dr2.GetInt32(0);
                            Tarima.ItemCode = dr2.GetString(1);
                            Tarima.cantidadUnidades = dr2.GetDecimal(2);
                            Tarima.pesoKg = dr2.GetDecimal(3);
                            Tarima.CodBodega = dr2.GetString(4);
                            Tarima.Lote_producto_termiado = dr2.GetInt32(5);
                            Tarima.IdTurno = dr2.GetInt32(6);
                            Tarima.FechaProduccion = dr2.GetDateTime(7);
                            Tarima.FechaVencimiento = dr2.GetDateTime(8);
                            Tarima.BarcodeTarima = dr2.GetString(9);
                            //Lista de tarimas
                            ListaTarimas.Add(Tarima);

                            //Resumen de lotes
                            AddTarimaToLoteResumen(Tarima);
                        }
                        dr2.Close();

                        //Posteo de Entrada PT en SAP
                        foreach (dsReport.Lotes_to_SAPRow row in dsReport1.Lotes_to_SAP)
                        {
                            //Header SAP
                            SAPbobsCOM.Company oCmp = dp.CompanyMake("interfaz.alosy", "Aq4x_3Fj2#");
                            SAPbobsCOM.Documents EntryH = oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oInventoryGenEntry);
                            EstadoConexionActual = StatusConexionSAP.Conectado;
                            DateTime HoyDate = dp.Now();
                            EntryH.DocDate = HoyDate;
                            EntryH.TaxDate = HoyDate;
                            EntryH.Comments = "Generado desde Interfaz automática ALOSY";

                            //Detalle de Lineas
                            EntryH.Lines.AccountCode = "_SYS00000000037";
                            EntryH.Lines.WarehouseCode = "BG008";

                            EntryH.Lines.ItemCode = row.ItemCode;
                            EntryH.Lines.Quantity = Convert.ToDouble(row.peso);//Kg
                            EntryH.Lines.UserFields.Fields.Item("U_Sacos").Value = Convert.ToDouble(row.sacos); //Sacos

                            EntryH.Lines.BatchNumbers.Quantity = Convert.ToDouble(row.peso);//KG
                            EntryH.Lines.BatchNumbers.UserFields.Fields.Item("U_Sacos").Value = Convert.ToDouble(row.sacos);//Unidades
                            EntryH.Lines.BatchNumbers.UserFields.Fields.Item("U_U_Turno").Value = row.turno.ToString();
                            EntryH.Lines.BatchNumbers.ExpiryDate = row.fecha_vencimiento;
                            EntryH.Lines.BatchNumbers.ManufacturingDate = row.fecha_prd;
                            EntryH.Lines.BatchNumbers.AddmisionDate = HoyDate;
                            EntryH.Lines.BatchNumbers.BatchNumber = row.lote.ToString();//Lote
                            EntryH.Lines.UnitPrice = 38.00;

                            //EntryH.Lines.BatchNumbers.ManufacturerSerialNumber = tarx.BarcodeTarima; //"Atributo1 _ Dato TARIMA3";
                            EntryH.Lines.Add();//Guardar Linea

                            string errMsg = "";
                            int errNum = 0;

                            errNum = EntryH.Add();//Guardar Header
                            string DocEntry = oCmp.GetNewObjectKey();//Numero header sap
                            string docType = oCmp.GetNewObjectType();
                            int iDocEntryHeaderSAP = dp.ValidateNumberInt32(DocEntry);

                       

                            if (errNum == 0)//Guardo con exito
                            {
                                //Update Tarimas como subidas
                                foreach (ItemTarima tarx in ListaTarimas)
                                {
                                    if (tarx.ItemCode == row.ItemCode && tarx.Lote_producto_termiado == row.lote && iDocEntryHeaderSAP > 0)
                                    {
                                        SqlCommand cmd3 = new SqlCommand("sp_set_tarima_as_up_to_sap_pt", con);
                                        cmd3.CommandType = CommandType.StoredProcedure;
                                        cmd3.Parameters.AddWithValue("@DocEntry", DocEntry);
                                        cmd3.Parameters.AddWithValue("@idTarima", tarx.idTarima);
                                        cmd3.ExecuteNonQuery();
                                    }
                                }
                                //Mensaje de operacion exitosa.
                                SetErrorGrid("Entrada de Mercancias Exitosa! DocEntry: " + iDocEntryHeaderSAP.ToString() + " ", "Notificación");
                            }
                            else
                            {
                                oCmp.GetLastError(out errNum, out errMsg);
                                //Guardar Mensaje en el grid del error
                                SetErrorGrid("timerLotesPT_SAP: " + errMsg, "Error");
                            }

                            try
                            {
                                EstadoConexionActual = StatusConexionSAP.Desconectado;
                                Thread.Sleep(100);
                                oCmp.Disconnect();
                            }
                            catch { }

                        }//End Foreach Lotes resumen para headers de SAP

                        //Limpiar ambas listas para la siguiente ejecucion.
                        dsReport1.Lotes_to_SAP.Clear();
                        ListaTarimas.Clear();
                    }
                }
                else
                {
                    return;//No hay tarimas que procesar
                }

                con.Close();
            }
            catch (Exception ex)
            {
                //CajaDialogo.Error(ec.Message);
                SetErrorGrid("timerLotesPT_SAP: " + ex.Message, "");
            }
        }

        private void AddTarimaToLoteResumen(ItemTarima tarima)
        {
            if(dsReport1.Lotes_to_SAP.Count == 0)
            {
                //Insertar el row inicial
                dsReport.Lotes_to_SAPRow row = dsReport1.Lotes_to_SAP.NewLotes_to_SAPRow();
                row.ItemCode = tarima.ItemCode;
                row.lote = tarima.Lote_producto_termiado;
                row.peso = tarima.pesoKg;
                row.turno = tarima.IdTurno.ToString();
                row.sacos = tarima.cantidadUnidades;
                row.fecha_prd = tarima.FechaProduccion;
                row.fecha_vencimiento = tarima.FechaVencimiento;
                dsReport1.Lotes_to_SAP.AddLotes_to_SAPRow(row);
                dsReport1.AcceptChanges();
                return;
            }

            bool Row_Exist = false;
            foreach (dsReport.Lotes_to_SAPRow rowi in dsReport1.Lotes_to_SAP)
            {
                if (tarima.ItemCode == rowi.ItemCode && tarima.Lote_producto_termiado == rowi.lote)
                {
                    Row_Exist = true;
                }
            }

            //foreach (dsReport.Lotes_to_SAPRow rowi in dsReport1.Lotes_to_SAP)
            //{
            //if(tarima.ItemCode == rowi.ItemCode && tarima.Lote_producto_termiado == rowi.lote)
            if (Row_Exist)
            {
                //Sumar cantidades
                foreach (dsReport.Lotes_to_SAPRow rowi in dsReport1.Lotes_to_SAP)
                {
                    if (tarima.ItemCode == rowi.ItemCode && tarima.Lote_producto_termiado == rowi.lote)
                    {
                        rowi.sacos += tarima.cantidadUnidades;
                        rowi.peso += tarima.pesoKg;
                        break;
                    }
                }
            }
            else
            {
                //Insertar un nuevo row
                dsReport.Lotes_to_SAPRow row = dsReport1.Lotes_to_SAP.NewLotes_to_SAPRow();
                row.ItemCode = tarima.ItemCode;
                row.lote = tarima.Lote_producto_termiado;
                row.peso = tarima.pesoKg;
                row.turno = tarima.IdTurno.ToString();
                row.sacos = tarima.cantidadUnidades;
                row.fecha_prd = tarima.FechaProduccion;
                row.fecha_vencimiento = tarima.FechaVencimiento;
                dsReport1.Lotes_to_SAP.AddLotes_to_SAPRow(row);
                dsReport1.AcceptChanges();
            }
            //}
        }

        private void timerSubirOrdenesCompra_Tick(object sender, EventArgs e)
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("dbo.sp_get_cant_ordenes_compra_pendientes_subir_a_sap", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                int ContadorOrdenesCompraPendientes = 0;
                if (dr.Read())
                    ContadorOrdenesCompraPendientes = dr.GetInt32(0);

                dr.Close();

                ArrayList ListaOrdenesH = new ArrayList();
                if (ContadorOrdenesCompraPendientes > 0)
                {
                    if (EstadoConexionActual == StatusConexionSAP.Desconectado)
                    {
                        bool guardado = false;
                        SqlCommand cmd2 = new SqlCommand("dbo.sp_get_cant_ordenes_compra_pendientes_subir_a_sap_list", con);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            //ItemTarima Tarima = new ItemTarima();
                            int IdOrdenCompra = dr2.GetInt32(0);
                            OrdenCompraALOSY OrdenH = new OrdenCompraALOSY();
                            if (OrdenH.RecuperarRegistro(IdOrdenCompra))
                            {
                                //Lista de ordenes de compra
                                ListaOrdenesH.Add(OrdenH);
                            }
                        }
                        dr2.Close();

                        //Posteo de Orden de Compra en SAP
                        foreach (OrdenCompraALOSY OrdenH_forSAP in ListaOrdenesH)
                        {
                            //Header SAP
                            SAPbobsCOM.Company oCmp = dp.CompanyMake("interfaz.alosy", "Aq4x_3Fj2#");
                            SAPbobsCOM.Documents PO = oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oPurchaseOrders);
                            EstadoConexionActual = StatusConexionSAP.Conectado;
                            //SAPbobsCOM.Documents PO = oCmp.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);
                            DateTime HoyDate = dp.Now();
                            PO.DocDate = OrdenH_forSAP.DocDate;
                            PO.TaxDate = OrdenH_forSAP.TaxDate;
                            PO.DocDueDate = OrdenH_forSAP.DocDueDate;
                            PO.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseOrders;

                            //PO.AuthorizationCode = 

                            //PO.DocumentsOwner = 81;

                            PO.CardCode = OrdenH_forSAP.CardCode;
                            PO.CardName = OrdenH_forSAP.CardName;
                            PO.Address = OrdenH_forSAP.Address;
                            //PO.ContactPersonCode = OrdenH_forSAP.ContactCode;



                            PO.NumAtCard = OrdenH_forSAP.NumAtCard;

                            PO.UserFields.Fields.Item("U_TipoOrden").Value = OrdenH_forSAP.TipoOrden.ToString();
                            //PO.UserFields.Fields.Item("U_AquaExoneracion").Value = OrdenH_forSAP.AquaExoneracion;
                            //PO.UserFields.Fields.Item("U_FechaExoneracion").Value = OrdenH_forSAP.FechaExoneracion;
                            PO.UserFields.Fields.Item("U_incoterm").Value = OrdenH_forSAP.U_incoterm;

                            if (string.IsNullOrEmpty(OrdenH_forSAP.Comments))
                            {
                                PO.Comments = "N/D";
                            }
                            else
                            {
                                if(OrdenH_forSAP.Comments.Length > 253) 
                                {
                                    PO.Comments = OrdenH_forSAP.Comments.Substring(0,253);
                                }
                                else
                                {
                                    PO.Comments = OrdenH_forSAP.Comments;
                                }
                            }

                            PO.DocTotal = Convert.ToDouble(OrdenH_forSAP.DocTotal);
                            
                            string DocCurrency_ = OrdenH_forSAP.DocCur.Trim();
                            PO.DocCurrency = DocCurrency_;
                            //PO.RelatedEntry
                            //PO.VatSum = Convert.ToDouble(OrdenH_forSAP.ISV);

                            //PO.Series = 16;// int.Parse("16N");
                            //PO.DocNum = 211000002;
                            

                            SqlCommand cmdLines = new SqlCommand("sp_get_orden_compra_d_for_sap", con);
                            cmdLines.CommandType = CommandType.StoredProcedure;
                            cmdLines.Parameters.AddWithValue("@id_ordenH", OrdenH_forSAP.Id);
                            SqlDataReader reader = cmdLines.ExecuteReader();
                            while (reader.Read())
                            {
                                PO.Lines.ItemCode = reader.IsDBNull(reader.GetOrdinal("ItemCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("ItemCode"));
                                PO.Lines.ItemDescription = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
                                //PO.Lines.UserFields.Fields.Item("Capitulo_Codigo").Value = reader.IsDBNull(reader.GetOrdinal("Capitulo_Codigo")) ? string.Empty : reader.GetString(reader.GetOrdinal("Capitulo_Codigo"));
                                //PO.Lines.UserFields.Fields.Item("Partida_Arancelaria").Value = reader.IsDBNull(reader.GetOrdinal("Partida_Arancelaria")) ? string.Empty : reader.GetString(reader.GetOrdinal("Partida_Arancelaria"));

                                PO.Lines.Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Quantity")));
                                PO.Lines.Price = reader.IsDBNull(reader.GetOrdinal("Unite_Price")) ? 0 : Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("Unite_Price")));
                                PO.Lines.DiscountPercent = reader.IsDBNull(reader.GetOrdinal("DiscPrcnt")) ? Convert.ToDouble(0.00) : Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("DiscPrcnt")));                                
                                string currency_ = reader.IsDBNull(reader.GetOrdinal("Currency")) ? string.Empty : reader.GetString(reader.GetOrdinal("Currency")).Trim();
                                PO.Lines.Currency = currency_;
                                PO.Lines.TaxCode = reader.IsDBNull(reader.GetOrdinal("TaxCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("TaxCode"));
                                PO.Lines.WarehouseCode = reader.IsDBNull(reader.GetOrdinal("WhsCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("WhsCode")).Trim();
                                PO.Lines.TaxTotal = reader.IsDBNull(reader.GetOrdinal("isv")) ? 0 : Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal("isv")));
                                
                                //if (!string.IsNullOrEmpty(OrdenH_forSAP.AquaExoneracion))
                                //{
                                //    PO.Lines.UserFields.Fields.Item("U_Rubro").Value = "N/D";
                                //    PO.Lines.UserFields.Fields.Item("U_AQUA_CAP").Value = "N/D";
                                //}

                                int IdBaseRef_NumSolicitud = reader.IsDBNull(reader.GetOrdinal("base_ref")) ? 0 : Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("base_ref")));

                                //if (OrdenH_forSAP.DocNumSolicitud > 0)
                                if(IdBaseRef_NumSolicitud>0)
                                {
                                    //PO.Lines.BaseEntry = OrdenH_forSAP.DocNumSolicitud;
                                    //PO.Lines.BaseType = 
                                    PO.Lines.BaseEntry = IdBaseRef_NumSolicitud;
                                    PO.Lines.BaseType = 1470000113;
                                    int LineaNumSolicitud = reader.IsDBNull(reader.GetOrdinal("num_linea_solicitud_d")) ? 0 : Convert.ToInt32(reader.GetInt32(reader.GetOrdinal("num_linea_solicitud_d")));
                                    PO.Lines.BaseLine = LineaNumSolicitud;
                                    //PO.Lines.VatGroup = "IPPN0";
                                    //PO.Lines.AccountCode = "6020-1500";

                                    //-1, 0, 1470000113 = Purchase Request, 17 = Sales Order, 22 = Purchase Orders, 23 = Sales Quotation, 540000006 = Purchase Quotation
                                }

                                if(PO.Lines.Price<=0)
                                {
                                    throw new Exception("Se Intento crear una OC con una linea con precio <=0, id Orden H: " + OrdenH_forSAP.Id.ToString());
                                }

                                PO.Lines.Add();
                            }
                            reader.Close();

                            int res = PO.Add();
                           
                            string errMsg = "";
                            int errNum = 0;
                            if (res == 0)
                            {
                                string DocEntry = oCmp.GetNewObjectKey();
                                OrdenH_forSAP.UpdateStatusOrderH(OrdenH_forSAP.Id, dp.ValidateNumberInt32(DocEntry));
                                //MessageBox.Show("Add Purchase Order successfull");
                                SetErrorGrid("Creacion de OC Exitosa! DocEntry: " + DocEntry + " ", "Notificación");
                            }
                            else
                            {
                                // MessageBox.Show(ocompany.GetLastErrorDescription()); //@scope_identity
                                oCmp.GetLastError(out errNum, out errMsg);
                                //Guardar Mensaje en el grid del error
                                SetErrorGrid("timerSubirOrdenesCompra: " + errMsg, "Error");
                            }

                            try
                            {
                                EstadoConexionActual = StatusConexionSAP.Desconectado;
                                Thread.Sleep(100);
                                oCmp.Disconnect();
                                //EstadoConexionActual = StatusConexionSAP.Desconectado;
                            }
                            catch { }

                        }//End Foreach Lotes resumen para headers de SAP

                        //Limpiar ambas listas para la siguiente ejecucion.
                        dsReport1.Lotes_to_SAP.Clear();
                        ListaOrdenesH.Clear();
                        con.Close();
                    }
                    
                }
                else
                {
                    EstadoConexionActual = StatusConexionSAP.Desconectado;
                    return;//No hay tarimas que procesar
                }
            }
            catch (Exception ex)
            {
                EstadoConexionActual = StatusConexionSAP.Desconectado;
                //CajaDialogo.Error(ec.Message);
                SetErrorGrid("timerSubirOrdenesCompra: " + ex.Message, "");
            }
        }
    }
}
