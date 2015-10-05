namespace ProcessingXmlInDotNet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;
    using System.Xml.Xsl;

    internal class Program
    {
        public static void Main()
        {
            // Genarating catalogue.xml
            var fileLocation = "../../catalogue.xml";
            new CatalogXmlFileCreator().GenerateCustomXML(50).Save(fileLocation);
            Console.WriteLine("catalogue.xml generated in xml/ !" + Environment.NewLine);

            GetArtistsAndTheNumberOfTheirAlbumsUsingDomParser(fileLocation);
            Console.WriteLine();

            GetArtistsAndTheNumberOfTheirAlbumsUsingXPath(fileLocation);
            Console.WriteLine();

            DeleteAllAlbumsWithPriceGreaterThan20UsingDomParser(fileLocation);

            ExtractAllSongTitlesUsingXmlReader(fileLocation);

            Console.WriteLine();
            ExtractAllSongTitlesUsingXDocumentLinq(fileLocation);

            ConvertPersonTxtToPersonXml();

            ExtractAllAlbumsAndTheirAuthorsInXml(fileLocation);

            CreateFileSystemXmlTree("../../").Save("../../directory.xml");

            Console.WriteLine();
            GetAllNewAlbumsPricesUsingXPath(fileLocation);

            Console.WriteLine();
            GetAllNewAlbumsPricesUsingLINQ(fileLocation);

            Console.WriteLine();
            XslTransformToHtml(fileLocation);

            Console.WriteLine();
            ValidateXmlAgainstXsd(fileLocation);
        }

        // Write program that extracts all different artists which are found in the catalog.xml.
        // For each author you should print the number of albums in the catalogue.
        // Use the DOM parser and a hash-table.
        private static void GetArtistsAndTheNumberOfTheirAlbumsUsingDomParser(string fileLocation)
        {
            var doc = new XmlDocument();
            doc.Load(fileLocation);

            var albums = doc.DocumentElement;
            var artistsTable = new Hashtable();
            foreach (XmlNode album in albums.ChildNodes)
            {
                var artist = album["artist"].InnerText;
                if (artistsTable.ContainsKey(artist))
                {
                    artistsTable[artist] = (int)artistsTable[artist] + 1;
                }
                else
                {
                    artistsTable.Add(artist, 1);
                }
            }

            foreach (DictionaryEntry artist in artistsTable)
            {
                Console.WriteLine("Artist: {0} has {1} albums in the current entry!", artist.Key, artist.Value);
            }
        }

        // Implement the previous using XPath.
        private static void GetArtistsAndTheNumberOfTheirAlbumsUsingXPath(string fileLocation)
        {
            var doc = new XmlDocument();
            doc.Load(fileLocation);

            var artists = doc.SelectNodes("/catalogue/album/artist");
            var artistsTable = new Hashtable();
            foreach (XmlNode artistNode in artists)
            {
                var artist = artistNode.InnerText;

                if (artistsTable.ContainsKey(artist))
                {
                    artistsTable[artist] = (int)artistsTable[artist] + 1;
                }
                else
                {
                    artistsTable.Add(artist, 1);
                }
            }

            Console.WriteLine("Artists and the number of produced albums: {{Using xPath}}");
            foreach (DictionaryEntry artist in artistsTable)
            {
                Console.WriteLine("Artist: {0} has {1} albums in the current entry!", artist.Key, artist.Value);
            }
        }

        // Using the DOM parser write a program to delete from catalog.xml all albums having price > 20.
        private static void DeleteAllAlbumsWithPriceGreaterThan20UsingDomParser(string fileLocation)
        {
            var doc = new XmlDocument();
            doc.Load(fileLocation);

            var albums = doc.DocumentElement;
            var albumsToBeDeleted = from XmlNode album in albums.ChildNodes
                                    let price = decimal.Parse(album["price"].InnerText)
                                    where price > 20m
                                    select album;

            foreach (var album in albumsToBeDeleted)
            {
                albums.RemoveChild(album);
            }
        }

        // Write a program, which using XmlReader extracts all song titles from catalog.xml.
        private static void ExtractAllSongTitlesUsingXmlReader(string fileLocation)
        {
            var songTitles = new List<string>();
            using (var reader = XmlReader.Create(fileLocation))
            {
                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element || reader.Name != "title")
                    {
                        continue;
                    }

                    var songTitle = reader.ReadElementContentAsString();
                    if (!songTitles.Contains(songTitle))
                    {
                        songTitles.Add(songTitle);
                    }
                }
            }

            Console.WriteLine(string.Join(", ", songTitles));
        }

        // Rewrite the same using XDocument and LINQ query.
        private static void ExtractAllSongTitlesUsingXDocumentLinq(string fileLocation)
        {
            var doc = XDocument.Load(fileLocation);

            var songTitles = from title in doc.Descendants("title")
                             select title.Value;

            // removes duplicate entries
            songTitles = songTitles.GroupBy(t => t).Select(tg => tg.First());
            Console.WriteLine(string.Join(", ", songTitles));
        }

        // In a text file we are given the name, address and phone number of given person(each at a
        // single line). Write a program, which creates new XML document, which contains these data
        // in structured XML format.
        private static void ConvertPersonTxtToPersonXml()
        {
            var personTxt = File.ReadAllLines("../../person.txt");
            var personXml = new XElement(
                "person",
                new XElement("name", personTxt[0]),
                new XElement("address", personTxt[1]),
                new XElement("phone", personTxt[2]));
            personXml.Save("../../person.xml");
        }

        // Write a program, which(using XmlReader and XmlWriter) reads the file catalog.xml and
        // creates the file album.xml, in which stores in appropriate way the names of all albums
        // and their authors.
        private static void ExtractAllAlbumsAndTheirAuthorsInXml(string fileLocation)
        {
            var albumsLocation = "../../album.xml";

            using (var reader = new XmlTextReader(fileLocation))
            {
                using (var writer = new XmlTextWriter(albumsLocation, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.IndentChar = ' ';
                    writer.Indentation = 2;

                    writer.WriteStartDocument();
                    writer.WriteStartElement("albums");

                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "name")
                        {
                            writer.WriteStartElement("album");
                            writer.WriteStartElement("name");
                            writer.WriteString(reader.ReadElementString());
                            writer.WriteEndElement();
                        }

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "artist")
                        {
                            writer.WriteStartElement("artist");
                            writer.WriteString(reader.ReadElementString());
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                        }
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }

        // Write a program to traverse given directory and write to a XML file its contents together
        // with all subdirectories and files. Use tags<file> and<dir> with appropriate attributes.
        // For the generation of the XML document use the class XmlWriter.
        private static void CreateDirectoryXml(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists)
            {
                Console.WriteLine("Directory doesn't exist");
                return;
            }


        }

        // Write a program to traverse given directory and write to a XML file its contents together
        // with all subdirectories and files. Use tags<file> and<dir> with appropriate attributes.
        // For the generation of the XML document use the class XmlWriter.

        // Rewrite the last exercises using XDocument, XElement and XAttribute.
        private static XElement CreateFileSystemXmlTree(string directory)
        {
            var directoryInfo = new DirectoryInfo(directory);
            return new XElement(
                "directory",
                new XAttribute("name", directoryInfo.Name),
                from fileInfo in directoryInfo.GetFiles() select new XElement("file", new XAttribute("name", fileInfo.Name)),
                from dir in Directory.GetDirectories(directory) select CreateFileSystemXmlTree(dir));
        }

        // Write a program, which extract from the file catalog.xml the prices for all albums, published
        // 5 years ago or earlier. Use XPath query.
        private static void GetAllNewAlbumsPricesUsingXPath(string fileLocation)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileLocation);
            var queryPath = "/catalogue/album/price[../year>2010]";

            XmlNodeList prices = doc.SelectNodes(queryPath);

            Console.WriteLine("Prices of all albums released no later than 5 years ago Using XPath:");
            foreach (XmlNode price in prices)
            {
                Console.Write(price.InnerText + ", ");
            }
        }

        // Rewrite the previous using LINQ query.
        private static void GetAllNewAlbumsPricesUsingLINQ(string fileLocation)
        {
            var doc = XDocument.Load(fileLocation);

            var prices = doc.Descendants("album").Where(x => int.Parse(x.Element("year").Value) > 2010).Select(x => x.Element("price").Value).ToList();

            Console.WriteLine("Prices of all albums released no later than 5 years ago Using LINQ: \n");
            Console.WriteLine(string.Join(", ", prices));
        }

        // Write a C# program to apply the XSLT stylesheet transformation on the file catalog.xml
        // using the class XslTransform.
        private static void XslTransformToHtml(string fileLocation)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("../../catalogue.xslt");
            xslt.Transform(fileLocation, "../../catalogue.html");
            Console.WriteLine("Catalogue.xml as HTML generated in catalogue.html");
        }

        // Using Visual Studio generate an XSD schema for the file catalog.xml.
        // Write a C# program that takes an XML file and an XSD file (schema) and validates the XML file against the schema.
        // Test it with valid XML catalogs and invalid XML catalogs.
        private static void ValidateXmlAgainstXsd(string fileLocation)
        {
            var schemas = new XmlSchemaSet();
            schemas.Add(string.Empty, "../../catalogue.xsd");

            XDocument doc = XDocument.Load(fileLocation);
            XDocument invalidDoc = XDocument.Load("../../invalidCatalogue.xml");

            Console.WriteLine("Validating a valid Xml against xsd results: ");
            doc.Validate(schemas, (sender, args) => Console.WriteLine(args.Message));
            Console.WriteLine("\nValidating an invalid xml against xsd results: ");
            invalidDoc.Validate(schemas, (sender, args) => Console.WriteLine(args.Message));
        }
    }
}