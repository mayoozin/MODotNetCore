﻿// See https://aka.ms/new-console-template for more information
using MODotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new();
//adoDotNetExample.Read();
adoDotNetExample.Create();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

Console.ReadLine();