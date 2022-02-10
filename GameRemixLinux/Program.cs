using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameRemixForLinux
{
    static class Program
    {
        static bool IsRunning = true;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RenderForm form = new RenderForm();
            form.FormClosing += Form_FormClosing;
            form.Show();

            DateTime dateTime = DateTime.Now;
            float frametime = 1;
            while (IsRunning)
            {
                form.Logic(frametime);
                form.Invalidate();
                Application.DoEvents();
                //Thread.Sleep(33);


                DateTime next = DateTime.Now;
                double totalMilliseconds = next.Subtract(dateTime).TotalMilliseconds;
                //Debug.WriteLine(totalMilliseconds);
                if (totalMilliseconds == 0)
                {
                    totalMilliseconds = 1;
                }
                frametime = (float)totalMilliseconds / 1000.0f;
                dateTime = next;
            }

            form.Close();
            form.Dispose();
        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRunning = false;
        }
    }

}


