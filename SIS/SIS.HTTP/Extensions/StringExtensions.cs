namespace SIS.HTTP.Extensions
{
    using System.Text;

    public static class StringExtensions
    {
        public static string Capitalize(this string text)
        {
            var result = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                result.Append(i == 0 ? char.ToUpper(text[i]) : char.ToLower(text[i]));
            }

            return result.ToString().TrimEnd();
        }
    }
    
}