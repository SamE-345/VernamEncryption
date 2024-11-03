using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Vernam
{ 
    internal class Program
    {
        public static class Global
        {
            public static string Key="";
        }   
       
        static void Main(string[] args)
        {
            
            string Ecrypt_Text = encrypt();
            string Decrypt_Txt = decrypt(Ecrypt_Text);
            
        }
        static string encrypt()
        {
            Console.WriteLine("Enter Plaintext");
            string plaintext = Console.ReadLine();

            Console.WriteLine("Enter Key");
            Global.Key = Console.ReadLine();

            // Plaintext converted into a binary array
            byte[] BinaryPlaintext = Encoding.ASCII.GetBytes(plaintext);
            // Key converted into binary array
            byte[] BinaryKey = Encoding.ASCII.GetBytes(Global.Key);
            int[] EncryptedText = new int[128];

            // XOR the binary key and binary plaintext
            for (int i = 0; i < BinaryPlaintext.Length; i++)
            {
                EncryptedText[i] = BinaryKey[i] ^ BinaryPlaintext[i];    
            }

            string chars = " ";
            int j = 0;
            while (EncryptedText[j] > 0)
            {
                // Convert back into ascii characters
                chars += (EncryptedText[j])+",";
                j++;
            }
            Console.WriteLine(chars);
            return chars;

        }
        static string decrypt(string EncryptedTxt)
        {
            // Splits the CSV numbers into an array
            string[] split = EncryptedTxt.Split(',');
            // Initialise array for the binary plaintext
            byte[] BinaryPlaintext = new byte[split.Length];
            

            for (int i = 0; i < split.Length-1; i++)
            {
                BinaryPlaintext[i] = ToBinary(split[i]); 
            }
           
            byte[] BinaryKey = Encoding.ASCII.GetBytes(Global.Key);
            int[] EncryptedText = new int[128];

            for (int i = 0; i < BinaryKey.Length; i++)
            {
                EncryptedText[i] = BinaryKey[i] ^ BinaryPlaintext[i];
            }
            string chars = "";
            int j = 0;
            while (EncryptedText[j] > 0)
            {
                chars += Convert.ToChar(EncryptedText[j]);
                
                j++;
            }
            Console.WriteLine(chars + " Decrypted");
            return chars;

            
        }
        static byte ToBinary(string num)
        {
           
            int AsciiNum = Convert.ToInt32(num);
            
            string binary = Convert.ToString(AsciiNum, 2);
            if (binary.Length != 8)
            {
                for (int i = 0; i < 8 - binary.Length; i++)
                {
                    binary = "0"+binary;
                }
            }
            byte binaryByte = Convert.ToByte(binary,2);
            return binaryByte;
            
        }
    }
}
