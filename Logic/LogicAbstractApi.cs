using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractApi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(int ballID, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(ballID, new PropertyChangedEventArgs(propertyName));
        }
        //public static LogicAbstractApi CreateApi(int w, int h, DataAbstractApi api = default(Data.DataAbstractApi))
        // {
        //  return new LogicApi(w, h, api ?? DataAbstractApi.CreateApi(w, h));
        //}

        
        public abstract IList CreateBalls(int amount);
        public abstract IList GetAllBalls();
        public abstract double GetBallXByID(int id);
        public abstract double GetBallYByID(int id);
        public abstract int GetBallsAmount();
        public abstract void MoveBall(int id);
        public abstract bool IsRunning();
        public abstract void Start();
        public abstract void Stop();
        public abstract int Width { get; }
        public abstract int Height { get; }

        public static LogicAbstractApi CreateApi(int w, int h, DataAbstractApi api = default(Data.DataAbstractApi))
        {
            return new LogicApi(w, h, api ?? DataAbstractApi.CreateDataApi(w, h));
        }

    }
}
