using NiazRooz.Datalayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace NiazRooz.Utility.Convertors
{
    public static class RawQueryConverter
    {
        public static List<T> RawSqlQuery<T>(string query, Func<DbDataReader, T> map)
        {
            using (var context = new NiazRoozDBContext())
            {
                using (var command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    context.Database.OpenConnection();

                    using (var result = command.ExecuteReader())
                    {
                        var entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        return entities;
                    }
                }
            }
        }
    }
}
