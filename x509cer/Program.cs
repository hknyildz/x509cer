using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
namespace x509cer
{
    class Program
    {
        static void Main(string[] args)
        {
            static void MakeCert()
            {
                var ecdsa = ECDsa.Create(); // generate asymmetric key pair
                var req = new CertificateRequest("cn=foobar", ecdsa, HashAlgorithmName.SHA512);
                var cert = req.CreateSelfSigned(DateTimeOffset.Now, DateTimeOffset.MaxValue);

                // Create PFX (PKCS #12) with private key
                File.WriteAllBytes("c:\\Users\\ahmet\\Desktop\\mycert.pfx", cert.Export(X509ContentType.Pfx, "P@55w0rd"));

                // Create Base 64 encoded CER (public key only)
                File.WriteAllText("c:\\Users\\ahmet\\Desktop\\mycert.cer",
                    "-----BEGIN CERTIFICATE-----\r\n"
                    + Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks)
                    + "\r\n-----END CERTIFICATE-----");
            }
            MakeCert();
        }
    }
}
