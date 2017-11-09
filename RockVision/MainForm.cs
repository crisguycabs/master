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
        public bool abiertoHomeForm = false;

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
        /// indica si la ventana esta abierta o no
        /// </summary>
        public bool abiertoNuevoProyectoDForm = false;

        /// <summary>
        /// instancia de NewProjectDForm
        /// </summary>
        public NewProjectDForm nuevoProyectoDForm = null;

        /// <summary>
        /// indica si la ventana esta abierta o no
        /// </summary>
        public bool abiertoCheckForm = false;

        /// <summary>
        /// instancia de CheckForm
        /// </summary>
        public CheckForm checkForm = null;

        /// <summary>
        /// proyecto actual de visualizacion
        /// </summary>
        public CProyectoV actualV = null;

        /// <summary>
        /// proyecto actual de caracterizacion dinamica
        /// </summary>
        public CProyectoD actualD = null;

        public bool abiertoProyectoDForm = false;

        public ProjectDForm proyectoDForm = null;

        /// <summary>
        /// vector FID para el calculo de la porosidad usando RMN
        /// </summary>
        public double fid = 0;

        /// <summary>
        /// vector FID estándar para el calculo de la porosidad usando RMN
        /// </summary>
        public double fidstd = 0;

        /// <summary>
        /// Volumen estándar para el calculo de la porosidad usando RMN
        /// </summary>
        public double vstd = 0;

        /// <summary>
        /// Volumen de la roca para el calculo de la porosidad usando RMN
        /// </summary>
        public double vroca = 0;

        /// <summary>
        /// Porosidad estimada usando RMN
        /// </summary>
        public double porRMN = 0;

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

            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;

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
            seldicom.Title="Escoga los archivos dicom a visualizar";

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

            homeForm.Close();

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

        /// <summary>
        /// Se crea un nuevo proyecto de caracterización dinámica
        /// </summary>
        public void NuevoProyectoD()
        {
            homeForm.Close();

            // Se abre el Form para visualizar los archivos dicom escogidos
            if (!abiertoNuevoProyectoDForm)
            {
                nuevoProyectoDForm = new NewProjectDForm();
                nuevoProyectoDForm.MdiParent = this;
                nuevoProyectoDForm.padre = this;
                this.abiertoNuevoProyectoDForm = true;
                nuevoProyectoDForm.Show();
            }
            else
            {
                nuevoProyectoDForm.Select();                
            }
        }

        /// <summary>
        /// se crea un nuevo proyecto a partir de la ruta del proyecto
        /// </summary>
        /// <param name="ruta"></param>
        public void AbrirProyectoV(string ruta)
        {
            // se muestra la ventana de espera
            ShowWaiting("Espere mientras RockVision crea el nuevo proyecto...");

            // se crea el proyecto a partir de la ruta
            this.actualV = new CProyectoV(ruta);
            this.AbrirVisualForm();

            // se cierra la ventana de espera
            CloseWaiting();
        }

        /// <summary>
        /// se crea un nuevo proyecto a partir de la ruta del proyecto
        /// </summary>
        /// <param name="ruta"></param>
        public void AbrirProyectoD(string ruta)
        {
            // se muestra la ventana de espera
            ShowWaiting("Espere mientras RockVision carga el proyecto...");

            // se crea el proyecto a partir de la ruta
            this.actualD = new CProyectoD(ruta);
            this.AbrirProyectoDForm();
            

            // se cierra la ventana de espera
            CloseWaiting();
        }

        public void AbrirProyectoDForm()
        {
            if (!abiertoProyectoDForm)
            {
                proyectoDForm = new ProjectDForm();
                proyectoDForm.MdiParent = this;
                proyectoDForm.padre = this;
                this.abiertoProyectoDForm = true;
                proyectoDForm.Show();
            }
            else
            {
                proyectoDForm.Select();
            }
        }

        public void CerrarNewProjectVForm()
        {
            this.abiertoNuevoProyectoVForm=false;
            this.nuevoProyectoVForm=null;        
        }

        public void CerrarNewProjectDForm()
        {
            this.abiertoNuevoProyectoDForm = false;
            this.nuevoProyectoDForm = null;
        }

        public void CerrarCheckForm()
        {
            this.abiertoCheckForm = false;
            this.checkForm = null;
        }

        public void CerrarProyectoDForm()
        {
            this.abiertoProyectoDForm = false;
            this.proyectoDForm = null;
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

        public double PorosidadRMN(double fid, double vstd, double fidstd, double vroca)
        {
            double porosidad = (fid * vstd) / (fidstd * vroca);
        }
    }
}
