using TwistedFizzBuzz;

namespace ConsoleAppTwisted
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Buzz Fizz Twisted from -20 to 127:");

            var tokens = new Token[] {
                new Token( 5, "Fizz"),
                new Token( 9, "Buzz"),
                new Token(27, "Bar")
            };

            SequenceGenerator.GenerateRange
            (
                tokens, -20, 127,
                (item, result) => Console.WriteLine("{0,3}, {1}", item, result)
            );
        }
    }
}