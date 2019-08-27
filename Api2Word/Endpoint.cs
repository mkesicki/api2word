using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api2Word
{
    public class Endpoint
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public String Url { get; set; }
        public String Method { get; set; }
        public String Request { get; set; }
        public String Response { get; set; }
        public String Headers { get; set; }
        public Int16 StatusCode { get; set; }
    }
}
