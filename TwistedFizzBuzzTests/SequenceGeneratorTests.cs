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

        public static IEnumerable<object[]> testDataStandardNegative => new List<object[]>
        {
            new object[] { SequenceGenerator.StandardTokens,  -1, "-1" },
            new object[] { SequenceGenerator.StandardTokens,  -2, "-2" },
            new object[] { SequenceGenerator.StandardTokens,  -3, "Buzz" },
            new object[] { SequenceGenerator.StandardTokens,  -5, "Fizz" },
            new object[] { SequenceGenerator.StandardTokens, -15, "BuzzFizz" },
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
        public void GenerateSingle_ThrowsOnEmptyToken()
        {
            var resultList = new List<string>();
            var emptyToken = new Token[] { };

            Assert.Throws<ArgumentException>(() => SequenceGenerator.GenerateSingle(emptyToken, 0));
        }

        [Fact]
        public void GenerateSingle_Standard_ForZero()
        {
            string result = SequenceGenerator.GenerateSingle(SequenceGenerator.StandardTokens, 0);

            Assert.Equal("0", result);
        }

        [Theory]
        [MemberData(nameof(testDataStandard))]
        public void GenerateSingle_ReturnsResults_ForPositive(Token[] tokens, int number, string expect)
        {
            string result = SequenceGenerator.GenerateSingle(tokens, number);

            Assert.Equal(expect, result);
        }

        [Theory]
        [MemberData(nameof(testDataStandardNegative))]
        public void GenerateSingle_ReturnsResults_ForNegatives(Token[] tokens, int number, string expect)
        {
            string result = SequenceGenerator.GenerateSingle(tokens, number);

            Assert.Equal(expect, result);
        }

        [Fact]
        public void GenerateRange_ReturnsResults_1_20()
        {
            var resultList = new List<string>();

            SequenceGenerator.GenerateRange(SequenceGenerator.StandardTokens, 1, 20, (item, result) => resultList.Add(result));

            resultList.Should().BeEquivalentTo(standardResult1to20);
        }

        [Fact]
        public void GenerateRange_ReturnsResults_20_1()
        {
            var resultList = new List<string>();
            var expect = standardResult1to20.Reverse();

            SequenceGenerator.GenerateRange(SequenceGenerator.StandardTokens, 20, 1, (item, result) => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public void GenerateRange_ReturnsResults_ForEvens()
        {
            var expect = new string[] {
                "-3", "Even",
                "-1", "0",
                "1", "Even",
                "3", "Even",
                "5", "Even",
                "7", "Even",
                "9", "Even"
            };

            var evenToken = new Token[] { new Token(2, "Even") };

            var resultList = new List<string>();

            SequenceGenerator.GenerateRange(evenToken, -3, 10, (item, result) => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public void GenerateFromArray_ReturnsResults()
        {
            var array = new int[] { -8, -1, 10, 8, 6, 4, 2, 1 };

            var expect = new string[] {
                "Even", "-1", "Even", "Even", "Even", "Even", "Even", "1"
            };

            var evenToken = new Token[] { new Token(2, "Even") };

            var resultList = new List<string>();

            SequenceGenerator.GenerateFromArray(evenToken, array, (item, result) => resultList.Add(result));

            resultList.Should().BeEquivalentTo(expect);
        }

        [Fact]
        public async Task GetTokenFromApi_ReturnsToken()
        {
            var token = await SequenceGenerator.GetTokenFromApi();

            token.Should().NotBeNull();

            token.word.Should().NotBeNullOrEmpty();
            token.multiple.Should().NotBe(0);
        }

        [Fact]
        public async Task GenerateSingleUsingApiToken_ReturnsResult()
        {
            var result = await SequenceGenerator.GenerateSingleUsingApiToken(50);

            result.Should().NotBeNull();
            result.Item1.Should().NotBeNull();
            result.Item1.word.Should().NotBeNull();
            result.Item1.multiple.Should().NotBe(0);
            result.Item2.Should().NotBeNullOrEmpty();
        }
    }
}
