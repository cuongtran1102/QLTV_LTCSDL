using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLTV.DAO
{
    public class Connection
    {
        private static SqlConnection conn;
        public Connection() 
        {
            conn = new SqlConnection("Server = DELL; Database = Library; Integrated Security = true");
        }

        public SqlConnection Connect()
        {
            return conn;
        }
    }
}
