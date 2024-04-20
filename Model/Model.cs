using Logic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Numerics;
using System.ComponentModel;
using Data;
using System.Reactive.Linq;
using System.Reactive;

namespace Model
{
    public class Model : ModelApi
    {
        private LogicApi logic;
        private int width;
        private int height;
        private int amount;
        private Canvas board;
        Ellipse[] ellipses;
        private IObservable<EventPattern<BallChangeEventArgs>> eventObservable = null;
        public override event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<BallChangeEventArgs> BallChanged;
        public IObservable<EventHandler> ballsChanged;
        

        public override IEllipse[] getBalls() {
            return ellipses; 
        }
        public Model(LogicApi logic,int width, int height, int amount) 
        {
            this.logic = logic;
            board = new Canvas();
            board.Width = width;
            board.Height = height;
            ellipses = new Ellipse[amount];
        }

        public override void Start()
        {
            
            for(int i = 0; i < amount; i++)
            {
                ellipses[i] = new Ellipse(logic.getObservs().ElementAt(i).X, logic.getObservs().ElementAt(i).Y);
                
                logic.PropertyChanged += OnBallChanged;
            }
        }

        private void OnBallChanged(object sender, PropertyChangedEventArgs args)
        {
            ObservableCollection<DataApi> w = logic.getObservs();
            for(int i = 0; i < amount; i++)
            {
                ellipses[i].x = w.ElementAt(i).X;
                ellipses[i].y = w.ElementAt(i).Y;
            }
        }

        public override void Stop()
        {
            logic.Stop();
        }

        public override IDisposable Subscribe(IObserver<IEllipse> observer)
        {
            return eventObservable.Subscribe(x => observer.OnNext(x.EventArgs.Ball), ex => observer.OnError(ex), () => observer.OnCompleted());
        }
    }
}
