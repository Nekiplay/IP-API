using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAPI
{
    public class IpResponce
    {
        public virtual string ip { get; set; }
        public virtual string city { get; set; }
        public virtual string country { get; set; }
        public virtual string continent { get; set; }
        public virtual string org { get; set; }
        public virtual string isp { get; set; }
        public virtual string asn { get; set; }
        public virtual string region { get; set; }
        public virtual string currency { get; set; }
        public virtual string currency_symbol { get; set; }
        public virtual string currency_code { get; set; }
        public virtual string timezone { get; set; }
    }
}
