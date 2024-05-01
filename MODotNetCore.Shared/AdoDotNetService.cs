using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MODotNetCore.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connection;

        public AdoDotNetService(string connection)
        {
            _connection = connection;
        }

        public List<T?> Query<T>(string query, object? parameters = null)
        {
            var list = new List<T?>();
            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                if (parameters is not null)
                {
                    cmd.Parameters.AddRange(GetParameters(parameters).ToArray());
                }
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                con.Close();
                string json = JsonConvert.SerializeObject(dataTable);  // C to json
                list = JsonConvert.DeserializeObject<List<T?>>(json); // Json to C
            };
            return list;
        }
        public List<SqlParameter> GetParameters<T>(T? obj)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            foreach (var item in obj.GetType().GetProperties())
            {
                parameters.Add(new SqlParameter(item.Name, item.GetValue(obj)));
            }
            return parameters;
        }
    }
}
