using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IMyVector : INotifyPropertyChanged
    {
        float x { get; }
        float y { get; }
    }
    public class BallChangeEventArgs : EventArgs
    {
        public IMyVector Ball { get; internal set; }
    }
}