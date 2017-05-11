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
    public partial class ProjectDForm : Form
    {
        public MainForm padre;

        public ProjectDForm()
        {
            InitializeComponent();
        }

        private void ProjectDForm_Load(object sender, EventArgs e)
        {
            padre.actualD.datacubos[0].CrearBitmapThread();
            pictureBox1.Image = padre.actualD.datacubos[0].dataCube[0].bmp;
        }
    }
}
