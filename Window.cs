using System;
using System.Drawing;
using System.Windows.Forms;

namespace Don_t_hit_the_corner
{

    public partial class Window : Form
    {
        private readonly Timer timer;
        private BufferedGraphicsContext context;
        private BufferedGraphics buffer;
        private readonly Ball ball;
        private Rectangle wall;
        private Timer wallTimer;
        private int wallOpacity;

        public Window()
        {
            InitializeComponent();
            InitializeBuffer();
            ball = new Ball(new Point(0, 0), new Size(50, 50), 2);
            timer = new Timer
            {
                Interval = 1000 / 200
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            KeyDown += Window_KeyDown;
            wallOpacity = 255;
            wallTimer = new Timer
            {
                Interval = 50
            };
            wallTimer.Tick += WallTimer_Tick;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (ball.Direction.X < 0)
                    {
                        ball.Direction.X = -ball.Direction.X;
                        wall = new Rectangle(ball.Position.X - 20, ball.Position.Y - 15, 20, 80);
                        KeyDown -= Window_KeyDown;
                        wallTimer.Start();
                    }
                    break;
                case Keys.Right:
                    if (ball.Direction.X > 0)
                    {
                        ball.Direction.X = -ball.Direction.X;
                        wall = new Rectangle(ball.Position.X + 50, ball.Position.Y - 15, 20, 80);
                        KeyDown -= Window_KeyDown;
                        wallTimer.Start();
                    }
                    break;
                case Keys.Up:
                    if (ball.Direction.Y < 0)
                    {
                        ball.Direction.Y = -ball.Direction.Y;
                        wall = new Rectangle(ball.Position.X - 15, ball.Position.Y - 20, 80, 20);
                        KeyDown -= Window_KeyDown;
                        wallTimer.Start();
                    }
                    break;
                case Keys.Down:
                    if (ball.Direction.Y > 0)
                    {
                        ball.Direction.Y = -ball.Direction.Y;
                        wall = new Rectangle(ball.Position.X - 15, ball.Position.Y + 50, 80, 20);
                        KeyDown -= Window_KeyDown;
                        wallTimer.Start();
                    }
                    break;
                default:
                    break;
            }
        }

        private void WallTimer_Tick(object sender, EventArgs e)
        {
            wallOpacity -= 5;
            if (wallOpacity <= 0)
            {
                wall = Rectangle.Empty;
                wallOpacity = 255;
                wallTimer.Stop();
                KeyDown += Window_KeyDown;
            }
        }
        private void InitializeBuffer()
        {
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.ClientSize.Width + 1, this.ClientSize.Height + 1);
            buffer = context.Allocate(this.CreateGraphics(), this.ClientRectangle);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ball.Update();
            Redraw();
        }

        private void Redraw()
        {
            buffer.Graphics.Clear(Color.Black);
            ball.Draw(buffer.Graphics);
            if (!wall.IsEmpty)
            {
                Color wallColor = Color.FromArgb(wallOpacity, Color.White);
                buffer.Graphics.FillRectangle(new SolidBrush(wallColor), wall); // draw the wall if it exists
            }
            buffer.Render(Graphics.FromHwnd(Handle));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }

    private void InitializeComponent()
        {
            base.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "Dont hit the corner";
            this.ResumeLayout(false);
        }
    }
}
