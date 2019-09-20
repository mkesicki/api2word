using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace Api2Word.Config
{
    internal class Postman : IConfig
    {
        public String Url { get; set; }
        public IParser Parser { get; set; }
        public IFormatter Formatter { get; set; }

        public Dictionary<String, String> Authorization { get; set; }
        public Dictionary<String, String> Config { get; set; }
        public Dictionary<String, String> Styles { get; set; }
        public List<String> BlackList { get; set; }

        public String Path { get; set; }
        public String Env { get; set; }
        public String CollectionName { get; set; }

        public Postman(String path, String name)
        {
            Path = path;
            CollectionName = name;
            Authorization = new Dictionary<String, String>();
            Config = new Dictionary<String, String>();
            Styles = new Dictionary<String, String>();
            BlackList = new List<string>();
            ReadConfig();
            Parser = new Parser.Postman(Config["url"], CollectionName, Authorization, Env, BlackList);
            Formatter = new Formatter.Word(CollectionName, Styles);
        }

        public void ReadConfig()
        {
            Console.WriteLine("Read Config File: \"{0}\"", Path);

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

                items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("styles")];
                foreach (YamlMappingNode item in items)
                {
                    Styles.Add(item.Children.First().Key.ToString(), item.Children.First().Value.ToString());
                }

                if (mapping.Children.ContainsKey("environment"))
                {
                    var node = (YamlScalarNode)mapping.Children[new YamlScalarNode("environment")];
                    Env = node.ToString();
                    if (mapping.Children.ContainsKey("exludedEnvVariables"))
                    {
                        items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("exludedEnvVariables")];
                        foreach (YamlScalarNode item in items)
                        {
                            BlackList.Add(item.ToString());
                        }
                    }
                }
            }
        }
    }
}