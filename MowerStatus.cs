using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenaApi
{
    class MowerStatus
    {
        public string name{ get; set; }
        public string batteryLevel { get; set; }
        public string batteryState { get; set; }
        public string rfLinlLevel { get; set; }
        public string serial { get; set; }
        public string modelTyp { get; set; }
        public string rfLinkState { get; set; }
    }
}
