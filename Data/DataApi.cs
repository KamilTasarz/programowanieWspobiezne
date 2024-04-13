using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class DataApi : DataAbstractApi
    { 
        public override double Width { get; }
        public override double Height { get; }

        public DataApi (double w, double h)
        {
            Width = w;
            Height = h;
        }

        public override Ball CreateBall(int id, double x, double y, double diameter)
        {
            return new Ball(id, x, y, diameter);
        }
    }
}
