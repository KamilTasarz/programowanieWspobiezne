using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public interface IEllipse : INotifyPropertyChanged
    {
        
            float x { get; }
            float y { get; }
            float r { get; }
        
     
    }

    public class BallChangeEventArgs : EventArgs
    {
        public IEllipse Ball { get; internal set; }
    }
}
