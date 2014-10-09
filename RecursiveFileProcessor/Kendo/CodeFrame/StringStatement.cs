namespace RecursiveFileProcessor.Kendo.CodeFrame
{
    public class StringStatement : IStatement
    {
        public string Code { get; set; }
        
        public StringStatement(string code)
        {
            Code = code;
        }
        
        public override string ToString()
        {
            return Code;
        }
    }
}
