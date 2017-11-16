using Dapper;
using DapperCustomPropertyColumnMapping.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DapperCustomPropertyColumnMapping
{
    class Program
    {
        static void Main(string[] args)
        {
          //  DefaultTypeMap.MatchNamesWithUnderscores = true;
            
            GetAllCustomers();

            AddNewCustomer();

            Console.ReadLine();
        }

        private static void GetAllCustomers()
        {
            Console.WriteLine("All existing customers:");

            var connectionString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "select * from Customers";
                var customers = connection.Query<Customer>(sql);

                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}\t{1}\t{2}", customer.Id, customer.Email, customer.FirstName);
                }
            }
        }

        private static void AddNewCustomer()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["CustomerDB"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "insert into Customers (Email, first_name, last_name) values (@Email, @FirstName, @LastName)";
                var unique = DateTime.Now.Ticks;
                var result = connection.Execute(sql, new Customer()
                 {
                     Email = string.Format("newmail_{0}@example", unique),
                     FirstName = string.Format("First Name {0}", unique),
                     LastName = "TEST"
                 });
                Console.WriteLine("AddNewCustomer output is: " + result);
            }
        }
    }
}
