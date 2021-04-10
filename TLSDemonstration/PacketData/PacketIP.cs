using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace TLSDemonstration.PacketData
{
    public class PacketIP
    {
        private byte      _VersionAndHeader;
        private byte      _TypeOfService;
        private ushort    _TotalLenght;
        private ushort    _Identification;  
        private ushort    _FlagsAndOffset;
        private byte      _TTL;
        private byte      _Protocol;
        private short     _Checksum;
                                                   
        private uint      _SourceAddress;
        private uint      _DestinationAddress;
        
        private byte      _HeaderLength;
           
        private byte[]    _Data = new byte[8192];


        public PacketIP(byte[] bBuffer, int iReceived)
        {
                MemoryStream memoryStream = null;
                BinaryReader binaryReader = null;

                try
                {
                    memoryStream = new MemoryStream(bBuffer, 0, iReceived);
                    binaryReader = new BinaryReader(memoryStream);

                    _VersionAndHeader = binaryReader.ReadByte();

                    _TypeOfService = binaryReader.ReadByte();

                    _TotalLenght = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _Identification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _FlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _TTL = binaryReader.ReadByte();

                    _Protocol = binaryReader.ReadByte();

                    _Checksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _SourceAddress = (uint)(binaryReader.ReadInt32());

                    _DestinationAddress = (uint)(binaryReader.ReadInt32());

                    _HeaderLength = _VersionAndHeader;

                    _HeaderLength <<= 4;
                    _HeaderLength >>= 4;

                    _HeaderLength *= 4;

                    Array.Copy(bBuffer, _HeaderLength, _Data, 0, _TotalLenght - _HeaderLength);
                }
                finally
                {
                    binaryReader.Close();
                    memoryStream.Close();
                }
        }

        public string Version
        {
            get
            {
                
                if ((_VersionAndHeader >> 4) == 4)
                    return "IP v4";
                else if ((_VersionAndHeader >> 4) == 6)
                    return "IP v6";
                else
                    return "Unknown";
            }
        }

        public string HeaderLength
        {
            get { return _HeaderLength.ToString(); }
        }

        public ushort MessageLength
        {
            get{ return (ushort)(_TotalLenght - _HeaderLength); }
        }

        public string TypeOfService
        {
            get { return string.Format("0x{0:x2} ({1})", _TypeOfService, _TypeOfService); }
        }

        public string Flags
        {
            get
            {
                int iFlags = _FlagsAndOffset >> 13;
                if (iFlags == 2)
                    return "Not fragmented";
                else if (iFlags == 1)
                    return "Fragmented";
                else
                    return iFlags.ToString();
            }
        }

        public string FragmentationOffset
        {
            get
            {
                int iOffset = _FlagsAndOffset << 3;
                iOffset >>= 3;

                return iOffset.ToString();
            }
        }

        public string TTL
        {
            get{ return _TTL.ToString(); }
        }

        public string Protocol
        {
            get
            {
                if (_Protocol == 6)       
                    return "TCP";
                else
                    return "Unknown";
            }
        }

        public string Checksum
        {
            get{ return "0x" + _Checksum.ToString("x"); }
        }

        public IPAddress SourceAddress
        {
            get{ return new IPAddress(_SourceAddress); }
        }

        public IPAddress DestinationAddress
        {
            get { return new IPAddress(_DestinationAddress); }
        }

        public string TotalLength
        {
            get{ return _TotalLenght.ToString(); }
        }

        public string Identification
        {
            get{ return _Identification.ToString(); }
        }

        public byte[] Data
        {
            get{ return _Data; }
        }
    }
}
