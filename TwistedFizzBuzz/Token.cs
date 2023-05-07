namespace TwistedFizzBuzz
{
    public class Token
    {
        public int multiple { get; set; }
        public string word { get; set; }

        public Token(int multiple, string work)
        {
            this.multiple = multiple;
            this.word = work;
        }
    }
}