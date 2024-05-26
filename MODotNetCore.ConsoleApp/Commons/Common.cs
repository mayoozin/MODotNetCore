using Microsoft.Extensions.Configuration;
using MODotNetCore.ConsoleApp.DapperExample;
using MODotNetCore.ConsoleApp.EFCoreExamples;
using MODotNetCore.ConsoleApp.Model;
using MODotNetCore.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.Commons;

public class Common
{
    DbConnectionServices connectionServices = new DbConnectionServices();
    public void GetUserCommand()
    {
        //AdoDotNetExample dotNetExample = new AdoDotNetExample();
        DapperServices dotNetExample = new DapperServices(connectionServices);

        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.\n ");
            Console.WriteLine("Type 1 to View All Records.\n");
            Console.WriteLine("Type 2 to Create.\n");
            Console.WriteLine("Type 3 to Update.\n");
            Console.WriteLine("Type 4 to Delete.\n");
            Console.WriteLine("Type 5 to Select By BlogId.\n");

            string commandInput = Console.ReadLine();
            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\n Invalid Command. Please choose an option\n");
                continue;
            }
            int command = Convert.ToInt32(commandInput);
            switch (command)
            {
                case 0:
                    closeApp = true;
                    break;
                case 1:
                    dotNetExample.Read();
                    break;
                case 2:
                    dotNetExample.Create();
                    break;
                case 3:
                    dotNetExample.Update();
                    break;
                case 4:
                    dotNetExample.Delete();
                    break;
                case 5:
                    dotNetExample.SelectDataById();
                    break;
                // ...
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5.\n");
                    break;
            }
        }
    }
    public static BlogModel GetBlogData()
    {
        BlogModel newBlog = new BlogModel();

        try
        {
            while (string.IsNullOrEmpty(newBlog.BlogTitle))
            {
                Console.WriteLine("Please enter Blog Title");
                newBlog.BlogTitle = Console.ReadLine()!;
            }

            while (string.IsNullOrEmpty(newBlog.BlogAuthor))
            {
                Console.WriteLine("Please enter Blog Author");
                newBlog.BlogAuthor = Console.ReadLine()!;
            }

            while (string.IsNullOrEmpty(newBlog.BlogContent))
            {
                Console.WriteLine("Please enter Blog Content");
                newBlog.BlogContent = Console.ReadLine()!;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            throw;
        }

        return newBlog;
    }
}
