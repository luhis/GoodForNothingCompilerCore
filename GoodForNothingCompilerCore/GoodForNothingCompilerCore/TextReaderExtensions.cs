namespace GoodForNothingCompilerCore
{
    using System.IO;

    public static class TextReaderExtensions
    {
        public static char ReadCh(this TextReader tr) => (char)tr.Read();

        public static char PeekCh(this TextReader tr) => (char)tr.Peek();

        public static bool IsEmpty(this TextReader tr) => tr.Peek() == -1;
    }
}