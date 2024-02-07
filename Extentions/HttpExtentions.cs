using HtmlAgilityPack;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace LinkBox.Extentions
{
    public static class HttpExtentions
    {
        public static Uri CheckFormat(this string url)
        {
            try
            {
                var uri = new Uri(url);
                return uri;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<Uri> CheckAvailableAsync(this string url)
        {
            try
            {
                var uri = new Uri(url);
                using (var handler = new HttpClientHandler())
                using (var client = new HttpClient(handler))
                {
                   
                    var response = await client.GetAsync(uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                }
                return uri;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<string> DownloadHtmlAsync(this string url)
        {
            try
            {
                var uri = new Uri(url);
                return await uri.DownloadHtmlAsync();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static async Task<string> DownloadHtmlAsync(this Uri uri)
        {
            try
            {
                using (var handler = new HttpClientHandler() { AllowAutoRedirect = true })
                using (var client = new HttpClient(handler))
                {
                    using (var response = await client.GetAsync(uri))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (var content = response.Content)
                            {
                                return await content.ReadAsStringAsync();
                            }
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.Found && response.Headers.Location != null)
                        {
                            return await response.Headers.Location.DownloadHtmlAsync();
                        }
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static async Task<byte[]> DownloadByteAsync(this string url)
        {
            try
            {
                var uri = new Uri(url);
                using (var handler = new HttpClientHandler())
                using (var client = new HttpClient(handler))
                {
                    return await client.GetByteArrayAsync(uri);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string ToBaseUrl(this Uri uri)
        {
            var url = $"{uri.Scheme}://{uri.Host}";
            if (uri.Port != 80 && uri.Port != 443)
            {
                url = $"{url}:{uri.Port}";
            }
            return url;
        }
    }
}
