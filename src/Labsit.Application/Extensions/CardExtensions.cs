using System.Text;

namespace Labsit.Application.Extensions
{
    public static class CardExtensions
    {
        private static Random random = new Random();



        public static string GenerateHolderName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;

            string[] nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (nameParts.Length == 0)
                return string.Empty;

            string abbreviatedName = CapitalizeFirstLetter(nameParts[0]);

            for (int i = 1; i < nameParts.Length; i++)
                abbreviatedName += " " + CapitalizeFirstLetter(nameParts[i][0].ToString());

            return abbreviatedName;
        }

        public static string GenerateCreditCardNumber()
        {
            string prefix = "4";
            string accountNumber = GenerateDigits(15 - prefix.Length);
            string cardNumber = prefix + accountNumber;

            int[] cardNumberDigits = cardNumber.Select(c => int.Parse(c.ToString())).ToArray();
            int checkDigit = GetLuhnCheckDigit(cardNumberDigits);

            return cardNumber + checkDigit;
        }

        private static string CapitalizeFirstLetter(string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            return char.ToUpper(word[0]) + word.Substring(1).ToLower();
        }

        private static string GenerateDigits(int length)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                builder.Append(random.Next(0, 10));
            }
            return builder.ToString();
        }

        private static int GetLuhnCheckDigit(int[] digits)
        {
            int sum = 0;
            bool isEven = false;

            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int digit = digits[i];

                if (isEven)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                isEven = !isEven;
            }

            int checkDigit = (sum * 9) % 10;
            return checkDigit;
        }
    }
}


