using System;
using System.IO;

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
                            var encr_file = HashAlgorithmSHA512.HashArray(file_bytes);
                            File.WriteAllBytes(args[0] + "_file", encr_file);
                            break;
                        case "rijndael":
                            if (args.Length == 4)
                            {
                                var key_bytes = File.ReadAllBytes(args[2]);
                                var iv_bytes = File.ReadAllBytes(args[3]);

                            }
                            break;
                        case "rsa":
                            if (args.Length == 3)
                            {
                                var key_bytes = File.ReadAllBytes(args[2]);
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
