namespace ProcessingJsonInDotNet
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string Uri = "https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
            const string FileName = "../../TelerikAcademyYouTubeRss.xml";

            var rssDownloader = new RssDownloader();

            rssDownloader.Download(Uri, FileName);

            var parser = new XmlToJsonParser();

            string json = parser.Parse(FileName);

            var jObject = JObject.Parse(json);
            GetAllVideoTitles(jObject);

            var videos = GetAllVideosFromJSON(jObject);
            videos = MakeValidEmbedableVideoLinks(videos);
            var html = GenerateHTMLFromVideos(videos);

            File.WriteAllText("../../parsedFeed.html", html);
        }

        private static void GetAllVideoTitles(JObject json)
        {
            var entries = json["feed"]["entry"]
                .Select(y => y["title"]).ToList();

            Console.WriteLine("Video Titles found in the RSS Feed: \n" + string.Join("\r\n", entries) + "\n");
            Console.WriteLine("--------------");
        }

        private static IEnumerable<Video> GetAllVideosFromJSON(JObject json)
        {
            return json["feed"]["entry"].Select(x => JsonConvert.DeserializeObject<Video>(x.ToString()));
        }

        private static IEnumerable<Video> MakeValidEmbedableVideoLinks(IEnumerable<Video> videos)
        {
            var makeValidEmbedableVideoLinks = videos as Video[] ?? videos.ToArray();

            foreach (var video in makeValidEmbedableVideoLinks)
            {
                video.Link.Href = video.Link.Href.Replace("watch?v=", "embed/");
            }

            return makeValidEmbedableVideoLinks;
        }

        private static string GenerateHTMLFromVideos(IEnumerable<Video> videos)
        {
            var html = new StringBuilder();

            html.Append("<!DOCTYPE html><html>" +
                "<head>" +
                "<style>" +
                "body { " +
                        "background-color:#00FF22;" +
                        "}" +
                    "div { " +
                        "width: 420px; height: 450px; padding:10px; margin:5px; background-color:#AAA; border-radius:10px " +
                        "}" +
                "</style>" +
                "</head>" +
                "<body>");

            foreach (var video in videos)
            {
                html.Append("<div> <h4>" + video.Title + "</h4>" +
                                  "<iframe width=\"420\" height=\"345\" " + "src=\"" + video.Link.Href + "\" " +
                                  "frameborder=\"0\" allowfullscreen></iframe>" +
                                  "<br /><a href=\"" + video.Link.Href + "\">Watch on Youtube</a></div>");
            }
            html.Append("</body></html>");

            return html.ToString();
        }
    }
}
