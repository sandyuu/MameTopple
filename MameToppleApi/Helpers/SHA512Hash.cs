using System.Security.Cryptography;
using System.Text;
using MameToppleApi.Interfaces;

namespace MameToppleApi.Helpers
{
    public class SHA512Hash : IEncryptionAdapter
    {
        public bool Verify(string encoded, string password)
        {
            var encodedpassword = HashPassword(password);
            if (encoded == encodedpassword)
                return true;
            return false;
        }
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }

            StringBuilder sb;

            using (SHA512 sha512 = SHA512.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(password);

                byte[] encryption = sha512.ComputeHash(byteArray);


                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    sb.Append(encryption[i].ToString("x2"));
                }
            }

            return sb.ToString(); ;
        }
    }
}