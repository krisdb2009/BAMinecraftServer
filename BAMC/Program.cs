using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BAMC
{
    class Program
    {
        static readonly TcpListener Server = new TcpListener(IPAddress.Any, 25565);
        static void Main(/*string[] args*/)
        {
            Console.WriteLine("Starting server...");
            Server.Start();
            Console.WriteLine("Accepting connections...");
            AcceptConnectLoop();
            Console.WriteLine("Done.");
            Console.ReadLine();
        }
        static void AcceptConnectLoop()
        {
            Task.Run(() => {
                while (true)
                {
                    TcpClient client = Server.AcceptTcpClient();
                    new BAMCClient(client);
                }
            });
        }
    }
}
