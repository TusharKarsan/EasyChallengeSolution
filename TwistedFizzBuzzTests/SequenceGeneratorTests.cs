namespace TwistedFizzBuzzTests
{
    public class SequenceGeneratorTests
    {
        public static IEnumerable<object[]> testDataStandard => new List<object[]>
        {
            new object[] { SequenceGenerator.StandardTokens,  1, "1" },
            new object[] { SequenceGenerator.StandardTokens,  2, "2" },
            new object[] { SequenceGenerator.StandardTokens,  3, "Buzz" },
            new object[] { SequenceGenerator.StandardTokens,  5, "Fizz" },
            new object[] { SequenceGenerator.StandardTokens, 15, "BuzzFizz" },
        };

        public static string[] standardResult1to20 = 
        {
            "1",
            "2",
            "Buzz",
            "4",
            "Fizz",
            "Buzz",
            "7",
            "8",
            "Buzz",
            "Fizz",
            "11",
            "Buzz",
            "13",
            "14",
            "BuzzFizz",
            "16",
            "17",
            "Buzz",
            "19",
            "Fizz"
        };

        [Fact]
        public void GenerateSingleThrowsOnEmptyToken()
        {
            var resultList = new List<string>();
            var emptyToken = new Token[] { };

            Assert.Throws<ArgumentException>(() => SequenceGenerator.GenerateSingle(emptyToken, 0));
        }

        [Theory]
        [MemberData(nameof(testDataStandard))]
        public void GenerateSingleReturnsResults(Token[] tokens, int number, string expect)
        {
            string result = SequenceGenerator.GenerateSingle(tokens, number);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void GenerateRangeReturnsResults()
        {
            var resultList = new List<string>();

            SequenceGenerator.GenerateRange(SequenceGenerator.StandardTokens, 1, 20, result => resultList.Add(result));

            resultList.Should().BeEquivalentTo(standardResult1to20);
        }

        [Fact]
        public void GenerateReverseRangeReturnsResults()
        {
            var resultList = new List<string>();
            var expect = standardResult1to20.Reverse();

            SequenceGenerator.GenerateRange(SequenceGenerator.StandardTokens, 20, 1, result => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public void GenerateRangeReturnsResultsForEvens()
        {
            var expect = new string[] {
                "1", "Even",
                "3", "Even",
                "5", "Even",
                "7", "Even",
                "9", "Even"
            };

            var evenToken = new Token[] { new Token(2, "Even") };

            var resultList = new List<string>();

            SequenceGenerator.GenerateRange(evenToken, 1, 10, result => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public void GenerateFromArrayWhenNullWorks()
        {
            var array = new int[] { 10, 8, 6, 4, 2, 1 };

            var expect = new string[] {
                "Even", "Even", "Even", "Even", "Even", "1"
            };

            var evenToken = new Token[] { new Token(2, "Even") };

            var resultList = new List<string>();

            SequenceGenerator.GenerateFromArray(evenToken, array, result => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }
    }
}