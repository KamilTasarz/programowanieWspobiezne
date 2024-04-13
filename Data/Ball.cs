using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;

namespace Data
{
    internal class Ball : IBall
    {
        protected int id;

        private double x;
        private double y;
        private double diameter;
        private readonly Random random = new Random();
        private Task task;
        public override void CreateTaskMove(CancellationToken cancellationToken)
        {
            task = Move(cancellationToken);
        }

        private async Task Move(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                x += (random.NextDouble() - 0.5) * 10;
                y += (random.NextDouble() - 0.5) * 10;
                await Task.Delay(100, cancellationToken);
                RaisePropertyChanged();
            }
        }

        public int ID { get => id; }
        public double X { get => x; set => this.x = value;}
        public double Y { get => y; set => this.y = value;}
        public double Diameter { get => diameter; }

        public Ball(int id, double x, double y, double diameter) 
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.diameter = diameter;
        }
    }
}
