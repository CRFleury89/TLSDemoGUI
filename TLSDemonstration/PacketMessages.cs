using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLSDemonstration
{
    public class PacketMessages
    {
        public string Time { get; set; }
        public string Source { get; set; }
        public int SrcPort { get; set; }
        public string Destination { get; set; }
        public int DestPort { get; set; }
        public string Protocol { get; set; }
        public int PkgSize { get; set; }

    }
}
