using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockVision
{
    public partial class SplashScreenForm : Form
    {
        /// <summary>
        /// Temporizador de la pantalla Splash
        /// </summary>
        Timer tmr;

        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private void SplashScreenForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.RoyalBlue, 1, ButtonBorderStyle.Solid, Color.DarkBlue, 1, ButtonBorderStyle.Solid, Color.DarkBlue, 1, ButtonBorderStyle.Solid, Color.DarkBlue, 1, ButtonBorderStyle.Solid);
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            //after 3 sec stop the timer
            tmr.Stop();
            //display mainform
            MainForm mf = new MainForm();
            mf.Show();
            //hide this form
            this.Hide();
        }

        private void SplashScreenForm_Shown(object sender, EventArgs e)
        {
            tmr = new Timer();
            //set time interval 3 sec
            tmr.Interval = 3000;
            //starts the timer
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }        

        private void SplashScreenForm_Load(object sender, EventArgs e)
        {
            this.Refresh();
        }
    }
}
