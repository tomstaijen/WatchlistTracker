using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using HtmlAgilityPack;
using WatchlistTracker.Extensions;

namespace WatchlistTracker.Logic
{
    public class PreDB
    {
        public static string BASE_URL = "http://predb.me/";
        public static int MAX_DISTANCE = 10;

        public IEnumerable<Release> SearchMovie(string title)
        {
            return Search(title, "movies");
        } 

        public IEnumerable<Release> Search(string title, string category)
        {
            var stream = new WebClient().OpenRead(BASE_URL + "?search=cat:" + category + "+" + string.Join("+", title.Split(new []{','})));
            var document = new HtmlDocument();
            document.Load(stream);
            var posts = document.DocumentNode.SelectNodes("//div[@class='post']");
            if( posts == null )
                return new Release[]{};

            var postlist = posts.Take(5);

            var result = new List<Release>();
            foreach (var post in postlist)
            {
                var releasename = post.SelectNodes("div/div[@class='p-c p-c-title']/h2/a").First().InnerText;
                var compareString = releasename.Substring(0, title.Length-1);
                var distance = compareString.Distance(title);
                if( distance < (title.Length/4) )
                {
                    var id = Int64.Parse(post.Attributes.Single(a => a.Name == "id").Value);
                    var release = Get(id);
                    if( release.Title.Distance(title) < release.Title.Length/4)
                        result.Add(release);
                }
            }
            return result;
        }

        public Release Get(long id)
        {
            var stream = new WebClient().OpenRead(BASE_URL + "?post=" + id);
            var document = new HtmlDocument();
            document.Load(stream);
            var date = new DateTime(Int64.Parse(document.DocumentNode.SelectNodes("//span[@class='p-time']").First().Attributes.Single(a => a.Name == "data").Value) * 10000000).AddYears(1969);
            var attributes = document.DocumentNode.SelectNodes("//div[@class='pb-r ']");
            var properties = attributes.Where(a => !string.IsNullOrEmpty(a.ChildNodes[0].InnerText)).ToDictionary(a => a.ChildNodes[0].InnerText, a => a.ChildNodes[1].InnerText);
            return new Release(properties, date);
        }
    }

    public class Release
    {
        public DateTime Date { get; private set; }
        public string Title { get; private set; }
        public string ReleaseName { get; private set; }
        public string Group { get; private set; }
        public string[] Tags { get; private set; }

        public int Distance { get; set; }

        public Release(Dictionary<string,string> properties, DateTime date)
        {
            Title = properties.GetValueOrDefault("Title");
            ReleaseName = properties.GetValueOrDefault("Rlsname");
            Group = properties.GetValueOrDefault("Group");
            Tags = properties.GetValueOrDefault("Tags", string.Empty).Split(new char[] {','}).Select(str => str.Trim()).ToArray();
            Date = date;
        }
        
    }
}