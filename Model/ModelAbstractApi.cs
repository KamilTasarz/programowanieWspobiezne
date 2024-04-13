using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Model
{
    public abstract class ModelAbstractApi
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void Add();
        public abstract void Remove();
        public abstract void Create();
        public abstract void Delete();
        public abstract int ballCounter { get; set; }
        public abstract int widthBoard { get; }
        public abstract int heightBoard { get; }
    }
}
