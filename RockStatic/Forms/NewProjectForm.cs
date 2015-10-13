using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RockStatic
{
    public partial class NewProjectForm : Form
    {
        /* Un nuevo proyecto requiere de seleccionar la ruta de las imagenes (o dycom) para HIGH energy y LOW energy
         * Estas rutas se deben guardar dentro de una instancia del tipo Proyecto
         */

        #region variables de clase

        /// <summary>
        /// Instancia del MainForm
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Lista temporal donde se guardan los nombres de las imagenes HIGH seleccionadas
        /// </summary>
        public List<string> tempHigh;

        /// <summary>
        /// Lista temporal donde se guardan los nombres de las imagenes LOW seleccionadas
        /// </summary>
        public List<string> tempLow;

        #endregion

        Point lastClick;

        public NewProjectForm()
        {
            InitializeComponent();
        }

        private void NewProjectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarNuevoProyectoForm();
        }

        /// <summary>
        /// Boton Cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.padre.AbrirHome();
        }

        private void NewProjectForm_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            tempHigh = new List<string>();
            tempLow = new List<string>();
        }

        /// <summary>
        /// Se revisa que, de existir elementos, haya un numero igual de elementos HIGH y LOW
        /// </summary>
        /// <returns></returns>
        public bool CheckLargos()
        {
            bool check = true;

            try
            {
                if (tempHigh.Count== tempLow.Count)
                {
                    // largos iguales
                    lblError.Visible = false;
                    btnCrear.Enabled = true;
                }
                else
                {
                    // largos diferentes
                    lblError.Visible = true;
                    btnCrear.Enabled = false;
                }
            }
            catch
            {
                // uno de los dos vectores no ha sido definido aun
                lblError.Visible = false;
                btnCrear.Enabled = false;
            }

            return check;
        }

        /// <summary>
        /// Se seleccionan los elementos HIGH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelHigh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openHigh = new OpenFileDialog();
            openHigh.Multiselect = true;

            tempHigh = new List<string>();

            if (openHigh.ShowDialog() == DialogResult.OK)
            {
                // se muestra la ventana de espera
                padre.ShowWaiting("Espere mientras RockStatic carga los elementos seleccionados...");

                for (int i = 0; i < openHigh.FileNames.Length; i++) tempHigh.Add(openHigh.FileNames[i]);
                pictHigh.Image = Properties.Resources.greenTick;
                btnCheckHigh.Enabled = true;
                CheckLargos();

                // se cierra la ventana NewProyectForm
                padre.CloseWaiting();
            }
        }

        /// <summary>
        /// Se seleccionan los elementos LOW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelLow_Click(object sender, EventArgs e)
        {
            OpenFileDialog openLow = new OpenFileDialog();
            openLow.Multiselect = true;

            tempLow = new List<string>();

            if (openLow.ShowDialog() == DialogResult.OK)
            {
                // se muestra la ventana de espera
                padre.ShowWaiting("Espere mientras RockStatic carga los elementos seleccionados...");

                for (int i = 0; i < openLow.FileNames.Length; i++) tempLow.Add(openLow.FileNames[i]);
                pictLow.Image = Properties.Resources.greenTick;
                btnCheckLow.Enabled = true;
                CheckLargos();

                // se cierra la ventana NewProyectForm
                padre.CloseWaiting();
            }
        }

        private void btnCheckHigh_Click(object sender, EventArgs e)
        {
            if (padre.abiertoCheckForm)
            {
                // la ventana ya esta abierta, se debe preguntar que hacer con los cambios realizados en la instancia que se encuentra abierta
                if (MessageBox.Show("Ya esta revisando un set de elementos.\nDesea guardar el set actual antes de continuar?", "Set en revision", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    padre.checkForm.btnGuardar_Click(sender, e);
                }
                else padre.checkForm.btnCancelar_Click(sender, e);

                // los botones btnCancelar y btnGuardar envian a cerrar el CheckForm, por lo que de igual manera se debe volver a crear la instancia
            }

            // se abre la ventana CheckForm y se pasa el List de elementos HIGH para su revision
            padre.checkForm = new CheckForm();
            padre.checkForm.Text = "REVISAR ELEMENTOS HIGH";
            padre.checkForm.lblTitulo.Text = "REVISAR ELEMENTOS HIGH";
            padre.checkForm.SetList(tempHigh);
            padre.checkForm.MdiParent = this.MdiParent;
            padre.checkForm.padre = this.padre;
            padre.checkForm.filesHigh = true;
            padre.checkForm.newProjectForm = this;
            padre.abiertoCheckForm = true;
            padre.checkForm.Show();
        }

        private void btnCheckLow_Click(object sender, EventArgs e)
        {
            if (padre.abiertoCheckForm)
            {
                // la ventana ya esta abierta, se debe preguntar que hacer con los cambios realizados en la instancia que se encuentra abierta
                if (MessageBox.Show("Ya esta revisando un set de elementos.\nDesea guardar el set actual antes de continuar?", "Set en revision", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    padre.checkForm.btnGuardar_Click(sender, e);
                }
                else padre.checkForm.btnCancelar_Click(sender, e);

                // los botones btnCancelar y btnGuardar envian a cerrar el CheckForm, por lo que de igual manera se debe volver a crear la instancia
            }

            // se abre la ventana CheckForm y se pasa el List de elementos LOW para su revision
            padre.checkForm = new CheckForm();
            padre.checkForm.Text = "REVISAR ELEMENTOS LOW";
            padre.checkForm.lblTitulo.Text = "REVISAR ELEMENTOS LOW";
            padre.checkForm.SetList(tempLow);
            padre.abiertoCheckForm = true;
            padre.checkForm.MdiParent = this.MdiParent;
            padre.checkForm.padre = this.padre;
            padre.checkForm.filesHigh = false;
            padre.checkForm.newProjectForm = this;
            padre.checkForm.Show();
        }

        /// <summary>
        /// Se guarda la List de elementos modificados en el CheckForm en la instancia de CProyecto temp de NewProjectForm
        /// </summary>
        /// <param name="lista">List de string a guardar en la instancia de CProyecto</param>
        /// <param name="filesHigh">true: se guarda en HIGH; false: se guarda en LOW</param>
        public void SetTemp(List<string> lista, bool filesHigh)
        {
            if (filesHigh) tempHigh=lista;
            else tempLow=lista;
        }

        /// <summary>
        /// Se crea el nuevo proyecto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCrear_Click(object sender, EventArgs e)
        {
            // se crea un nuevo proyecto que contiene toda la informacion recogida en esta ventana
            // y se prepara para guardar en disco
            CProyecto temp = new CProyecto();

            // se escoge el lugar donde se va a guardar el proyecto
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = txtNewName.Text + ".rsp";
            saveFile.Filter = "Proyecto RockStatic (*.rsp)|*.rsp";
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                // se muestra la ventana de espera
                padre.ShowWaiting("Espere mientras RockStatic crea el nuevo proyecto...");

                // se crea la carpeta
                DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(saveFile.FileName));
                
                // se nombra el proyecto
                temp.name = (Path.GetFileNameWithoutExtension(saveFile.FileName));

                // nombre y ruta de la carpeta del proyecto
                string folderPath = di.ToString() + "\\" + temp.name;
                temp.SetFolderPath(folderPath);
                Directory.CreateDirectory(folderPath);

                // se crea la carpeta de imagenes HIGH
                string folderHigh = folderPath + "\\high";
                Directory.CreateDirectory(folderHigh);

                // se crea la carpeta de imagenes LOW
                string folderLow = folderPath + "\\low";
                Directory.CreateDirectory(folderLow);

                // se cargan al proyecto los byte[] de los elementos HIGH
                temp.SetHigh(tempHigh);

                // se cargan al proyecto los byte[] de los elementos LOW
                temp.SetLow(tempLow);

                // se crea el proyecto
                temp.Crear();

                // se cierra el form de espera
                padre.CloseWaiting();

                // se cierra la ventana NewProyectForm
                this.Close();

                // se abre el nuevo proyecto
                padre.AbrirProyecto(folderPath + "\\" + Path.GetFileName(saveFile.FileName));
            }            
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void lblTitle_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnSelHigh_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSelHigh_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void NewProjectForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid); 
        }      
    }
}
