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
            IP.API.Port icmp = new IP.API.Port("locksxreenxs");
            bool valid = icmp.CheckPort(80); /* Maximum power 4 */
            Console.WriteLine(valid);
            Console.ReadKey();
        }
    }
}
