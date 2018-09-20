using Dapper.Contrib.Extensions;

namespace PartialUpdate
{
    [Table("Employees")]
    public class EmployeePartialTest
    {
        public int Id { get; set; }

        public string FullName { get; set; }
    }
}