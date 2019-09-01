using System;
using System.Collections.Generic;
using Api2Word.Parser;

namespace Api2Word
{
    public class Endpoint
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public String Method { get; set; }
        public List<Body> Body { get; set; }
        public String Response { get; set; }
        public Dictionary<String, String> Headers { get; set; }
        public Dictionary<String, String> QueryParams { get; set; }
        public Int16 StatusCode { get; set; }

        public Endpoint()
        {
            Headers = new Dictionary<String, String>();
            QueryParams = new Dictionary<String, String>();
            Body = new List<Body>();
        }
    }

    public class Body {
        public String BodyType { get; set; }
        public String Key { get; set; }
        public String Value { get; set; }
        public String Type { get; set; }
        public String Description { get; set; }

        public Body(Urlencoded urlencoded)
        {
            BodyType = "urlencoded";
            Key = urlencoded.Key;
            Value = urlencoded.Value;
            Type = urlencoded.Type;
            Description = urlencoded.Description;
        }

        public Body(Formdata formdata)
        {
            BodyType = "formdata";
            Key = formdata.Key;
            Value = formdata.Value;
            Type = formdata.Type;
        }

        public Body(String raw)
        {
            BodyType = "raw";
            Value = raw;
        }
    }
}
