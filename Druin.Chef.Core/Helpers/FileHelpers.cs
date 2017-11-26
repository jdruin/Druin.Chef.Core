using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Authentication
{
    public static class FileHelpers
    {
        public static string ToBase64EncodedSha1String(this byte[] input)
        {
            using (var stream = new MemoryStream(input))
            {
                var s = SHA1.Create();

                return Convert.ToBase64String(s.ComputeHash(stream));
            }

        }

        public static string ToChefChecksum(this byte[] input)
        {
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(input);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("x2"));

            }

            return sb.ToString();
        }

        public static byte[] ToMD5Hash(this byte[] input)
        {
            var md5 = MD5.Create();

            var hash = md5.ComputeHash(input);

            return hash;

        }

    }
}
