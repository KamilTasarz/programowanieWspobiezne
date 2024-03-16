using Concurrent;
using NUnit.Framework;

namespace ConcurrentTests
{
    
    public class Tests
    {
        [Test]
        public static void TestDivideClass()
        {
            DivideClass divideClass = new DivideClass();
            Assert.Throws<DivideByZeroException>(() => divideClass.divide(1, 0));
        }
    }
}