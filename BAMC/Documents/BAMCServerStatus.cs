using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BAMC.Documents.BAMCServerStatus
{
    public class BAMCServerStatus
    {
        public BAMCVersionProtocol version { get; set; } = new BAMCVersionProtocol();
        public BAMCOnlinePlayers players { get; set; } = new BAMCOnlinePlayers();
        public BAMCServerDescription description { get; set; } = new BAMCServerDescription();
        //public string favicon { get; set; }
    }
    public class BAMCVersionProtocol
    {
        public string name { get; set; }
        public int protocol { get; set; }
    }
    public class BAMCOnlinePlayers
    {
        public int max { get; set; }
        public int online { get; set; }
    }
    public class BAMCServerDescription
    {
        public string text { get; set; }
    }
}
