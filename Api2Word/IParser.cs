using RestSharp;
using System;
using System.Collections.Generic;

namespace Api2Word
{
    public interface IParser
    {
        String Name { get; set; }
        String Url { get; set; }
        List<Endpoint> Endpoints { get; }

        void Authorize(RestRequest request);

        IRestResponse GetCollection();

        List<Endpoint> GetEndpoints();
    }
}