using BAMC.Packets;
using System;
using System.Collections.Generic;
using System.Text;
using static BAMC.BAMCEnumerables;

namespace BAMC
{
    static class BAMCProtocolProcessor
    {
        public static void ProcessGenericPacket(BAMCPacket packet)
        {
            Console.WriteLine("Packet Length: " + packet.Length);
            Console.WriteLine("Packet ID: " + packet.PacketID);
            Console.WriteLine("Current State: " + packet.Client.State);
            Console.WriteLine("Packet Payload: " + BitConverter.ToString(packet.Payload));

            if (packet.PacketID == PacketID.Handshake && packet.Client.State == BAMCClientState.Handshaking)
            {
                ProcessInitialHandshake(new BAMCPacketInitialHandshake(packet));
            }

            Console.WriteLine();
        }

        public static void ProcessInitialHandshake(BAMCPacketInitialHandshake packet)
        {
            Console.WriteLine("Protocol Version: " + packet.MCProtocolVersion);
            Console.WriteLine("Server Address: " + packet.ServerAddress);
            Console.WriteLine("Server Port: " + packet.ServerPort);
            Console.WriteLine("Next State: " + packet.NextState);

            packet.Client.State = packet.NextState;
        }
    }
}
