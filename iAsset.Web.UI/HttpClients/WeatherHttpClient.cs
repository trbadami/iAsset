using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace iAsset.Web.UI.HttpClients
{
    public class WeatherHttpClient: IWeatherHttpClient
    {
        HttpClient httpClient;
        private string url;
        protected readonly string _serviceBaseAddress = ConfigurationManager.AppSettings["WebAPIServiceBaseAddress"].ToString();
        private readonly string _jsonMediaType = "application/json";
        private string _locationWebAPIRoute = ConfigurationManager.AppSettings["LocationWebAPIRoute"];
        private string _weatherWebAPIRoute = ConfigurationManager.AppSettings["WeatherWebAPIRoute"];
        private string _serviceMethod = string.Empty;

        public WeatherHttpClient(){
            url = _serviceBaseAddress + _locationWebAPIRoute;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
        }


        public HttpResponseMessage GetCities(string country)
        {
            _serviceMethod = url + "/GetCities?Country="+ country;
            HttpResponseMessage responseMessage = httpClient.GetAsync(_serviceMethod).Result;
            return responseMessage;
        }

        public HttpResponseMessage GetCountries()
        {

            _serviceMethod = url + "/GetCountries";
            HttpResponseMessage responseMessage = httpClient.GetAsync(_serviceMethod).Result;
            
            return responseMessage;
        }

        public HttpResponseMessage GetWeather(string city, string country)
        {
            url = _serviceBaseAddress + _weatherWebAPIRoute;
            _serviceMethod = url + "/GetCityWeather?city=" + city + "&country=" + country;
            HttpResponseMessage responseMessage = httpClient.GetAsync(_serviceMethod).Result;
            
            return responseMessage;
        }
    }
}