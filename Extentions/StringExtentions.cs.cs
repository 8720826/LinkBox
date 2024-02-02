using System.Security.Cryptography;
using System.Text;

namespace LinkBox.Extentions
{
    public static class StringExtentions
    {
        public static string ToMd5(this string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = MD5.HashData(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
