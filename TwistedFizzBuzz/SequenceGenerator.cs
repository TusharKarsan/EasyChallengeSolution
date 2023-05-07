using System.Text.Json;

namespace TwistedFizzBuzz
{
    public static class SequenceGenerator
    {
        /// <summary>
        /// Standard Buzz Fizz tokens.
        /// </summary>
        public static readonly Token[] StandardTokens = new Token[] {
            new Token(3, "Buzz"),
            new Token(5, "Fizz")
        };

        public static string GenerateSingle(Token[] tokens, int number)
        {
            if (tokens == null || tokens.Length == 0)
                throw new ArgumentException("At least one token is required.", nameof(tokens));

            string result = "";

            foreach (Token token in tokens)
            {
                if (number != 0 && number % token.multiple == 0)
                    result += token.word;
            }

            if (result.Length == 0)
                return number.ToString();
            else
                return result;
        }

        public static async Task<Token> GetTokenFromApi()
        {
            Token? token = null;

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync("https://rich-red-cocoon-veil.cyclic.app/random");
                token = JsonSerializer.Deserialize<Token>(json);
            }

            if (token == null)
                throw new Exception("Error retrieving token");
            return token;
        }

        public static async Task<Tuple<Token, string>> GenerateSingleUsingApiToken(int number)
        {
            var token = await GetTokenFromApi();
            var result = GenerateSingle(new Token[] { token }, number);
            return new Tuple<Token, string>(token, result);
        }

        /// <summary>
        /// For a given range, makes callback for each item's result.
        /// </summary>
        /// <param name="tokens">multipliers and their words</param>
        /// <param name="start">starting number</param>
        /// <param name="untilInclusive">inclusive finish number</param>
        /// <param name="callback so that a large dataset can be handled efficiently"></param>
        public static void GenerateRange(Token[] tokens, int start, int untilInclusive, Action<int, string> callback)
        {

            if (start < untilInclusive)
            {
                for (int i = start; i <= untilInclusive; i++)
                {
                    callback(i, GenerateSingle(tokens, i));
                }
            }
            else
            {
                for (int i = start; i >= untilInclusive; i--)
                {
                    callback(i, GenerateSingle(tokens, i));
                }
            }
        }

        public static void GenerateFromArray(Token[] tokens, int[] numberArray, Action<int, string> callback)
        {
            if (numberArray == null || numberArray.Length == 0)
                return;

            foreach(int item in numberArray)
            {
                callback(item, GenerateSingle(tokens, item));
            }
        }
    }
}