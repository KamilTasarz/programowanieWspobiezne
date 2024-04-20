using Data;
using System.Collections.ObjectModel;
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

        public abstract ObservableCollection<DataApi> getObservs();
        public static LogicApi CreateLogicApi(int width, int height, int amount)
        {
            return new Logic(width, height, amount);
        }
    }
}
