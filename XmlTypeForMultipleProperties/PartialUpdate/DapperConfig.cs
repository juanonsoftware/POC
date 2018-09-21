using Dapper.Contrib.Extensions;

namespace PartialUpdate
{
    public static class DapperConfig
    {
        public static void RegisterTableMapper()
        {
            var mapper = new TableNameMapper();
            SqlMapperExtensions.TableNameMapper = mapper.GetTableName;
        }
    }
}