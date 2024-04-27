using Dapper;
using Microsoft.EntityFrameworkCore;
using MODotNetCore.ConsoleApp.Commons;
using MODotNetCore.ConsoleApp.Commons.Queries;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreServices
    {
        private readonly AppDbContext db = new AppDbContext();

        public void Run()
        {
            //Read();
            //Edit(1);
            //Edit(11);
            //Create("title", "author", "content");
            //Update(2003, "title 2", "author 2", "content 2");
            //Delete(2003);
        }

        public void Read()
        {
            List<BlogModel> blogs = db.Blogs.AsNoTracking()
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
            try
            {
                Console.WriteLine("Connection Open \n\n");
                BlogModel newBlog = Common.GetBlogData();

                db.Blogs.Add(newBlog);
                var result = db.SaveChanges();

                string message = result > 0 ? "\n\n Saving Successful" : "Saving Failed";
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void Update()
        {
            string message = string.Empty;
            var res = 0;
            int blogId = 0;

            try
            {
                Console.WriteLine("\n\nPlease type the Id of the record would like to Update. Type 0 to return to main menu.\n\n");
                string commandInput = Console.ReadLine();

                if (string.IsNullOrEmpty(commandInput))
                {
                    message = "\nYou have to type an Id.\n";
                    goto result;
                }
                Common common = new Common();
                blogId = int.Parse(commandInput);

                if (blogId == 0) common.GetUserCommand();
                Console.WriteLine("Connection Open \n\n");
                BlogModel newBlog = Common.GetBlogData();

                newBlog.BlogId = blogId.ToString();
                db.Update(newBlog);
                res = db.SaveChanges();

                message = res > 0 ? "\n\n Update Successful" : "Update Failed";

                result:
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void Delete()
        {
            int blogId = 0;
            string message = string.Empty;
            var res = 0;
            try
            {
                Console.WriteLine("\n\nPlease type the Id of the record would like to Delete. Type 0 to return to main menu.\n\n");
                string commandInput = Console.ReadLine();

                if (string.IsNullOrEmpty(commandInput))
                {
                    message = "\nYou have to type an Id.\n";
                    goto Result;
                }
                Common common = new Common();
                blogId = int.Parse(commandInput);
                var item = new BlogModel
                {
                    BlogId = blogId.ToString()
                };

                if (blogId == 0) common.GetUserCommand();
                var blogData = db.Blogs.FirstOrDefault(x => x.BlogId == blogId.ToString());

                Console.WriteLine("Connection Open \n\n");

                db.Blogs.Remove(blogData);
                res = db.SaveChanges();
                message = res > 0 ? "\n\n Delete Successful" : "Delete Failed";

                Result:
                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
