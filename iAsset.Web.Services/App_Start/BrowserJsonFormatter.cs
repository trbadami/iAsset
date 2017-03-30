using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Xml;

namespace iAsset.Web.Services.App_Start
{
    public class BrowserJsonFormatter  : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            this.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            //this.SerializerSettings.Formatting = Formatting.Indented;
        }

        public override void SetDefaultContentHeaders(Type type, System.Net.Http.Headers.HttpContentHeaders headers, System.Net.Http.Headers.MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        }
    }
}