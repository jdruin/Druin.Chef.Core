using System;
using System.Security.Cryptography;
using System.Text;


namespace Druin.Chef.Core.Authentication
{
    internal static class Digester
    {
        internal static string HashFile(byte[] file, double protocolVersion)
        {
            var hasher = Algorithm(protocolVersion);

            return Convert.ToBase64String(hasher.ComputeHash(file));
        }

        internal static string HashString(string stringToHash, double protocolVersion)
        {
            var hasher = Algorithm(protocolVersion);

            return Convert.ToBase64String(hasher.ComputeHash(Encoding.Default.GetBytes(stringToHash)));
        }

        internal static HashAlgorithm Algorithm(double version)
        {
            switch (version)
            {
                case 1.0:
                    return SHA1.Create();
                case 1.1:
                    return SHA1.Create();
                case 1.3:
                    return SHA256.Create();
                default:
                    return null;
            }
        }


    }
}