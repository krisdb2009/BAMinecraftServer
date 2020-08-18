using System.Text;
using System.Text.Unicode;

namespace BAMC
{
    static class BAMCByteDecoder
    {
        /// <summary>
        /// Gets a Variable Integer from a byte array.
        /// </summary>
        /// <param name="source">byte[]: Source Array</param>
        /// <param name="StartPosition">int: Starting position to look for the Var Int.</param>
        /// <param name="value">out int: Value of the Var Int.</param>
        /// <returns>int: The new position in the array.</returns>
        public static int GetVarInt(this byte[] source, int StartPosition, out int value)
        {
            value = 0;
            var count = 0;
            while (count < 5)
            {
                byte @byte = source[StartPosition + count];
                value |= @byte << 7 * count;
                if (@byte == 0) break;
                count++;
                if ((@byte & 0b10000000) != 0b10000000) break;
            }
            return StartPosition + count;
        }
        public static int GetString(this byte[] source, int StartPosition, out string value)
        {
            int index = source.GetVarInt(StartPosition, out int length);
            value = Encoding.UTF8.GetString(source, index, length);
            return index + length;
        }
        public static int GetUShort(this byte[] source, int StartPosition, out ushort value)
        {
            value = 0;
            if (source.Length - StartPosition >= 2)
            {
                value = (ushort)((source[StartPosition++] << 8) | source[StartPosition++]);
            }
            return StartPosition;
        }
    }
}