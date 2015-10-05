namespace ProcessingXmlInDotNet.Models
{
    using System.Collections.Generic;

    internal class Album
    {
        public Album(string name, string artist, int year, string producer, decimal price, IList<Song> songs)
        {
            this.Name = name;
            this.Artist = artist;
            this.Year = year;
            this.Producer = producer;
            this.Price = price;

            this.Songs = new List<Song>();

            foreach (var song in songs)
            {
                this.Songs.Add(song);
            }
        }

        public string Name
        {
            get; set;
        }

        public string Artist
        {
            get; set;
        }

        public int Year
        {
            get; set;
        }

        public string Producer
        {
            get; set;
        }

        public decimal Price
        {
            get; set;
        }

        public IList<Song> Songs
        {
            get; private set;
        }
    }
}