using System;

namespace Api2Word
{
    public interface IStyler
    {
        String Title { get; set; }

        String EndpointTitle { get; set; }

        String Method { get; set; }

        void SetTitleStyle(Object obj);

        void SetEndpointTitleStyle(Object obj);

        void SetMethodStyle(Object obj);
    }
}