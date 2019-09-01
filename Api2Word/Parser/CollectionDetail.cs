using System.Collections.Generic;
using System;

namespace Api2Word.Parser
{
    public class Info
    {
        public string _postman_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schema { get; set; }
    }

    public class ProtocolProfileBehavior
    {
        public bool DisableBodyPruning { get; set; }
    }

    public class Urlencoded
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class Formdata
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

    public class File
    {
    }

    public class Body
    {
        public string Mode { get; set; }
        public List<Urlencoded> Urlencoded { get; set; }
        public List<Formdata> Formdata { get; set; }
        public string Raw { get; set; }
        public File File { get; set; }
    }

    public class Query
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Url
    {
        public string Raw { get; set; }
        public List<string> Host { get; set; }
        public List<string> Path { get; set; }
        public List<Query> Query { get; set; }
    }

    public class Header {
        public String Key { get; set; }
        public String Value { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }

    public class Request
    {
        public string Method { get; set; }
        public List<Header> Header { get; set; }
        public Body Body { get; set; }
        public Url Url { get; set; }
        public String Description { get; set; }
    }

    public class Response {

        public List<Header> Header { get; set; }
        public String Name { get; set; }
        public String Status { get; set; }
        public Int16 Code { get; set; }
        public String Body { get; set; }
    }

    public class Item2
    {
        public string Name { get; set; }
        public string _postman_id { get; set; }
        public ProtocolProfileBehavior ProtocolProfileBehavior { get; set; }
        public Request Request { get; set; }
        public List<Response> Response { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public List<Item2> item { get; set; }
        public string _postman_id { get; set; }
        public string Description { get; set; }
    }

    public class Collection
    {
        public Info Info { get; set; }
        public List<Item> Item { get; set; }
    }

    public class CollecitonDetail
    {
        public Collection Collection { get; set; }
    }
}
