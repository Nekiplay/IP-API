using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IPAPI.Services
{
    public class ICMP
    {
        public string IpAddres;
        public float Resived = 0.0f;
        public int Time;
        // {"bytesseq":64,"ttlping":56,"time":10.347}
        public void Init(string ip)
        {
            this.IpAddres = ip;

            WebRequest request = WebRequest.Create("https://www.ipvoid.com/ping/");
            request.Method = "POST"; // для отправки используется метод Post
                                     // данные для отправки
            string data = "host=" + ip;
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
                    string end = reader.ReadToEnd();
                    string re = Regex.Match(end, "(.*) packets transmitted, (.*) received, (.*)% packet loss, time (.*)ms</textarea>").Groups[2].Value;
                    string re2 = Regex.Match(end, "(.*) packets transmitted, (.*) received, (.*)% packet loss, time (.*)ms</textarea>").Groups[4].Value;
                    this.Resived = float.Parse(re);
                    this.Time = int.Parse(re2);
                }
            }
            response.Close();
        }
    }
}
