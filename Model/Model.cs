using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Model : ModelApi
    {
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;

        public LogicApi logic { get; set; }
        public override event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        MyVector[] ModelBall;

        public override MyVector[] GetBalls()
        {
            return ModelBall;
        }

        public Model(int width, int height, int amount)
        {
            logic = LogicApi.CreateLogicApi(width, height, amount);
            eventObservable = Observable.FromEventPattern<BallChangeEventArgs>(this, "BallChanged");
            
            ModelBall = new MyVector[amount];
            for (int i = 0; i < amount; i++)
            {
                MyVector ball = new MyVector(logic.GetPositions()[i][0], logic.GetPositions()[i][1]);
                ModelBall[i] = ball;
                logic.PropertyChanged += OnBallChanged;
            }
        }
        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {
            for (int i = 0; i < logic.GetPositions().Length; i++)
            {
                ModelBall[i].x = logic.GetPositions()[i][0];
                ModelBall[i].y = logic.GetPositions()[i][1];
            }
        }
        

        public override void Start()
        {
            logic.Start();
        }

        public override void Stop()
        {
            logic.Stop();
        }

        public override IDisposable Subscribe(IObserver<IMyVector> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }
    }
}
