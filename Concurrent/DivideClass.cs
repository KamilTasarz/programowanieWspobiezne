using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrent
{
    public class DivideClass
    {
        public double divide(double divident, double divisor)
        {
            if (divisor != 0)
            {
                return divident / divisor;
            }
            else
            {
                throw new DivideByZeroException();
            }
        }
    }
}
