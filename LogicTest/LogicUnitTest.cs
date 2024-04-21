
using Logic;

namespace LogicTest
{
    public class LogicUnitTest
    {
        [Test]
        public void GetPositionsTest()
        {
            LogicApi api = LogicApi.CreateLogicApi(200, 200, 3);
            float[][] pos = api.GetPositions();
            Assert.That(pos.Length == 3);
            foreach (float[] position in pos)
            {
                Assert.That(position[0] < 200 && position[0] > 0);
                Assert.That(position[1] < 200 && position[1] > 0);
            }
        }

        [Test]
        public void StartTest()
        {
            LogicApi api = LogicApi.CreateLogicApi(200, 200, 3);
            float[][] pos = api.GetPositions();
            api.Start();
            int i = 0;
            foreach (float[] position in api.GetPositions())
            {
                Assert.That(position[0] != pos[i][0]);
                Assert.That(position[1] != pos[i][1]);
                i++;
            }
        }

        [Test]
        public void StopTest()
        {
            LogicApi api = LogicApi.CreateLogicApi(200, 200, 3);
            
            api.Start();

            float[][] pos1 = api.GetPositions();
            api.Stop();
            float[][] pos2 = api.GetPositions();
           
            for(int i = 0; i < 3; i++)
            {
                Assert.That(pos1[i][0] == pos2[i][0]);
                Assert.That(pos1[i][1] == pos2[i][1]);
            }
        }

        [Test]
        public void CollisionsTest()
        {
            LogicApi api = LogicApi.CreateLogicApi(200, 200, 1);
            var ball = api.CreateBall();
            Assert.IsFalse(api.isCollisionUpDown(ball));
            Assert.IsFalse(api.isCollisionLeftRight(ball));
            
        }
    }
}