using Dapper;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.DapperExample
{
    public class DapperServices
    {
        private readonly DbConnectionServices _dbConnection;

        public DapperServices(DbConnectionServices dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Read()
        {
            IDbConnection db = new SqlConnection(_dbConnection.GetConnectionString());
            List<BlogModel> blogs = db.Query(CommonQuery.SelectQuery)
                .Select(x => new BlogModel()
                {
                    BlogId = x.BlogId.ToString(),
                    BlogTitle = x.BlogTitle,
                    BlogAuthor = x.BlogAuthor,
                    BlogContent = x.BlogContent
                })
                .ToList();
            foreach (BlogModel item in blogs)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("----------------------------");
            }
            Console.ReadLine();
            Console.Clear();
        }

        public void Create()
        {

        }
        public void Update()
        {

        }
        public void Delete()
        {

        }
        public void SelectDataById()
        {

        }

    }
}
