using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_Set_mail.Classes
{
    public class OrdenCompraALOSY
    {
        private int _id;
        private string _cardCode;
        private string _cardName;
        private string _address;
        private int _contactCode;
        private string _numAtCard;
        private string _state;
        private int _docNum;
        private DateTime _docDate;
        private DateTime _docDueDate;
        private DateTime _taxDate;
        private int _tipoOrden;
        private string _aquaExoneracion;
        private DateTime _fechaExoneracion;
        private string _comments;
        private decimal _isv;
        private decimal _docTotal;
        private string _curSource;
        private string _docCur;
        private decimal _docRate;
        private int _docNumSolicitud;
        private bool _postedInSap;
        private string _U_incoterm;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string CardCode
        {
            get { return _cardCode; }
            set { _cardCode = value ?? throw new ArgumentNullException(nameof(CardCode)); }
        }

        public string CardName
        {
            get { return _cardName; }
            set { _cardName = value ?? throw new ArgumentNullException(nameof(CardName)); }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value ?? throw new ArgumentNullException(nameof(Address)); }
        }

        public int ContactCode
        {
            get { return _contactCode; }
            set { _contactCode = value; }
        }

        public string NumAtCard
        {
            get { return _numAtCard; }
            set { _numAtCard = value ?? throw new ArgumentNullException(nameof(NumAtCard)); }
        }

        public string State
        {
            get { return _state; }
            set { _state = value ?? throw new ArgumentNullException(nameof(State)); }
        }

        public int DocNum
        {
            get { return _docNum; }
            set { _docNum = value; }
        }

        public DateTime DocDate
        {
            get { return _docDate; }
            set { _docDate = value; }
        }

        public DateTime DocDueDate
        {
            get { return _docDueDate; }
            set { _docDueDate = value; }
        }

        public DateTime TaxDate
        {
            get { return _taxDate; }
            set { _taxDate = value; }
        }

        public int TipoOrden
        {
            get { return _tipoOrden; }
            set { _tipoOrden = value; }
        }

        public string AquaExoneracion
        {
            get { return _aquaExoneracion; }
            set { _aquaExoneracion = value ?? throw new ArgumentNullException(nameof(AquaExoneracion)); }
        }

        public DateTime FechaExoneracion
        {
            get { return _fechaExoneracion; }
            set { _fechaExoneracion = value; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value ?? throw new ArgumentNullException(nameof(Comments)); }
        }

        public decimal ISV
        {
            get { return _isv; }
            set { _isv = value; }
        }

        public decimal DocTotal
        {
            get { return _docTotal; }
            set { _docTotal = value; }
        }

        public string CurSource
        {
            get { return _curSource; }
            set { _curSource = value ?? throw new ArgumentNullException(nameof(CurSource)); }
        }

        public string DocCur
        {
            get { return _docCur; }
            set { _docCur = value ?? throw new ArgumentNullException(nameof(DocCur)); }
        }

        public decimal DocRate
        {
            get { return _docRate; }
            set { _docRate = value; }
        }

        public int DocNumSolicitud
        {
            get { return _docNumSolicitud; }
            set { _docNumSolicitud = value; }
        }

        public bool PostedInSap
        {
            get { return _postedInSap; }
            set { _postedInSap = value; }
        }

        public string U_incoterm
        {
            get { return _U_incoterm; }
            set { _U_incoterm = value; }
        }
        
        public bool Recuperado { get; set; }

        public bool UpdateStatusOrderH(int pId, int pDocEntry)
        {
            bool Updated = false;
            DataOperations dp = new DataOperations();

            using (SqlConnection connection = new SqlConnection(dp.ConnectionStringALOSY))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.sp_set_update_order_sap_to_ALOSY", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_ordenH", pId);
                    command.Parameters.AddWithValue("@docEntry", pDocEntry);
                    Updated = Convert.ToBoolean(command.ExecuteScalar());   
                    connection.Close();
                }
                catch (Exception ex)
                {
                    CajaDialogo.Error(ex.Message);
                }
            }
            return Updated;
        }

        // Method to fill properties using the query result
        public bool RecuperarRegistro(int pId)
        {
            DataOperations dp = new DataOperations();

            using (SqlConnection connection = new SqlConnection(dp.ConnectionStringALOSY))
            {
                try
                {
                    SqlCommand command = new SqlCommand("dbo.sp_get_orden_compra_h", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_ordenH", pId);



                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    Recuperado = false;

                    if (reader.Read())
                    {
                        //OrdenCompraALOSY ordenCompra = new OrdenCompraALOSY();

                        Id = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32(reader.GetOrdinal("id"));
                        CardCode = reader.IsDBNull(reader.GetOrdinal("CardCode")) ? string.Empty : reader.GetString(reader.GetOrdinal("CardCode"));
                        CardName = reader.IsDBNull(reader.GetOrdinal("CardName")) ? string.Empty : reader.GetString(reader.GetOrdinal("CardName"));
                        Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? string.Empty : reader.GetString(reader.GetOrdinal("Address"));
                        ContactCode = reader.IsDBNull(reader.GetOrdinal("ContactCode")) ? 0 : reader.GetInt32(reader.GetOrdinal("ContactCode"));
                        NumAtCard = reader.IsDBNull(reader.GetOrdinal("NumAtCard")) ? string.Empty : reader.GetString(reader.GetOrdinal("NumAtCard"));
                        State = reader.IsDBNull(reader.GetOrdinal("State")) ? string.Empty : reader.GetString(reader.GetOrdinal("State"));
                        DocNum = reader.IsDBNull(reader.GetOrdinal("DocNum")) ? 0 : reader.GetInt32(reader.GetOrdinal("DocNum"));
                        DocDate = reader.IsDBNull(reader.GetOrdinal("DocDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DocDate"));
                        DocDueDate = reader.IsDBNull(reader.GetOrdinal("DocDueDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("DocDueDate"));
                        TaxDate = reader.IsDBNull(reader.GetOrdinal("TaxDate")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("TaxDate"));
                        TipoOrden = reader.IsDBNull(reader.GetOrdinal("U_TipoOrden")) ? 0 : reader.GetInt32(reader.GetOrdinal("U_TipoOrden"));
                        AquaExoneracion = reader.IsDBNull(reader.GetOrdinal("U_AquaExoneracion")) ? string.Empty : reader.GetString(reader.GetOrdinal("U_AquaExoneracion"));
                        FechaExoneracion = reader.IsDBNull(reader.GetOrdinal("U_FechaExoneracion")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("U_FechaExoneracion"));
                        Comments = reader.IsDBNull(reader.GetOrdinal("Comments")) ? string.Empty : reader.GetString(reader.GetOrdinal("Comments"));
                        ISV = reader.IsDBNull(reader.GetOrdinal("ISV")) ? 0 : reader.GetDecimal(reader.GetOrdinal("ISV"));
                        DocTotal = reader.IsDBNull(reader.GetOrdinal("DocTotal")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DocTotal"));
                        CurSource = reader.IsDBNull(reader.GetOrdinal("CurSource")) ? string.Empty : reader.GetString(reader.GetOrdinal("CurSource"));
                        DocCur = reader.IsDBNull(reader.GetOrdinal("DocCur")) ? string.Empty : reader.GetString(reader.GetOrdinal("DocCur"));
                        DocRate = reader.IsDBNull(reader.GetOrdinal("DocRate")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DocRate"));
                        DocNumSolicitud = reader.IsDBNull(reader.GetOrdinal("DocNumSolicitud")) ? 0 : reader.GetInt32(reader.GetOrdinal("DocNumSolicitud"));
                        PostedInSap = reader.IsDBNull(reader.GetOrdinal("posted_in_sap")) ? false : reader.GetBoolean(reader.GetOrdinal("posted_in_sap"));
                        U_incoterm = reader.IsDBNull(reader.GetOrdinal("U_incoterm")) ? string.Empty : reader.GetString(reader.GetOrdinal("U_incoterm"));
                        
                        Recuperado = true;
                        // Do whatever you want with the ordenCompra object here
                    }
                    reader.Close();

                }
                catch(Exception ex)
                {
                    CajaDialogo.Error(ex.Message);
                }
            }
            return Recuperado;
        }//End recuperar registro
    }
}
