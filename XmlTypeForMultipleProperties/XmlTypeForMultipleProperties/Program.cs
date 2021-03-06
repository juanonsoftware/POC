﻿using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace XmlTypeForMultipleProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee1 = new Employee()
            {
                FullName = "Employee ONE",
                Birthday = new DateTime(1999, 1, 1),
                Email2 = "one@devcovery.com"
            };

            var employee2 = new Employee()
            {
                FullName = "Employee TWO",
                Birthday = new DateTime(1999, 10, 10),
                Email2 = "two@devcovery.com"
            };

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDb"].ConnectionString))
            {
                const string sql = "INSERT INTO Employees (FullName, XmlProperties) VALUES (@FullName, @XmlProperties)";

                var count = con.Execute(sql, new List<Employee>
                {
                    employee1,
                    employee2
                });

                Console.WriteLine("Inserted: " + count);
            }

            Console.ReadLine();
        }
    }
}
