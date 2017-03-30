using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace iAsset.Web.UI.HttpClients
{
    public interface IWeatherHttpClient
    {
        HttpResponseMessage GetCountries();
        HttpResponseMessage GetCities(string country);
        HttpResponseMessage GetWeather(string city, string country);
    }
}
