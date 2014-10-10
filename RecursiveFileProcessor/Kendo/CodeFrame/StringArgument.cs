namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class StringArgument : IArgumentBase
    {
        public string ArgName { get; set; }

        public StringArgument(string argName)
        {
            ArgName = argName;
        }

        public override string ToString()
        {
            return ArgName;
        }

        public int Indent { get; set; }
    }
}
