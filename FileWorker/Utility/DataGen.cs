using System.Globalization;
using System.Text;

namespace FileWorker.Utility
{
    public static class DataGen
    {
        private enum Lang
        {
            Latin,
            Cyrillic
        }

        private static readonly Dictionary<Lang, char[]> _alphabet = new()
        {
            {
                Lang.Latin,
                ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'] },
            {
                Lang.Cyrillic,
                ['А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я']
            },
        };

        public static string GenerateDataRow()
        {
            return $"{RandomDate():dd.MM.yyyy}||{RandomString(10, Lang.Latin)}||{RandomString(10, Lang.Cyrillic)}||{RandomEven(100000000)}||{RandomDouble(1, 20, 8).ToString("F8", CultureInfo.InvariantCulture)}||";
        }

        private static DateTime RandomDate()
        {
            var startDate = DateTime.Today.AddYears(-5);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(RandomNumGen.Next(range));
        }

        private static string RandomString(int length, Lang lang)
        {
            var res = new StringBuilder();
            var alpha = _alphabet[lang];

            for (int i = 0; i < length; i++)
            {
                res.Append(alpha[RandomNumGen.Next(alpha.Length)]);
            }

            return res.ToString();
        }

        private static int RandomEven(int max, int min = 0)
        {
            return RandomNumGen.Next(min, max / 2) * 2;
        }

        private static double RandomDouble(int min, int max, int precision = 8)
        {
            var randDouble = RandomNumGen.NextDouble();
            var ranged = randDouble * (max - min) + min;
            var rounded = double.Round(ranged, precision);

            return rounded;
        }

    }
}