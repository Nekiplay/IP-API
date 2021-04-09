# IP-API
**API for IP**

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/9cbc5881925b4ae19b01297d9c8e0e0d)](https://www.codacy.com/gh/Nekiplay/IP-API/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=Nekiplay/IP-API&amp;utm_campaign=Badge_Grade)

**If open Port:**
```C#
IP.API.Port port = new IP.API.Port("IP");
bool valid = port.CheckPort(80);
```

**Get IP Information:**
```C#
IP.API.Information IpInformation = new IP.API.Information("IP");

bool ipvalid = IpInformation.IP.IpValid;
string country = IpInformation.IP.Country;
string city = IpInformation.IP.City;
string region = IpInformation.IP.Region;

string asn = IpInformation.IP.ASN;
string provider = IpInformation.IP.Provider;

double Latitude = IpInformation.IP.Latitude;
double Longitude = IpInformation.IP.Longitude;

double CountryPopulation = IpInformation.IP.CountryPopulation;
``` 

**Get ICMP Traffic:**
```C#
IP.API.ICMP icmp = new IP.API.ICMP("IP");
int icmppower = icmp.Power; /* Maximum power 4 */
``` 
