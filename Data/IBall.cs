using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class IBall : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        int ID { get; }
        double x { get; set; }
        double y { get; set; }
        double diameter { get; }

        public abstract void CreateTaskMove(CancellationToken cancellationToken);
        internal void RaisePropertyChanged()
        {
            PropertyChanged?.Invoke(ID, new PropertyChangedEventArgs(null));
        }


    }
}
