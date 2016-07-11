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
    /// <summary>
    /// Form para mostrar un mensaje de espera
    /// </summary>
    public partial class WaitingForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm de la aplicacion
        /// </summary>
        public MainForm padre;

        #endregion

        /// <summary>
        /// 
        /// </summary>
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
