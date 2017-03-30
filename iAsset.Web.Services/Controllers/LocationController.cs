using iAsset.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using iAsset.Web.Services.ViewModels;
using System.Xml;
using System.Web.Helpers;
using System.Globalization;
using System.Data;
using System.IO;
using System.Web.Http.Cors;

namespace iAsset.Web.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LocationController : ApiController, ILocationController
    {
        WeatherService.GlobalWeatherSoapClient weatherService;
        
        public LocationController()
        {
             weatherService = new WeatherService.GlobalWeatherSoapClient();
        }

        [HttpGet]
        [Route("api/Location/GetCities")]
        //[ActionName("GetCities")]
        public IHttpActionResult Get(string country)
        {
            weatherService.Open();
           
            var result = weatherService.GetCitiesByCountry(country);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(result));
          
            weatherService.Close();
            ds.Tables[0].DefaultView.Sort = "City";
            DataTable dt = ds.Tables[0].DefaultView.ToTable();
            return Ok(dt);
        }


        [HttpGet]
        [Route("api/Location/GetCountries")]
        public IHttpActionResult GetCountries()
        {
            weatherService.Open();
            List<VMCountry> CountryList = new List<VMCountry>();
            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            int i=0;
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (CountryList.Where(c => c.CountryName == R.EnglishName).Count() == 0)
                {
                    CountryList.Add(
                        new VMCountry { CountryID = i, CountryName = R.EnglishName });
                    i++;
                }
            }
            
            weatherService.Close();
            return Ok(CountryList.OrderBy(c => c.CountryName).ToList());
        }

        
    }
}
