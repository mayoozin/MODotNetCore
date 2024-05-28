// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using MODotNetCore.ConsoleAppHttpClient.Model;

Console.WriteLine("Hello, World!");
string jsonStr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonStr); // Json to C#

Console.WriteLine(jsonStr);

foreach (var item in model.questions)
{
    Console.WriteLine(item.questionNo);
}

Console.ReadLine();



