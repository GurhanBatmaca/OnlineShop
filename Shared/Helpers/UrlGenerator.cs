namespace Shared.Helpers
{
    public static class UrlGenerator
    {
        public static string Create(string url)
        {

            return url.Trim()
                        .ToLower()
                        .Replace(" ", "-")
                        .Replace(".", "-")
                        .Replace("ş", "s")
                        .Replace("ç", "c")
                        .Replace("ü", "u")
                        .Replace("ğ", "g")
                        .Replace("ı", "i")
                        .Replace("ö", "o")
                        .Replace("'", "-");
        }
    }
}