using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Libs
{
    public class Encrypt
    {
        public static string iv = "";
        public static string key = "";

        static public String EncryptRJ256(string plainText)
        {
            var encoding = System.Text.Encoding.UTF8;//new UTF8Encoding();

            var Key = ComputeSha256Hash(key);//encoding.GetBytes(EncryptStringMD5(key));
            var IV = Md5Sum_Raw(iv);//encoding.GetBytes(EncryptStringMD5(iv));

            byte[] encrypted;

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = PaddingMode.PKCS7;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;
                    rj.Key = Key;
                    rj.IV = IV;

                    var ms = new MemoryStream();

                    using (var cs = new CryptoStream(ms, rj.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        using (var sr = new StreamWriter(cs))
                        {
                            sr.Write(plainText);
                            sr.Flush();
                            cs.FlushFinalBlock();
                        }
                        encrypted = ms.ToArray();
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        static public String DecryptRJ256(string input)
        {
            byte[] cypher = Convert.FromBase64String(input);

            var sRet = "";

            var Key = ComputeSha256Hash(key);//encoding.GetBytes(EncryptStringMD5(key));
            var IV = Md5Sum_Raw(iv);//encoding.GetBytes(EncryptStringMD5(iv));

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = PaddingMode.PKCS7;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 256;
                    rj.BlockSize = 128;
                    rj.Key = Key;
                    rj.IV = IV;
                    var ms = new MemoryStream(cypher);

                    using (var cs = new CryptoStream(ms, rj.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            sRet = sr.ReadLine();
                        }
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return sRet;
        }

        static byte[] ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                return bytes;
            }
        }

        public static byte[] Md5Sum_Raw(string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(bytes);
        }
    }
}
