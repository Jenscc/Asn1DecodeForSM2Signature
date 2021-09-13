using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Asn1Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            string asn1Str = "304402200673B17352B56B4CF1DC1C13C4387FA56982C8AB189ED876233094E56B2DB1840220649543EECA5DD91DD5A006E0A51F4800612D5875A3E8E993CC6784C1AC3DC73C";
            int index = 0;
            int bitLength = 0;
            string r = null;
            string s = null;

            //get r
            index = 6;
            bitLength = Convert.ToInt32(new string(asn1Str.ToCharArray(), index, 2), 16)*2;
            index += 2;
            r = new string(asn1Str.ToCharArray(), index, bitLength);
            if (r.StartsWith("00"))
                r = new string(r.ToCharArray(), 2, r.Length - 2);

            //get s
            index += bitLength + 2;
            bitLength = Convert.ToInt32(new string(asn1Str.ToCharArray(), index, 2), 16) * 2;
            index += 2;
            s = new string(asn1Str.ToCharArray(), index, bitLength);
            if (s.StartsWith("00"))
                s = new string(s.ToCharArray(), 2, s.Length - 2);

            Console.WriteLine(asn1Str);
            Console.WriteLine(r);
            Console.WriteLine(s);
        }
    }
}

