using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iAsset.Web.Services.ViewModels
{
    public class VMCity
    {
        public int CityID { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public int CountryID { get; set; } 
    }
}