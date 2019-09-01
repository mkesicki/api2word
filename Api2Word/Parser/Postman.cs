﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Api2Word.Parser
{
    class Postman : IParser
    {
        public String Name { get; set; }
        public String Id { get; set; }
        public String Url { get; set; }

        private RestClient Client;

        private Collection Collection;

        private List<Endpoint> _endpoints { get; set; }

        public List<Endpoint> Endpoints  {
            get { return _endpoints; }
            set { _endpoints = new List<Endpoint>(value); }
        }

        public Dictionary<String, String> Authorization { get; set; }

        public Postman(String url, String name, Dictionary<String, String> auth) {
            _endpoints = new List<Endpoint>();
            Name = name;
            Authorization = auth;
            Url = url;
            Client = new RestClient(Url);

            GetCollection();
        }

        public void Authorize(RestRequest request)
        {
            request.AddHeader("x-api-key", Authorization["key"]);
        }

        public IRestResponse GetCollection()
        {
            System.Console.WriteLine("Get collection: {0}", Name);

            RestRequest request = new RestRequest("collections", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            Authorize(request);

            IRestResponse response = Client.Execute(request);

            RestSharp.Serialization.Json.JsonDeserializer deserializer = new RestSharp.Serialization.Json.JsonDeserializer(); 
            CollectionList collections  = deserializer.Deserialize<CollectionList>(response);

            foreach (CollectionInfo collection in collections.Collections) {
                if (collection.Name.Equals(Name)) {
                    Id = collection.Id;
                    System.Console.WriteLine("Found collection ID: {0}", Id);
                }
            }

            if (Id != null)
            {
                System.Console.WriteLine("Search for collection with id: {0}", Id);

                request = new RestRequest("collections/{id}");
                request.AddUrlSegment("id", Id);
                Authorize(request);
                response = Client.Execute(request);

                CollecitonDetail detail = deserializer.Deserialize<CollecitonDetail>(response);
                System.Console.WriteLine("Collection size: {0}", detail.Collection.Item.Count);
                Collection = detail.Collection;
            } else {
                String message = "Collection " + Name + " not found!!!";
                throw new Exception(message);
            }

            return response;
        }

        public List<Endpoint> GetEndpoints()
        {
            System.Console.WriteLine("Get list of endpoints");

            Endpoint endpoint;

            foreach (Item item in Collection.Item) {

                //foreach with enpdoints
                foreach (Item2 item2 in item.item)
                {
                    endpoint = new Endpoint();

                    System.Console.WriteLine("Parse endpoint: {0}", item2.Name);
                    endpoint.Title = item2.Name;
                    endpoint.Method = item2.Request.Method;
                    endpoint.Description = item2.Request.Description;
                    endpoint.Url = item2.Request.Url.Raw;

                    //get query params
                    if (item2.Request.Url.Query != null)
                    {
                        foreach (Query query in item2.Request.Url.Query)
                        {
                            endpoint.QueryParams.Add(query.Key, query.Value);
                        }
                    }

                    //get headers
                    if (item2.Request.Header != null) { 
                        foreach ( Header header in item2.Request.Header)
                        {
                            if (!endpoint.Headers.ContainsKey(header.Key))
                            {
                                endpoint.Headers.Add(header.Key, header.Value);
                            }
                        }
                    }

                    //parse body
                    if (item2.Request.Body != null)
                    {
                        if (item2.Request.Body.Mode.Equals("raw"))
                        {
                            endpoint.Body.Add(new Api2Word.Body(item2.Request.Body.Raw));
                        }

                        if (item2.Request.Body.Mode.Equals("urlencoded"))
                        {
                            List<Api2Word.Body> bodies = new List<Api2Word.Body>();
                            foreach (Urlencoded urlencoded in item2.Request.Body.Urlencoded)
                            {
                                bodies.Add(new Api2Word.Body(urlencoded));
                            }
                            endpoint.Body = bodies;
                        }

                        if (item2.Request.Body.Mode.Equals("formdata")) {
                            List<Api2Word.Body> bodies = new List<Api2Word.Body>();
                            foreach (Formdata formdata in item2.Request.Body.Formdata)
                            {
                                bodies.Add(new Api2Word.Body(formdata));
                            }
                            endpoint.Body = bodies;
                        }
                    }
                    

                    //add more modes ??

                    Endpoints.Add(endpoint);
                }
            }

            return Endpoints;

        }
    }
}
