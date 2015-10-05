namespace ProcessingXmlInDotNet.Models
{
    internal class Song
    {
        public Song(string name, double duration)
        {
            this.Title = name;
            this.Duration = duration;
        }

        public string Title
        {
            get; set;
        }

        public double Duration
        {
            get; set;
        }
    }
}