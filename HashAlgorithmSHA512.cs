using System.Security.Cryptography;

namespace ConsoleApp2
{
    class HashAlgorithmSHA512
    {
        public static byte[] HashArray(byte[] array)
        {
            using (var shaM = new SHA512Managed()) {
                return shaM.ComputeHash(array);
            }
        }
    }
}
