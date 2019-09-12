using System;
using System.Collections.Generic;

namespace Api2Word
{
    public interface IFormatter
    {
        IStyler Styler { get; set; }

        String Name { get; set; }

        Object AddTable(int rows, int columns);

        void AddDescription(Endpoint endpoint);

        void AddBody(Endpoint endpoint);

        void ParseEndpoint(Endpoint endpoint);

        void AddDocumentTitle();

        void AddTitle(Endpoint endpoint);

        void AddUrl(Endpoint endpoint);

        void ParseHeaders(Dictionary<String, String> headers);

        void ParseQueryParams(Dictionary<String, String> queryParameters);

        void ParseBody(String type, List<Body> bodies);

        void SaveFile();

        void OpenFile(string path, string name);
    }
}