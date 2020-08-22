using System;
using System.Data;
using System.Data.SqlClient;

namespace GdNet.DataArt
{
    public static class SqlHelper
    {
        public static void LoadDataThenProcess(string connectionString, string commandText, Action<IDataReader> handlerAction)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = commandText;

                    using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        handlerAction(reader);
                    }
                }
            }
        }
    }
}