using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace iAsset.Web.Services.Interfaces
{
    public interface ILocationController
    {
        IHttpActionResult GetCountries();
        IHttpActionResult Get(string country);
    }
}
