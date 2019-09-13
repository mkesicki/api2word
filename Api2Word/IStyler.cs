using System;

namespace Api2Word
{
    public interface IStyler
    {
        String Title { get; set; }

        String EndpointTitle { get; set; }

        String Method { get; set; }

        String URL { get; set; }

        String Description { get; set; }

        String Table { get; set; }

        String Table1Row { get; set; }

        String HeaderTitle { get; set; }

        String QueryTitle { get; set; }

        String BodyTitle { get; set; }

        String ResponseTitle { get; set; }

        String ResponseName { get; set; }

        void SetTitleStyle(Object obj);

        void SetEndpointTitleStyle(Object obj);

        void SetMethodStyle(Object obj, String method);

        void SetUrlStyle(Object obj);

        void SetHeaderTitleStyle(Object obj);

        void SetQueryParametersStyle(Object obj);

        void SetDescriptionStyle(Object obj);

        void SetBodyTitleStyle(Object obj);

        void SetResponseTitleStyle(Object obj);

        void SetResponseNameStyle(Object obj);

        void SetTableStyle(Object obj, String style = "");
    }
}