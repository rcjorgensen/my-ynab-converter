using Domain.Payee;
using Xunit;

namespace Test
{
    public class PayeeFinderTests
    {
        [Fact]
        public void Test1()
        {
            var str1 = "DK-NOTA 167 LIDL NORDHAVN";
            var str2 = "Nordhavn Cykler";
            var str3 = "Lidl Nordhavn";

            var overlap1 = PayeeFinder.GetOverlap(str1, str2);
            var overlap2 = PayeeFinder.GetOverlap(str1, str3);

            Assert.Equal("Nordhavn", overlap1);
            Assert.Equal("Lidl Nordhavn", overlap2);
        }
    }
}
