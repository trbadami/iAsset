using iAsset.Web.UI.HttpClients;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using iAsset.Web.Services.ViewModels;

namespace iAsset.Web.UI.Controllers
{
    public class WeatherController : BaseController
    {
        private IWeatherHttpClient _httpClient;
        private List<VMWeather> _weatherList;
        
        public WeatherController()
        {
            _httpClient = new WeatherHttpClient();
            _weatherList = new List<VMWeather>();
        }

        public WeatherController(IWeatherHttpClient weatherHttpClient)
        {
            _httpClient = weatherHttpClient;

        }
        //
        // GET: /Location/
        public ActionResult Index()
        {
            HttpResponseMessage response = _httpClient.GetCountries();
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                var countries = JsonConvert.DeserializeObject<List<VMCountry>>(responseData);
                ViewBag.CountryList = countries;
                ViewBag.CityList = new List<VMCity>();
            }

            return View();
        }

        [HttpGet]
        public ActionResult GetCities(string country)
        {
            HttpResponseMessage response = _httpClient.GetCities(country);
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                var cities = JsonConvert.DeserializeObject<List<VMCity>>(responseData);
                ViewBag.CityList = cities;
                return Json(cities, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult GetWeather(string city, string country)
        {
            HttpResponseMessage response = _httpClient.GetWeather(city, country);
            if (response.IsSuccessStatusCode)
            {
                var responseData = response.Content.ReadAsStringAsync().Result;
                var weather = JsonConvert.DeserializeObject<List<VMWeather>>(responseData);
                return Json(weather, JsonRequestBehavior.AllowGet);
            }
            return HttpNotFound();
        }
	}
}