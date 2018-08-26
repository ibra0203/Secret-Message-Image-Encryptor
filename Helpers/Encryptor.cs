using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace MessageDecoder
{

    //A class that handles Encryption operations
    public class Encryptor
    {
        /*Encryption 
        Input => Encoded bytes => Binary=> pixels

        Decryption 
        pixels => binary => string =>decryption*/

        //takes in a string and a key and returns binary
        public static string FinalEncrypt(string toEncrypt, string key)
        {
            string result = "";
            //Encrypt the message into a base64 string byte array using the key
            byte[] encr = Encrypt(toEncrypt, key);
            //Convert the byte array into a binary string
            result = ByteToBinary(encr);
            //Return the binary string
            return result;
        }

        //takes in binary and a key and returns decrypted string
        public static string FinalDecrypt(string binary, string key)
        {
            string result = "";
            //Convert the binary string into base64 string
            string decryptMe = BinaryToString(binary);
            //Decrypt the base64 string using the key
            result = Decrypt(decryptMe, key);
            //Return the resulting message
            return result;
        }

        //Encrypt message with key using the MD5 hashing algorithm. Returns a base64 byte array. 
        public static byte[] Encrypt(string ToEncrypt, string key)

        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(ToEncrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

            TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider();
            t.Key = keyArray;
            t.Mode = CipherMode.ECB;
            t.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = t.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            t.Clear();
            return resultArray;
        }

        //Decrypts the message from the base64 string and the key
        public static string Decrypt(string ToDecrypt, string key)
        {
            string result = "";
            try
            {
                byte[] decryptArray = Convert.FromBase64String(ToDecrypt);
                byte[] keyArray;
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                TripleDESCryptoServiceProvider t = new TripleDESCryptoServiceProvider();
                t.Key = keyArray;
                t.Mode = CipherMode.ECB;
                t.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = t.CreateDecryptor();

                byte[] resultArray = cTransform.TransformFinalBlock(decryptArray, 0, decryptArray.Length);
                t.Clear();

                result = UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch(Exception ex)
            {
                result = null;
            }
            return result;
        }

        //Converts the base64 string byte array into a binary string
        public static string ByteToBinary(byte[] toConvert)
        {
            string result = "";
            result = string.Join("",
                toConvert.Select(x => Convert.ToString(x, 2).PadLeft(8, '0')));
            return result;
        }

        //Converts a binary string into a base64 string
        public static string BinaryToString(string _binary)
        {
            string result = "";
            //Convert to byte first
            int numOfBytes = _binary.Length / 8;
            byte[] bytes = new byte[numOfBytes];
            for (int i = 0; i < numOfBytes; ++i)
            {
                bytes[i] = Convert.ToByte(_binary.Substring(8 * i, 8), 2);
            }
            result = BytesToUnreadableString(bytes);
            return result;
        }
        //Converts a byte array into a base64 string
        public static string BytesToUnreadableString(byte[] _b)
        {
            string result = "";
            result = Convert.ToBase64String(_b, 0, _b.Length);
            return result;
        }

        
    }
}