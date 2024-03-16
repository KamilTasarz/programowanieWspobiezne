namespace ConcurrentTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestProgram()
        {
            DivideClass divideClass = new DivideClass();
            Assert.Throws(DivideByZeroException, divideClass.divide(1, 0));
        }
    }
}