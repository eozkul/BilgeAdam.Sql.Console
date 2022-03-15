using BilgeAdam.Sql.WinApp.Managers;
using System.Configuration;

namespace BilgeAdam.Sql.WinAppV1
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Connection = new DataConnection();
        }
        internal DataConnection Connection { get; set; }
        private void frmMain_Load(object sender, EventArgs e)
        {
           
            dgvProducts.DataSource = Connection.LoadProducts().ToList();
        }

        private void OnFilter_Requested(object sender, EventArgs e)
        {
            dgvProducts.DataSource = null;
            dgvProducts.DataSource = Connection.FilterProducts(txtProductName.Text, chkInStock.Checked);

        }

    }
}