using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:update order by modifying Shippeddate
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public partial class frmModifyOrder : Form
    {
        public frmModifyOrder()
        {
            InitializeComponent();
        }

        public Order order;//order data

        //update order data by modifying shippedDate
        private void btnAccept_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order();
            newOrder.OrderID = order.OrderID;
            newOrder.CustomerID = order.CustomerID;
            newOrder.OrderDate = order.OrderDate;
            newOrder.RequiredDate = order.RequiredDate;
            this.PutOrderData(newOrder);
            if (Validator.IsNull(newOrder.ShippedDate) && Validator.IsNull(newOrder.RequiredDate) && Validator.IsNull(newOrder.OrderDate))//date values are not null
            {
                
                    if ((Validator.CompareTo(newOrder.ShippedDate, newOrder.OrderDate) == 1 ||
                     Validator.CompareTo(newOrder.ShippedDate, newOrder.OrderDate) == 0) &&
                     (Validator.CompareTo(newOrder.ShippedDate, newOrder.RequiredDate) == 0 ||
                     Validator.CompareTo(newOrder.ShippedDate, newOrder.RequiredDate) == -1))//new value for ShippedDate is not earlier than OrderDate and no later than RequiredDate
                    {
                        try
                        {
                            if (!OrderDB.UpdateOrder(order, newOrder))
                            {
                                MessageBox.Show("Another user has updated or " +
                                    "deleted that customer.", "Database Error");
                                this.DialogResult = DialogResult.Retry;
                            }
                            else
                            {
                                order = newOrder;
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString());
                        }
                    }
                    else //new value for ShippedDate is earlier than OrderDate or later than RequiredDate
                    {
                        MessageBox.Show("Make sure that the new value for ShippedDate is not earlier than OrderDate and no later than RequiredDate");
                    }
                
            }
            else //date values have null values
            {
                try
                {
                    if (!OrderDB.UpdateOrder(order, newOrder))
                    {
                        MessageBox.Show("Another user has updated or " +
                            "deleted that customer.", "Database Error");
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        order = newOrder;
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }
            }
        }

        //get modified ShippedDate
        private void PutOrderData(Order order)
        {

            if (txtShipDate.Text != "" )//not null
            {
                if (Validator.IsDatetime(txtShipDate))//is DateTime value
                {
                    order.ShippedDate = Convert.ToDateTime(txtShipDate.Text);
                }
                
            }
            else
            {
                order.ShippedDate = null;
            }
        }

        //load ShippedDate
        private void frmModifyOrder_Load(object sender, EventArgs e)
        {
            this.DisplayOrder();
        }

        //display old ShippedDate
        private void DisplayOrder()
        {
            if (Validator.IsNull(order.ShippedDate)) //not null
            {
                txtShipDate.Text = order.ShippedDate.ToString().Substring(0, 10); //show only date information
            }

            else
            {
                txtShipDate.Text = null;
            }
        }

        //close form
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
