namespace ProcessingJsonInDotNet
{
    using System.Net;

    public class RssDownloader
    {
        public void Download(string uri, string fileName)
        {

            using (var webClient = new WebClient())
            {
                webClient.BaseAddress = uri;
                webClient.DownloadFile(uri, fileName);
            }
        }
    }
}
