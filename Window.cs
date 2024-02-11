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

        public Window()
        {
            InitializeComponent();
            InitializeBuffer();
            ball = new Ball(new Point(0, 0), new Size(50, 50), 5);
            timer = new Timer
            {
                Interval = 1000 / 60
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            this.KeyDown += Window_KeyDown;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (ball.Direction.X < 0)
                    {
                        ball.Direction.X = -ball.Direction.X;
                    }
                    break;
                case Keys.Right:
                    if (ball.Direction.X > 0)
                    {
                        ball.Direction.X = -ball.Direction.X;
                    }
                    break;
                case Keys.Up:
                    if (ball.Direction.Y < 0)
                    {
                        ball.Direction.Y = -ball.Direction.Y;
                    }
                    break;
                case Keys.Down:
                    if (ball.Direction.Y > 0)
                    {
                        ball.Direction.Y = -ball.Direction.Y;
                    }
                    break;
                default:
                    break;
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
