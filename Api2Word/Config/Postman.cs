using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using System.IO;

namespace Api2Word.Config
{
   class Postman : IConfig
    {
        public String Url { get; set; }
        public IParser Parser { get; set; }
        public IFormatter Formatter { get; set; }

        public Dictionary<String, String> Authorization { get; set; }
        public Dictionary<String, String> Config { get; set; }

        public String Path { get; set; }
        public String CollectionName { get; set; }

        public Postman(String path, String name) {
            Path = path;
            CollectionName = name;
            Authorization = new Dictionary<String, String>();
            Config = new Dictionary<String, String>();
            ReadConfig();
        }

        public void ReadConfig() {

            System.Console.WriteLine("Read Config File: \"{0}\"", Path);

            //read YAML
            using (var reader = new StreamReader(Path))
            {
                // Load the stream
                var yaml = new YamlStream();
                yaml.Load(reader);
                // List all the items
                var mapping =
               (YamlMappingNode)yaml.Documents[0].RootNode;

                var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("config")];
                foreach (YamlMappingNode item in items)
                {
                    Config.Add(item.Children.First().Key.ToString(), item.Children.First().Value.ToString());
                }

                items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("authorization")];
                foreach (YamlMappingNode item in items)
                {
                    Authorization.Add(item.Children.First().Key.ToString(), item.Children.First().Value.ToString());
                }

                foreach (KeyValuePair<String, String> kvp in Config)
                {
                    System.Console.WriteLine("Config--{0}:{1}", kvp.Key, kvp.Value);
                }

                foreach (KeyValuePair<String, String> kvp in Authorization)
                {
                    System.Console.WriteLine("Auth--{0}:{1}", kvp.Key, kvp.Value);
                }
            }


        }
    }
}
