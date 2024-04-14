using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections;

namespace Model
{
    public class ModelApi : ModelAbstractApi
    {
        public override int ballCounter {
            get { return ballCounter; }
            set
            {
                ballCounter = value;
            }
        }

        public override int widthBoard { get; }

        public override int heightBoard { get; }

        private readonly LogicAbstractApi logic;
        private List<Ellipse> elipses;
        public Canvas board { get; set; }

        public ModelApi(int w, int h)
        {
            widthBoard = w;
            heightBoard = h;
            logic.PropertyChanged += Update;
            elipses = new List<Ellipse>();
            board = new Canvas
            {
                Width = w,
                Height = h
            };
        }

        public override void Add()
        {
            ballCounter++;
        }

        public override IList Create(int amount)
        {
            
            IList list = logic.CreateBalls(amount);

            for (int i = 0; i < amount; i++)
            {
                var newEllipse = new Ellipse { Width = 40, Height = 40, Fill = Brushes.Blue };
                elipses.Add(newEllipse);

                double positionX = logic.GetBallXByID(i) - 20;
                double positionY = logic.GetBallYByID(i) - 20;
                Canvas.SetLeft(newEllipse, positionX);
                Canvas.SetTop(newEllipse, positionY);

                board.Children.Add(newEllipse);
                if (logic.IsRunning())
                {
                    logic.Stop();
                    logic.Start();
                }
            }
            //ballAmount += amount;
            return list;
        }

        public override IList Delete(int amount)
        {
            if (amount > elipseList.Count)
            {
                amount = elipseList.Count;
            }
            else if (amount < 0)
            {
                return logic.GetAllBalls();
            }
            IList list = logic.DeleteBalls(amount);
            for (int i = 0; i < amount; i++)
            {
                var deleteElipse = elipseList[^1];
                board.Children.Remove(deleteElipse);
                elipseList.Remove(elipseList[^1]);
                if (logic.IsRunning())
                {
                    logic.Stop();
                    logic.Start();
                }
            }
            //ballAmount -= amount;

            return list;
        }


        public override void Start()
        {
            Create();
            logic.Start();
        }

        public override void Stop()
        {
            logic.Stop();
        }

        //private void Update(object sender, PropertyChangedEventArgs args)
        //{
        //    int id = (int)sender;
        //    board.SetLeft (elipses[id - 1], logic.GetBallXByID(id));
        //    board.SetTop(elipses[id - 1], logic.GetBallYByID(id));
        //}
    }
}
