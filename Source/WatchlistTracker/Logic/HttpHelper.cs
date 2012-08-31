using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WatchlistTracker.Logic
{
    public class HttpHelper
    {
        public static HtmlDocument ReadHtml(string url)
        {
            var document = new HtmlDocument();
            using (var stream = new WebClient().OpenRead(url))
            {
                document.Load(stream);
            }
            return document;
        }

        public static JObject PostJsonReadObject(string url, object data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            using( var streamWriter = new StreamWriter(request.GetRequestStream()))
            using( var writer = new JsonTextWriter(streamWriter))
            {
                new JsonSerializer().Serialize(writer, data);
            }
            try
            {
                var response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                var text = reader.ReadToEnd();
                return JObject.Parse(text);
            }
            catch(Exception e)
            {
                return null;
            }
            
        }

        public static JArray ReadJsonArray(string url)
        {
            using (var stream = new WebClient().OpenRead(url))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                return JArray.Parse(text);
            }
        }
        
        public static JObject ReadJsonObject(string url)
        {
            using (var stream = new WebClient().OpenRead(url))
            {
                StreamReader reader = new StreamReader(stream);
                string text = reader.ReadToEnd();
                return JObject.Parse(text);
            }
        }

    }

}