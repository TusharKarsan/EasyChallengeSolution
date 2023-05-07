using TwistedFizzBuzz;

namespace ConsoleAppStandard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Buzz Fizz Standard for 100 numbers:");

        SequenceGenerator.GenerateRange
            (
                SequenceGenerator.StandardTokens, 1, 100,
                (item, result) => Console.WriteLine("{0,3}, {1}", item, result)
            );
        }
    }
}
