namespace MODotNetCore.ConsoleAppHttpClient.Shared;

public class DevCode
{
    static string ToNumber(string num)
    {
        num.Replace("၃", "3");
        return num;
    }
}