using System.Security.Cryptography;
using System.Text;

namespace LinkBox.Extentions
{
    public static class StringExtentions
    {
        public static string CheckIsNullOrEmpty(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            return input;
        }


    }
}
