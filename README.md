# IP-API
API for IP

**Validate Port:**
```C#
IP.API.Port port = new IP.API.Port("IP");
bool valid = port.CheckPort(80);
```

**Get IP Information:**
```C#
IP.API.V1 v1 = new IP.API.V1("IP");
string city = v1.IP.City;
``` 

**Get ICMP Traffic:**
```C#
IP.API.ICMP icmp = new IP.API.ICMP("IP");
int icmppower = icmp.Power; /* Maximum power 4 */
``` 
