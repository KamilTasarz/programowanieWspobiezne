using Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Model
{
    public class MyVector : IMyVector
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float X;
        private float Y;
        private float diameter;

        public float x
        {
            get { return X; }
            set
            {
                if (X != value)
                {
                    X = value;
                    RaisePropertyChanged();
                }
            }
        }

        public float y
        {
            get { return Y; }
            set
            {
                if (Y != value)
                {
                    Y = value;
                    RaisePropertyChanged();
                }
            }
        }
        public float Diameter { get { return diameter; } }


        public MyVector(float posX, float posY, float diameter)
        {
            this.X = posX;
            this.Y = posY;
            this.diameter = diameter;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}