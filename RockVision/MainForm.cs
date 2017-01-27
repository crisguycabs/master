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
    public partial class MainForm : Form
    {
        #region variables de diseñador

        /// <summary>
        /// Instancia de la ventana
        /// </summary>
        public VisualForm visualForm = null;

        /// <summary>
        /// indica si la ventana esta abierta o no
        /// </summary>
        public bool abiertoVisualForm = false;

        /// <summary>
        /// Instancia del form WaitingForm
        /// </summary>
        public WaitingForm waitingForm;

        /// <summary>
        /// Variable que indica si la ventana WaitingForm esta abierta o no
        /// </summary>
        public bool abiertoWaitingForm;

        Point lastClick;

        /// <summary>
        /// indica si la ventana esta abierta o no
        /// </summary>
        bool abiertoHomeForm = false;

        /// <summary>
        /// instancia de HomeForm
        /// </summary>
        public HomeForm homeForm = null;

        /// <summary>
        /// indica si la ventana esta abierta o no
        /// </summary>
        public bool abiertoNuevoProyectoVForm = false;

        /// <summary>
        /// instancia de NewProjectVForm
        /// </summary>
        public NewProjectVForm nuevoProyectoVForm = null;

        /// <summary>
        /// proyecto actual de visualizacion
        /// </summary>
        public CProyectoV actualV = null;

        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // rutina de preparacion de la interfaz personalizada
            this.tableLayoutPanel1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
            menuMain.BackColor = Color.FromArgb(255, 255, 255);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.Refresh();

            AbrirHomeForm();
        }

        public void AbrirHomeForm()
        {
            if (!abiertoHomeForm)
            {
                homeForm = new HomeForm();
                homeForm.MdiParent = this;
                homeForm.padre = this;
                this.abiertoHomeForm = true;
                homeForm.Show();
            }
            else
            {
                homeForm.Select();
            }
        }

        public void CerrarHomeForm()
        {
            this.abiertoHomeForm = false;
            this.homeForm = null;
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.RoyalBlue, r);
            }
            if (e.Row == 1 || e.Row == 1)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.White, r);
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) this.Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                btnMaximize.BackgroundImage = RockVision.Properties.Resources.demaximize;

            }
            else
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.BackgroundImage = RockVision.Properties.Resources.maximize;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            btnMaximize_Click(sender, e);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        /// <summary>
        /// Se muestra la ventana WaitingForm, con el mensaje que se muestra como argumento
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar en pantalla</param>
        public void ShowWaiting(string mensaje)
        {
            if (!this.abiertoWaitingForm)
            {
                waitingForm = new WaitingForm();
                waitingForm.lblTexto.Text = mensaje;
                abiertoWaitingForm = true;
                waitingForm.Show();
                Application.DoEvents();
            }
            else
            {
                waitingForm.lblTexto.Text = mensaje;
                Application.DoEvents();
            }
        }

        /// <summary>
        /// Cierra la ventana WaitingForm
        /// </summary>
        public void CloseWaiting()
        {
            abiertoWaitingForm = false;
            waitingForm.Close();
            Application.DoEvents();
        }

        /// <summary>
        /// Se crea un nuevo proyecto de visualizacion
        /// </summary>
        public void NuevoProyectoV()
        {
            OpenFileDialog seldicom = new OpenFileDialog();
            seldicom.Multiselect = true;

            List<string> temp= new List<string>();

            if (seldicom.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < seldicom.FileNames.Length; i++)
                    temp.Add(seldicom.FileNames[i]);                
            }
            else
            {
                // se cancela abrir un nuevo proyecto
                return;
            }

            CerrarHomeForm();

            // Se abre el Form para visualizar los archivos dicom escogidos
            if (!abiertoNuevoProyectoVForm)
            {
                nuevoProyectoVForm = new NewProjectVForm();
                nuevoProyectoVForm.MdiParent = this;
                nuevoProyectoVForm.padre = this;
                this.abiertoNuevoProyectoVForm = true;
                nuevoProyectoVForm.elementos = temp;
                nuevoProyectoVForm.Show();
            }
            else
            {
                nuevoProyectoVForm.Select();
                nuevoProyectoVForm.elementos = temp;
            }
        }

        public void CerrarNewProjectVForm()
        {
            this.abiertoNuevoProyectoVForm=false;
            this.nuevoProyectoVForm=null;        
        }

        public void AbrirVisualForm()
        {
            if (!abiertoVisualForm)
            {
                visualForm = new VisualForm();
                visualForm.MdiParent = this;
                visualForm.padre = this;
                this.abiertoVisualForm = true;
                visualForm.Show();
            }
            else
            {
                visualForm.Select();
            }
        }

        public void CerrarVisualForm()
        {
            this.abiertoVisualForm = false;
            visualForm = null;
        }
    }
}
