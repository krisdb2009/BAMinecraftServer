using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;

namespace BAMC
{
    static class BAMCByteEncoder
    {
        public static List<byte> WriteVarInt(this List<byte> buffer, int value)
        {
            while (true)
            {
                if (value > 127)
                {
                    buffer.Add((byte)((value & 0b1111111) | 0b10000000));
                    value >>= 7;
                }
                else
                {
                    buffer.Add((byte)value);
                    break;
                }
            }
            return buffer;
        }
        public static List<byte> WriteString(this List<byte> buffer, string value)
        {
            byte[] @string = Encoding.UTF8.GetBytes(value);
            buffer.WriteVarInt(@string.Length);
            buffer.AddRange(@string);
            return buffer;
        }
    }
}