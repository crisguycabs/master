using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockStatic
{
    public partial class WaitingForm : Form
    {
        #region variables de clase

        public MainForm padre;

        #endregion

        public WaitingForm()
        {
            InitializeComponent();
        }

        private void WaitingForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
        }
    }
}
