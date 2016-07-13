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
    /// <summary>
    /// Form para seleccionar los DICOMs High y Low que componen un proyecto
    /// </summary>
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

        Point lastClick;

        /// <summary>
        /// Instancia temporal del phantom1 en High
        /// </summary>
        public CPhantom tempPhantom1High;

        /// <summary>
        /// Instancia temporal del phantom2 en High
        /// </summary>
        public CPhantom tempPhantom2High;

        /// <summary>
        /// Instancia temporal del phantom3 en High
        /// </summary>
        public CPhantom tempPhantom3High;

        /// <summary>
        /// Instancia temporal del phantom1 en Low
        /// </summary>
        public CPhantom tempPhantom1Low;

        /// <summary>
        /// Instancia temporal del phantom2 en Low
        /// </summary>
        public CPhantom tempPhantom2Low;

        /// <summary>
        /// Instancia temporal del phantom3 en Low
        /// </summary>
        public CPhantom tempPhantom3Low;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public NewProjectForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento capturado al cerrar el Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            
            // se crean los valores de los phantoms por defecto
            tempPhantom1High = new CPhantom(1237.865,46.125,2.2, 11.8);
            tempPhantom2High = new CPhantom(868.77,39,2.16, 8.7);
            tempPhantom3High = new CPhantom(15.3275,36.7,1, 7.5);
            tempPhantom1Low = new CPhantom(1434.195,50.985,2.2, 11.8);
            tempPhantom2Low = new CPhantom(916.1,36.775,2.16, 8.7);
            tempPhantom3Low = new CPhantom(23.8,35.9,1, 7.5);
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

                for (int i = 0; i < openHigh.FileNames.Length; i++)
                    tempHigh.Add(openHigh.FileNames[i]);

                // se cargan los DICOMS de manera temporal
                
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

                for (int i = 0; i < openLow.FileNames.Length; i++) 
                    tempLow.Add(openLow.FileNames[i]);
                                
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
                padre.checkForm.btnCerrar_Click(sender, e);
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

            padre.ShowWaiting("Espere mientras RockStatic genera las imagenes...");
            padre.checkForm.Show();
            padre.CloseWaiting();
        }

        private void btnCheckLow_Click(object sender, EventArgs e)
        {
            if (padre.abiertoCheckForm)
            {
                padre.checkForm.btnCerrar_Click(sender, e);
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

            padre.ShowWaiting("Espere mientras RockStatic genera las imagenes...");
            padre.checkForm.Show();
            padre.CloseWaiting();
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

        /// <summary>
        /// Se centra el Form con respecto al MDI parent
        /// </summary>
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

        private void radPhantoms_CheckedChanged(object sender, EventArgs e)
        {
            radNoPhantoms.Checked = !radPhantoms.Checked;
            btnPhantoms.Enabled = radNoPhantoms.Checked;
            btnPhantoms2.Enabled = radPhantoms.Checked;
        }

        private void btnPhantoms_Click(object sender, EventArgs e)
        {
            if (padre.abiertoPhantomsForm)
            {
                padre.phantomForm.btnCerrar_Click(sender, e);
            }

            // se abre la ventana CheckForm y se pasa el List de elementos HIGH para su revision
            padre.phantomForm = new PhantomsForm();
            padre.phantomForm.Text = "MODELO DE PHANTOMS";
            padre.phantomForm.lblTitulo.Text = "MODELO DE PHANTOMS";
            padre.phantomForm.MdiParent = this.MdiParent;
            padre.phantomForm.padre = this.padre;
            padre.phantomForm.newProjectForm = this;
            padre.abiertoPhantomsForm = true;

            padre.phantomForm.Show();            
        }

        private void btnPhantoms2_Click(object sender, EventArgs e)
        {
            if (padre.abiertoPhantoms2Form)
            {
                padre.phantoms2Form.btnCerrar_Click(sender, e);
            }

            // se abre la ventana CheckForm y se pasa el List de elementos HIGH para su revision
            padre.phantoms2Form = new Phantoms2Form();
            padre.phantoms2Form.Text = "MODELO DE PHANTOMS";
            padre.phantoms2Form.lblTitulo.Text = "MODELO DE PHANTOMS";
            padre.phantoms2Form.MdiParent = this.MdiParent;
            padre.phantoms2Form.padre = this.padre;
            padre.phantoms2Form.newProjectForm = this;
            padre.abiertoPhantoms2Form = true;

            padre.phantoms2Form.Show();          
        }

        private void NewProjectForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2),this.DisplayRectangle);                           
        }      
    }
}
