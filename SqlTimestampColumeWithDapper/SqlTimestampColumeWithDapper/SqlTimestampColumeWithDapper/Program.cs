using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SqlTimestampColumeWithDapper
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Versioning { get; set; }
    }

    public static class DbHelper
    {
        private static IDbConnection GetDbConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultDb"].ConnectionString);
        }

        public static IList<Item> GetItems()
        {
            using (var connection = GetDbConnection())
            {
                var sql = "select Id, Name, Versioning from Item";
                return connection.Query<Item>(sql).ToList();
            }
        }

        public static bool UpdateItem(Item item)
        {
            using (var connection = GetDbConnection())
            {
                var sql = "update Item set Name=@name, UpdatedAt=GETDATE() where Id=@id and Versioning=@versioning";
                var ret = connection.Execute(sql, item);
                return (ret == 1);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var items = DbHelper.GetItems();

            Console.WriteLine("Number of items: " + items.Count);
            Console.WriteLine("Enter item id to modify: ");

            var itemId = int.Parse(Console.ReadLine());

            var updateResult = DbHelper.UpdateItem(items.First(x => x.Id == itemId));
            Console.WriteLine("Update result: " + updateResult.ToString());
            Console.ReadLine();
        }
    }
}
