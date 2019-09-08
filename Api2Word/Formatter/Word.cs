using System;
using System.IO;
using System.Reflection;
using Xceed.Words.NET;

namespace Api2Word.Formatter
{
    public class Word : IFormatter
    {
        public IStyler Styler { get; set; }

        public String Name { get; set; }

        private String filePath;

        private readonly String path = @"\results\";

        private DocX Document;

        public Word(String collectionName)
        {
            Styler = new Styler.Word();
            Name = collectionName;
            OpenFile(path, Name);
        }

        void IFormatter.AddBody(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddDescription(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        public void AddMethod(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddRequest(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddResponse(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddTableRow(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddSection(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddStatusCode(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        void IFormatter.AddTable(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        public void ParseEndpoint(Endpoint endpoint)
        {
            AddTitle(endpoint);
        }

        public void AddTitle(Endpoint endpoint)
        {
            Paragraph p = Document.InsertParagraph();
            Styler.SetMethodStyle(p.Append(endpoint.Method));
            Styler.SetEndpointTitleStyle(p.Append(" " + endpoint.Title));
        }

        public void AddDocumentTitle()
        {
            Styler.SetTitleStyle(Document.InsertParagraph(Name + " - Documentation generated at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        public void SaveFile()
        {
            Console.WriteLine("Save file: " + filePath);
            Document.Save();
        }

        public void OpenFile(string path, string name)
        {
            // Get the path of the executing assembly.
            String location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = location + path;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            filePath = path + name + ".docx";

            Console.WriteLine("Create file: " + filePath);
            Document = DocX.Create(filePath);
            AddDocumentTitle();
        }
    }
}