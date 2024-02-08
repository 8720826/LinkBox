using HtmlAgilityPack;
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

        public static string ReadTitle(this string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var titleNodes = doc.DocumentNode.SelectNodes("//title");
            if (titleNodes == null)
            {
                return "";
            }
            return titleNodes.FirstOrDefault()?.InnerText ?? "";
        }

        public static string ReadDescription(this string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var metaTags = doc.DocumentNode.SelectNodes("//meta[@name='description']");
            if (metaTags == null)
            {
                return "";
            }
            return metaTags.FirstOrDefault()?.Attributes["content"]?.Value ?? "";
        }

        public static string ReadFavicon(this string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var metaTags = doc.DocumentNode.SelectNodes("//meta[@name='icon']");
            if (metaTags == null || !metaTags.Any())
            {
                metaTags = doc.DocumentNode.SelectNodes("//meta[@name='shortcut']");
            }
            if (metaTags == null)
            {
                return "";
            }
            return metaTags.FirstOrDefault()?.Attributes["href"]?.Value ?? "";
        }


        public static string ToMd5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
