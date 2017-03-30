﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iAsset.Web.Services.ViewModels
{
    public class VMWeather
    {

        public int Hour { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
        public string Wind { get; set; }
        public string  Visibility { get; set; }
        public string SkyCondition { get; set; }
        public string Temperature { get; set; }
        public string DewPoint { get; set; }
        public string RelativeHumidity { get; set; }
        public string Pressure { get; set; }
    }
}