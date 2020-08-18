using BAMC.Enumerables;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BAMC.Packets.Outbound
{
    class BAMCStatusResponse : BAMCPacket
    {
        public string JSONResponse;
        public BAMCStatusResponse()
        {
            PacketID = PacketID.Handshake;
        }
        public override void Send()
        {
            Payload = new List<byte>().WriteString(JSONResponse).ToArray();
            base.Send();
        }
    }
}
