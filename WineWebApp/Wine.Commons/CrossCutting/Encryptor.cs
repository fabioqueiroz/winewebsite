using System;
using System.Collections.Generic;
using System.Text;

namespace Wine.Commons.CrossCutting
{
    public class Encryptor
    {
        public static byte[] Hash(string stringToHash, byte[] salt)
        {
            byte[] hashString;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hmac.Key = salt;
                hashString = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(stringToHash));
            }

            return hashString;
        }

        public static bool PasswordChecker(string password, byte[] salt, byte[] hashInDb)
          {
            bool isValid = true;

            byte[] result = Hash(password, salt);

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] != hashInDb[i])
                {
                    isValid = false;
                    break;
                }                   
            }

            return isValid;
        }
    }
}
