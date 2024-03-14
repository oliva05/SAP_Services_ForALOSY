using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Set_mail.Classes
{
    public class Functions
    {
        string destinatario;
        string copia;
        public Functions()
        {

        }

        public string Destinatario { get => destinatario; set => destinatario = value; }
        public string Copia { get => copia; set => copia = value; }

        public bool GenerarEmailAsistencia()
        {
            bool r = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("[sp_get_si_debe_evaluar_reporte_asistencia_rrhhv2]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@idbodega", idBodega);
                r = Convert.ToBoolean(cmd.ExecuteScalar()); //Devuelve true para poder revisar el tema de reportes

                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_si_debe_evaluar_reporte_asistencia_rrhh " + ec.Message);
                //SetErrorGrid("Function sp_insert_en_odrf_enviadas SQL Store Procedure. Error: " + ex.Message);
            }
            return r;
        }

        public bool GenerarEmailAsistenciaNocturna()
        {
            bool r = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("[sp_get_si_debe_evaluar_reporte_asistencia_rrhhv3]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@idbodega", idBodega);
                r = Convert.ToBoolean(cmd.ExecuteScalar()); //Devuelve true para poder revisar el tema de reportes

                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_si_debe_evaluar_reporte_asistencia_rrhhv3 " + ec.Message);
                //SetErrorGrid("Function sp_insert_en_odrf_enviadas SQL Store Procedure. Error: " + ex.Message);
            }
            return r;
        }

        public bool GenerarEmailResumenOvertime()
        {
            bool r = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("[sp_get_si_debe_evaluar_resumen_horas_extra]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@idbodega", idBodega);
                r = Convert.ToBoolean(cmd.ExecuteScalar()); //Devuelve true para poder revisar el tema de reportes

                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_si_debe_evaluar_resumen_horas_extra" + ec.Message);
                //SetErrorGrid("Function sp_insert_en_odrf_enviadas SQL Store Procedure. Error: " + ex.Message);
            }
            return r;
        }

            /// <summary>
        /// El id grupo tambien significa el id de departamento.
        /// </summary>
        /// <param name="pIdGrupo"></param>
        /// <returns></returns>
        public bool RecuperarDestinatariosYCopia(int pIdGrupo)
        {
            bool r = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_destino_copia_email_asistencia", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@idbodega", idBodega);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    destinatario = dr.GetString(0);
                    copia = dr.GetString(1);
                }
                dr.Close();
                r = true;
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error(ec.Message);
            }
            return r;
        }



        //sp_get_lista_empleados_no_marcaron_por_fecha
        public DataTable GetListaNoMarcaronporFecha(DateTime pfecha, int pIdDestinatario)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("sp_get_detalle_empleados_no_marcaron_by_fecha_y_destinatario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_no_marcaron_by_fecha_y_destinatario " + ec.Message);
            }
            return tab;
        }

        public DataTable GetListaNoMarcaronporFechaNocturno(DateTime pfecha, int pIdDestinatario)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_detalle_empleados_no_marcaron_by_fecha_hora_y_destinatario]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_no_marcaron_by_fecha_y_destinatario " + ec.Message);
            }
            return tab;
        }

        public DataTable GetDetalleTrackingPorID(int id_tracking)
        {
            DataTable tracking = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringAMS);
                con.Open();
                SqlCommand cmd = new SqlCommand("dbo.sp_get_tracking_compras_detail_by_id", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_tracking", id_tracking);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tracking);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_tracking_compras_detail_by_id " + ec.Message);
            }
            return tracking;
        }


        public DataTable GetResumenHorasExtra()
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_resumen_horas_extra_rrhh]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                //cmd.Parameters.AddWithValue("@fecha", pfecha);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_resumen_horas_extra_rrhh" + ec.Message);
            }
            return tab;
        }


        public DataTable GetListaSiMarcaronporFecha(DateTime pfecha, int pIdDestinatario)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("sp_get_detalle_empleados_si_marcaron_by_fecha_y_destinatario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_si_marcaron_by_fecha_y_destinatario" +  ec.Message);
            }
            return tab;
        }

        public DataTable GetListaMarcaronTarde(DateTime pfecha, int pIdDestinatario, TimeSpan pVar)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_detalle_empleados_que_marcaron_tarde_by_fecha_y_destinatario]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                cmd.Parameters.AddWithValue("@hora", pVar);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_que_marcaron_tarde_by_fecha_y_destinatario" + ec.Message);
            }
            return tab;
        }
        public DataTable GetListaMarcaronTardeNocturno(DateTime pfecha, int pIdDestinatario, TimeSpan pVar)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_detalle_empleados_que_marcaron_tarde_by_fecha_y_destinatario_nocturno]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                cmd.Parameters.AddWithValue("@hora", pVar);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_que_marcaron_tarde_by_fecha_y_destinatario" + ec.Message);
            }
            return tab;
        }

        public DataTable GetListaMarcaronOnTime(DateTime pfecha, int pIdDestinatario, TimeSpan pVar)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_detalle_empleados_que_marcaron_ontime_by_fecha_y_destinatario]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                cmd.Parameters.AddWithValue("@hora", pVar);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_get_detalle_empleados_que_marcaron_ontime_by_fecha_y_destinatario" + ec.Message);
            }
            return tab;
        }

        public DataTable GetListaMarcaronOnTimeNocturno(DateTime pfecha, int pIdDestinatario, TimeSpan pVar)
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("[sp_get_detalle_empleados_que_marcaron_ontime_by_fecha_y_destinatario_nocturno]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pfecha);
                cmd.Parameters.AddWithValue("@hora", pVar);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS [sp_get_detalle_empleados_que_marcaron_ontime_by_fecha_y_destinatario_nocturno]" + ec.Message);
            }
            return tab;
        }

        public bool GuardarDia()
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_insert_log_envio_correo_asistencia", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_insert_log_envio_correo_asistencia" + ec.Message);
                return false;
            }
        }

        public bool GuardarNoche()
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("[sp_insert_log_envio_correo_asistencia_noche]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_destinatario", pIdDestinatario);
                //cmd.Parameters.AddWithValue("@dt_inicial", pdesde);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_insert_log_envio_correo_asistencia" + ec.Message);
                return false;
            }
        }


        public bool ActualizarCorreoTrackingComoEnviado(int id_tracking)
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringAMS);
                con.Open();

                SqlCommand cmd = new SqlCommand("dbo.[set_notificacion_tracking_correo_enviado]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_tracking", id_tracking);

                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("AMS set_notificacion_tracking_correo_enviado" + ec.Message);
                return false;
            }
        }

        public bool GuardarDiaHorasExtra()
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                SqlCommand cmd = new SqlCommand("[sp_insert_log_envio_correo_horas_extra]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_destinatario", pIdDestinatario);
                //cmd.Parameters.AddWithValue("@dt_inicial", pdesde);
                //cmd.Parameters.AddWithValue("@dt_final", phasta);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS sp_insert_log_envio_correo_horas_extra" + ec.Message);
                return false;
            }
        }


        public string GetNumeroSemana(DateTime pFecha)
        {
            string numsemana = "0";
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("SELECT DATEPART( wk, @fecha);", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                cmd.Parameters.AddWithValue("@fecha", pFecha);
                numsemana = cmd.ExecuteScalar().ToString();
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS SELECT DATEPART( wk, @fecha); " + ec.Message);
            }
            return numsemana;
        }


        public int GetNumeroSemanaResumenHorasExtra()
        {
            int numsemana = 0;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringCostos);
                con.Open();

                //SqlCommand cmd = new SqlCommand("sp_get_lista_empleados_no_marcaron_por_fecha", con);
                SqlCommand cmd = new SqlCommand("select cast((SELECT DATEPART( wk, Getdate())-1)as varchar(2))", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id_destino", pIdDestinatario);
                //cmd.Parameters.AddWithValue("@fecha", pFecha);
                numsemana = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ACS select cast((SELECT DATEPART( wk, Getdate())-1)as varchar(2)) " + ec.Message);
            }
            return numsemana;
        }

        public DataTable GetMateriasPrimasPendientesRequisa()
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection conn = new SqlConnection(dp.ConnectionStringALOSY);
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_get_materias_primas_pendientes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adat = new SqlDataAdapter(cmd);
                adat.Fill(tab);
                conn.Close();
            }
            catch (Exception ex)
            {
                CajaDialogo.Error("LOSA [sp_get_materias_primas_pendientes] "+ ex.Message);
            }

            return tab;
        }


        public bool GuardarLogEnvioRequisasAbiertas()
        {
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_insert_log_envio_correo_mp_pendiente_requisa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("LOSA sp_insert_log_envio_correo_mp_pendiente_requisa" + ec.Message);
                return false;
            }
        }

        public bool ValidarSiSeEnvioCorreoHoyRequisasAbiertas()
        {
            bool r = false;
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("[dbo].sp_get_si_ya_se_envio_correo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                r = Convert.ToBoolean(cmd.ExecuteScalar());

                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("ALOSY [sp_get_si_ya_se_envio_correo]" + ec.Message);
            }
            return r;
        }

        public DataTable GetTarimasEscaneadasBloqueadas()
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_tarimas_bloqueados_en_escaneo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adat = new SqlDataAdapter(cmd);
                adat.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("LOSA [sp_get_tarimas_bloqueados_en_escaneo]" + ec.Message);
            }
            return tab;
        }

        internal DataTable GetListaLotesCerrados()
        {
            DataTable tab = new DataTable();
            try
            {
                DataOperations dp = new DataOperations();
                SqlConnection con = new SqlConnection(dp.ConnectionStringALOSY);
                con.Open();

                SqlCommand cmd = new SqlCommand("sp_get_list_notifiacion_sin_enviar_lotes_cerrados", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adat = new SqlDataAdapter(cmd);
                adat.Fill(tab);
                con.Close();
            }
            catch (Exception ec)
            {
                CajaDialogo.Error("LOSA [sp_get_list_notifiacion_sin_enviar_lotes_cerrados]" + ec.Message);
            }
            return tab;
        }
    }
}
