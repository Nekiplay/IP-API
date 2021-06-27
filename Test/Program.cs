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
            IPAPI.Services.ICMP information = new IPAPI.Services.ICMP();
            information.Init("5.35.117.227");
            Console.WriteLine("Отправлено: " + information.Resived);
            Console.WriteLine("ICMP время ответа: " + information.Time);
            Console.ReadKey();
        }
    }
}
