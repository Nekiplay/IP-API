using IPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            IP.API.Information information = new IP.API.Information("51.15.42.223");
            bool istor = information.IsTorAsync().Result;
            Console.WriteLine("Страна: " + information.IP.Country);
            Console.WriteLine("Регион: " + information.IP.Region);
            Console.WriteLine("Город: " + information.IP.City);
            Console.WriteLine("Провайдер: " + information.IP.Provider);
            Console.WriteLine("Это Tor? " + istor.ToString().Replace("True", "Да").Replace("False", "Нет."));
            Console.ReadKey();
        }
    }
}
