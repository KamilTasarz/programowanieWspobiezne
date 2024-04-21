using Data;

namespace DataTest
{
    public class DataUnitTest
    {
        

        [Test]
        public void CorrectCoordinatesTest()
        {
            DataApi api = DataApi.CreateBall(3, 4, 20);
            Assert.That(api.X == 3);
            Assert.That(api.Y == 4);
            Assert.That(api.GetRadius() == 20);
        }
        [Test]
        public void CorrectVelocityTest()
        {
            DataApi api = DataApi.CreateBall(3, 4, 20);
            api.SetVelocityX(5);
            Assert.That(api.GetVelocityX() == 5);
            api.SetVelocityY(7);
            Assert.That(api.GetVelocityY() == 7);
        }


    }
}