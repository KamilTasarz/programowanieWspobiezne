using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Model
{
    public abstract class ModelAbstractApi : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract void Start();
        public abstract void Stop();
        public abstract void Add();
        public abstract int ballCounter { get; set; }
        public abstract int widthBoard { get; }
        public abstract int heightBoard { get; }

        public abstract IList Create(int amount);

        public abstract IList Delete(int amount);

        public Canvas board { get; set; }

        public static ModelAbstractApi CreateApi(int w, int h)
        {
            return new ModelApi(w, h);
        }

        protected virtual void OnPropertyChanged(int ball, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(ball, new PropertyChangedEventArgs(propertyName));
        }

    }
}
