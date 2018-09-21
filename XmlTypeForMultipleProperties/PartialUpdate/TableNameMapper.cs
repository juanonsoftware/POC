using System;
using Dapper.Contrib.Extensions;

namespace PartialUpdate
{
    public class TableNameMapper : SqlMapperExtensions.ITableNameMapper
    {
        public string GetTableName(Type type)
        {
            if (type.FullName == typeof(Employee2).FullName)
            {
                return "Employees";
            }

            throw new NotImplementedException("Missing definition for type: " + type.FullName);
        }
    }
}