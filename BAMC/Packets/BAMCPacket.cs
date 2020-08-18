using BAMC.Enumerables;
using System.Collections.Generic;

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
            index = RawPacket.ReadVarInt(index, out Length);
            int indexCompare = RawPacket.ReadVarInt(index, out int packetType);
            Length -= indexCompare - index;
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
        public BAMCPacket() { }
        public virtual void Send()
        {
            byte[] packetID = new List<byte>().WriteVarInt((int)PacketID).ToArray();
            List<byte> buffer = new List<byte>();
            buffer.WriteVarInt(Payload.Length + packetID.Length);
            buffer.AddRange(packetID);
            buffer.AddRange(Payload);
            Client.Stream.Write(buffer.ToArray());
            Client.Stream.Flush();
        }
    }
}
