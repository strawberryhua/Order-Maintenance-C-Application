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
      * Description:create OrderDB class to retrieve and update data from a database
      * Date:Jan 7 ,2017
      * Authour:Hua Hong
      * Course Code:CPRG200
      **************************/
    public static class OrderDB
    {
        //retrieve order data from Orders table
        public static Order GetOrder(int OrderID)
        {
            Order order = null;//found Order
            //define connection
            SqlConnection connection = NorthwindDB.GetConnection();

            //define the select query command
            string selectString = "select OrderID, CustomerID, OrderDate, RequiredDate, Shippeddate " +
                                  "from Orders " +
                                  "where OrderID = @OrderID";

            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            selectCommand.Parameters.AddWithValue("@OrderID", OrderID);

            try
            {
                // open the connection
                connection.Open();

                // execute the query
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);


                // process the result if any
                if (reader.Read()) // if there is Order
                {
                    order = new Order();
                    order.OrderID = (int)reader["OrderID"];
                    order.CustomerID = reader["CustomerID"].ToString();
                    order.OrderDate = GetNullableDatetime(reader, "OrderDate");
                    order.RequiredDate = GetNullableDatetime(reader, "RequiredDate");
                    order.ShippedDate = GetNullableDatetime(reader, "ShippedDate");
                }
            }
            catch (Exception ex)
            {
                throw ex; // let the form handle it
            }
            finally
            {
                connection.Close(); // close connecto no matter what
            }

            return order;

        }

        // updates existing order record and returns bool success flag
        public static bool UpdateOrder(Order oldOrder, Order newOrder)
        {
            bool successful = false;
            SqlConnection connection = NorthwindDB.GetConnection();

            //define update query command
            string updateString = "update Orders set " +
                                  "ShippedDate = @NewShippedDate " +
                                  "where " + // update succeeds only if record not changed by other users
                                  "CustomerID = @OldCustomerID and " +
                                  "OrderDate = @OldOrderDate and " +
                                  "RequiredDate = @OldRequiredDate and " +
                                  "(ShippedDate  = @OldShippedDate or " +
                                  "ShippedDate IS NULL and @OldShippedDate IS NULL)"; 
            SqlCommand updateCommand = new SqlCommand(updateString, connection);
            updateCommand.Parameters.AddWithValue("@OldCustomerID", oldOrder.CustomerID);
            updateCommand.Parameters.AddWithValue("@OldOrderDate", oldOrder.OrderDate);
            updateCommand.Parameters.AddWithValue("@OldRequiredDate", oldOrder.RequiredDate);
            if (oldOrder.ShippedDate != null)//not null
            {
                updateCommand.Parameters.AddWithValue("@OldShippedDate", oldOrder.ShippedDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldShippedDate", DBNull.Value);
            }
            if (newOrder.ShippedDate != null)//not null
            {
                updateCommand.Parameters.AddWithValue("@NewShippedDate", newOrder.ShippedDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewShippedDate", DBNull.Value);
            }
           
            try
            {
                connection.Open();
                int count = updateCommand.ExecuteNonQuery();
                if (count == 1)//updated a order from database
                    successful = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return successful;
        }

        //identify if Datetime data is Null
        public static DateTime? GetNullableDatetime(SqlDataReader reader, string fieldName)//referenced from http://stackoverflow.com/questions/18550769/sqldatareader-best-way-to-check-for-null-values-sqldatareader-isdbnull-vs-dbnul
        {
            if (reader[fieldName] != DBNull.Value)
            {
                return (DateTime)reader[fieldName];
            }
            return null;
        }

        //make a list to contain OrderId
        public static List<OrderId> GetOrderIds()
        {
            List<OrderId> orderIds = new List<OrderId>(); // make an empty list
            OrderId oi; // reference to new state object
            // create connection
            SqlConnection connection = NorthwindDB.GetConnection();

            // create select command
            string selectString = "select OrderID from Orders order by OrderID";

            SqlCommand selectCommand = new SqlCommand(selectString, connection);
            try
            {
                // open connection
                connection.Open();
                // run the select command and process the results adding OrderID to the list
                SqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())// process next row
                {
                    oi = new OrderId();
                    oi.OrderID = (int)reader["OrderID"];

                    orderIds.Add(oi);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex; // throw it to the form to handle
            }
            finally
            {
                connection.Close();
            }
            return orderIds;
        }


    }
}
