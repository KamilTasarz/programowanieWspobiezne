﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAbstractApi
    {
        public abstract ObservableCollection<IBall> Balls { get; }
        public abstract double Width { get; }
        public abstract double Height { get; }
    }
}
