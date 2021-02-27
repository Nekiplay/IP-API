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
IP.API.V1 v1 = new IP.API.V1("IP");

bool ipvalid = v1.IP.IpValid;
string country = v1.IP.Country;
string city = v1.IP.City;
string region = v1.IP.Region;

string asn = v1.IP.ASN;
string provider = v1.IP.Provider;

double Latitude = v1.IP.Latitude;
double Longitude = v1.IP.Longitude;

double CountryPopulation = v1.IP.CountryPopulation;
``` 

**Get ICMP Traffic:**
```C#
IP.API.ICMP icmp = new IP.API.ICMP("IP");
int icmppower = icmp.Power; /* Maximum power 4 */
``` 
