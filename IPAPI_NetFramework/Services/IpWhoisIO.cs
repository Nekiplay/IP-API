using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPAPI.Services
{
    public class IpWhoisIO : IpResponce
    {
        public override string ip { get; set; }
        public override string city { get; set; }
        public override string country { get; set; }
        public override string continent { get; set; }
        public override string org { get; set; }
        public override string isp { get; set; }
        public override string asn { get; set; }
        public override string region { get; set; }
        public override string currency { get; set; }
        public override string currency_symbol { get; set; }
        public override string currency_code { get; set; }
        public override string timezone { get; set; }
        public bool Init(string ip)
        {
            try
            {
                this.ip = ip;
                using (WebClient wc = new WebClient())
                {
                    string resonce = wc.DownloadString("https://ipwhois.app/xml/" + ip);
                    this.city = Regex.Match(resonce, "<city>(.*)</city>").Groups[1].Value;
                    this.continent = Regex.Match(resonce, "<continent>(.*)</continent>").Groups[1].Value;
                    this.country = Regex.Match(resonce, "<country>(.*)</country>").Groups[1].Value;
                    this.isp = Regex.Match(resonce, "<isp>(.*)</isp>").Groups[1].Value;
                    this.timezone = Regex.Match(resonce, "<timezone>(.*)</timezone>").Groups[1].Value;
                    this.org = Regex.Match(resonce, "<org>(.*)</org>").Groups[1].Value;
                    this.asn = Regex.Match(resonce, "<asn>(.*)</asn>").Groups[1].Value;
                    this.currency = Regex.Match(resonce, "<currency>(.*)</currency>").Groups[1].Value;
                    this.currency_symbol = Regex.Match(resonce, "<currency_symbol>(.*)</currency_symbol>").Groups[1].Value;
                    this.currency_code = Regex.Match(resonce, "<currency_code>(.*)</currency_code>").Groups[1].Value;
                    this.region = Regex.Match(resonce, "<region>(.*)</region>").Groups[1].Value;
                }

                return true;
            }
            catch { return false; }
        }
        public async Task<bool> AsyncInitAsync(string ip)
        {
            this.ip = ip;
            Task<bool> task = new Task<bool>(() =>
            {
                return Init(ip);
            });
            return await task;
        }
    }
}
