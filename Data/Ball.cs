using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    internal class Ball : DataApi
    {
        private float x;
        private float y;
        private float velocityX;
        private float velocityY;
        private float radius;
        
        public override float X
        {
            get { return x; }
            set
            {
                x = value;
            }
        }

        public override float Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }
        public override float GetVelocityX() { return velocityX; }
        public override float GetVelocityY() { return velocityY; }
        public override float GetRadius() { return radius; }
        public override void SetVelocityY(float newVelocityY) 
        {  
           velocityY = newVelocityY; 
        }
        public override void SetVelocityX(float newVelocityX) 
        {  
           velocityX = newVelocityX; 
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
