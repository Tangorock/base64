using System.IO;
namespace Base64
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] vstupData = new char[4];
            byte[] decodeData = new byte[4];
            byte[] vystupData = new byte[3];

            FileStream fileReader = new FileStream("zadani\\Obrazek.b64", FileMode.Open, FileAccess.Read);
            FileStream fileWriter = new FileStream("vysledek\\Obrazek.jpg", FileMode.Create, FileAccess.Write);

            //FileStream fileReader = new FileStream("zadani\\Text.b64", FileMode.Open, FileAccess.Read);
            //FileStream fileWriter = new FileStream("vysledek\\TextVysledek.txt", FileMode.Create, FileAccess.Write);

            BinaryReader binReader = new BinaryReader(fileReader);
            BinaryWriter binWriter = new BinaryWriter(fileWriter);

            while (fileReader.Position < fileReader.Length)
            {
                int i = 0;
                while ((i < 4) && (fileReader.Position < fileReader.Length))
                {
                    vstupData[i] = binReader.ReadChar();

                    if ((vstupData[i] > 64) && (vstupData[i] < 91)) decodeData[i] = (byte)(vstupData[i] - 65);
                    else if ((vstupData[i] > 96) && (vstupData[i] < 123)) decodeData[i] = (byte)(vstupData[i] - 97 + 26);
                    else if ((vstupData[i] > 47) && (vstupData[i] < 58)) decodeData[i] = (byte)(vstupData[i] - 48 + 52);
                    else if (vstupData[i] == '+') decodeData[i] = 62;
                    else if (vstupData[i] == '/') decodeData[i] = 63;
                    else vstupData[i] = (char)128;

                    if (vstupData[i] != 128) i++;
                }
                vystupData[0] = (byte)(((decodeData[0] & 0x3F) << 2) + ((decodeData[1] & 0x30) >> 4));
                vystupData[1] = (byte)(((decodeData[1] & 0x0F) << 4) + ((decodeData[2] & 0x3C) >> 2));
                vystupData[2] = (byte)(((decodeData[2] & 0x03) << 6) + decodeData[3]);

                for (int j = 0; j < 3; j++) binWriter.Write(vystupData[j]);
            }
            binReader.Close();
            binWriter.Close();
        }
    }
}
