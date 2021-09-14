using Org.BouncyCastle.Utilities.Encoders;
using System;

namespace Asn1Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            string asn1Str = "304402200673B17352B56B4CF1DC1C13C4387FA56982C8AB189ED876233094E56B2DB1840220649543EECA5DD91DD5A006E0A51F4800612D5875A3E8E993CC6784C1AC3DC73C";
            //string[] RS = GetRAndS(asn1Str);
            string rStr = "6A9F365BCC93B1828072B0C997076E044FA0F325BA1D7122D2F6FC3869C3AC65";
            string sStr = "8F2820C4CC48121C3A994D02015B557234C9A827DAB1C7858ACF7207F075E3F8";

            string asn1 = Asn1Encode(rStr, sStr);
            Console.WriteLine(asn1);
        }
        
        static string[] GetRAndS(string asn1Str)
        {
            int index = 0;
            int bitLength = 0;
            string r = null;
            string s = null;

            //get r
            index = 6;
            bitLength = Convert.ToInt32(new string(asn1Str.ToCharArray(), index, 2), 16) * 2;
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
            return new string[] { r, s };
        }

        static string Asn1Encode(string r, string s)
        {
            string asn1Str = null;
            byte firstHalfByte = 0;
            int firstBit = 0;

            byte[] rByte = Hex.Decode(r);
            firstHalfByte = rByte[0];
            firstBit = firstHalfByte >> 7;
            if (firstBit == 1)
                r = "00" + r;

            byte[] sByte = Hex.Decode(s);
            firstHalfByte = sByte[0];
            firstBit = firstHalfByte >> 7;
            if (firstBit == 1)
                s = "00" + s;

            int rLength = r.Length / 2;
            int sLength = s.Length / 2;

            string rs = "02" + rLength.ToString("X") + r + "02" + sLength.ToString("X") + s;
            int rsLength = rs.Length / 2;
            asn1Str = "30" + rsLength.ToString("X") + rs;
            return asn1Str;
        }
    }
}

