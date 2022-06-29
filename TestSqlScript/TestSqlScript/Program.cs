using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestSqlScript
{
    class Program
    {
        static int Main()
        {
            Console.WriteLine("App starts at: " + DateTime.Now);

            using (var dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString))
            {
                dbConnection.Open();

                Console.WriteLine("-> Command starts at: " + DateTime.Now);

                using (var dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandType = CommandType.Text;
                    dbCommand.CommandText = ConfigurationManager.AppSettings["SqlCommandText"];
                    var nbRows = 0;

                    switch (ConfigurationManager.AppSettings["CommandMode"])
                    {
                        case "ExecuteReader":
                            var reader = dbCommand.ExecuteReader();

                            Console.WriteLine("--> Reading rows...");

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    nbRows += 1;
                                }
                            }

                            break;

                        case "ExecuteNonQuery":
                            nbRows = dbCommand.ExecuteNonQuery();
                            break;

                        default:
                            throw new ApplicationException("Please check CommandMode for supported values");
                    }

                    Console.WriteLine("--> Rows returns: " + nbRows);
                }

                Console.WriteLine("-> Command completes at: " + DateTime.Now);
            }

            Console.WriteLine("App completes at: " + DateTime.Now);

            return 0;
        }
    }
}
