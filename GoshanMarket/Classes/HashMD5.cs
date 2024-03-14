using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoshanMarket.Classes
{
    public class HashMD5
    {
        public static string Hashinfo(string password)
        {
            MD5 hash = MD5.Create();
            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] h = hash.ComputeHash(b);
            StringBuilder builder = new StringBuilder();
            foreach (var a in h)
            {
                builder.Append(a.ToString("X2"));
            }
            return Convert.ToString(builder);
        }
    }
}
