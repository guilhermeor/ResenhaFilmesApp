namespace ResenhaFilmesApp.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str).Equals(false);
        }
    }
}
