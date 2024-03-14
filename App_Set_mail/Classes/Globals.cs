    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App_Set_mail
{
    //enum listaAmbientes{ Produccion, Desarrollo, HAMACHI };

    public class Globals
    {
        //Cambiar aqui: ------------------------------------
        //listaAmbientes value = listaAmbientes.Desarrollo;
        //--------------------------------------------------

        public enum Especies
        {
            Tilapia = 1,
            Camaron = 2
        }

        #region Credenciales Producción

        //WINCC
        public static string CMS_ServerPellet = "AQFSVR010\\AQFSVR010";
        public static string CMS_ServerExtruder = "AQFSVR010\\AQFSVR010";
        public static string CMS_DB_User = "losa_user_db";
        public static string CMS_DB_Pass = "";
        public static string CMS_ActiveDB = "process_data";


        //ACS (Costos)

        public static string CTS_ServerAddress = "AQFSVR010\\AQFSVR010";
        public static string CTS_ServerName = "Servidor Productivo";
        public static string CTS_ActiveDB = "ACS";
        public static string CTS_DB_User = "losa_user_db";
        public static string CTS_DB_Pass = "AquaF33dHN2014";

        //public static string CTS_ServerAddress = "6XJ7QD2-PC";
        //public static string CTS_ServerName = "Servidor Productivo";
        //public static string CTS_ActiveDB = "ACS";
        //public static string CTS_DB_User = "sa";
        //public static string CTS_DB_Pass = "Marathon00";


        // APMS(Aquafeed Pocess Management System) Para prueba #1
        //public static string APMS_Server = @"AQFSVR005\AQFPRD";
        //public static string APMS_DB_User = "sa";
        //public static string APMS_DB_Pass = "AquaF33dHN2017";
        //public static string APMS_ActiveDB = "APMS";
        // APMS(Aquafeed Pocess Management System) Para prueba #1
        public static string APMS_Server = @"AQFSVR008\AQFSVR008";
        public static string APMS_DB_User = "sa";
        public static string APMS_DB_Pass = "AquaF33dHN2017";
        public static string APMS_ActiveDB = "APMS";

        //public static string APMS_Server = @"6XJ7QD2-PC";
        //public static string APMS_DB_User = "sa";
        //public static string APMS_DB_Pass = "Marathon00";
        //public static string APMS_ActiveDB = "APMS";

        //ODOO
        public static string odoo_ServerAddress = "AQFSVR003";
        public static string odoo_ServerName = "Servidor Productivo";
        public static string odoo_ActiveDB = "aquafeed";
        public static string odoo_DB_User = "aquafeed";
        public static string odoo_DB_Pass = "Aqua3820";

        //ODOO2
        public static string odoo2_ServerAddress = "AQFSVR003";
        public static string odoo2_ServerName = "Servidor Productivo";
        public static string odoo2_ActiveDB = "AFF";
        public static string odoo2_DB_User = "aquafeed";
        public static string odoo2_DB_Pass = "Aqua3820";


        //ODOO4
        public static string odoo4_ServerAddress = "AQFSVR003";
        public static string odoo4_ServerName = "Servidor Productivo";
        public static string odoo4_ActiveDB = "odoo_sap";
        public static string odoo4_DB_User = "aquafeed";
        public static string odoo4_DB_Pass = "Aqua3820";


        ////Promix -- TEST ---
        public static string prinin_ServerAddress = "9DR5P32";
        public static string prinin_ServerName = "Development Server";
        public static string prinin_ActiveDB = "PRININ";
        public static string prinin_DB_User = "sa";
        public static string prinin_DB_Pass = "";

        //SAP
        public static string ActiveDBSDK = "AQUA";
        //public static string ActiveDBSDK = "AQUA_TEST";
        public static string ServerSDK = @"AQFSVR006\AQFSVR006";
        public static string ServerlicenseSDK = "aqfsvr006";

        public static string SAP_ServerAddress = @"AQFSVR006\AQFSVR006";
        public static string SAP_ServerName = "Servidor de Desarrollo";
        public static string SAP_ActiveDB = "AQUA";
        public static string SAP_DB_User = "sa";
        public static string SAP_DB_Pass = "Aqua2018";

        public static string SAP2_ServerAddress = @"AQFSVR006\AQFSVR006";
        public static string SAP2_ServerName = "Servidor de Desarrollo";
        public static string SAP2_ActiveDB = "ACS";
        public static string SAP2_DB_User = "sa";
        public static string SAP2_DB_Pass = "Aqua2018";

        public static string odoo5_ServerAddress = "10.50.11.137";
        public static string odoo5_ServerName = "Servidor Odoo Virtual";
        public static string odoo5_ActiveDB = "dbaquafeed_bk";
        public static string odoo5_DB_User = "vegeta";
        public static string odoo5_DB_Pass = "aquaf33d19";

        //AMSv
        public static string AMS_ServerAddress = $"AQFSVR007\\AQFSVR007";
        public static string AMS_ServerName = $"AQFSVR007\\AQFSVR007";
        public static string AMS_ActiveDB = "AMS";
        public static string AMS_DB_User = "sa";
        public static string AMS_DB_Pass = "AquaF33dHN2014";//


        //Web Tickets AMS
        public static string Tickets_AMS_ServerAddress = $"A2NWPLSK14SQL-v03.shr.prod.iad2.secureserver.net";
        public static string Tickets_AMS_ServerName = $"A2NWPLSK14SQL-v03.shr.prod.iad2.secureserver.net";
        public static string Tickets_AMS_ActiveDB = "AMS";
        public static string Tickets_AMS_DB_User = "aquafeed";
        public static string Tickets_AMS_DB_Pass = "?23k0rzT";//


        //LOSA
        public static string ALOSY_ServerAddress = "AQFSVR010\\AQFSVR010";
        public static string ALOSY_ServerName = "AQFSVR010\\AQFSVR010";
        public static string ALOSY_ActiveDB = "LOSA"; //BASE PRODUCTIVA
        //public static string LOSA_ActiveDB = "LOSA2"; //BASE DE PRUEBAS
        public static string ALOSY_DB_User = "losa_user_db";
        public static string ALOSY_DB_Pass = " AquaF33dHN2014";


        //Web Tickets AMS
        public static int Conf_Email_Port = 0;
        //public static string Tickets_AMS_ServerName = $"A2NWPLSK14SQL-v03.shr.prod.iad2.secureserver.net";
        //public static string Tickets_AMS_ActiveDB = "AMS";
        //public static string Tickets_AMS_DB_User = "aquafeed";
        //public static string Tickets_AMS_DB_Pass = "?23k0rzT";//

        #endregion

        #region Credenciales Desarrollo
        //////WINCC
        //public static string CMS_ServerPellet = @"JFTDF12\SQLEXPRESS";
        //public static string CMS_ServerExtruder = @"JFTDF12\SQLEXPRESS";
        //public static string CMS_DB_User = "sa";
        //public static string CMS_DB_Pass = "AquaFeed2016";
        //public static string CMS_ActiveDB = "process_data";

        ////APMS (Aquafeed Pocess Management System)
        ////public static string APMS_Server        = @"JFTDF12\SQLEXPRESS";
        //public static string APMS_Server = @"localhost";
        //public static string APMS_DB_User = "sa";
        //public static string APMS_DB_Pass = "AquaFeed2016";
        //public static string APMS_ActiveDB = "APMS";

        ////ACS (Aquafeed Costing System)
        ////ACS (Costos)
        //public static string CTS_ServerAddress = "AQFSVR003";
        //public static string CTS_ServerName = "Servidor Productivo";
        //public static string CTS_ActiveDB = "ACS";
        //public static string CTS_DB_User = "sa";
        //public static string CTS_DB_Pass = "AquaF33dHN2014";

        //Odoo    
        //public static string odoo_ServerAddress = "AQFSVR003";
        //public static string odoo_ServerName = "Servidor Productivo";
        //public static string odoo_ActiveDB = "RRHH";
        //public static string odoo_DB_User = "aquafeed";
        //public static string odoo_DB_Pass = "Aqua3820";


        ////ODOO2
        //public static string odoo2_ServerAddress = "AQFSVR003";
        //public static string odoo2_ServerName = "Servidor Productivo";
        //public static string odoo2_ActiveDB = "AFF";
        //public static string odoo2_DB_User = "aquafeed";
        //public static string odoo2_DB_Pass = "Aqua3820";

        ////Promix
        //public static string prinin_ServerAddress = "9DR5P32";
        //public static string prinin_ServerName = "Development Server";
        //public static string prinin_ActiveDB = "PRININ";
        //public static string prinin_DB_User = "sa";
        //public static string prinin_DB_Pass = "Promix1620";
        #endregion

        #region Credenciales Desarrollo (HAMACHI)
        ////WINCC
        //public static string CMS_ServerPellet = "25.15.38.196";
        //public static string CMS_ServerExtruder = "25.15.38.196";

        //public static string CMS_DB_User = "sa";
        //public static string CMS_DB_Pass = "AquaFeed2016";
        //public static string CMS_ActiveDB = "process_data";

        ////ACS (Costos)
        //public static string CTS_ServerAddress = "25.15.38.196";
        //public static string CTS_ServerName = "Servidor de Desarrollo";
        //public static string CTS_ActiveDB = "ACS";

        //public static string CTS_DB_User = "sa";
        //public static string CTS_DB_Pass = "AquaFeed2016";

        ////ODOO
        //public static string odoo_ServerAddress = "25.15.38.196";
        //public static string odoo_ServerName = "Servidor de Desarrollo";
        //public static string odoo_ActiveDB = "pruebas";

        //public static string odoo_DB_User = "aquafeed"; //"aquafeed";
        //public static string odoo_DB_Pass = "Aqua3820";
        #endregion
    }
}