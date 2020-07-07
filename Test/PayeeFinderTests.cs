using Domain.Payee;
using System;
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

            var overlap1 = PayeeFinder.CalculateOverlap(str1, str2);
            var overlap2 = PayeeFinder.CalculateOverlap(str1, str3);

            Assert.Equal(8, overlap1);
            Assert.Equal(4+9, overlap2);
        }
    }
}
