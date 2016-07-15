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
    /// Form que funciona como HOME para cada proyecto abierto
    /// </summary>
    public partial class ProjectForm : Form
    {
        #region variables de clase

        public MainForm padre;

        #endregion

        Point lastClick;

        public ProjectForm()
        {
            InitializeComponent();
        }

        private void ProjectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.CerrarProjectForm();
        }

        private void ProjectForm_Load(object sender, EventArgs e)
        {
            // se carga la informacion del proyect
            SetForm();
        }

        /// <summary>
        /// Establece todos los elementos visuales del Form
        /// </summary>
        public void SetForm()
        {
            // nombre de la forma
            this.Text = padre.actual.name.ToUpper();
            this.lblProyecto.Text = padre.actual.name.ToUpper();
            
            // segmentacion
            if (padre.actual.segmentacionDone)
            {
                pictSegHigh.Image = RockStatic.Properties.Resources.greenTick;
                lblSeg.Text = "La segmentacion de los slides se completo exitosamente";
            }
            else
            {
                pictSegHigh.Image = RockStatic.Properties.Resources.redX;
                lblSeg.Text = "Aun no ha realizado la segmentacion de los slides";
            }

            // areas
            if (padre.actual.areasDone)
            {
                pictAreasHigh.Image = RockStatic.Properties.Resources.greenTick;
                lblArea.Text = "Se seleccionaron las areas de interes exitosamente";
            }
            else
            {
                pictAreasHigh.Image = RockStatic.Properties.Resources.redX;
                lblArea.Text = "Aun no ha seleccionado las areas de interes";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.padre.actual.Salvar();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // se cierra esta ventana, y todas las demas, y se abre el HomeForm
            if (padre.abiertoCheckForm) padre.checkForm.Close();
            if (padre.abiertoNuevoProyectoForm) padre.nuevoProyectoForm.Close();

            this.padre.AbrirHome();
            this.Close();
        }

        /// <summary>
        /// Se inicia la seleccion de areas para los elementos HIGH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAreasHigh_Click(object sender, EventArgs e)
        {
            // se verifica primero que primero se hallan segmentado correctamente los elementos HIGH y LOW
            if (!padre.actual.segmentacionDone)
            {
                MessageBox.Show("No es posible realizar la seleccion de areas de interes para los elementos.\n\nRealize primero la segmentacion de los elementos HIGH.", "Error al iniciar la seleccion de areas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }     
            else
            {
                // se ha realizado la segmentacion de los slide, se puede proceder a realizar la seleccion de areas
                if(!padre.abiertoSelectAreasForm)
                {
                    this.padre.selecAreasForm = new SelectAreasForm();
                    this.padre.selecAreasForm.Text = "SELECCION DE AREAS";
                    this.padre.selecAreasForm.label4.Text = "SELECCION DE AREAS";

                    this.padre.selecAreasForm.MdiParent = this.MdiParent;
                    this.padre.selecAreasForm.padre = this.padre;

                    this.padre.abiertoSelectAreasForm = true;
                    this.padre.selecAreasForm.Show();

                    // segunda ventana

                    this.padre.selecAreas2Form = new SelectAreas2Form();
                    this.padre.selecAreas2Form.Text = "SELECCION DE AREAS";
                    this.padre.selecAreas2Form.label4.Text = "SELECCION DE AREAS";

                    this.padre.selecAreas2Form.padre = this.padre;

                    this.padre.abiertoSelectAreas2Form = true;
                    this.padre.selecAreas2Form.Show();

                    if (Screen.AllScreens.Length > 1)
                    {
                        this.padre.selecAreas2Form.Location = new Point(Screen.AllScreens[1].WorkingArea.X + Screen.AllScreens[1].WorkingArea.Width / 2 - padre.selecAreas2Form.Width / 2, Screen.AllScreens[1].WorkingArea.Y + Screen.AllScreens[1].WorkingArea.Height / 2 - padre.selecAreas2Form.Height / 2);
                    }
                    
                    this.padre.selecAreasForm.Select();

                }
                else
                {
                    this.padre.selecAreasForm.Select();
                }
            }
        }

        private void lblProyecto_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void lblProyecto_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void btnSegHigh_Click(object sender, EventArgs e)
        {
            if (!padre.abiertoSegmentacionForm)
            {
                this.padre.segmentacionForm = new SegmentacionForm();
                this.padre.segmentacionForm.Text = "SEGMENTACION";
                this.padre.segmentacionForm.lblTitulo.Text = "SEGMENTACION";

                this.padre.segmentacionForm.MdiParent = this.MdiParent;
                this.padre.segmentacionForm.padre = this.padre;

                this.padre.abiertoSegmentacionForm = true;
                this.padre.segmentacionForm.Show();                
            }
            else
            {
                this.padre.segmentacionForm.Select();                
            }
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void lblProyecto_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnSegHigh_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSegHigh_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void ProjectForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }            
    }
}
