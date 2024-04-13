using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    class LogicApi : LogicAbstractApi
    {
        
        private DataApi data;

        public override int Width => throw new NotImplementedException();

        public override int Height => throw new NotImplementedException();

        public override IList CreateBalls(int amount)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override IList GetAllBalls()
        {
            throw new NotImplementedException();
        }

        public override int GetBallsAmount()
        {
            throw new NotImplementedException();
        }

        public override double GetBallXByID(int id)
        {
            throw new NotImplementedException();
        }

        public override double GetBallYByID(int id)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool IsRunning()
        {
            throw new NotImplementedException();
        }

        public override void MoveBall(int id)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        protected override void RaisePropertyChanged(int ballID, [CallerMemberName] string propertyName = null)
        {
            base.RaisePropertyChanged(ballID, propertyName);
        }
    }
}
