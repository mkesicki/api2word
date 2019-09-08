using System;
using System.Collections.Generic;

namespace Api2Word
{
    public interface IConfig
    {
        Dictionary<String, String> Authorization { get; set; }
        Dictionary<String, String> Config { get; set; }

        String Path { get; set; }
        String CollectionName { get; set; }
        IParser Parser { get; set; }
        IFormatter Formatter { get; set; }
        String Url { get; set; }

        void ReadConfig();
    }
}