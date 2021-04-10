using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLSDemonstration.PacketData
{
    class PacketType
    {
        PacketIP _ip;
        PacketTcp _tcp;

        public PacketType(PacketIP ip)
        {
            _ip = ip;
        }
        public PacketType(PacketIP ip, PacketTcp tcp)
        {
            _ip = ip;
            _tcp = tcp;
        }

        public PacketIP IP
        {
            get { return _ip; }
        }
        public PacketTcp TCP
        {
            get { return _tcp; }
        }
    }
}
