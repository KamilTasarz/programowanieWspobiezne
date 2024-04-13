using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DataApi : DataAbstractApi
    {
        private ObservableCollection<IBall> balls;
        public override double Width { get; }
        public override double Height { get; }

        public override ObservableCollection<IBall> Balls { get => balls; }

        DataApi (double w, double h)
        {
            Width = w;
            Height = h;
        }

    }
}
