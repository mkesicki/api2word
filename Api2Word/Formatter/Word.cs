using System;
using System.Collections.Generic;
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

        public Word(String collectionName, Dictionary<String, String> styles)
        {
            Styler = new Styler.Word(styles);
            Name = collectionName;
            OpenFile(path, Name);
        }

        void IFormatter.AddBody(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        public void AddDescription(Endpoint endpoint)
        {
            Paragraph p = Document.InsertParagraph();
            Styler.SetDescriptionStyle(p.Append(endpoint.Description));
        }

        public void AddUrl(Endpoint endpoint)
        {
            Table table = (Table)AddTable(1, 1);
            table.Rows[0].Cells[0].Paragraphs[0].Append(endpoint.Url);
            Styler.SetTableStyle(table);
            Document.InsertParagraph("").InsertTableAfterSelf(table);
        }

        public Object AddTable(int rows, int columns)
        {
            Table table = Document.AddTable(rows, columns);

            return table;
        }

        public void AddTitle(Endpoint endpoint)
        {
            Paragraph p = Document.InsertParagraph();
            Styler.SetMethodStyle(p.Append(endpoint.Method), endpoint.Method);
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

        public void ParseHeaders(Dictionary<String, String> headers)
        {
            Table table = (Table)AddTable(headers.Count + 1, 2);

            Styler.SetHeaderTitleStyle(Document.InsertParagraph("Headers"));
            table.Rows[0].Cells[0].Paragraphs[0].Append("Name").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Value").Bold();

            int i = 1;
            foreach (KeyValuePair<String, String> header in headers)
            {
                table.Rows[i].Cells[0].Paragraphs[0].Append(header.Key);
                table.Rows[i].Cells[1].Paragraphs[0].Append(header.Value);
                i++;
            }
            Styler.SetTableStyle(table);
            Document.InsertParagraph("").InsertTableAfterSelf(table);
        }

        public void ParseQueryParams(Dictionary<String, String> queryParams)
        {
            Table table = (Table)AddTable(queryParams.Count + 1, 2);
            table.Rows[0].Cells[0].Paragraphs[0].Append("Name").Bold();
            table.Rows[0].Cells[1].Paragraphs[0].Append("Value").Bold();

            Styler.SetQueryParametersStyle(Document.InsertParagraph("Query Parameters"));

            int i = 1;
            foreach (KeyValuePair<String, String> query in queryParams)
            {
                table.Rows[i].Cells[0].Paragraphs[0].Append(query.Key);
                table.Rows[i].Cells[1].Paragraphs[0].Append(query.Value);
                i++;
            }
            Styler.SetTableStyle(table);
            Document.InsertParagraph("").InsertTableAfterSelf(table);
        }

        public void ParseBody(String type, List<Body> bodies)
        {
            Table table;
            if (type.Equals("raw"))
            {
                table = (Table)AddTable(bodies.Count, 1);
                table.Rows[0].Cells[0].Paragraphs[0].Append(bodies[0].Value);
                Styler.SetTableStyle(table, Styler.Table1Row);
            }
            else
            {
                table = (Table)AddTable(bodies.Count + 1, type.Equals("urlencoded") ? 4 : 3);

                table.Rows[0].Cells[0].Paragraphs[0].Append("Name").Bold();
                table.Rows[0].Cells[1].Paragraphs[0].Append("Value").Bold();
                table.Rows[0].Cells[2].Paragraphs[0].Append("Type").Bold();
                if (type.Equals("urlencoded"))
                {
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Description").Bold();
                }

                int i = 1;
                foreach (var body in bodies)
                {
                    table.Rows[i].Cells[0].Paragraphs[0].Append(body.Key);
                    table.Rows[i].Cells[1].Paragraphs[0].Append(body.Value);
                    table.Rows[i].Cells[2].Paragraphs[0].Append(body.Type);
                    if (type.Equals("urlencoded"))
                    {
                        table.Rows[i].Cells[3].Paragraphs[0].Append(body.Description);
                    }
                    i++;
                }
                Styler.SetTableStyle(table);
            }

            Document.InsertParagraph("").InsertTableAfterSelf(table);
        }

        public void ParseResponse(List<Response> responses)
        {
            foreach (Response response in responses)
            {
                Styler.SetResponseNameStyle(Document.InsertParagraph(response.Name + " [" + response.Status + "]"));
                Document.InsertParagraph("");
                Document.InsertParagraph("Response Status Code: " + response.StatusCode);
                Table table = (Table)AddTable(1, 1);
                table.Rows[0].Cells[0].Paragraphs[0].Append(response.Body);
                Styler.SetTableStyle(table, Styler.Table1Row);
                Document.InsertParagraph("").InsertTableAfterSelf(table);
            }
        }

        public void ParseEndpoint(Endpoint endpoint)
        {
            AddTitle(endpoint);
            AddUrl(endpoint);
            AddDescription(endpoint);

            if (endpoint.Headers.Count > 0)
            {
                ParseHeaders(endpoint.Headers);
            }

            if (endpoint.QueryParams.Count > 0)
            {
                ParseQueryParams(endpoint.QueryParams);
            }

            if (endpoint.Body.Count > 0)
            {
                Styler.SetBodyTitleStyle(Document.InsertParagraph("Body [" + endpoint.BodyMode + "]"));
                ParseBody(endpoint.BodyMode, endpoint.Body);
            }

            if (endpoint.Response.Count > 0)
            {
                Styler.SetResponseTitleStyle(Document.InsertParagraph("Responses"));
                ParseResponse(endpoint.Response);
            }
        }
    }
}