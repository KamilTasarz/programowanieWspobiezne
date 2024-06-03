using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
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
        private int id;
        private float velocityX;
        private float velocityY;
        private float radius;
        private float mass;
        private LoggerApi logger;
        
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

        public override float Mass 
        { 
            get { return mass; } 
        }

        public override int ID
        {
            get { return id; }
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

        public override async Task movement(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                X += GetVelocityX();
                Y += GetVelocityY();
                logger.CreateLog(CreateLogMsg());
                RaisePropertyChanged();
                await Task.Delay(40);
            }
        }


        

        public override void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(ID, new PropertyChangedEventArgs(propertyName));
        }


        public Ball (int identifier, float x, float y, float radius, LoggerApi log) {
            this.x = x;
            this.y = y;
            id = identifier;
            this.radius = radius;
            logger = log;
            mass = 0.008f * radius * radius * radius; //masa dla kuli o srednicy 10 cm przyjmujemy 1kg, zatem gestosc to 0.002 kg/cm3
        }

        private string CreateLogMsg()
        {
            string dateStamp, data;
            string log = "";

            if (this != null)
            {
                data = "{" + String.Format("\"ID\": \"{0}\", \"X\": \"{1}\", \"Y\": \"{2}\", \"VelocityX\": \"{3}\", \"VelocityY\": \"{4}\"", ID, X, Y, GetVelocityX(), GetVelocityY()) + "}";
          
                dateStamp = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff");
                log = "{" + String.Format("\"Date\": \"{0}\", \"Ball\":{1}", dateStamp, data) + "}";
            }

            return log;
        }

    }
}
