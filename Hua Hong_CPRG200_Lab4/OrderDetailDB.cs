using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hua_Hong_CPRG200_Lab4
{
    /*************************
     * Description:create OrderDetailDB class to display only relevant order details at any time and calculate Order Total. 
     * Date:Jan 7 ,2017
     * Authour:Hua Hong
     * Course Code:CPRG200
     **************************/
    public static class OrderDetailDB
    {
        //display only relevant order details
        public static DataTable GetDataTable(int orderId)
        {
           //define connection
            SqlConnection connection = NorthwindDB.GetConnection();

            DataTable dt = new DataTable(); //empty DataTable object
            try
            {
                // open the connection
                connection.Open();


                SqlDataAdapter sda = new SqlDataAdapter("select * from [Order Details] where OrderID ="+ orderId, connection);

                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex; // let the form handle it
            }
            finally
            {
                connection.Close(); 
            }

            return dt;
        }
         //calculate Order Total
        public static decimal GetOrderTotal(int orderId)
        {
            //define connection
            SqlConnection connection = NorthwindDB.GetConnection();

            //define a select query command
            string selectString = "select OrderID,sum(Unitprice*(1-Discount)*Quantity) as Order_Total from [Order Details] where OrderID = @OrderID " +
                                  "group by OrderID";
            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            selectCommand.Parameters.AddWithValue("@OrderID",orderId);
            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);

                // process the result 
                if (reader.Read())
                {
                    
                    return Convert.ToDecimal(reader["Order_Total"]);
                   
                }
            }
            catch (Exception ex)
            {
                throw ex; // let the form handle it
            }
            finally
            {
                connection.Close(); // close connect
            }

            return 0;
        }
    }
}
