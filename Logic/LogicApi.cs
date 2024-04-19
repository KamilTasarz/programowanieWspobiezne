using Data;
using System.ComponentModel;

namespace Logic
{
    public abstract class LogicApi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void Start(); //bool, żeby zakonczyc
        public abstract void Stop();
        public abstract DataApi CreateBall();
        public abstract void updatePosition(DataApi ball);
        public abstract void updateVelocity(DataApi ball);
        public abstract bool isCollision(DataApi ball);

    }
}
