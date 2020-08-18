using BAMC.Enumerables;
using BAMC.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BAMC
{
    public class BAMCClient
    {
        public static List<BAMCClient> Clients = new List<BAMCClient>();
        public TcpClient TcpClient;
        public Stream Stream;
        public StreamReader StreamReader;
        public StreamWriter StreamWriter;
        public BAMCClientState State = BAMCClientState.Handshaking;
        public BAMCClient(TcpClient TcpClient)
        {
            this.TcpClient = TcpClient;
            Stream = TcpClient.GetStream();
            StreamReader = new StreamReader(Stream, Encoding.ASCII);
            StreamWriter = new StreamWriter(Stream, Encoding.ASCII);
            Clients.Add(this);
            ClientMessageLoop();
        }
        public void Disconnect()
        {
            Console.WriteLine("Client disconnected: " + TcpClient.Client.RemoteEndPoint.ToString() + ".");
            Clients.Remove(this);
            TcpClient.Close();
        }
        private void ClientMessageLoop()
        {
            Task.Run(() => {
                try
                {
                    while (true)
                    {
                        byte[] buffer = new byte[256];
                        int count = Stream.Read(buffer, 0, 256);
                        if (count == 0)
                        {
                            Disconnect();
                            break;
                        }
                        BAMCProtocolProcessor.ProcessGenericPacket(new BAMCPacket(buffer, this));
                    }
                }
                catch (Exception)
                {
                    Disconnect();
                }
            });
        }
        private void RespondToPing()
        {
            StreamWriter.Write("\x7a\x00\x78{\"description\":{\"text\":\"A Minecraft Server\"},\"players\":{\"max\":20,\"online\":0},\"version\":{\"name\":\"1.16.2\",\"protocol\":751}}");
            StreamWriter.Flush();
        }
    }
}
