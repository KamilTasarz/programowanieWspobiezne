using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive;
using Logic;
using System.Numerics;



namespace Model
{
    public abstract class ModelApi : IObservable<IMyVector>
    {
        public static ModelApi CreateApi(int x, int y, int amount)
        {
            LogicApi logic = LogicApi.CreateLogicApi(x, y, amount);
            return new Model(logic, amount);
        }
        public abstract void Start();
        public abstract void Stop();
        public abstract IDisposable Subscribe(IObserver<IMyVector> observer);
        public abstract MyVector[] GetBalls();

        public abstract event PropertyChangedEventHandler PropertyChanged;
    }

   
}