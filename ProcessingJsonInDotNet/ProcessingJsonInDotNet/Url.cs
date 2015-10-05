namespace ProcessingJsonInDotNet
{
    using Newtonsoft.Json;

    public class Url
    {
        [JsonProperty("@rel")]
        public string Rel { get; set; }

        [JsonProperty("@href")]
        public string Href { get; set; }
    }
}
