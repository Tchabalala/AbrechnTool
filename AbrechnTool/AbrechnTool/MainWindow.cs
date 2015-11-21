using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbrechnTool
{
    public partial class MainWindow : Form
    {
        #region Konstruktor(en)
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Klassenvariable(n)
        protected DataSet _dsUserDaten = null;
        #endregion

        #region Eigenschaft(en)
        public DataSet UserDaten
        {
            set
            {
                this._dsUserDaten = value;
            }
        }
        #endregion
    }
}
