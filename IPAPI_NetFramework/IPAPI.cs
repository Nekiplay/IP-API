using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            public class Information
            {
                public string IpAddres;
                public InformationResponse IP;

                public class InformationResponse
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

                public async Task<bool> IsTorAsync()
                {
                    bool first_detect = false;
                    bool two_detect = false;
                    Task firs_detect_task = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string responce = wc.DownloadString("https://check.torproject.org/torbulkexitlist?ip=1.1.1.1");
                                List<string> splited = new List<string>();
                                splited.AddRange(responce.Split('\n'));
                                foreach (string sp in splited)
                                {
                                    if (!string.IsNullOrEmpty(sp) && sp.Replace(" ", "") == this.IpAddres.Replace(" ", ""))
                                    {
                                        first_detect = true;
                                    }
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            first_detect = false;
                        }
                    });
                    Task two_detect_task = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            using (WebClient wc = new WebClient())
                            {
                                string responce = wc.DownloadString("https://www.dan.me.uk/torlist/");
                                List<string> splited = new List<string>();
                                splited.AddRange(responce.Split('\n'));
                                foreach (string sp in splited)
                                {
                                    if (!string.IsNullOrEmpty(sp) && sp.Replace(" ", "") == this.IpAddres.Replace(" ", ""))
                                    {
                                        two_detect = true;
                                    }
                                }
                            }
                        }
                        catch (WebException ex)
                        {
                            two_detect = false;
                        }
                    });
                    await firs_detect_task;
                    //await two_detect_task;
                    if (first_detect == true)
                    {
                        return true;
                    }
                    else if (two_detect == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                public Information(string ip)
                {
                    this.IpAddres = ip;

                    using (WebClient wc = new WebClient())
                    {
                        string Response = wc.DownloadString("https://ipapi.co/" + this.IpAddres + "/xml/");

                        string error = Regex.Match(Response, "\"error\": (.*),").Groups[1].Value;

                        if (error != "true")
                        {
                            InformationResponse v1Response = new InformationResponse();
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
                            this.IP = new InformationResponse
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
