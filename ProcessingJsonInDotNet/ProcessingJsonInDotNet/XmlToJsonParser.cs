namespace ProcessingJsonInDotNet
{
    using System.Xml.Linq;

    using Newtonsoft.Json;

    public class XmlToJsonParser
    {
        public string Parse(string fileName)
        {
            var doc = XDocument.Load(fileName);
            return JsonConvert.SerializeXNode(doc);
        }
    }
}
