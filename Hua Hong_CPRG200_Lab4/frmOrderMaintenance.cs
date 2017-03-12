using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:retrieve and update data from a database by writing code that uses ADO.NET objects
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public partial class frmOrderMaintenance : Form
    {
        public frmOrderMaintenance()
        {
            InitializeComponent();
        }

        private Order order; // the current order

        //call meathod to load orderId in combobox 
        private void frmOrderMaintenance_Load(object sender, EventArgs e)
        {
           
            this.LoadOrderIDComboBox(); //load OrderID in the combobox from database
            cbOrderID.SelectedIndex = -1;
            txtCustomerID.Enabled = false;
            txtOrderDate.Enabled = false;
            txtRequiredDate.Enabled = false;
            txtOrderTotal.Enabled = false;
            txtShippedDate.Enabled = false;
            btnUpdate.Enabled = false;
           
            
        }

        //create meathod to load orderId in combobox
        private void LoadOrderIDComboBox()
        {
            List<OrderId> orderId = new List<OrderId>(); //empty list
            try
            {
                orderId = OrderDB.GetOrderIds();
                cbOrderID.DataSource = orderId;
                cbOrderID.DisplayMember = "OrderID";
                cbOrderID.ValueMember = "OrderID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
        
        //get order data 
        private void btnGetOrder_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(cbOrderID))//one of the orderID is selected
            {
                int orderId = Convert.ToInt32(cbOrderID.Text);//get orderId

                this.GetOrder(orderId);
                
                this.DisplayOrder();
                btnUpdate.Enabled = true;

                try
                {
                    dataGridView1.DataSource = OrderDetailDB.GetDataTable(orderId);
                    txtOrderTotal.Text = OrderDetailDB.GetOrderTotal(orderId).ToString("c"); //get order total
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
               
                txtOrderTotal.Enabled = false;
            } 
            
        }

        //get order data by selecting OrderID
        private void GetOrder(int orderID)
        {
            try
            {
                order = OrderDB.GetOrder(orderID);//retrieve Order data from database
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        //display order data on the form
        private void DisplayOrder()
        {
            
            txtCustomerID.Text = order.CustomerID.ToString();
            txtOrderDate.Text = order.OrderDate.ToString().Substring(0, 10);//display only the date information 
            txtRequiredDate.Text = order.RequiredDate.ToString().Substring(0,10);//display only the date information
            if (Validator.IsNull(order.ShippedDate))//shippedDate is not null
            {
                txtShippedDate.Text = order.ShippedDate.ToString().Substring(0, 10); //display only the date information
            }
            else //ShippedDate is null
            {
                txtShippedDate.Text = order.ShippedDate.ToString();
            }
            btnUpdate.Enabled = true;
            
        }

        //update ShippedDate
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmModifyOrder modifyOrderForm = new frmModifyOrder(); //create a object
            
            modifyOrderForm.order = order;
            DialogResult result = modifyOrderForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                order = modifyOrderForm.order;
                this.DisplayOrder();
            }
            else if (result == DialogResult.Retry)
            {
                this.GetOrder(order.OrderID);
               
            }
        }

        //close the form
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //prepare for next order search
        private void btnClear_Click(object sender, EventArgs e)
        {
            cbOrderID.SelectedIndex = -1;
            txtCustomerID.Text = "";
            txtOrderDate.Text = "";
            txtRequiredDate.Text = "";
            txtShippedDate.Text = "";
            dataGridView1.DataSource = null;
            txtOrderTotal.Clear();
            btnUpdate.Enabled = false;
        }

        //clear current values of controls if selected value changes in combobox
        private void cbOrderID_SelectedValueChanged(object sender, EventArgs e)
        {
            txtCustomerID.Text = "";
            txtOrderDate.Text = "";
            txtRequiredDate.Text = "";
            txtShippedDate.Text = "";
            dataGridView1.DataSource = null;
            txtOrderTotal.Clear();
            btnUpdate.Enabled = false;
        }
    }
}
