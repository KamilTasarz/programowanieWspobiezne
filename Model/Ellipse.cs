using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ellipse : IEllipse
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private float X;
        private float Y;
        

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

        public float r { get; internal set; }

        public Ellipse(float posX, float posY)
        {
            this.X = posX;
            this.Y = posY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
