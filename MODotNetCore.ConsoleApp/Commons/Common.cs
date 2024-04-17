using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.Commons
{
    public class Common
    {
        public void GetUserCommand()
        {
            AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
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
                        adoDotNetExample.Read();
                        break;
                    case 2:
                        adoDotNetExample.Create();
                        break;
                    case 3:
                        adoDotNetExample.Update();
                        break;
                    case 4:
                        adoDotNetExample.Delete();
                        break;
                    case 5:
                        adoDotNetExample.SelectDataById();
                        break;
                    // ...
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5.\n");
                        break;
                }
            }
        }
    }
}
