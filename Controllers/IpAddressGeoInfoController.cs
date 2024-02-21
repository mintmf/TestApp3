using Microsoft.AspNetCore.Mvc;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IpAddressGeoInfoController : ControllerBase
    {
        [HttpGet]
        [Route("{ip}")]
        public IActionResult Get([FromRoute] string ip) 
        {
            using (TestAppDbContext db = new TestAppDbContext())
            {
                var ipAddressGeoInfo = db.IpAddressGeoInfo.Where(i => i.Ip == ip).FirstOrDefault();

                if (ipAddressGeoInfo == null)
                {
                    var ipInfo = new IpAddressGeoInfo();

                    try
                    {
                        string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);

                        ipInfo = JsonConvert.DeserializeObject<IpAddressGeoInfo>(info);

                        if (ipInfo.City == null)
                        {
                            var ipBogon = db.IpBogons.Where(i => i.Ip == ip).FirstOrDefault();

                            if (ipBogon == null)
                            {
                                db.IpBogons.AddRange(new IpBogon
                                {
                                    Ip = ip
                                });
                                db.SaveChanges();

                                return Ok(JsonConvert.SerializeObject(new IpBogonDto
                                {
                                    Ip = ip,
                                    Bogon = "true"
                                }));
                            }
                            else
                            {
                                return Ok(JsonConvert.SerializeObject(new IpBogonDto
                                {
                                    Ip = ipBogon.Ip,
                                    Bogon = "true"
                                }));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }


                    db.IpAddressGeoInfo.AddRange(ipInfo);
                    db.SaveChanges();

                    return Ok(new IpAddressGeoInfoDto
                    {
                        Ip = ipInfo.Ip,
                        City = ipInfo.City,
                        Region = ipInfo.Region,
                        Country = ipInfo.Country,
                        Loc = ipInfo.Loc,
                        Org = ipInfo.Org,
                        Postal = ipInfo.Postal,
                        Timezone = ipInfo.Timezone,
                        Readme = ipInfo.Readme
                    });
                }
                else
                {
                    return Ok(new IpAddressGeoInfoDto
                    {
                        Ip = ipAddressGeoInfo.Ip,
                        City = ipAddressGeoInfo.City,
                        Region = ipAddressGeoInfo.Region,
                        Country = ipAddressGeoInfo.Country,
                        Loc = ipAddressGeoInfo.Loc,
                        Org = ipAddressGeoInfo.Org,
                        Postal = ipAddressGeoInfo.Postal,
                        Timezone = ipAddressGeoInfo.Timezone,
                        Readme = ipAddressGeoInfo.Readme
                    });
                }
            }
        }
    }
}
