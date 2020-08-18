﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BAMC
{
    public static class BAMCEnumerables
    {
        public enum PacketID
        {
            Handshake = 0x00
        }
        public enum BAMCClientState
        {
            Handshaking = 0,
            Status = 1,
            Login = 2,
            Play = 3
        }
    }
}