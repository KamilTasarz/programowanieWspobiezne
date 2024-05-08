using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ComponentModel;
using System.Threading;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Logic : LogicApi, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        //stol
        private int width;
        private int height;
        private DataApi[] balls;

        private List<Task> tasks = new List<Task>();

        //bool
        private CancellationToken isRunning;
        private CancellationTokenSource source;
        private Mutex mutex = new Mutex();

        public DataApi[] GetBalls() { return balls; }


        public Logic(int width, int height, int amount)
        {
            
            this.width = width;
            this.height = height;
            balls = new DataApi[amount];
            


            for (int i = 0; i < amount; i++)
            {
                balls[i] = CreateBall();
                updateVelocity(balls[i], false);
            }
        }


        Random random = new Random();
        public override DataApi CreateBall()
        {
            int radius = 10;
            
            float x = random.NextInt64(radius, width - radius);
            float y = random.NextInt64(radius, height - radius);

            float diameter = 2 * radius;
            bool done = false;
            if (balls.Length == 0)
            {
                done = true;
            }
            while (!done)
            {
                foreach (var tempBall in balls)
                {
                    if (tempBall == null)
                    {
                        done = true;
                        break;
                    }
                    double xdiff = tempBall.X - x;
                    double ydiff = tempBall.Y - y;
                    double distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                    if (distance <= diameter + 5)
                    {
                        x = random.NextInt64(radius, width - radius);
                        y = random.NextInt64(radius, height - radius);

                        break;
                    }
                }
                done = true;
            }
            return DataApi.CreateBall(x, y, radius);
        }

        public override float[][] GetPositions()
        {
            float[][] positions = new float[balls.Length][];
            for (int i = 0;i < balls.Length;i++)
            {
                positions[i] = new float[2];
                positions[i][0] = balls[i].X;
                positions[i][1] = balls[i].Y;
            }

            return positions;
        }

        public override bool isCollisionUpDown(DataApi ball)
        {
            return ball.Y <=0 || ball.Y >= height;
        }
        public override bool isCollisionLeftRight(DataApi ball)
        {
            return ball.X <= 0 || ball.X >= width;
        }

        private async Task zadanie(CancellationToken token, DataApi ball)
        {

            ball.PropertyChanged += RelayBallUpdate;
            while (!token.IsCancellationRequested)
            {
                updatePosition(ball);
                await Task.Delay(20); 
            }
        }

        public override void Start()
        {
            source = new CancellationTokenSource();
            foreach (var ball in balls)
            {

                CancellationToken running = source.Token;
                Task task = zadanie(running, ball);
                tasks.Add(task);
            }
        }
    

        public override void Stop()
        {
            source.Cancel();
            tasks.Clear();
        }

        public override void updatePosition(DataApi ball)
        {
            ball.X += ball.GetVelocityX();
            ball.Y += ball.GetVelocityY();
            CheckAll(ball);
            UpdateBallPosition(ball);
        }

        public override void updateVelocity(DataApi ball, bool UpDown)
        {
            Random random = new Random();
            float velX = ball.GetVelocityX();
            float velY = ball.GetVelocityY();
            if (ball.GetVelocityX() == 0)
            {
                do
                {
                    velX = random.NextInt64(1, 5) - 3;
                    velY = random.NextInt64(1, 5) - 3;
                } while (velX == 0 || velY == 0);
            }
            else if (UpDown)
            {
                 velY = -ball.GetVelocityY();
            }
            else
            {
                velX = -ball.GetVelocityX();
            }
            ball.SetVelocityX(velX);
            ball.SetVelocityY(velY);
            ball.RaisePropertyChanged(nameof (ball.GetVelocityX));
            ball.RaisePropertyChanged(nameof (ball.GetVelocityY));
        }

        private void CheckAll(DataApi ball)
        {
            mutex.WaitOne();
            if (isCollisionUpDown(ball))
            {
                updateVelocity(ball, true);
            }
            else if (isCollisionLeftRight(ball))
            {
                updateVelocity(ball, false);
            }
            checkBallCollisons(ball);
            

            mutex.ReleaseMutex();
        }

        private void checkBallCollisons(DataApi ball)
        {
            float diameter = 2 * balls[0].GetRadius();
            foreach(var tempBall in balls)
            {
                if (tempBall != ball)
                {
                    double xdiff = tempBall.X - ball.X;
                    double ydiff = tempBall.Y - ball.Y;
                    double distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                    if  (distance <= diameter)
                    {
                        float tempX = ball.GetVelocityX();
                        float tempY = ball.GetVelocityY();
                        ball.SetVelocityX(tempBall.GetVelocityX());
                        ball.SetVelocityY(tempBall.GetVelocityY());
                        tempBall.SetVelocityX(tempX);
                        tempBall.SetVelocityY(tempY);
                    }
                }
            }
        }

        public void UpdateBallPosition(DataApi ball)
        {
            ball.RaisePropertyChanged(nameof(ball.X));
            ball.RaisePropertyChanged(nameof(ball.Y));
        }

        private void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            
            PropertyChanged?.Invoke(this, args);
        }

        private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
        {
            
            this.OnPropertyChanged(args);
        }

    }
}
