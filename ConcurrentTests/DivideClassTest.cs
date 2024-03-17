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
        [Test]
        public static void TestDivideClass2()
        {
            DivideClass divideClass = new DivideClass();
            Assert.That(divideClass.divide(5, 2.5) == 2);
        }
    }
}