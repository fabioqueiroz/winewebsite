using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.CrossCutting;

namespace Wine.Test.Encryption
{
    [TestClass]
    public class HashTester
    {
        [TestMethod]
        public void HashingString()
        {
            // arrange
            string password = "1234";
            byte[] salt = { 0x00C9, 0x00C8, 0x00C1 };

            // act           
            var result = Encryptor.Hash(password, salt);

            // assert
            Assert.IsNotNull(result);
        }

        [TestMethod]

        public void Checker()
        {
            // arrange
            string password = "1234";
            byte[] salt = { 0x00C9, 0x00C8, 0x00C1 };

            var hashPasswordFromDb = Encryptor.Hash(password, salt);
            // act
            var result = Encryptor.PasswordChecker(password, salt, hashPasswordFromDb);

            // assert
            Assert.IsTrue(result);
        }
    }
}
