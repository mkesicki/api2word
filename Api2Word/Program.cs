﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Api2Word
{
    internal class Program
    {
        private static String configPath, collectionName, formatterName, collectionType;

        private static int Main(string[] args)
        {
            if (args.Length < 3)
            {
                System.Console.WriteLine("You need to pass 3 parameters: \n" +
                    "collection type" +
                    "path to config file \n" +
                    "name of Collection to parse \n"
                );

                return 1;
            }

            collectionType = args[0].ToLower();
            configPath = args[1];
            collectionName = args[2];

            if (!File.Exists(configPath))
            {
                System.Console.WriteLine("Invalid config file path.");

                return 1;
            }

            collectionType = collectionType.First().ToString().ToUpper() + collectionType.Substring(1);

            String objectType = "Api2Word.Config." + collectionType;
            Type type = Type.GetType(objectType);
            IConfig config = (IConfig)Activator.CreateInstance(type, configPath, collectionName);
            IParser parser = config.Parser;
            List<Endpoint> endpoints = parser.GetEndpoints();

            return 0;
        }
    }
}