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
            if (padre.actual.GetSegmentacionHigh()) pictSegHigh.Image = RockStatic.Properties.Resources.greenTick;
            else pictSegHigh.Image = RockStatic.Properties.Resources.redX;

            if (padre.actual.GetSegmentacionLow()) pictSegLow.Image = RockStatic.Properties.Resources.greenTick;
            pictSegLow.Image = RockStatic.Properties.Resources.redX;

            // areas
            if (padre.actual.GetAreasHigh()) pictAreasHigh.Image = RockStatic.Properties.Resources.greenTick;
            else pictAreasHigh.Image = RockStatic.Properties.Resources.redX;

            if (padre.actual.GetAreasLow()) pictAreasLow.Image = RockStatic.Properties.Resources.greenTick;
            else pictAreasLow.Image = RockStatic.Properties.Resources.redX;
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
            if (!padre.actual.GetSegmentacionHigh())
            {
                MessageBox.Show("No es posible realizar la seleccion de areas de interes para los elementos HIGH.\n\nRealize primero la segmentacion de los elementos HIGH.", "Error al iniciar la seleccion de areas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!padre.actual.GetSegmentacionLow())
            {
                MessageBox.Show("No es posible realizar la seleccion de areas de interes para los elementos HIGH.\n\nRealize primero la segmentacion de los elementos LOW.", "Error al iniciar la seleccion de areas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }

        private void btnAreasLow_Click(object sender, EventArgs e)
        {
            // se verifica primero que primero se hallan segmentado correctamente los elementos HIGH y LOW
            if (!padre.actual.GetSegmentacionHigh())
            {
                MessageBox.Show("No es posible realizar la seleccion de areas de interes para los elementos LOW.\n\nRealize primero la segmentacion de los elementos HIGH.", "Error al iniciar la seleccion de areas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!padre.actual.GetSegmentacionLow())
            {
                MessageBox.Show("No es posible realizar la seleccion de areas de interes para los elementos LOW.\n\nRealize primero la segmentacion de los elementos LOW.", "Error al iniciar la seleccion de areas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                this.padre.segmentacionForm.Text = "SEGMENTACION HIGH";
                this.padre.segmentacionForm.lblTitulo.Text = "SEGMENTACION HIGH";

                this.padre.segmentacionForm.MdiParent = this.MdiParent;
                this.padre.segmentacionForm.padre = this.padre;

                this.padre.abiertoSegmentacionForm = true;
                this.padre.segmentacionForm.Show();                
            }
            else
            {
                if (this.padre.segmentacionForm.lblTitulo.Text == "SEGMENTACION HIGH")
                {
                    // si esta abierta la segmentacion HIGH y se marca el boton HIGH
                    this.padre.segmentacionForm.Select();
                }
                else
                {
                    // si esta abierta la segmentacion LOW y se marca el boton HIGH
                    if (MessageBox.Show("La solicitado realizar la segmentacion de los elementos HIGH, pero se estan segmentando los elementos LOW.\r\n\r\nDesea cancelar la segmentacion de los elementos LOW?", "Advertencia: Segmentacion en proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // se ha solicitado cambiar de elementos a segmentar
                        this.padre.segmentacionForm.Close();

                        this.padre.segmentacionForm = new SegmentacionForm();
                        this.padre.segmentacionForm.Text = "SEGMENTACION HIGH";
                        this.padre.segmentacionForm.lblTitulo.Text = "SEGMENTACION HIGH";

                        this.padre.segmentacionForm.MdiParent = this.MdiParent;
                        this.padre.segmentacionForm.padre = this.padre;

                        this.padre.abiertoSegmentacionForm = true;
                        this.padre.segmentacionForm.Show();
                    }
                    else
                    {
                        // se ha solicitado continuar con la segmentacion de los elementos LOW
                        this.padre.segmentacionForm.Select();
                    }
                }
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

        private void btnSegLow_Click(object sender, EventArgs e)
        {
            if (!padre.abiertoSegmentacionForm)
            {
                this.padre.segmentacionForm = new SegmentacionForm();
                this.padre.segmentacionForm.Text = "SEGMENTACION LOW";
                this.padre.segmentacionForm.lblTitulo.Text = "SEGMENTACION LOW";

                this.padre.segmentacionForm.MdiParent = this.MdiParent;
                this.padre.segmentacionForm.padre = this.padre;

                this.padre.abiertoSegmentacionForm = true;
                this.padre.segmentacionForm.Show();
            }
            else
            {
                if (this.padre.segmentacionForm.lblTitulo.Text == "SEGMENTACION LOW")
                {
                    // si esta abierta la segmentacion LOW y se marca el boton LOW
                    this.padre.segmentacionForm.Select();
                }
                else
                {
                    // si esta abierta la segmentacion LOW y se marca el boton HIGH
                    if (MessageBox.Show("La solicitado realizar la segmentacion de los elementos LOW, pero se estan segmentando los elementos HIGH.\r\n\r\nDesea cancelar la segmentacion de los elementos HIGH?", "Advertencia: Segmentacion en proceso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // se ha solicitado cambiar de elementos a segmentar
                        this.padre.segmentacionForm.Close();

                        this.padre.segmentacionForm = new SegmentacionForm();
                        this.padre.segmentacionForm.Text = "SEGMENTACION LOW";
                        this.padre.segmentacionForm.lblTitulo.Text = "SEGMENTACION LOW";

                        this.padre.segmentacionForm.MdiParent = this.MdiParent;
                        this.padre.segmentacionForm.padre = this.padre;

                        this.padre.abiertoSegmentacionForm = true;
                        this.padre.segmentacionForm.Show();
                    }
                    else
                    {
                        // se ha solicitado continuar con la segmentacion de los elementos HIGH
                        this.padre.segmentacionForm.Select();
                    }
                }
            }
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
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid); 
        }            
    }
}
