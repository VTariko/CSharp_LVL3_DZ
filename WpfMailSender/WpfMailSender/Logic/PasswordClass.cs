using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WpfMailSender.Logic
{
    /// <summary>
    /// Класс для работы с паролями
    /// </summary>
    class PasswordClass
    {
        private static readonly string SaltKey = "S@LT&KEY";
        private static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        /// <summary>
        /// Метод шифрования
        /// </summary>
        /// <param name="plainText">Шифруемая строка</param>
        /// <param name="sharedSecret">Ключевое слово</param>
        /// <returns></returns>
        public static byte[] Encrypt(string plainText, string sharedSecret)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return cipherTextBytes;
        }

        /// <summary>
        /// Метод дешифровки
        /// </summary>
        /// <param name="cipherTextBytes">Расшифруемая строка</param>
        /// <param name="sharedSecret">Ключевое слово</param>
        /// <returns></returns>
        public static string Decrypt(byte[] cipherTextBytes, string sharedSecret)
        {
            byte[] keyBytes = new Rfc2898DeriveBytes(sharedSecret, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }
    }
}
