/*Create a XML file representing catalogue.
The catalogue should contain albums of different artists.
For each album you should define: name, artist, year, producer, price and a list of songs.
Each song should be described by title and duration.
*/

namespace ProcessingXmlInDotNet
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    using Models;

    internal class CatalogXmlFileCreator
    {
        public XDocument GenerateCustomXML(int entries)
        {
            var doc = new XDocument { Declaration = new XDeclaration("1.0", "utf-8", null) };
            var root = new XElement("catalogue");
            doc.Add(root);
            var data = this.GenerateAlbums(entries);
            foreach (var entry in data)
            {
                var album = new XElement("album",
                    new XElement("name", entry.Name),
                    new XElement("artist", entry.Artist),
                    new XElement("year", entry.Year),
                    new XElement("producer", entry.Producer),
                    new XElement("price", entry.Price));

                var songs = new XElement("songs");
                foreach (var song in entry.Songs)
                {
                    var currSong = new XElement("song",
                        new XElement("title", song.Title),
                        new XElement("duration", song.Duration));

                    songs.Add(currSong);
                }

                album.Add(songs);

                root.Add(album);
            }

            return doc;
        }

        private IList<Album> GenerateAlbums(int entries)
        {
            var artists = new string[] {
                "Pesho",
                "Pochivni Dni",
                "Rigana",
                "Gosho",
                "Mc Petko",
                "Lamqta",
                "Snoop Kolyo",
                "Muhanela",
                "KURaBEATkata",
                "Marja Mugrunden",
                "Wightnish",
                "PoldClay",
                "Hans Mizzer",
                "Mindsey Twirling"
            };
            var name = new string[]
            {
                "Mnogo stanahte",
                "Te sa cherni",
                "Beloto",
                "gibsoKartoniteee",
                "89",
                "Pop Hits 2015",
                "Le Shitty Kygo's remixes",
                "90te godini",
                "Zlatnite hitove na Lamqta",
                "Le Deamond Diaries",
                "Behold",
                "Sunshine",
                "Beyond Space",
                "Shatter Me",
                "Swag",
                "Bustin Jieber Hits",
                "Golden Arrow"
            };
            var year = new int[]
            {
                1998, 2005, 2016, 2001, 2004, 2002, 2010, 2012, 2015
            };
            var producer = "Drisko";

            var prices = new decimal[] {
                14.5m,
                22m,
                34m,
                15m,
                9.99m,
                19.99m,
                24.99m,
                21.99m,
                2.99m,
                6.79m,
                29.99m
            };

            var songs = new List<IList<Song>>();
            songs.Add(new List<Song>() { new Song("1. Te sa zeleni", 3.12), new Song("2. Pachata", 4.40), new Song("3. Nie sme veliki", 2.32) });
            songs.Add(new List<Song>() { new Song("1. Chervenite momcheta", 5.02), new Song("2. Loshite momcheta", 3.43), new Song("3. Tq se mqta", 6.00) });
            songs.Add(new List<Song>() { new Song("1. Zdrasti mace", 5.52), new Song("2. Chao murshoo", 2.58) });
            songs.Add(new List<Song>() { new Song("1. Hills", 4.33), new Song("2. Call me", 4.02), new Song("3. Shake it", 3.38), new Song("4. Another Generic Pop Song", 6.22), new Song("5. I see fire (Kygo)", 5.34) });
            songs.Add(new List<Song>() { new Song("1. Break From This World", 4.53), new Song("2. Globus", 3.02), new Song("3. Coco", 3.21), new Song("4. Ceremonials", 4.42), new Song("5. Dark Colours", 5.21) });
            songs.Add(new List<Song>() { new Song("1. V Pop", 3.12), new Song("2. Heist", 412), new Song("3. Night Vision", 3.38), new Song("4. Fake Tlight", 5.21), new Song("5. Mindsey Twirling", 5.34) });
            songs.Add(new List<Song>() { new Song("1. Chervenite momcheta", 5.02), new Song("2. Loshite momcheta", 3.43), new Song("3. Tq se mqta", 6.00) });
            songs.Add(new List<Song>() { new Song("1. Dont believe what they say", 5.52), new Song("2. 6-feet underground", 2.34), new Song("3. Baby Oooh", 6.00), new Song("4. Down on ze ground", 4.2), new Song("5. The Grey", 3.28), new Song("6. Off with her head", 3.06) });

            var artistRNG = new Random(12355);
            var namesRNG = new Random(45622);
            var yearsRNG = new Random(78911);

            var list = new List<Album>();
            for (int i = 0; i < entries; i++)
            {
                list.Add(new Album(name[(namesRNG.Next() % name.Length)], artists[(artistRNG.Next() % artists.Length)], year[(yearsRNG.Next() % year.Length)], producer, prices[(yearsRNG.Next() % prices.Length)], songs[(i % songs.Count)]));
            }

            return list;
        }
    }
}