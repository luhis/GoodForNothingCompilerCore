namespace GoodForNothingCompilerCore.Tests
{
    using System.Collections.Generic;
    using System.Text;

    public static class ExampleTokenStreams
    {
        public static readonly IEnumerable<object> PrintThatsItFolks = new object[]
            {"print", new StringBuilder("that's it folks!"), ArithToken.Semi};

        public static readonly IEnumerable<object> Addition = new object[]
            {"var", "x", ArithToken.Equal, 2, ArithToken.Mul, 3, ArithToken.Semi};
    }
}