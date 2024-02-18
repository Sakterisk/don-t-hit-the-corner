using System;
using System.Drawing;

namespace Don_t_hit_the_corner
{
    public class Direction
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Direction(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class Ball
    {
        public Point Position { get; set; }
        private readonly Size Size;
        private Brush ForegroundColor;
        public int Speed { get; set; }
        public Direction Direction { get; set; }

        public Ball(Point position, Size size, int speed)
        {
            Position = position;
            Size = size;
            Speed = speed;
            ForegroundColor = Brushes.Red;
            Direction = new Direction(1,1);
        }

        public void Update()
        {
            CheckCollision();
            Position = new Point(Position.X + Direction.X * Speed, Position.Y + Direction.Y * Speed);
        }

        private void CheckCollision()
        {
            if (Position.X < 0 || Position.X > 800 - Size.Width)
            {
                Direction.X = -Direction.X;
                randomColor();
            }
            if (Position.Y < 0 || Position.Y > 600 - Size.Height)
            {
                Direction.Y = -Direction.Y;
                randomColor();
            }
        }   

        private void randomColor()
        {
            Random random = new Random();
            ForegroundColor = new SolidBrush(Color.FromArgb(255, random.Next(25,230), random.Next(25, 230), random.Next(25, 230)));
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(ForegroundColor, new Rectangle(Position, Size));
        }
    }
}
