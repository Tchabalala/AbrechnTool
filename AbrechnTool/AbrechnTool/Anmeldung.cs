using AllgLib;
using SqlLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AbrechnTool
{
    public partial class Anmeldung : Form
    {
        #region Klassenvariable(n)
        protected DataSet _dsRightsGroups = null;
        #endregion
        #region Eigenschaft(en)
        #endregion
        #region Konstruktor(en)
        public Anmeldung()
        {
            InitializeComponent();

            MySqlConnection.Server = Properties.Settings.Default["Server"].ToString();
            MySqlConnection.Database = Properties.Settings.Default["Database"].ToString();
            MySqlConnection.Connect();

            this._dsRightsGroups = MySqlConnection.ExecuteStoredProcedureInDataSet("Get_RightsGroups", null);
        }
        #endregion

        #region btCancel_Click
        private void btCancel_Click(object sender, System.EventArgs e)
        {
            MySqlConnection.Disconnect();

            this.Close();
        }
        #endregion

        #region btNewUser_Click
        private void btNewUser_Click(object sender, System.EventArgs e)
        {
            string cUsername = this.tb_Username.Text;
            string cPassword = this.tb_Password.Text;

            string cUserEnc = DeEncoding.Encode(cUsername);
            string cPassEnc = DeEncoding.Encode(cPassword);

            string cProc = "Append_User";
            List<SqlParameter> aParam = new List<SqlParameter>();
            SqlParameter param = new SqlParameter("@rightsgroupid", SqlDbType.Int);
            param.Value = this.GetRightsGroupID();
            aParam.Add(param);

            param = new SqlParameter("@loginname", SqlDbType.NVarChar);
            param.Value = cUserEnc;
            aParam.Add(param);

            param = new SqlParameter("@loginpassword", SqlDbType.NVarChar);
            param.Value = cPassEnc;
            aParam.Add(param);

            MySqlConnection.ExecuteStoredProcedure(cProc, aParam);
        }
        #endregion

        #region btLogin_Click
        private void btLogin_Click(object sender, EventArgs e)
        {
            DataSet userdaten = this.GetUserDaten();

            if (userdaten == null || userdaten.Tables.Count == 0)
            {
                MessageBox.Show("Benutzer nicht gefunden!", "Anmeldung fehlgeschlagen");
            }
            else
            {
            }
        }
        #endregion

        #region GetRightsGroupID
        protected int GetRightsGroupID()
        {
            int ret = 0;

            if (this._dsRightsGroups.Tables.Count >= 1)
            {
                DataTable dtRightsGroups = this._dsRightsGroups.Tables[0];

                if (dtRightsGroups.Rows.Count >= 1 && dtRightsGroups.Columns.Contains("rightsgroupid")) {
                    ret = Convert.ToInt32(dtRightsGroups.Rows[0]["rightsgroupid"]);
                }
            }

            return ret;
        }
        #endregion

        #region GetUserDaten
        protected DataSet GetUserDaten()
        {
            DataSet ret = null;

            try
            {
                string cProc = "Get_UserDaten";

                string cUsername = this.tb_Username.Text;
                string cPassword = this.tb_Password.Text;

                string cUserEnc = DeEncoding.Encode(cUsername);
                string cPassEnc = DeEncoding.Encode(cPassword);

                List<SqlParameter> aParam = new List<SqlParameter>();
                SqlParameter param = new SqlParameter("@loginname", SqlDbType.NVarChar);
                param.Value = cUserEnc;
                aParam.Add(param);

                param = new SqlParameter("@loginpassword", SqlDbType.NVarChar);
                param.Value = cPassEnc;
                aParam.Add(param);

                ret = MySqlConnection.ExecuteStoredProcedureInDataSet(cProc, aParam);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fehler beim Abruf der Userdaten");
            }

            return ret;
        }
        #endregion
    }
}
