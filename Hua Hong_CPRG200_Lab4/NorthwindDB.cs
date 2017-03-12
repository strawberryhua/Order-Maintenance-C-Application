using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hua_Hong_CPRG200_Lab4
{
   public static class NorthwindDB
    {
        public static SqlConnection GetConnection()
        {
            string connString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\NORTHWND.MDF;Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
