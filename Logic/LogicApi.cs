using Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Logic
{
    public abstract class LogicApi
    {
        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract void Start();
        public abstract void Stop();
        public abstract DataApi CreateBall();
        public abstract void updatePosition(DataApi ball);
        public abstract void updateVelocity(DataApi ball, bool UpDown);
        public abstract bool isCollisionUpDown(DataApi ball);
        public abstract bool isCollisionLeftRight(DataApi ball);
        public abstract float[][] GetPositions();

        public static LogicApi CreateLogicApi(int width, int height, int amount)
        {
            return new Logic(width, height, amount);
        }
    }
}
