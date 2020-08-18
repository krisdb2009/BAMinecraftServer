using BAMC.Enumerables;

namespace BAMC.Packets.Inbound
{
    public class BAMCInitialHandshake : BAMCPacket
    {
        public int MCProtocolVersion;
        public string ServerAddress;
        public ushort ServerPort;
        public BAMCClientState NextState;
        public BAMCInitialHandshake(BAMCPacket Packet) : base(Packet)
        {
            int index = 0;
            index = Payload.ReadVarInt(index, out MCProtocolVersion);
            index = Payload.ReadString(index, out ServerAddress);
            index = Payload.ReadUShort(index, out ServerPort);
            Payload.ReadVarInt(index, out int nextState);
            NextState = (BAMCClientState)nextState;
        }
    }
}