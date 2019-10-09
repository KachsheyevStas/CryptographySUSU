using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 2)
            {
                try
                {
                    var file_bytes = File.ReadAllBytes(args[1]);

                    switch (args[0])
                    {
                        case "hash_sha512":
                            var hash_file = HashAlgorithmSHA512.HashArray(file_bytes);
                            File.WriteAllBytes(args[0] + "_file", hash_file);
                            break;
                        case "aes":
                            if (args.Length == 4)
                            {
                                var key_bytes = File.ReadAllBytes(args[2]);
                                var iv_bytes = File.ReadAllBytes(args[3]);
                                var decr_by_aes_file = SymmetricAlgorithmAES.DecryptData(file_bytes, key_bytes, iv_bytes);
                                File.WriteAllBytes(args[0] + "_file", decr_by_aes_file);
                            }
                            else
                            {
                                using (var myAes = new AesManaged())
                                {
                                    myAes.GenerateKey();
                                    myAes.GenerateIV();
                                    var encr_by_aes_file = SymmetricAlgorithmAES.EncryptData(file_bytes, myAes.Key, myAes.IV);
                                    File.WriteAllBytes(args[0] + "_file", encr_by_aes_file);
                                    File.WriteAllBytes(args[0] + "_key", myAes.Key);
                                    File.WriteAllBytes(args[0] + "_iv", myAes.IV);
                                }
                            }
                            break;
                        case "rsa":
                            if (args.Length == 3)
                            {
                                string xml_string = File.ReadLines(args[2]).Skip(0).First();
                                var decr_by_rsa_file = AsymmetricAlgorithmRSA.DecryptData(file_bytes, xml_string);
                                File.WriteAllBytes(args[0] + "_file", decr_by_rsa_file);
                            }
                            else
                            {
                                using (var RSA = new RSACryptoServiceProvider())
                                {
                                    string xml_string = RSA.ToXmlString(true);
                                    var encr_by_rsa_file = AsymmetricAlgorithmRSA.EncryptData(file_bytes, xml_string);
                                    File.WriteAllBytes(args[0] + "_file", encr_by_rsa_file);
                                    File.WriteAllText(args[0] + "_xml_string", xml_string);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Error: File not found", e);
                }
            }
        }
    }



}
