using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace IPAPI
{
    public class IP
    {
        public class API
        {
            public class Port
            {
                public string IpAddres;
                public Port(string ip)
                {
                    this.IpAddres = ip;
                }
                public bool CheckPort(int port)
                {
                    WebRequest request = WebRequest.Create("https://www.ipfingerprints.com/scripts/getPortsInfo.php");
                    request.Method = "POST"; // для отправки используется метод Post
                                             // данные для отправки
                    string data = "remoteHost=" + this.IpAddres + "&start_port=" + port + "&end_port=" + port + "&normalScan=Yes&scan_type=connect&ping_type=none";
                    // преобразуем данные в массив байтов
                    byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                    // устанавливаем тип содержимого - параметр ContentType
                    request.ContentType = "application/x-www-form-urlencoded";
                    // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
                    request.ContentLength = byteArray.Length;

                    //записываем данные в поток запроса
                    using (Stream dataStream = request.GetRequestStream())
                    {
                        dataStream.Write(byteArray, 0, byteArray.Length);
                    }

                    WebResponse response = request.GetResponse();
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            string responce = reader.ReadToEnd();
                            Console.WriteLine(responce);
                            if (responce.Contains("fail") && responce.Contains("closed") || responce.Contains("Invalid IP address or hostname."))
                            {
                                response.Close();
                                return false;
                            }
                            else
                            {
                                response.Close();
                                return true;
                            }
                        }
                    }
                }
            }
            public class ICMP
            {
                public string IpAddres;
                public int Power;
                public ICMP(string ip)
                {
                    this.IpAddres = ip;

                    using (WebClient wc2 = new WebClient())
                    {
                        string Response2 = wc2.DownloadString("https://api.hackertarget.com/nping/?q=" + ip);
                        string rcvd = Regex.Match(Response2, "Rcvd: (.*) Lost").Groups[1].Value;
                        rcvd = rcvd.Replace("(", "Data: ").Replace(")", "");
                        rcvd = rcvd.Replace(" |", "");
                        string rcvddata = Regex.Match(rcvd, "Data: (.*)").Groups[1].Value;
                        string rcvdnodata = Regex.Match(rcvd, "(.*) Data: (.*)").Groups[1].Value;
                        if (rcvdnodata == "1" || rcvdnodata == "2" || rcvdnodata == "3" || rcvdnodata == "4")
                        {
                            Power = int.Parse(rcvdnodata);
                        }
                        else
                        {
                            Power = 0;
                        }
                    }
                }
            }
            public class V1
            {
                public string IpAddres;
                public V1Response IP;

                public class V1Response
                {
                    public bool IpValid;

                    public string Country;
                    public string City;
                    public string Region;

                    public string ASN;
                    public string Provider;

                    public double Latitude;
                    public double Longitude;

                    public double CountryPopulation;

                }

                public V1(string ip)
                {
                    this.IpAddres = ip;

                    using (WebClient wc = new WebClient())
                    {
                        string Response = wc.DownloadString("https://ipapi.co/" + this.IpAddres + "/xml/");

                        string error = Regex.Match(Response, "\"error\": (.*),").Groups[1].Value;

                        if (error != "true")
                        {
                            V1Response v1Response = new V1Response();
                            v1Response.IpValid = true;

                            v1Response.City = Regex.Match(Response, "<city>(.*)</city>").Groups[1].Value;
                            v1Response.Region = Regex.Match(Response, "<region>(.*)</region>").Groups[1].Value;
                            v1Response.Country = Regex.Match(Response, "<country_name>(.*)</country_name>").Groups[1].Value;

                            v1Response.ASN = Regex.Match(Response, "<asn>(.*)</asn>").Groups[1].Value;
                            v1Response.Provider = Regex.Match(Response, "<org>(.*)</org>").Groups[1].Value;

                            v1Response.Latitude = double.Parse(Regex.Match(Response, "<latitude>(.*)</latitude>").Groups[1].Value.Replace(".", ","));
                            v1Response.Longitude = double.Parse(Regex.Match(Response, "<longitude>(.*)</longitude>").Groups[1].Value.Replace(".", ","));

                            v1Response.CountryPopulation = double.Parse(Regex.Match(Response, "<country_population>(.*)</country_population>").Groups[1].Value.Replace(".", ","));

                            this.IP = v1Response;
                        }
                        else
                        {
                            this.IP = new V1Response
                            {
                                IpValid = false,
                            };
                        }
                    }
                }
            }
        }
    }
}
