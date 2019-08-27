using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Api2Word;

namespace Api2Word.Config
{
   class Postman : IConfig
    {
        public String Url { get; set; }
        public IParser Parser { get; set; }
        public IFormatter Formatter { get; set; }

        public Dictionary<String, String> Authorization { get; set; }

        public String Path { set { Path = value; } }
        public String CollectionName { get { return CollectionName; }  set { CollectionName = value; } }

        public Postman(String path, String name) {
            Path = path;
            CollectionName = name;
            Authorization = new Dictionary<String, String>();
            ReadConfig();
        }

        public void ReadConfig() {
        }
    }
}
