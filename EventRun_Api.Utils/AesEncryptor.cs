using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventRun_Api.Utils
{
    public class AesEncryptor
    {
        static byte[] key = Convert.FromBase64String("DcZ6AQxh2F7/Lqqb0rlwAhA2fy57JuLdyRBq5spbJYE=");
        static byte[] iv = Convert.FromBase64String("5mSnyTtUookKdc/datO4Dg==");

        public static byte[] GenerarLLave()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateKey();
                return aesAlg.Key;
            }
        }
        public static string EncriptarAES(string texto)
        {
            byte[] llave = key;
            byte[] version = iv;
            if (texto == null || texto.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (llave == null || llave.Length <= 0)
                throw new ArgumentNullException("Key");
            if (version == null || version.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = llave;
                aesAlg.IV = version;
                aesAlg.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(texto);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(encrypted);

        }
    }
}
