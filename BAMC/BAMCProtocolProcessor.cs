using BAMC.Documents.BAMCServerStatus;
using BAMC.Enumerables;
using BAMC.Packets;
using BAMC.Packets.Inbound;
using BAMC.Packets.Outbound;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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

            Console.WriteLine();

            if (packet.PacketID == PacketID.Handshake && packet.Client.State == BAMCClientState.Handshaking)
            {
                ProcessInitialHandshake(new BAMCInitialHandshake(packet));
                return;
            }

            if (packet.PacketID == PacketID.PingPong && packet.Client.State == BAMCClientState.Status)
            {
                ProcessPingResponse(packet);
                return;
            }
        }

        public static void ProcessInitialHandshake(BAMCInitialHandshake packet)
        {
            Console.WriteLine("Protocol Version: " + packet.MCProtocolVersion);
            Console.WriteLine("Server Address: " + packet.ServerAddress);
            Console.WriteLine("Server Port: " + packet.ServerPort);
            Console.WriteLine("Next State: " + packet.NextState);

            var asdf = new BAMCStatusResponse()
            {
                Client = packet.Client,
                JSONResponse = JsonSerializer.Serialize(new BAMCServerStatus()
                {
                    version =
                    {
                        name = "1.16.2",
                        protocol = 751
                    },
                    players =
                    {
                        max = 10000,
                        online = 8468
                    },
                    description =
                    {
                        text = "BAMC Native Server"
                    }
                })
            };
            asdf.Send();

            packet.Client.State = packet.NextState;
        }

        public static void ProcessPingResponse(BAMCPacket packet)
        {
            packet.Send();
            packet.Client.Disconnect();
        }
    }
}
