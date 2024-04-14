using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;
using NUnit.Framework.Internal.Commands;
using static NUnit.Framework.RetryAttribute;

namespace ViewModel
{

    public class ViewModelMain : BaseViewModel
    {
        private ModelAbstractApi model;
        private int Width;
        private int Height;
        private int Diameter = 40;
        private bool _isStopEnabled = false;
        private bool _isStartEnabled = false;
        private bool _isAddEnabled = true;
        private bool _isDeleteEnabled = false;
        private int _BallVal;


        public ViewModelMain() 
        {
            BallVal = 0;
            Width = 600;
            Height = 400;
            model = ModelAbstractApi.CreateApi(Width, Height);
            StopCmd = new Commands(Stop, null);
            StartCmd = new Commands(Start, null);
            AddCmd = new Commands(Add, null);
            DeleteCmd = new Commands(Delete, null);

        }

        public ICommand AddCmd { get; set; }
        public ICommand StopCmd { get; set; }
        public ICommand StartCmd { get; set; }
        public ICommand DeleteCmd { get; set; }

        public void Start()
        { 
            model.Start();
        }

        public void Stop()
        {
            model.Stop();
        }

        public void Add()
        {
            model.Add();
        }

        public void Delete()
        {
            model.Delete();
        }

        public int BallVal
        {
            get { return _BallVal; }
            set
            {
                _BallVal = value;
                OnPropertyChanged();
            }
        }

    
    }
}
