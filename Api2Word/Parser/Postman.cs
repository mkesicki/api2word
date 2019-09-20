using RestSharp;
using System;
using System.Collections.Generic;

namespace Api2Word.Parser
{
    internal class Postman : IParser
    {
        public String Name { get; set; }
        public String Id { get; set; }
        public String Url { get; set; }

        private RestClient Client;

        private Collection Collection;

        private List<Endpoint> _endpoints { get; set; }

        private String Env { get; set; }

        private List<String> BlackList;

        public List<Endpoint> Endpoints
        {
            get { return _endpoints; }
            set { _endpoints = new List<Endpoint>(value); }
        }

        public Dictionary<String, String> Authorization { get; set; }

        public Postman(String url, String name, Dictionary<String, String> auth, String env, List<String> blacklist)
        {
            _endpoints = new List<Endpoint>();
            Name = name;
            Authorization = auth;
            Url = url;
            Client = new RestClient(Url);
            Env = env;
            BlackList = blacklist;

            GetCollection();
        }

        public void Authorize(RestRequest request)
        {
            request.AddHeader("x-api-key", Authorization["key"]);
        }

        public IRestResponse GetCollection()
        {
            Console.WriteLine("Get collection: {0}", Name);

            RestRequest request = new RestRequest("collections", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            Authorize(request);

            IRestResponse response = Client.Execute(request);

            RestSharp.Serialization.Json.JsonDeserializer deserializer = new RestSharp.Serialization.Json.JsonDeserializer();
            CollectionList collections = deserializer.Deserialize<CollectionList>(response);

            foreach (CollectionInfo collection in collections.Collections)
            {
                if (collection.Name.Equals(Name))
                {
                    Id = collection.Id;
                    Console.WriteLine("Found collection ID: {0}", Id);
                }
            }

            if (Id != null)
            {
                Console.WriteLine("Get collection by id: {0}", Id);

                request = new RestRequest("collections/{id}");
                request.AddUrlSegment("id", Id);
                Authorize(request);
                response = Client.Execute(request);

                //parse environment
                if (Env != null && Env.Equals("") == false)
                {
                    Console.WriteLine("Get Environment: " + Env);
                    RestRequest envRequest = new RestRequest("environments");
                    Authorize(envRequest);
                    IRestResponse envResponse = Client.Execute(envRequest);

                    deserializer = new RestSharp.Serialization.Json.JsonDeserializer();
                    EnvList envs = deserializer.Deserialize<EnvList>(envResponse);
                    String EnvId = "";

                    foreach (EnvInfo env in envs.Environments)
                    {
                        if (env.Name.Equals(Env))
                        {
                            EnvId = env.Id;
                            Console.WriteLine("Found environment ID: {0}", EnvId);

                            break;
                        }
                    }

                    if (!EnvId.Equals(""))
                    {
                        envRequest = new RestRequest("environments/{id}");
                        envRequest.AddUrlSegment("id", EnvId);
                        Authorize(envRequest);
                        envResponse = Client.Execute(envRequest);
                        EnvironmentBody envBody = deserializer.Deserialize<EnvironmentBody>(envResponse);

                        foreach (Value value in envBody.Environment.Values)
                        {
                            if (!BlackList.Contains(value.Key))
                            {
                                Console.WriteLine("Parse environment value: " + value.Key + " [" + value.value + "]");

                                response.Content = response.Content.Replace("{{" + value.Key + "}}", value.value);
                            }
                        }
                    }
                }

                CollecitonDetail detail = deserializer.Deserialize<CollecitonDetail>(response);
                Console.WriteLine("Number of collections' endpoints: {0}", detail.Collection.Item.Count);
                Collection = detail.Collection;
            }
            else
            {
                String message = "Collection " + Name + " not found!!!";
                throw new Exception(message);
            }

            return response;
        }

        public List<Endpoint> GetEndpoints()
        {
            Console.WriteLine("Get list of endpoints");

            Endpoint endpoint;

            foreach (Item item in Collection.Item)
            {
                //foreach with enpdoints
                foreach (Item2 item2 in item.item)
                {
                    endpoint = new Endpoint();

                    Console.WriteLine("Parse endpoint: {0}", item2.Name);
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
                    if (item2.Request.Header != null)
                    {
                        foreach (Header header in item2.Request.Header)
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
                            endpoint.BodyMode = "raw";
                            endpoint.Body.Add(new Api2Word.Body(item2.Request.Body.Raw));
                        }

                        if (item2.Request.Body.Mode.Equals("urlencoded"))
                        {
                            endpoint.BodyMode = "urlencoded";
                            List<Api2Word.Body> bodies = new List<Api2Word.Body>();
                            foreach (Urlencoded urlencoded in item2.Request.Body.Urlencoded)
                            {
                                bodies.Add(new Api2Word.Body(urlencoded));
                            }
                            endpoint.Body = bodies;
                        }

                        if (item2.Request.Body.Mode.Equals("formdata"))
                        {
                            endpoint.BodyMode = "formdata";
                            List<Api2Word.Body> bodies = new List<Api2Word.Body>();
                            foreach (Formdata formdata in item2.Request.Body.Formdata)
                            {
                                bodies.Add(new Api2Word.Body(formdata));
                            }
                            endpoint.Body = bodies;
                        }

                        //add more modes ??
                    } //body parse

                    //parse responses
                    if (item2.Response != null)
                    {
                        List<Api2Word.Response> responses = new List<Api2Word.Response>();

                        foreach (Response response in item2.Response)
                        {
                            Console.WriteLine("Endpoint response: {0}", response.Name);
                            responses.Add(new Api2Word.Response()
                            {
                                Status = response.Status,
                                StatusCode = response.Code,
                                Body = response.Body,
                                Name = response.Name
                            });
                        }
                        endpoint.Response = responses;
                    }

                    Endpoints.Add(endpoint);
                }
            }

            return Endpoints;
        }
    }
}