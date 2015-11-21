using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MySQLConnection
{
    public static class MySqlConnection
    {
        #region Klassenvariable(n)
        private static SqlConnection _sqlconn = null;
        private static string _server = null;
        private static string _database = null;
        #endregion
        #region Eigenschaft(en)
        public static string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }
        public static string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }
        #endregion

        #region Connect
        public static bool Connect()
        {
            bool lRet = false;

            string connectionstring = "server=" + Server + ";database=" + Database + ";user id=sa;password=sqladmin";
            try
            {
                _sqlconn = new SqlConnection(connectionstring);
                _sqlconn.Open();

                lRet = true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("SQL-Verbindung konnte nicht hergestellt werden!");
            }

            return lRet;
        }
        #endregion
        #region Disconnect
        public static void Disconnect()
        {
            try
            {
                _sqlconn.Close();
            }
            catch (System.Exception ex)
            {
            }
        }
        #endregion
        #region ExecuteSelectInDataSet
        public static DataSet ExecuteSelectInDataSet(string select)
        {
            DataSet dsRet = new DataSet();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(select, _sqlconn);

                adapter.Fill(dsRet);
            }
            catch (System.Exception e)
            {
            }
            return dsRet;
        }
        #endregion
    }
}
