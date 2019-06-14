using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Wine.Data;

namespace Wine.Test.Encryption
{
    [TestClass]
    public class CertificationTest
    {
        [TestMethod]
        public void ImportTestCertificate()
        {
            var certificate = new X509Certificate2(@"C:\Projects\WineWebApp\WineWebApp\Wine.Test\Encryption\cert.cer");

            Assert.IsNotNull(certificate.GetPublicKey());
        }

        [TestMethod]
        public void SignTestCertificate()
        {
            var certificate = new X509Certificate2(@"C:\Projects\WineWebApp\WineWebApp\Wine.Test\Encryption\cert.cer");

            var country = new Country {  ID = 4, Name = "test name"};

            string serializeObj = JsonConvert.SerializeObject(country);

            byte[] countryData = Encoding.ASCII.GetBytes(serializeObj);

            using (RSA rsa = certificate.GetRSAPublicKey())
            {
                var validation = rsa.SignData(countryData, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
            }

            Assert.IsNotNull(certificate.GetPublicKey());
        }
    }
}

