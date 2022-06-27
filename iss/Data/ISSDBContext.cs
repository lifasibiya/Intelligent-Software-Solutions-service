using Microsoft.EntityFrameworkCore;
using iss.Models;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace iss.Data
{
    public class ISSDBContext: DbContext
    {
        private static string? _configuration;
        public ISSDBContext(DbContextOptions<ISSDBContext> options, IConfiguration configuration): base(options)
        {
            _configuration = configuration.GetSection("ConnectionStrings")["iss"];
        }

        public static IEnumerable<T> ExcuteProcs<T>(string storedProc, DynamicParameters? param)
        {
            return SqlConnection().Query<T>(storedProc, param, commandType: CommandType.StoredProcedure);
        }

        static SqlConnection SqlConnection()
        {
            return new SqlConnection(_configuration);
        }
    }
}
