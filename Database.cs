using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestion_taller
{
    public class Database
    {
       
        
         public static string connectionString = "Data Source=localhost;Initial Catalog=TallerBD;Integrated Security = true";
         SqlConnection conn = new SqlConnection(connectionString);   
         
        public static SqlConnection ConexionBD()
        {
           return new SqlConnection(connectionString);
        }
       
    }
    
}
