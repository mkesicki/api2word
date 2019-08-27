using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api2Word
{
    public interface IConfig
    {
        Dictionary<String, String> Authorization { get; set; }
        Dictionary<String, String> Config { get; set; }

        String Path { get;  set; }
        String CollectionName { get; set; }
        IParser Parser { get; set; }
        IFormatter Formatter { get; set; }
        String Url { get; set; }

        void ReadConfig();
       // void SetParser();
//void SetFormatter();
       // IFormatter GetFormatter();
        //IParser GetParser();

    }
}
