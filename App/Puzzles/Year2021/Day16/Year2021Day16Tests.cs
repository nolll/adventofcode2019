using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace App.Puzzles.Year2021.Day16
{
        public class Year2021Day16Tests
    {
        [Test]
        public void Part1()
        {
            var decoder = new BitsDecoder();
            var result = decoder.CountTypes(Input);

            Assert.That(result, Is.EqualTo(result));
        }

        [Test]
        public void BinaryString()
        {
            var result = BitsPacket.FromHex("D2FE28");

            Assert.That(result.Binary, Is.EqualTo("110100101111111000101"));
        }

        [Test]
        public void PacketVersion()
        {
            var result = BitsPacket.FromBinary("110100101111111000101000");

            Assert.That(result.Version, Is.EqualTo(6));
        }

        [Test]
        public void PacketType()
        {
            var result = BitsPacket.FromBinary("110100101111111000101000");

            Assert.That(result.Type, Is.EqualTo(4));
        }

        [Test]
        public void LiteralValue()
        {
            var result = BitsPacket.FromBinary("110100101111111000101000");

            Assert.That(result.LiteralValue, Is.EqualTo(2021));
        }

        [Test]
        public void Part2()
        {
            var result = 0;

            Assert.That(result, Is.EqualTo(0));
        }

        private const string Input = "A052E04CFD9DC0249694F0A11EA2044E200E9266766AB004A525F86FFCDF4B25DFC401A20043A11C61838600FC678D51B8C0198910EA1200010B3EEA40246C974EF003331006619C26844200D414859049402D9CDA64BDEF3C4E623331FBCCA3E4DFBBFC79E4004DE96FC3B1EC6DE4298D5A1C8F98E45266745B382040191D0034539682F4E5A0B527FEB018029277C88E0039937D8ACCC6256092004165D36586CC013A008625A2D7394A5B1DE16C0E3004A8035200043220C5B838200EC4B8E315A6CEE6F3C3B9FFB8100994200CC59837108401989D056280803F1EA3C41130047003530004323DC3C860200EC4182F1CA7E452C01744A0A4FF6BBAE6A533BFCD1967A26E20124BE1920A4A6A613315511007A4A32BE9AE6B5CAD19E56BA1430053803341007E24C168A6200D46384318A6AAC8401907003EF2F7D70265EFAE04CCAB3801727C9EC94802AF92F493A8012D9EABB48BA3805D1B65756559231917B93A4B4B46009C91F600481254AF67A845BA56610400414E3090055525E849BE8010397439746400BC255EE5362136F72B4A4A7B721004A510A7370CCB37C2BA0010D3038600BE802937A429BD20C90CCC564EC40144E80213E2B3E2F3D9D6DB0803F2B005A731DC6C524A16B5F1C1D98EE006339009AB401AB0803108A12C2A00043A134228AB2DBDA00801EC061B080180057A88016404DA201206A00638014E0049801EC0309800AC20025B20080C600710058A60070003080006A4F566244012C4B204A83CB234C2244120080E6562446669025CD4802DA9A45F004658527FFEC720906008C996700397319DD7710596674004BE6A161283B09C802B0D00463AC9563C2B969F0E080182972E982F9718200D2E637DB16600341292D6D8A7F496800FD490BCDC68B33976A872E008C5F9DFD566490A14";
    }

    public class BitsDecoder
    {
        public int CountTypes(string input)
        {
            var outerBinary = BitsPacket.FromHex(input);

            return 0;
        }
    }

    public class BitsPacket
    {
        private int _consumedBits = 0;

        public string Binary { get; }
        public int Version { get; }
        public int Type { get; }
        public int LiteralValue { get; }
        public List<BitsPacket> SubPackets { get; }
        public int BinaryLength => _consumedBits;

        private string ConsumeBinary(string binary, int count)
        {
            _consumedBits += count;
            return binary[count..];
        }

        private BitsPacket(string binary)
        {
            Binary = binary;
            Version = GetPacketVersion(binary);
            binary = ConsumeBinary(binary, 3);
            Type = GetPacketType(binary);
            binary = ConsumeBinary(binary, 3);

            if (Type == 4)
            {
                LiteralValue = GetLiteralValue(binary);
            }
            else
            {
                SubPackets = new List<BitsPacket>();
                var lengthTypeId = binary[..1];
                binary = ConsumeBinary(binary, 1);

                if (lengthTypeId == "0")
                {
                    var subPacketLength = GetSubPacketLength(binary);
                    binary = ConsumeBinary(binary, 15);
                    var totalSubPacketBits = 0;

                    while (totalSubPacketBits < subPacketLength)
                    {
                        var subPacket = FromBinary(binary);
                        var binaryLength = subPacket.BinaryLength;
                        totalSubPacketBits += binaryLength;
                        ConsumeBinary(binary, binaryLength);
                        SubPackets.Add(subPacket);
                    }
                    
                }
                else
                {
                    var subPacketCount = GetSubPacketCount(binary);
                    binary = ConsumeBinary(binary, 11);
                    var totalSubPacketBits = 0;
                    var i = 0;

                    while (i < subPacketCount)
                    {
                        var subPacket = FromBinary(binary);
                        var binaryLength = subPacket.BinaryLength;
                        totalSubPacketBits += binaryLength;
                        ConsumeBinary(binary, binaryLength);
                        SubPackets.Add(subPacket);
                        i++;
                    }
                }
            }
        }

        private int GetSubPacketCount(string binary)
        {
            var s = binary[..11];
            return Convert.ToInt32(s, 2);
        }

        private int GetSubPacketLength(string binary)
        {
            var s = binary[..15];
            return Convert.ToInt32(s, 2);
        }

        private int GetLiteralValue(string binary)
        {
            ConsumeBinary(binary, 15);
            var part1 = binary.Substring(1, 4);
            var part2 = binary.Substring(6, 4);
            var part3 = binary.Substring(11, 4);
            var s = $"{part1}{part2}{part3}";
            return Convert.ToInt32(s, 2);
        }

        private int GetPacketVersion(string bitString)
        {
            var s = bitString[..3];
            return Convert.ToInt32(s, 2);
        }

        private int GetPacketType(string bitString)
        {
            var s = bitString[..3];
            return Convert.ToInt32(s, 2);
        }

        public static BitsPacket FromHex(string hexString)
        {
            var binaryStrings = hexString.ToCharArray().Select(o => CharToBitString[o]);
            var binaryString = string.Join("", binaryStrings).TrimEnd('0');
            return new BitsPacket(binaryString);
        }

        public static BitsPacket FromBinary(string binaryString)
        {
             return new(binaryString);
        }

        private static readonly Dictionary<char, string> CharToBitString = new()
        {
            {'0', "0000"},
            {'1', "0001"},
            {'2', "0010"},
            {'3', "0011"},
            {'4', "0100"},
            {'5', "0101"},
            {'6', "0110"},
            {'7', "0111"},
            {'8', "1000"},
            {'9', "1001"},
            {'A', "1010"},
            {'B', "1011"},
            {'C', "1100"},
            {'D', "1101"},
            {'E', "1110"},
            {'F', "1111"}
        };
    }
}