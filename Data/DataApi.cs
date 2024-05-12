using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Data
{
    public abstract class DataApi
    {
        
        public abstract float X { get; set; }
        public abstract float Y { get; set; }
        public abstract float Mass { get; }
        public abstract int ID { get; }

        public abstract event PropertyChangedEventHandler? PropertyChanged;

        public abstract float GetVelocityX();
        public abstract void SetVelocityX(float newVelocityX);
        public abstract float GetVelocityY();
        public abstract void SetVelocityY(float newVelocityY);
        public abstract float GetRadius();

        public abstract Task movement(CancellationToken token);
        public static DataApi CreateBall(int id, float x, float y, float radius) 
        { 
            return new Ball(id, x, y, radius); 
        }
        public abstract void RaisePropertyChanged([CallerMemberName] string? propertyName = null);
    }
}
