using System.Security.Cryptography;
using System.Text;

namespace EventRun_Api.Utils
{
    public class Security
    {
        public static string GetSHA256(string strToEncrypt)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new();
            byte[]? stream = null;
            StringBuilder sb = new();
            stream = sha256.ComputeHash(encoding.GetBytes(strToEncrypt));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
