using System;
using System.Collections.Generic;
using System.Data;

namespace GdNet.DataArt
{
    public static class DataReaderExtensions
    {
        public static IEnumerable<T> ConvertTo<T>(this IDataReader dataReader, Func<IDataReader, T> mapAction)
        {
            while (dataReader.Read())
            {
                yield return mapAction(dataReader);
            }
        }
    }
}
