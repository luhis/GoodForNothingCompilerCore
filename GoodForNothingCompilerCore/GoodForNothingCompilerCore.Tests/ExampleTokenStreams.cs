namespace GoodForNothingCompilerCore.Tests
{
    using System.Collections.Generic;
    using System.Text;

    public static class ExampleTokenStreams
    {
        public static readonly IEnumerable<object> PrintThatsItFolks = new object[]
            {"print", new StringBuilder("that's it folks!"), ArithToken.Semi};
    }
}