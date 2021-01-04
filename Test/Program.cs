using IPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IP.API.Port port = new IP.API.Port("IP");
            bool valid = port.CheckPort(80);
            Console.WriteLine(valid);
            Console.ReadKey();
        }
    }
}
