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
using System.Web.Http.Cors;

namespace iAsset.Web.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WeatherController : ApiController, IWeatherController
    {
        WeatherService.GlobalWeatherSoapClient weatherService;

        public WeatherController()
        {
             weatherService = new WeatherService.GlobalWeatherSoapClient();
        }
        
        [HttpGet]
        [Route("api/Weather/GetCityWeather")]
        public IHttpActionResult Get(string city, string country)
        {
            //weatherService.Open();
            var result = weatherService.GetWeather(city, country);
            if (result == "Data Not Found")
            {
                //call another service
                List<VMWeather> hourlyWeather = new List<VMWeather>()
                {
                    new VMWeather{ Hour=1, Location = "Sydney", Time="01", Wind="18 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="16 C", DewPoint="21 C", RelativeHumidity="85%", Pressure="1015.9 hPa"},
                    new VMWeather{ Hour=2, Location = "Sydney", Time="02", Wind="20 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="17 C", DewPoint="21 C", RelativeHumidity="86%", Pressure="1016.9 hPa"},
                    new VMWeather{ Hour=3, Location = "Sydney", Time="03", Wind="21 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="18 C", DewPoint="21 C", RelativeHumidity="87%", Pressure="1017.9 hPa"},
                    new VMWeather{ Hour=4, Location = "Sydney", Time="04", Wind="22 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="19 C", RelativeHumidity="89%", Pressure="1018.9 hPa"},
                    new VMWeather{ Hour=5, Location = "Sydney", Time="05", Wind="21 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="20 C", RelativeHumidity="90%", Pressure="1019.9 hPa"},
                    new VMWeather{ Hour=6, Location = "Sydney", Time="06", Wind="20 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="21 C", RelativeHumidity="91%", Pressure="1015.9 hPa"},
                    new VMWeather{ Hour=7, Location = "Sydney", Time="07", Wind="19 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="22 C", RelativeHumidity="92%", Pressure="1016.9 hPa"},
                    new VMWeather{ Hour=8, Location = "Sydney", Time="09", Wind="18 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="23 C", RelativeHumidity="93%", Pressure="1017.9 hPa"},
                    new VMWeather{ Hour=9, Location = "Sydney", Time="00", Wind="17 km/h",  Visibility="9 km", SkyCondition="Cloudy", Temperature="22 C", DewPoint="24 C", RelativeHumidity="94%", Pressure="1018.9 hPa"},

                };
                return Ok(hourlyWeather);
            }
            weatherService.Close();
            return Ok(result);
        }
    }
}
