using System.Security.Cryptography;
using System.Text;

namespace TimeTrakAPI.Helpers
{
    public static class SecurityManager
    {
        private static string AES_KEY = "lskf85nsjrls745wlgot068enh867eki";

        public static string Encrypt(string clearText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            if (!string.IsNullOrEmpty(clearText))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
                    aes.IV = iv;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream,encryptor,CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new  StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(clearText);
                            }
                            array = memoryStream.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(array);
            }
            else
            {
                return String.Empty;   
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            if (!string.IsNullOrEmpty(cipherText))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(AES_KEY);
                    aes.IV = iv;

                    ICryptoTransform decryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
