using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.Shared;

public class DapperService
{
    private readonly string _connection;

    public DapperService(string connectionString)
    {
        _connection = connectionString;
    }

    public List<T?> Query<T>(string query, object? parameters = null)
    {
        using IDbConnection db = new SqlConnection(_connection);
        List<T?> lst = db.Query<T>(query, parameters).ToList();
        return lst;
    }

    public T QueryFirstOrDefault<T>(string query, object? parameters = null)
    {
        using IDbConnection con = new SqlConnection(_connection);
        var item = con.Query<T>(query, parameters).FirstOrDefault();
        return item;
    }

    public int Execute(string query, object? parameters = null)
    {
        using IDbConnection con = new SqlConnection(_connection);
        var result = con.Execute(query, parameters);
        return result;
    }
}
