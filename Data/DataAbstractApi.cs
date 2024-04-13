using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    abstract class DataAbstractApi
    {
        public abstract double Width { get; }
        public abstract double Height { get; }

        public abstract Ball CreateBall(int id, double x, double y, double diameter);

        public static DataApi CreateDataApi(double w, double h)
        {
            return new DataApi(w, h);
        }
    }
}
