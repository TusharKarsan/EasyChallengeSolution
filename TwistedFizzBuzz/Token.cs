namespace TwistedFizzBuzz
{
    public class Token
    {
        public int multiple { get; set; }
        public string word { get; set; }

        public Token()
        {
            this.multiple = 0;
            this.word = "";
        }

        public Token(int multiple, string word)
        {
            this.multiple = multiple;
            this.word = word;
        }
    }
}