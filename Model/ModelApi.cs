using Logic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;


namespace Model
{
    public abstract class ModelApi : IObservable<IEllipse>
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract IEllipse[] getBalls();
      
        public static ModelApi CreateModelApi(int width, int height, int amount)
        {

            Model model = new Model(LogicApi.CreateLogicApi(width, height, amount), width, height, amount);
            return model;
        }

        public abstract IDisposable Subscribe(IObserver<IEllipse> observer);
        public abstract event PropertyChangedEventHandler PropertyChanged;

    }
}
