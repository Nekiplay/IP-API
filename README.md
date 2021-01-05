# IP-API
**API for IP**

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
