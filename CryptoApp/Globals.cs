using System.Text;

namespace SimpleBlockchain
{
    public static class Globals
    {
        public static Encoding Encoding => Encoding.UTF8;
        public static double Gas => 0;
        public static byte[] DefaultExponent => new byte[] { 0x01, 0x00, 0x01 };
    }
}
