using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Druin.Chef.Core.Authentication
{
    public static class StringHelpers
    {
        public static string ToBase64EncodedSha1String(this string input)
        {
            var s = SHA1.Create();
            return Convert.ToBase64String(s.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }

        public static IEnumerable<string> Split(this string input, int length)
        {
            for (int i = 0; i < input.Length; i += length)
                yield return input.Substring(i, Math.Min(length, input.Length - i));
        }

        public static string ToBase64String(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));

        }

    }
}
