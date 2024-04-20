using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Commands startcmd { get; set; }
        public Commands stopcmd { get; set; }
        private ModelApi model;
        private int ilosc;
        public ObservableCollection<IEllipse> ballsToDraw { get; } = new ObservableCollection<IEllipse>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel() 
        {
            model = ModelApi.CreateModelApi(600, 600, 3);
            startcmd = new Commands(Start);
            stopcmd = new Commands(Stop);
        }

        public void Start(object obj)
        {
            model = ModelApi.CreateModelApi(750, 350, chooseBallAmount);
            IDisposable observer = model.Subscribe<IEllipse>(x => ballsToDraw.Add(x));
            foreach (IEllipse ball in model.getBalls())
            {
                ballsToDraw.Add(ball);
            }
            model.Start();

        }
        public void Stop(object obj)
        {
            model.Stop();
        }

        public int chooseBallAmount
        {
            get { return ilosc; }
            set
            {
                ilosc = value;
            }
        }
    }
}
