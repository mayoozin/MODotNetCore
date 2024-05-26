using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODotNetCore.ConsoleApp.Model;

public class Users
{
    public string UserName;
    public string Password;
    private int id;

    public Users(string username, string password)
    {
        this.UserName = username;
        this.Password = password;
    }
}
