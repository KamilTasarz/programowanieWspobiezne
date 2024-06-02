using Data;
using System.Diagnostics;

namespace DataTest
{
    public class DataUnitTest
    {
        

        [Test]
        public void CorrectCoordinatesTest()
        {
        
            DataApi api = DataApi.CreateBall(0, 3, 4, 20, LoggerApi.CreateApi());
            Assert.That(api.X == 3);
            Assert.That(api.Y == 4);
            Assert.That(api.GetRadius() == 20);
        }
        [Test]
        public void CorrectVelocityTest()
        {
            DataApi api = DataApi.CreateBall(0, 3, 4, 20, LoggerApi.CreateApi());
            api.SetVelocityX(5);
            Assert.That(api.GetVelocityX() == 5);
            api.SetVelocityY(7);
            Assert.That(api.GetVelocityY() == 7);
        }
        [Test]
        public void CorrectMassAndIDTest()
        {
            DataApi api = DataApi.CreateBall(0, 3, 4, 10, LoggerApi.CreateApi());
            Assert.That(api.Mass == (0.008f * 10 * 10 * 10));
            Assert.That(api.ID == 0);
        }
        [Test]
        public void CorrectMovementTest() 
        {
            DataApi api = DataApi.CreateBall(0, 3, 4, 10, LoggerApi.CreateApi());
            api.SetVelocityX(5);
            api.SetVelocityY(5);
            api.X = 0;
            api.Y = 0;
            CancellationTokenSource cts = new CancellationTokenSource();
            api.movement(cts.Token);
            cts.Cancel();
            Assert.That(api.X != 0);
            Assert.That(api.Y != 0);
        }


    }
}