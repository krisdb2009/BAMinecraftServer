using System;
using System.Collections.Generic;
using System.Text;
using static BAMC.BAMCEnumerables;

namespace BAMC.Packets
{
    public class BAMCPacket
    {
        public BAMCClient Client;
        public int Length;
        public PacketID PacketID;
        public byte[] Payload;
        public BAMCPacket(byte[] RawPacket, BAMCClient Client)
        {
            int index = 0;
            index = RawPacket.GetVarInt(index, out Length);
            int packetType;
            RawPacket.GetVarInt(index, out packetType);
            PacketID = (PacketID)packetType;
            Payload = new byte[Length];
            this.Client = Client;
            for (int count = 0; count < Length; count++)
            {
                Payload[count] = RawPacket[count + 2];
            }
        }
        public BAMCPacket(BAMCPacket Packet)
        {
            Length = Packet.Length;
            PacketID = Packet.PacketID;
            Payload = Packet.Payload;
            Client = Packet.Client;
        }
    }
    public class BAMCPacketInitialHandshake : BAMCPacket
    {
        public int MCProtocolVersion;
        public string ServerAddress;
        public ushort ServerPort;
        public BAMCClientState NextState;
        public BAMCPacketInitialHandshake(BAMCPacket Packet) : base(Packet)
        {
            int index = 0;
            index = Payload.GetVarInt(index, out MCProtocolVersion);
            index = Payload.GetString(index, out ServerAddress);
            index = Payload.GetUShort(index, out ServerPort);
            int nextState;
            Payload.GetVarInt(index, out nextState);
            NextState = (BAMCClientState)nextState;
        }
    }
}
