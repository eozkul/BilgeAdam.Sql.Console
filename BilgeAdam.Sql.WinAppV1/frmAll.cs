using BilgeAdam.Sql.WinApp.Managers;
using BilgeAdam.Sql.WinApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilgeAdam.Sql.WinApp
{
    public partial class frmAll : Form
    {
        public frmAll()
        {
            InitializeComponent();
        }

        private void frmAll_Load(object sender, EventArgs e)
        {
            var connection = new GenericDataConnection();
            dgvCategories.DataSource = connection.Load<Category>("SELECT CategoryID AS ID,CategoryName AS Name FROM Categories");
            dgvProducts.DataSource = connection.Load<Product>("SELECT ProductID,ProductName,UnitsInStock,UnitPrice,Discontinued FROM Products");
            dgvEmployees.DataSource = connection.Load<Employee>("SELECT EmployeeID,CONCAT(FirstName,' ',LastName) AS FullName FROM Employees ");
            dgvCustomers.DataSource = connection.Load<Customer>("SELECT CustomerID,CompanyName,ContactName,Phone FROM Customers");
            dgvSuppliers.DataSource = connection.Load<Supplier>("SELECT SupplierID,CompanyName FROM Suppliers");
            dgvShippers.DataSource = connection.Load<Shipper>("SELECT ShipperID,CompanyName FROM Shippers");
        }
    }
}
