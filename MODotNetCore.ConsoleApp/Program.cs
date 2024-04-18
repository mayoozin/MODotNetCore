// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using MODotNetCore.ConsoleApp.Commons;
using MODotNetCore.ConsoleApp.Model;

internal class Program
{
    private static IConfiguration? _iconfiguration;
    private static void Main(string[] args)
    {
        //GetAppSettingsFile();
        UserAuth();
        Environment.Exit(0);
    }
    static void GetAppSettingsFile()
    {
        var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _iconfiguration = builder.Build();
    }
    static void UserAuth()
    {
        bool successfull = false;
        bool isRegister = false;
        var arrUsers = new Users[]
        {
            new Users("mayoo","123"),
            new Users("DotNetTraining","123")
        };

        Console.WriteLine("---Welcome My Console Channel ---- \n\n");
        Console.WriteLine("--- Login 1 && Register 2 ---- \n\n");

        Start:
        if (isRegister)
        {
            Console.WriteLine("---You can login! Please Press 1 ---- \n\n");
        }
        var input = Console.ReadLine();
        if (input != "1" && input != "2")
        {
            return;
        }

        while (!successfull || isRegister)
        {
            if (input == "1")
            {
                Console.WriteLine("Write your username:");
                var username = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                var password = Console.ReadLine();

                foreach (Users user in arrUsers)
                {
                    if (username == user.UserName && password == user.Password)
                    {
                        Console.WriteLine("\n\n __________You have successfully logged in_____________ !!! \n\n");
                        //Console.ReadLine();
                        successfull = true;
                        goto Action;
                        //break;
                    }
                    else successfull = false;
                }

                if (!successfull)
                {
                    Console.WriteLine("Your username or password is incorect, try again !!!");
                }

            }

            else if (input == "2")
            {

                Console.WriteLine("Enter your username:");
                var userName = Console.ReadLine();

                Console.WriteLine("Enter your password:");
                var password = Console.ReadLine();

                Array.Resize(ref arrUsers, arrUsers.Length + 1);
                arrUsers[arrUsers.Length - 1] = new Users(username: userName, password: password);
                Console.WriteLine("____Register Successfully____ \n\n");
                successfull = true;
                isRegister = true;
                goto Start;

            }
            else
            {
                Console.WriteLine("Try again !!!");
                break;
            }

        }

        Console.WriteLine("Hello, From CRUD !");
        Action:
        Common common = new();
        common.GetUserCommand();
    }
}