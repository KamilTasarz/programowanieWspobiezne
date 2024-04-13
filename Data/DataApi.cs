using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class DataApi : DataAbstractApi
    { 
        public override double Width { get; }
        public override double Height { get; }

        DataApi (double w, double h)
        {
            Width = w;
            Height = h;
        }
    }
}
