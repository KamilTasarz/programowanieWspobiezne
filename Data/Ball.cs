using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Ball : DataApi, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;

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



        public override void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Random random = new Random();

        public Ball (float x, float y, float radius) {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }

    }
}
