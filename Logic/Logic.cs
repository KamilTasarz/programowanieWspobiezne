using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;

namespace Logic
{
    public class Logic : LogicApi
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private int width;
        private int height;
        private DataApi[] balls;
        private List<Task> tasks;
        private CancellationToken isRunning;
        private CancellationTokenSource source;
        private ObservableCollection<DataApi> observableData = new ObservableCollection<DataApi>();
        public Logic(int width, int height, int amount)
        {
            this.width = width;
            this.height = height;
            balls = new Ball[amount];
            
            for (int i = 0; i < amount; i++)
            {
                balls[i] = CreateBall();
            }
        }

        public override ObservableCollection<DataApi> getObservs() {  return observableData; }

        Random random = new Random();
        public override DataApi CreateBall()
        {
            int radius = 20;
            float x = random.NextInt64(radius, width - radius);
            float y = random.NextInt64(radius, height - radius);
            return DataApi.CreateBall(x, y, radius);
        }

        public override void Start()
        {
            //if (!isRunning.IsCancellationRequested) 
            //{
                foreach (var ball in balls)
                {
                    updateVelocity(ball);
                    isRunning = source.Token;
                    tasks = new List<Task>();
                    Task task = new Task(() => //jak nie zadziala zmienic na Thread
                    {
                        while (isRunning.IsCancellationRequested)
                        {
                            if (isCollision(ball))
                            {
                                updateVelocity(ball);
                            }
                            updatePosition(ball);
                            ball.PropertyChanged += OnPropertyChanged;
                            Task.Delay(100);
                        }
                    });
                    
                    tasks.Add(task);
                    task.Start();
                }
            //}
        }

        public override void Stop()
        {
            //foreach(var task in tasks)
            //{
                source.Cancel();
            //}
        }

        public override void updatePosition(DataApi ball)
        {
            ball.X += ball.GetVelocityX();
            ball.Y += ball.GetVelocityY();
            ball.OnPropertyChanged(nameof(ball.X));
            ball.OnPropertyChanged(nameof(ball.Y));
        }

        public override void updateVelocity(DataApi ball)
        {
            Random random = new Random();
            float velX;
            float velY;
            do 
            {
                velX = random.NextInt64(1, 5) - 3;
                velY = random.NextInt64(1, 5) - 3;
            } while (velX == 0 || velY == 0);
            ball.SetVelocityX(velX);
            ball.SetVelocityY(velY);
            ball.OnPropertyChanged(nameof (ball.GetVelocityX));
            ball.OnPropertyChanged(nameof (ball.GetVelocityY));
        }

        public override bool isCollision(DataApi ball)
        {
            return ball.X <= ball.GetRadius() || ball.Y <= ball.GetRadius() || ball.X >= width - ball.GetRadius() || ball.Y >= height - ball.GetRadius();
        }

        public void OnPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }
    }
}
