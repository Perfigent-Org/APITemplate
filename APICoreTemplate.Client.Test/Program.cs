using APICoreTemplate.Client.V1;
using System;

namespace APICoreTemplate.Client.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("DB Connection Check");

            ApiClient client = new ApiClient(10);

            var responce = client.DB.CheckConnectionAsync(default).GetAwaiter().GetResult();

            Console.WriteLine($"StatusCode: {responce.StatusCode}");
        }
    }
}
