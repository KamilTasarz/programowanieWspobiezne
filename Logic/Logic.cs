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
using System.Drawing;
using System.Numerics;

namespace Logic
{
    public class Logic : LogicApi, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler PropertyChanged;

        //stol
        private int width;
        private int height;
        private DataApi[] balls;

        //private List<Task> tasks = new List<Task>();

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
                balls[i] = CreateBall(i);
                updateVelocity(balls[i], false);
            }
        }

        public override float GetRadius(int id)
        {
            return balls[id].GetRadius();
        }


        public override DataApi CreateBall(int id)
        {
            Random random = new Random();
            //long radius = random.NextInt64(8, 12);
            long radius = 10;
            
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
            return DataApi.CreateBall(id, x, y, radius);
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
            return ball.Y <= 0 || ball.Y >= height;
        }
        public override bool isCollisionLeftRight(DataApi ball)
        {
            return ball.X <= 0 || ball.X >= width;
        }


        public override void Start()
        {
            source = new CancellationTokenSource();
            foreach (var ball in balls)
            {
                CancellationToken running = source.Token;
                ball.PropertyChanged += RelayBallUpdate;
                ball.movement(running);
            }
        }
    

        public override void Stop()
        {
            source.Cancel();
        }

        public override void updatePosition(DataApi ball)
        {
            ball.X += ball.GetVelocityX();
            ball.Y += ball.GetVelocityY();
            CheckAll(ball);
            ball.RaisePropertyChanged();
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
        }

        public override void InBoundries(DataApi ball)
        {
            float size = ball.GetRadius() / 2;
            if (ball.X < size)
            {
                if (ball.Y < size)
                {
                    ball.X = size;
                    ball.Y = size;
                }
                else if (ball.Y + size > height)
                {
                    ball.X = size;
                    ball.Y = height - size;
                }
                else
                {
                    ball.X = size;
                }
            }
            else if (ball.X + size > width)
            {
                if (ball.Y < size)
                {
                    ball.X = width - size;
                    ball.Y = size;
                }
                else if (ball.Y + size > height)
                {
                    ball.X = width - size;
                    ball.Y = height - size;
                }
                else
                {
                    ball.X = width - size;
                }
            }
            else
            {
                if (ball.Y < size)
                {
                    ball.Y = size;
                }
                else if (ball.Y + size > height)
                {
                    ball.Y = height - size;
                }
            }
        }

        private void CheckAll(DataApi ball)
        {
            mutex.WaitOne();
            if (isCollisionUpDown(ball))
            {
                InBoundries(ball);
                updateVelocity(ball, true);
            }
            else if (isCollisionLeftRight(ball))
            {
                InBoundries(ball);
                updateVelocity(ball, false);
            }
            checkBallCollisons(ball);

            OnPropertyChanged();
            mutex.ReleaseMutex();
        }

        public override void checkBallCollisons(DataApi ball)
        {
            
            foreach(var tempBall in balls)
            {
                float diameter = ball.GetRadius() + tempBall.GetRadius();
                if (tempBall != ball)
                {
                    double xdiff = tempBall.X - ball.X;
                    double ydiff = tempBall.Y - ball.Y;
                    double distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff) - 0.2f;
                    if  (distance <= diameter)
                    {
                        //1 -> ball, 2 -> tempball ---- dodany kod z masą, ale nie potrzebny, ponieważ przyjeliśmy kule jednakowej wielkości
                        float finVelX1 = ((ball.Mass - tempBall.Mass) / (ball.Mass + tempBall.Mass)) * ball.GetVelocityX() + ((2 * tempBall.Mass) / (ball.Mass + tempBall.Mass)) * tempBall.GetVelocityX();
                        float finVelX2 = ((tempBall.Mass - ball.Mass) / (ball.Mass + tempBall.Mass)) * tempBall.GetVelocityX() + ((2 * ball.Mass) / (ball.Mass + tempBall.Mass)) * ball.GetVelocityX();
                        float finVelY1 = ((ball.Mass - tempBall.Mass) / (ball.Mass + tempBall.Mass)) * ball.GetVelocityY() + ((2 * tempBall.Mass) / (ball.Mass + tempBall.Mass)) * tempBall.GetVelocityY();
                        float finVelY2 = ((tempBall.Mass - ball.Mass) / (ball.Mass + tempBall.Mass)) * tempBall.GetVelocityY() + ((2 * ball.Mass) / (ball.Mass + tempBall.Mass)) * ball.GetVelocityY();
                        

                        float val = ball.GetRadius() + tempBall.GetRadius() - (float) distance;

                        while (distance <= diameter)
                        {
                            ball.X -= 0.001f * ball.GetVelocityX();
                            ball.Y -= 0.001f * ball.GetVelocityY();
                            xdiff = tempBall.X - ball.X;
                            ydiff = tempBall.Y - ball.Y;
                            distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                        }

                        while (distance <= diameter)
                        {
                            tempBall.X -= 0.001f * tempBall.GetVelocityX();
                            tempBall.Y -= 0.001f * tempBall.GetVelocityY();
                            xdiff = tempBall.X - ball.X;
                            ydiff = tempBall.Y - ball.Y;
                            distance = Math.Sqrt(xdiff * xdiff + ydiff * ydiff);
                        }

                        /*float tempvel = ball.GetVelocityX();
                        float tempvel2 = ball.GetVelocityY();

                        ball.SetVelocityX(tempBall.GetVelocityX());

                        tempBall.SetVelocityX(tempvel);

                        ball.SetVelocityY(tempBall.GetVelocityY());
                        
                        tempBall.SetVelocityY(tempvel2);*/

                        ball.SetVelocityX(finVelX1);

                        tempBall.SetVelocityX(finVelX2);

                        ball.SetVelocityY(finVelY1);
                        
                        tempBall.SetVelocityY(finVelY2);
                    }
                }
            }
        }

        

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void RelayBallUpdate(object source, PropertyChangedEventArgs args)
        {
            CheckAll(balls[(int) source]);
        }

        
    }
}
