using System.ComponentModel;

namespace Data
{
    public abstract class DataApi : INotifyPropertyChanged
    {
        
        public abstract float X { get; set; }
        public abstract float Y { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract float GetVelocityX();
        public abstract void SetVelocityX(float newVelocityX);
        public abstract float GetVelocityY();
        public abstract void SetVelocityY(float newVelocityY);
        public abstract float GetRadius();
        public static DataApi CreateBall(float x, float y, float radius) 
        { 
            return new Ball(x, y, radius); 
        }
        public abstract void OnPropertyChanged(string propertyName)
    }
}
