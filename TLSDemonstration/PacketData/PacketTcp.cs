using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace TLSDemonstration.PacketData
{
    public class PacketTcp
    {
        private ushort _SourcePort;      
        private ushort _DestinationPort;  
        private uint   _SequenceNumber;  
        private uint   _AckNumber;
        private ushort _DataOffsetAndFlags;
        private ushort _Window; 
        private short  _Checksum;
                                                    
        private ushort _UrgentPointer;   

        private byte   _HeaderLength; 
        private ushort _MessageLength;  
        private byte[] _Data = new byte[4096];
       
        public PacketTcp(byte [] bBuffer, int iReceived)
        {
                MemoryStream memoryStream = null;
                BinaryReader binaryReader = null;

                try
                {
                    memoryStream = new MemoryStream(bBuffer, 0, iReceived);
                    binaryReader = new BinaryReader(memoryStream);

                    _SourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _DestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _SequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());

                    _AckNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());

                    _DataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _Window = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _Checksum = (short)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _UrgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    _HeaderLength = (byte)(_DataOffsetAndFlags >> 12);
                    _HeaderLength *= 4;

                    _MessageLength = (ushort)(iReceived - _HeaderLength);

                    Array.Copy(bBuffer, _HeaderLength, _Data, 0, iReceived - _HeaderLength);
                }
                catch (Exception) { }

                finally
                {
                    binaryReader.Close();
                    memoryStream.Close();
                }
        }

        public string SourcePort
        {
            get { return _SourcePort.ToString(); }
        }

        public string DestinationPort
        {
            get { return _DestinationPort.ToString(); }
        }

        public string SequenceNumber
        {
            get { return _SequenceNumber.ToString(); }
        }

        public string AcknowledgementNumber
        {
            get
            {
                if ((_DataOffsetAndFlags & 0x10) != 0)
                    return _AckNumber.ToString();
                else
                    return "";
            }
        }

        public string HeaderLength
        {
            get { return _HeaderLength.ToString(); }
        }

        public string WindowSize
        {
            get { return _Window.ToString(); }
        }

        public string UrgentPointer
        {
            get
            {
                if ((_DataOffsetAndFlags & 0x20) != 0)
                    return _UrgentPointer.ToString();
                else
                    return "";
            }
        }

        public string Flags
        {
            get
            {
                int iFlags = _DataOffsetAndFlags & 0x3F;
 
                string strFlags = string.Format ("0x{0:x2} ", iFlags);

                if ((iFlags & 0x01) != 0)
                    strFlags += "FIN  ";

                if ((iFlags & 0x02) != 0)
                    strFlags += "SYN  ";

                if ((iFlags & 0x04) != 0)
                    strFlags += "RST  ";

                if ((iFlags & 0x08) != 0)
                    strFlags += "PSH  ";

                if ((iFlags & 0x10) != 0)
                    strFlags += "ACK  ";

                if ((iFlags & 0x20) != 0)
                    strFlags += "URG ";

                if (strFlags.Contains("()"))
                    strFlags = strFlags.Remove(strFlags.Length - 3);

                else if (strFlags.Contains(", )"))
                    strFlags = strFlags.Remove(strFlags.Length - 3, 2);

                return strFlags;
            }
        }

        public string Checksum
        {
            get { return "0x" + _Checksum.ToString("x"); }
        }

        public byte[] Data
        {
            get { return _Data; }
        }

        public string MessageLength
        {
            get { return _MessageLength.ToString(); }
        }
    }
}
