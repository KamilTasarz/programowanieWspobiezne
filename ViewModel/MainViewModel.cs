using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public RelayCommand StartCmd { get; set; }
        public RelayCommand StopCmd { get; set; }
        public int Width { get; }
        public int Height { get; }
 
        private ModelApi model;
        public int ilosc;
        public ObservableCollection<IMyVector> ellipsesCooridinates { get; } = new ObservableCollection<IMyVector>();

        public MainViewModel()
        {
            StartCmd = new RelayCommand(Start);
            StopCmd = new RelayCommand(Stop);
            Width = 400;
            Height = 400;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Start(object obj)
        {

            ellipsesCooridinates.Clear();
            model = ModelApi.CreateApi(Width - 20, Height - 20, chooseBallAmount);
            IDisposable observer = model.Subscribe<IMyVector>(x => ellipsesCooridinates.Add(x));
            foreach (IMyVector b in model.GetBalls())
            {
                ellipsesCooridinates.Add(b);
            }
            model.Start();
        }

        private void Stop(object obj)
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