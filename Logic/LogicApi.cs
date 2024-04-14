using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Data;

namespace Logic
{
    public class LogicApi : LogicAbstractApi
    {
        private ObservableCollection<IBall> balls;

        private DataAbstractApi data;
        Random random = new Random();

        public override int Width { get; }

        public override int Height { get; }

        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;

        public LogicApi(int w, int h, DataAbstractApi api)
        {
            Width = w;
            Height = h;
            data = api;
        }

        public override IList CreateBalls(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                double x = random.Next(0, Width);
                double y = random.Next(0, Height);

                balls.Add(data.CreateBall(i, x, y));
            }
            return balls;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override IList GetAllBalls()
        {
            return balls;
        }

        public override int GetBallsAmount()
        {
            return balls.Count;
        }

        public override double GetBallXByID(int id)
        {
            foreach(IBall b in balls)
            {
                if (b.ID  == id)
                {
                    return b.X;
                }
            }
            return 0;
        }

        public override double GetBallYByID(int id)
        {
            foreach (IBall b in balls)
            {
                if (b.ID == id)
                {
                    return b.Y;
                }
            }
            return 0;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsRunning()
        {
            throw new NotImplementedException();
        }

        //public override void MoveBall(int id)
        //{
        //    foreach (IBall b in balls)
        //    {
        //        if (b.ID == id)
        //        {
        //            b.CreateTaskMove(new CancellationToken(false));
        //        }
        //    }
        //}

        public override void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            foreach(IBall b in balls)
            {
                b.CreateTaskMove(_cancellationToken);
            }
        }

        public override void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void RaisePropertyChanged(int ballID, [CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(ballID, propertyName);
        }

        public override void MoveBall(int id)
        {
            throw new NotImplementedException();
        }
    }
}
