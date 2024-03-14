using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;  


namespace App_Set_mail
{
    class DataOperations
    {
        #region Connection Credentials

        internal string ConnectionStringCostos = @"Server=" + Globals.CTS_ServerAddress + @";
                                                   Database=" + Globals.CTS_ActiveDB + @";
                                                   user Id=" + Globals.CTS_DB_User + @";
                                                   Password=" + Globals.CTS_DB_Pass + ";";

//        internal string ConnectionStringCostos_History = @"Server=" + Globals.CTSH_ServerAddress + @";
//                                                           Database=" + Globals.CTSH_ActiveDB + @";
//                                                           User Id=" + Globals.CTSH_DB_User + @";
//                                                           Password=" + Globals.CTSH_DB_Pass + ";";

        internal string ConnectionStringConsola = @";Database=" + Globals.CMS_ActiveDB + @";
                                                    User Id=" + Globals.CMS_DB_User + @";
                                                    Password=" + Globals.CMS_DB_Pass + ";";

        internal string ConnectionStringPelletServer = @"Server=" + Globals.CMS_ServerPellet + @";
                                                       Database=" + Globals.CMS_ActiveDB + @";
                                                       User Id=" + Globals.CMS_DB_User + @";
                                                       Password=" + Globals.CMS_DB_Pass + ";";

        internal string ConnectionStringExtruderServer = @"Server=" + Globals.CMS_ServerExtruder + @";
                                                       Database=" + Globals.CMS_ActiveDB + @";
                                                       User Id=" + Globals.CMS_DB_User + @";
                                                       Password=" + Globals.CMS_DB_Pass + ";";

        //        internal string ConnectionStringODOO = @"Provider=PostgreSQL OLE DB Provider;
        //                                                 Data Source=" + Globals.odoo_ServerAddress + @";
        //                                                 location=" + Globals.odoo_ActiveDB + @";
        //                                                 User ID=" + Globals.odoo_DB_User + @";
        //                                                 password=" + Globals.odoo_DB_Pass + @";";

        internal string ConnectionSAP_OnlySELECT = @"Server=" + Globals.SAP_ServerAddress + @";
                                                        Database=" + Globals.SAP_ActiveDB + @";
                                                         User Id=" + Globals.SAP_DB_User + @";
                                                           Password=" + Globals.SAP_DB_Pass + ";";

        internal string ConnectionSAP_OnlySELECT2 = @"Server=" + Globals.SAP2_ServerAddress + @";
                                                        Database=" + Globals.SAP2_ActiveDB + @";
                                                         User Id=" + Globals.SAP2_DB_User + @";
                                                           Password=" + Globals.SAP2_DB_Pass + ";";

        internal string ConnectionStringODOO = @"Server=" + Globals.odoo_ServerAddress + @";
                                                 Port=5432;
                                                 Database=" + Globals.odoo_ActiveDB + @";
                                                 User Id=" + Globals.odoo_DB_User + @";
                                                 Password=" + Globals.odoo_DB_Pass + @";";

        internal string ConnectionStringODOO2 = @"Server=" + Globals.odoo2_ServerAddress + @";
                                                 Port=5432;
                                                 Database=" + Globals.odoo2_ActiveDB + @";
                                                 User Id=" + Globals.odoo2_DB_User + @";
                                                 Password=" + Globals.odoo2_DB_Pass + @";
                                                 CommandTimeout=20;";

        internal string ConnectionStringODOO4 = @"Server=" + Globals.odoo4_ServerAddress + @";
                                                 Port=5432;
                                                 Database=" + Globals.odoo4_ActiveDB + @";
                                                 User Id=" + Globals.odoo4_DB_User + @";
                                                 Password=" + Globals.odoo4_DB_Pass + @";";

        internal string ConnectionStringODOO5 = @"Server=" + Globals.odoo5_ServerAddress + @";
                                                 Port=5432;
                                                 Database=" + Globals.odoo5_ActiveDB + @";
                                                 User Id=" + Globals.odoo5_DB_User + @";
                                                 Password=" + Globals.odoo5_DB_Pass + @";";

        //       internal string ConnectionStringPRININ = @"Server=" + Globals.prinin_ServerAddress + @";
        //                                                   Database=" + Globals.prinin_ActiveDB + @";
        //                                                   User Id=" + Globals.prinin_DB_User + @";
        //                                                   Password=" + Globals.prinin_DB_Pass + ";";

        internal string ConnectionStringAPMS = @"Server=" + Globals.APMS_Server + @";
                                                       Database=" + Globals.APMS_ActiveDB + @";
                                                       User Id=" + Globals.APMS_DB_User + @";
                                                       Password=" + Globals.APMS_DB_Pass + ";";

        internal string ConnectionStringAMS = @"Server=" + Globals.AMS_ServerAddress + @";
                                                   Database=" + Globals.AMS_ActiveDB + @";
                                                   user Id=" + Globals.AMS_DB_User + @";
                                                   Password=" + Globals.AMS_DB_Pass + ";";

        internal string ConnectionStringTicketsAMS_Web = @"Server=" + Globals.Tickets_AMS_ServerAddress + @";
                                                   Database=" + Globals.Tickets_AMS_ActiveDB + @";
                                                   user Id=" + Globals.Tickets_AMS_DB_User + @";
                                                   Password=" + Globals.Tickets_AMS_DB_Pass + ";";


        internal string ConnectionStringALOSY = @"Server=" + Globals.ALOSY_ServerName + @";
                                                   Database=" + Globals.ALOSY_ActiveDB + @";
                                                   user Id=" + Globals.ALOSY_DB_User + @";
                                                   Password=" + Globals.ALOSY_DB_Pass + ";";
        
        //internal int Conf_Mail_port = 3535; 
        internal int Conf_Mail_port = 587;
        //internal string Conf_Mail_SMTP = "smtpout.secureserver.net";
        internal string Conf_Mail_SMTP = "outlook.office365.com";
        internal string Conf_Mail_UserName = "apps@aquafeedhn.net";
        internal string Conf_Mail_Password = "$Applications1620&$";
        internal string Conf_Mail_FromAddress = "apps@aquafeedhn.net";
        internal string Conf_Mail_DisplayName = "Aquafeed Apps";


        /// <summary>
        /// Objeto de conexion para SAP
        /// </summary>
        /// <returns></returns>
        //public SAPbobsCOM.Company Company()
        //{
        //    SAPbobsCOM.Company oCmp;
        //    oCmp = new SAPbobsCOM.Company();
        //    oCmp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
        //    oCmp.LicenseServer = "10.50.11.31";
        //    oCmp.Server = "10.50.11.31";
        //    oCmp.CompanyDB = "AQUA";
        //    oCmp.UserName = "manager";
        //    oCmp.Password = "sapserver";
        //    int result = oCmp.Connect();
        //    if (result != 0)
        //        //CajaDialogo.Information("Conexion exitosa a " + oCmp.CompanyName);
        //    //else
        //        CajaDialogo.Error("No se pudo realizar la coneccion al server de SAP. Error en el objeto company. Error: " + oCmp.GetLastErrorDescription());

        //    return oCmp;
        //}


        //public SAPbobsCOM.Company Company(string puser, string ppass)
        //{
        //    SAPbobsCOM.Company oCmp;
        //    oCmp = new SAPbobsCOM.Company();
        //    oCmp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
        //    oCmp.LicenseServer = "10.50.11.31";
        //    oCmp.Server = "10.50.11.31";
        //    oCmp.CompanyDB = "AQUA";
        //    oCmp.UserName = puser;
        //    oCmp.Password = ppass;
        //    int result = oCmp.Connect();
        //    if (result != 0)
        //        //CajaDialogo.Information("Conexion exitosa a " + oCmp.CompanyName);
        //        //else
        //        CajaDialogo.Error("No se pudo realizar la coneccion al server de SAP. Error en el objeto company. Error: " + oCmp.GetLastErrorDescription());

        //    return oCmp;
        //}


        public string GetConnectionString() 
        {
            return ConnectionStringCostos;
        }

        #endregion

        #region SAP DI API
        public SAPbobsCOM.Company CompanyMake(string puser, string ppass)
        {
            //SAPbobsCOM.Company oCmp;
            //oCmp = new SAPbobsCOM.Company();
            //oCmp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
            //oCmp.LicenseServer = "10.50.11.31";
            //oCmp.Server = "10.50.11.31";
            //oCmp.CompanyDB = "aqua_planillas";
            //oCmp.UserName = "manager";
            //oCmp.Password = "Aquaf33d22";
            SAPbobsCOM.Company oCmp;
            oCmp = new SAPbobsCOM.Company();
            oCmp.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2016;
            oCmp.LicenseServer = Globals.ServerlicenseSDK;
            oCmp.Server = Globals.ServerSDK;
            oCmp.CompanyDB = Globals.ActiveDBSDK;
            oCmp.UserName = puser;
            oCmp.Password = ppass;

            int result = oCmp.Connect();
            if (result != 0)
                //CajaDialogo.Information("Conexion exitosa a " + oCmp.CompanyName);
                //else
                CajaDialogo.Error("No se pudo realizar la coneccion al server de SAP. Error en el objeto company. Error: " + oCmp.GetLastErrorDescription());

            return oCmp;
        }
        #endregion


        #region ACS Related Methods

        /// <summary>
        /// Retorna el numero de materias primas inactivas en una formula
        /// </summary>
        /// <param name="idFormula">El id unico de la formula a buscar</param>
        /// <returns></returns>
        public int get_inactive_rm(int idFormula) 
        {
            DataTable ingredientes = new DataTable();
            int veces = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id_formula", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@peso_total", SqlDbType.Int));

            cmd.Parameters["@id_formula"].Value = idFormula;
            cmd.Parameters["@peso_total"].Value = Convert.ToDouble(2500);

            ingredientes = ACS_Exec_SP_Get_Data("PP_Plan_Ordenes_Get_Ingredientes", cmd);

            foreach (DataRow row in ingredientes.Rows) 
            {
                if (row["estado"].ToString() == "i") 
                {
                    veces++;
                }
            }

            return veces;
        }

        public DateTime Now()
        {
            DateTime date;
            try
            {
                string sql = "select CURRENT_TIMESTAMP";
                SqlConnection conn = new SqlConnection(ConnectionStringCostos);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                date = Convert.ToDateTime(cmd.ExecuteScalar());
            }
            catch (Exception ec)
            {
                date = DateTime.Today;
                CajaDialogo.Error("ACS" + ec.Message);
            }
            return date;
        }

        public DataSet ACS_GetSelectData(string FixedQuery)
        {
            DataSet data = new DataSet();

            SqlConnection Conn = new SqlConnection(ConnectionStringCostos);
            Conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(FixedQuery, Conn);
            adapter.Fill(data);

            Conn.Close();

            return data;
        }

        public int ACS_Exec_SP_GetID(string Procedure_Name, SqlCommand command, SqlParameter returnParameter)
        {
            Int32 ID;

            SqlConnection conn = new SqlConnection(ConnectionStringCostos);

            if (command.CommandType == CommandType.StoredProcedure)
            {
                command.Connection = conn;
                command.CommandText = Procedure_Name;
                conn.Open();
                command.ExecuteNonQuery();

                ID = Convert.ToInt32(returnParameter.Value);

                conn.Close();
            }
            else
            {
                ID = -1;
            }

            return ID;
        }

        public void ACS_Exec_SP(string Procedure_Name, SqlCommand command)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionStringCostos);

                if (command.CommandType == CommandType.StoredProcedure)
                {
                    command.Connection = conn;
                    command.CommandText = Procedure_Name;
                    conn.Open();
                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                CajaDialogo.Error("ACS" + ex.Message);
            }
        }

        public DataTable ACS_Exec_SP_Get_Data(string Procedure_Name, SqlCommand command)
        {
            DataTable data = new DataTable();

            SqlConnection conn = new SqlConnection(ConnectionStringCostos);

            command.CommandText = Procedure_Name;
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 3000;//150; // xJV 29/03/2017

            conn.Open();

            data.Load(command.ExecuteReader());

            conn.Close();

            return data;
        }

        public int ACS_InsertAndReturnID(string Command)
        {
            Int32 InsertedID;
            SqlConnection conn = new SqlConnection(ConnectionStringCostos);
            conn.Open();

            SqlCommand cmd = new SqlCommand(Command, conn);

            InsertedID = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            return InsertedID;
        }

        public void ACS_DoSmallDBOperation(string Command)
        {
            SqlConnection conn = new SqlConnection(ConnectionStringCostos);
            conn.Open();

            SqlCommand cmd = new SqlCommand(Command, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        #endregion

        #region APMS Related Methods

        public DataSet APMS_GetSelectData(string FixedQuery)
        {
            DataSet data = new DataSet();

            SqlConnection Conn = new SqlConnection(ConnectionStringAPMS);
            Conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(FixedQuery, Conn);
            adapter.Fill(data);

            Conn.Close();

            return data;
        }

        public int APMS_Exec_SP_GetID(string Procedure_Name, SqlCommand command, SqlParameter returnParameter)
        {
            Int32 ID;

            SqlConnection conn = new SqlConnection(ConnectionStringAPMS);

            if (command.CommandType == CommandType.StoredProcedure)
            {
                command.Connection = conn;
                command.CommandText = Procedure_Name;
                conn.Open();
                command.ExecuteNonQuery();

                ID = Convert.ToInt32(returnParameter.Value);

                conn.Close();
            }
            else
            {
                ID = -1;
            }

            return ID;
        }

        public void APMS_Exec_SP(string Procedure_Name, SqlCommand command)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionStringAPMS);

                if (command.CommandType == CommandType.StoredProcedure)
                {
                    command.Connection = conn;
                    command.CommandText = Procedure_Name;
                    conn.Open();
                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public DataTable APMS_Exec_SP_Get_Data(string Procedure_Name, SqlCommand command)
        {
            DataTable data = new DataTable();

            SqlConnection conn = new SqlConnection(ConnectionStringAPMS);

            command.CommandText = Procedure_Name;
            command.Connection = conn;
            command.CommandType = CommandType.StoredProcedure;

            conn.Open();

            data.Load(command.ExecuteReader());

            conn.Close();

            return data;
        }

        public int APMS_InsertAndReturnID(string Command)
        {
            Int32 InsertedID;
            SqlConnection conn = new SqlConnection(ConnectionStringAPMS);
            conn.Open();

            SqlCommand cmd = new SqlCommand(Command, conn);

            InsertedID = Convert.ToInt32(cmd.ExecuteScalar());

            conn.Close();

            return InsertedID;
        }

        public void APMS_DoSmallDBOperation(string Command)
        {
            SqlConnection conn = new SqlConnection(ConnectionStringAPMS);
            conn.Open();

            SqlCommand cmd = new SqlCommand(Command, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        #endregion

        #region ODOO Related Methods


        //public DataSet ODOO_GetSelectData(string FixedQuery)
        //{
        //    DataSet data = new DataSet();

        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO);
        //    Conn.Open();

        //    PgSqlDataAdapter adapter = new PgSqlDataAdapter(FixedQuery, Conn);
        //    adapter.Fill(data);

        //    Conn.Close();

        //    return data;
        //}

        //public DataSet ODOO_GetSP_Results(string ProcedureName, PgSqlCommand cmd)
        //{
        //    DataSet data = new DataSet();

        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = ProcedureName;
        //    cmd.Connection = Conn;

        //    Conn.Open();

        //    PgSqlDataAdapter adapter = new PgSqlDataAdapter(cmd);
        //    adapter.Fill(data);

        //    Conn.Close();

        //    return data;
        //}

        //public DataSet ODOO2_GetSP_Results(string ProcedureName, PgSqlCommand cmd)
        //{
        //    DataSet data = new DataSet();

        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO2);

        //    //cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = ProcedureName;
        //    cmd.Connection = Conn;

        //    Conn.Open();

        //    PgSqlDataAdapter adapter = new PgSqlDataAdapter(cmd);
        //    adapter.Fill(data);

        //    Conn.Close();

        //    return data;
        //}

        //public Double ODOO_Get_Exchange_Rate(int currency_id, string date)
        //{
        //    string query = string.Format(@"SELECT rate
	       //                                  FROM public.res_currency_rate
        //                                    WHERE currency_id = {0}
        //                                      AND x_fecha = to_date('{1}','MM-dd-yyyy')", currency_id, date);

        //    return Convert.ToDouble(ODOO_GetSelectData(query).Tables[0].Rows[0][0].ToString());
        //}

        //public void ODOO_Exec_Command(string FixedQuery, PgSqlCommand cmd)
        //{
        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO);
        //    Conn.Open();
        //    PgSqlTransaction trans = Conn.BeginTransaction();

        //    cmd.CommandText = FixedQuery;
        //    cmd.Connection = Conn;
        //    cmd.ExecuteNonQuery();

        //    trans.Commit();
        //    Conn.Close();
        //}

        //public void ACS_Exec_Command(string FixedQuery, PgSqlCommand cmd)
        //{
        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringCostos);
        //    Conn.Open();
        //    PgSqlTransaction trans = Conn.BeginTransaction();

        //    cmd.CommandText = FixedQuery;
        //    cmd.Connection = Conn;
        //    cmd.ExecuteNonQuery();

        //    trans.Commit();
        //    Conn.Close();
        //}

        //public void ODOO_Exec_SP(string procedure_name, PgSqlCommand command) 
        //{
        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO);
        //    Conn.Open();

        //    command.CommandType = CommandType.StoredProcedure;
        //    command.CommandText = procedure_name;
        //    command.Connection = Conn;
        //    command.ExecuteNonQuery();

        //    Conn.Close();
        //}

        //public int ODOO_Exec_SP_GetID(string procedure_name, PgSqlCommand command, PgSqlParameter returnParameter)
        //{
        //    //Antes: SqlParameter returnParameter
        //    //NpgsqlConnection Conn = new NpgsqlConnection(ConnectionStringODOO);
        //    //Conn.Open();

        //    //command.CommandType = CommandType.StoredProcedure;
        //    //command.CommandText = procedure_name;
        //    //command.Connection = Conn;
        //    //command.ExecuteNonQuery();

        //    //Int32 ID = Convert.ToInt32(returnParameter.Value);

        //    //Conn.Close();

        //    //return ID;

        //    Int32 ID;

        //    PgSqlConnection Conn = new PgSqlConnection(ConnectionStringODOO);

        //    command.Connection = Conn;
        //    command.CommandText = procedure_name;
        //    Conn.Open();

        //    command.CommandType = CommandType.StoredProcedure;
        //    //command.ExecuteNonQuery();
        //    ID = Convert.ToInt32(command.ExecuteScalar());

        //    //ID = Convert.ToInt32(returnParameter.Value);

        //    Conn.Close();

        //    return ID;


        //}

        //public int ODOO_InsertAndReturnID(string Command)
        //{
        //    Int32 InsertedID;
        //    PgSqlConnection conn = new PgSqlConnection(ConnectionStringODOO);
        //    conn.Open();

        //    PgSqlCommand cmd = new PgSqlCommand(Command, conn);

        //    InsertedID = Convert.ToInt32(cmd.ExecuteScalar());

        //    conn.Close();

        //    return InsertedID;
        //}

        //public void ODOO_DoSmallDBOperation(string Command)
        //{
        //    PgSqlConnection con = new PgSqlConnection(ConnectionStringODOO);
        //    con.Open();

        //    PgSqlCommand cmd = new PgSqlCommand(Command, con);
        //    cmd.ExecuteNonQuery();

        //    con.Close();
        //}

        #endregion

        #region WinCC_6_2 (Old Production) Related Methods
        
        public void WinCC_Exec_SP(string Procedure_Name, SqlCommand command)
        {
            SqlConnection conn = new SqlConnection(ConnectionStringPelletServer);

            if (command.CommandType == CommandType.StoredProcedure)
            {
                command.Connection = conn;
                command.CommandText = Procedure_Name;
                conn.Open();
                command.ExecuteNonQuery();

                conn.Close();
            }
        }

        public DataSet GetSelectData_SetServer(string FixedQuery, string SelectedServer)
        {
            DataSet data = new DataSet();

            SqlConnection Conn = new SqlConnection(string.Format("Server={0}{1}", SelectedServer, ConnectionStringConsola));
            Conn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter(FixedQuery, Conn);
            adapter.Fill(data);

            Conn.Close();

            return data;
        }

        #endregion

        #region Custom Methods
        //Method Implemented and Improved in the SecOps Class
        //public void SendEmailAlert(DataSet Receivers, int ColumnNumber, string Subject, string Body)
        //{
        //    MailMessage message = new MailMessage();
        //    SmtpClient smtp = new SmtpClient();

        //    message.From = new MailAddress("noreply@aquafeedhn.com", "Quality Data Analysis System v1.0");

        //    foreach (DataRow row in Receivers.Tables[0].Rows)
        //    {
        //        message.To.Add(new MailAddress(row[ColumnNumber].ToString()));
        //    }

        //    message.Subject = Subject;
        //    message.Body = Body;

        //    smtp.EnableSsl = false;
        //    smtp.Port = 80;
        //    smtp.Host = "smtpout.secureserver.net";
        //    smtp.UseDefaultCredentials = true;
        //    smtp.Credentials = new NetworkCredential("scans@aquafeedhn.com", "A1dd1cf460&");
        //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

        //    smtp.Send(message);
        //}
        public int ValidateNumberInt32(object val)
        {
            int valor = 0;
            try
            {
                valor = Convert.ToInt32(val);
                //valor = (int)val;
            }
            catch
            {
                try
                {
                    decimal valDecimal = Convert.ToDecimal(val);
                    valor = Decimal.ToInt32(valDecimal);
                }
                catch
                {
                    valor = 0;
                }
            }
            return valor;
        }
        #endregion

        #region Nutreco Related Methods

        //public DataSet PRININ_GetSelectData(string FixedQuery)
        //{
        //    DataSet data = new DataSet();

        //    SqlConnection Conn = new SqlConnection(ConnectionStringPRININ);
        //    Conn.Open();

        //    SqlDataAdapter adapter = new SqlDataAdapter(FixedQuery, Conn);
        //    adapter.Fill(data);

        //    Conn.Close();

        //    return data;
        //}

        #endregion

        #region Not Implemented Methods

        //public DataSet H_GetSelectData(string FixedQuery)
        //{
        //    DataSet data = new DataSet();

        //    SqlConnection Conn = new SqlConnection(ConnectionStringCostos_History);
        //    Conn.Open();

        //    SqlDataAdapter adapter = new SqlDataAdapter(FixedQuery, Conn);
        //    adapter.Fill(data);

        //    Conn.Close();

        //    return data;
        //}

        //public int H_InsertAndReturnID(string Command)
        //{
        //    Int32 InsertedID;
        //    SqlConnection conn = new SqlConnection(ConnectionStringCostos_History);
        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand(Command, conn);

        //    InsertedID = Convert.ToInt32(cmd.ExecuteScalar());

        //    conn.Close();

        //    return InsertedID;
        //}

        //public void H_DoSmallDBOperation(string Command)
        //{
        //    SqlConnection conn = new SqlConnection(ConnectionStringCostos_History);
        //    conn.Open();

        //    SqlCommand cmd = new SqlCommand(Command, conn);
        //    cmd.ExecuteNonQuery();

        //    conn.Close();
        //}

        public void NewLogEntry(string user, string location, string action)
        {
            string Command = @" INSERT INTO [QDAS].[dbo].[QDAS_Logs]
		                                    ([date],[time],[user],[location],[action])
                                     VALUES (SYSDATETIME(),SYSDATETIME()," + user + ",'" + location + "','" + action + "')";

            SqlConnection conn = new SqlConnection(ConnectionStringCostos);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Command, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        internal int Exec_SP_GetID()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
