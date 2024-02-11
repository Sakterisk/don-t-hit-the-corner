using System;
using System.Windows.Forms;

namespace Don_t_hit_the_corner
{
    
    public class MainClass
    {
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Window window = new Window();
            Application.Run(window);
        }
    }
}

