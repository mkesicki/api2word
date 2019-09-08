using System;

namespace Api2Word
{
    public interface IFormatter
    {
        IStyler Styler { get; set; }

        String Name { get; set; }

        void AddTable(Endpoint endpoint);

        void AddTableRow(Endpoint endpoint);

        void AddDescription(Endpoint endpoint);

        void AddMethod(Endpoint endpoint);

        void AddBody(Endpoint endpoint);

        void AddSection(Endpoint endpoint);

        void AddRequest(Endpoint endpoint);

        void AddResponse(Endpoint endpoint);

        void AddStatusCode(Endpoint endpoint);

        void ParseEndpoint(Endpoint endpoint);

        void AddDocumentTitle();

        void AddTitle(Endpoint endpoint);

        void SaveFile();

        void OpenFile(string path, string name);
    }
}