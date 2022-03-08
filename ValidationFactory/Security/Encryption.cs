using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ValidationFactory.Security
{
    public class Encryption : IEncryption,IDisposable
    {
        private readonly string _privateKey= "2756661284931169";      

        #region Utilty

        private byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    var toEncrypt = Encoding.Unicode.GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private string DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs, Encoding.Unicode))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        #endregion

        public string DecryptText(string text, string privateKey = "")
        {
            try
            {
                if (string.IsNullOrEmpty(text) || text == "null")
                    return string.Empty;

                if (string.IsNullOrEmpty(privateKey))
                    privateKey = _privateKey;

                using (var provider = new TripleDESCryptoServiceProvider())
                {
                    provider.Key = Encoding.ASCII.GetBytes(privateKey.Substring(0, 16));
                    provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

                    var buffer = Convert.FromBase64String(text);
                    return DecryptTextFromMemory(buffer, provider.Key, provider.IV);
                }
            }
            catch
            {
                throw new InvalidEncryptException();
            }
        }

        public string EncryptText(string text, string privateKey = "")
        {
            if (string.IsNullOrEmpty(text) || text == "null")
                return string.Empty;

            if (string.IsNullOrEmpty(privateKey))
                privateKey = _privateKey;

            using (var provider = new TripleDESCryptoServiceProvider())
            {
                provider.Key = Encoding.ASCII.GetBytes(privateKey.Substring(0, 16));
                provider.IV = Encoding.ASCII.GetBytes(privateKey.Substring(8, 8));

                var encryptedBinary = EncryptTextToMemory(text, provider.Key, provider.IV);
                return Convert.ToBase64String(encryptedBinary);
            }
        }

        public string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public (string encToken, string decToken) GenerateToken(string emailUser)
        {
            var token = new StringBuilder();

            var guid = Guid.NewGuid();
            var time = DateTime.Now;
            var email = emailUser;
            token.Append(email);
            token.Append("ß");
            token.Append(guid);
            token.Append("ß");
            token.Append(time);

            var encryptToken = EncryptText(token.ToString());

            return (encryptToken, token.ToString());
        }

        public string HashCreate(string value, string salt)
        {
            var valueBytes = Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
              value,
              Encoding.UTF8.GetBytes(salt),
              Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA512,
              10000,
              256 / 8);

            return System.Convert.ToBase64String(valueBytes) + "æ" + salt;
        }

        public bool ValidateHash(string value, string salt, string hash)
             => HashCreate(value, salt).Split('æ')[0] == hash;
        
        private bool disposed;
        ~Encryption()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed objects
            }
            // Dispose unmanaged objects
            disposed = true;
        }
    }
}
