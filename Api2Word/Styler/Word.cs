using System;
using Xceed.Words.NET;

namespace Api2Word.Styler
{
    internal class Word : IStyler
    {
        public String Title { get; set; }
        public String EndpointTitle { get; set; }
        public String Method { get; set; }
        public String URL { get; set; }
        public String Description { get; set; }
        public String Table { get; set; }
        public String Table1Row { get; set; }
        public String HeaderTitle { get; set; }
        public String QueryTitle { get; set; }
        public string BodyTitle { get; set; }
        public string ResponseName { get; set; }
        public string ResponseTitle { get; set; }

        public void SetStyle(object obj, String style)
        {
            Paragraph paragraph = (Paragraph)obj;
            paragraph.StyleName = style;

            obj = paragraph;
        }

        public void SetTitleStyle(object obj)
        {
            SetStyle(obj, Title);
        }

        public void SetEndpointTitleStyle(object obj)
        {
            SetStyle(obj, EndpointTitle);
        }

        public void SetMethodStyle(object obj, String method)
        {
            Paragraph paragraph = (Paragraph)obj;
            paragraph.Bold();

            switch (method.ToUpper())
            {
                case "POST":
                    paragraph.Color(System.Drawing.Color.FromName("Orange"));
                    break;

                case "GET":
                    paragraph.Color(System.Drawing.Color.FromName("Green"));
                    break;

                case "DELETE":
                    paragraph.Color(System.Drawing.Color.FromName("Red"));
                    break;

                default:
                    paragraph.Color(System.Drawing.Color.FromName("Blue"));
                    break;
            }
            obj = paragraph;
        }

        public void SetDescriptionStyle(object obj)
        {
            Paragraph paragraph = (Paragraph)obj;
            paragraph.StyleName = Description;
            paragraph.InsertParagraphBeforeSelf("");

            obj = paragraph;
        }

        public void SetTableStyle(object obj, String styleName = "LightListAccent1")
        {
            if (styleName.Equals(""))
            {
                styleName = "LightListAccent1";
            }
            Table table = (Table)obj;
            Enum.TryParse(styleName, out TableDesign style);

            table.Design = style;

            obj = table;
        }

        public void SetHeaderTitleStyle(Object obj)
        {
            SetStyle(obj, HeaderTitle);
        }

        public void SetQueryParametersStyle(Object obj)
        {
            SetStyle(obj, QueryTitle);
        }

        public void SetUrlStyle(object obj)
        {
            return;
        }

        public void SetBodyTitleStyle(object obj)
        {
            SetStyle(obj, BodyTitle);
        }

        public void SetResponseTitleStyle(object obj)
        {
            SetStyle(obj, ResponseTitle);
        }

        public void SetResponseNameStyle(object obj)
        {
            SetStyle(obj, ResponseName);
        }

        public Word()
        {
            //load from yaml configuration ?
            Title = "Title";
            EndpointTitle = "Heading1";
            Method = "Orange";
            Description = "Default";
            Table = "LightListAccent1";
            HeaderTitle = "Heading2";
            QueryTitle = "Heading2";
            BodyTitle = "Heading2";
            ResponseTitle = "Heading2";
            ResponseName = "Heading3";
            Table1Row = "TableGrid";
        }
    }
}