using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Data
{
    public abstract class IBall : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ID { get; }
        public double X { get; set; }
        public double Y { get; set; }
        public double diameter { get; }

        public abstract void CreateTaskMove(CancellationToken cancellationToken);
        internal void RaisePropertyChanged()
        {
            PropertyChanged?.Invoke(ID, new PropertyChangedEventArgs(null));
        }


    }
}
