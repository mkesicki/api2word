using System;
using Xceed.Words.NET;

namespace Api2Word.Styler
{
    internal class Word : IStyler
    {
        public String Title { get; set; }
        public String EndpointTitle { get; set; }
        public String Method { get; set; }

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

        public void SetMethodStyle(object obj)
        {
            //this looks like hack
            Paragraph paragraph = (Paragraph)obj;
            paragraph.Bold();
            paragraph.Color(System.Drawing.Color.FromName(Method));

            obj = paragraph;
        }

        public Word()
        {
            //load from yaml configuration ?
            Title = "Title";
            EndpointTitle = "Heading1";
            Method = "Orange";
        }
    }
}