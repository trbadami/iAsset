using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using iAsset.Web.UI;
using iAsset.Web.UI.Controllers;
using NUnit.Framework;
using iAsset.Web.UI.HttpClients;
using iAsset.Web.Services.ViewModels;
using Moq;
using System.Net.Http;

namespace iAsset.Web.UI.Tests.Controllers
{
   
    [TestFixture]
    public class WeatherControllerTest
    {

        Mock<IWeatherHttpClient> httpClient;
        List<VMWeather> weatherList;
        List<VMCountry> countryList;
        
        List<VMCity> cityList;
        List<VMWeather> hourlyWeather;
        Mock<HttpResponseMessage> responseMessage;
        string countries = "[{\"CountryID\":116,\"CountryName\":\"Afghanistan\"},{\"CountryID\":127,\"CountryName\":\"Albania\"},{\"CountryID\":5,\"CountryName\":\"Algeria\"},{\"CountryID\":58,\"CountryName\":\"Argentina\"}]";
        string cities ="[{\"Country\":\"Australia\",\"City\":\"Archerfield Aerodrome\"},{\"Country\":\"Australia\",\"City\":\"Amberley Aerodrome\"},{\"Country\":\"Australia\",\"City\":\"Alice Springs Aerodrome\"}]";
        string weather ="[{\"Hour\":1,\"Location\":\"Sydney\",\"Time\":\"01\",\"Wind\":\"18 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"16 C\",\"DewPoint\":\"21 C\",\"RelativeHumidity\":\"85%\",\"Pressure\":\"1015.9 hPa\"},{\"Hour\":2,\"Location\":\"Sydney\",\"Time\":\"02\",\"Wind\":\"20 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"17 C\",\"DewPoint\":\"21 C\",\"RelativeHumidity\":\"86%\",\"Pressure\":\"1016.9 hPa\"},{\"Hour\":3,\"Location\":\"Sydney\",\"Time\":\"03\",\"Wind\":\"21 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"18 C\",\"DewPoint\":\"21 C\",\"RelativeHumidity\":\"87%\",\"Pressure\":\"1017.9 hPa\"},{\"Hour\":4,\"Location\":\"Sydney\",\"Time\":\"04\",\"Wind\":\"22 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"19 C\",\"RelativeHumidity\":\"89%\",\"Pressure\":\"1018.9 hPa\"},{\"Hour\":5,\"Location\":\"Sydney\",\"Time\":\"05\",\"Wind\":\"21 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"20 C\",\"RelativeHumidity\":\"90%\",\"Pressure\":\"1019.9 hPa\"},{\"Hour\":6,\"Location\":\"Sydney\",\"Time\":\"06\",\"Wind\":\"20 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"21 C\",\"RelativeHumidity\":\"91%\",\"Pressure\":\"1015.9 hPa\"},{\"Hour\":7,\"Location\":\"Sydney\",\"Time\":\"07\",\"Wind\":\"19 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"22 C\",\"RelativeHumidity\":\"92%\",\"Pressure\":\"1016.9 hPa\"},{\"Hour\":8,\"Location\":\"Sydney\",\"Time\":\"09\",\"Wind\":\"18 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"23 C\",\"RelativeHumidity\":\"93%\",\"Pressure\":\"1017.9 hPa\"},{\"Hour\":9,\"Location\":\"Sydney\",\"Time\":\"00\",\"Wind\":\"17 km/h\",\"Visibility\":\"9 km\",\"SkyCondition\":\"Cloudy\",\"Temperature\":\"22 C\",\"DewPoint\":\"24 C\",\"RelativeHumidity\":\"94%\",\"Pressure\":\"1018.9 hPa\"}]";

        [SetUp]
        public void TestSetUp()
        {
            httpClient = new Mock<IWeatherHttpClient>(MockBehavior.Strict);
            
            weatherList = new List<VMWeather>();
            responseMessage = new Mock<HttpResponseMessage>();
            countryList = new List<VMCountry>();
            cityList = new List<VMCity>();
            hourlyWeather = new List<VMWeather>()
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
        }

        [Test]
        public void Index()
        {
            responseMessage.Object.Content = new StringContent("");
            httpClient.Setup(h => h.GetCountries()).Returns(responseMessage.Object);
            WeatherController controller = new WeatherController(httpClient.Object);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [Test]
        public void CountryList_IsNotNull()
        {
            responseMessage.Object.Content = new StringContent(countries);
            httpClient.Setup(h => h.GetCountries()).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            ViewResult result = controller.Index() as ViewResult;
            countryList = result.ViewBag.CountryList;
            Assert.IsNotNull(countryList);

        }


        [Test]
        public void CountryList_IsCountEqual()
        {
            responseMessage.Object.Content = new StringContent(countries);
            httpClient.Setup(h => h.GetCountries()).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            ViewResult result = controller.Index() as ViewResult;
            countryList = result.ViewBag.CountryList;
            Assert.AreEqual(4, countryList.Count());

        }

        [Test]
        [TestCase("Australia")]
        public void Country_City_IsNotNull(string country)
        {
            var httpClient = new Mock<IWeatherHttpClient>(MockBehavior.Strict);
            var responseMessage = new Mock<HttpResponseMessage>();
            responseMessage.Object.Content = new StringContent(cities);
            httpClient.Setup(h => h.GetCities(country)).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            JsonResult actual = controller.GetCities(country) as JsonResult;
            Assert.IsNotNull(actual.Data);
        }

        [Test]
        [TestCase("Australia")]
        public void Country_City_IsCountEqual(string country)
        {
            responseMessage.Object.Content = new StringContent(cities);
            httpClient.Setup(h => h.GetCities(country)).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            JsonResult actual = controller.GetCities(country) as JsonResult;
            dynamic data = actual.Data;
            Assert.AreEqual(3, data.Count);
        }

        [Test]
        [TestCase("Sydney","Australia")]
        public void City_Weather_IsNotNull(string city, string country)
        {
            responseMessage.Object.Content = new StringContent(weather);
            httpClient.Setup(h => h.GetWeather(city,country)).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            JsonResult actual = controller.GetWeather(city,country) as JsonResult;
            Assert.IsNotNull(actual.Data);
        }

        [Test]
        [TestCase("Sydney", "Australia")]
        public void City_Weather_IsCountEqual(string city, string country)
        {
            responseMessage.Object.Content = new StringContent(weather);
            httpClient.Setup(h => h.GetWeather(city, country)).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            JsonResult actual = controller.GetWeather(city, country) as JsonResult;
            dynamic data = actual.Data;
            Assert.AreEqual(hourlyWeather.Count(), data.Count);
        }


        [Test]
        [TestCase("Sydney", "Australia")]
        public void City_Weather_IsHourlyWeatherCorrect(string country, string city)
        {
            responseMessage.Object.Content = new StringContent(weather);
            httpClient.Setup(h => h.GetWeather(city, country)).Returns(responseMessage.Object);
            var controller = new WeatherController(httpClient.Object);
            JsonResult result = controller.GetWeather(city, country) as JsonResult;
            weatherList = (List<VMWeather>)result.Data;
            var hourlyWeatherCorrect = weatherList.All(w => hourlyWeather.Any(h => h.Location == w.Location && h.Temperature == w.Temperature));
            Assert.IsTrue(hourlyWeatherCorrect);
            
        }


        [TearDown]
        public void TestTearDown()
        {
            httpClient = null;
            responseMessage = null;
            weatherList = null;
            countryList = null;
            cityList = null;
            hourlyWeather = null;
        }

    }
}
