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
    /// Forms para visualizar las imagenes seleccionadas en la creacion de un nuevo proyecto
    /// </summary>
    public partial class CheckForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Referencia que indica si se han cargado los archivos HIGH (true) o los archivos LOW (false)
        /// </summary>
        public bool filesHigh;

        /// <summary>
        /// Referencia al form NewProjectForm que invoca
        /// </summary>
        public NewProjectForm newProjectForm;

        /// <summary>
        /// Lista de rutas de los DICOMs a cargar y revisar temporalmente
        /// </summary>
        public List<string> temp;

        /// <summary>
        /// Datacubo temporal para revisar los DICOMs cargados
        /// </summary>
        public MyDataCube tempDicom;

        #endregion

        Point lastClick;

        /// <summary>
        /// Form para revisar los DICOMs que se han seleccionado, de manera temporal, en NewProjectForm
        /// </summary>
        public CheckForm()
        {
            InitializeComponent();
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        /// <summary>
        /// Se presiona el boton Cancelar y se cancelan todos los cambios realizados sobre la lista de dicoms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se llena el ListBox, se reestablece el TrackBar
        /// </summary>
        public void SetForm()
        {
            // se llena el listbox
            lstElementos.Items.Clear();
            for (int i = 0; i < temp.Count; i++) lstElementos.Items.Add(GetNameFile((string)temp[i]));

            // se crean las imagenes del datacubo
            tempDicom = new MyDataCube(temp);
            tempDicom.CrearBitmapThread();

            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = newProjectForm.tempHigh.Count;
            trackElementos.Value = 1;
            lstElementos.SelectedIndex = 0;

            // se pinta la primera imagen
            pictElemento.Image = tempDicom.dataCube[0].bmp;

            // se genera el texto del counter
            txtCounter.Text = "1 de " + temp.Count.ToString();
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            if (filesHigh)
            {
                pictElemento.Image = tempDicom.dataCube[trackElementos.Value - 1].bmp;
                lstElementos.ClearSelected();
                lstElementos.SelectedIndex = trackElementos.Value - 1;
                txtCounter.Text = trackElementos.Value.ToString() + " de " + temp.Count.ToString();
            }
        }

        /// <summary>
        /// Se pasa una ruta completa y se extrae el nombre del archivo
        /// </summary>
        /// <param name="file">Ruta completa del archivo</param>
        /// <returns>Nombre del archivo, con su extension</returns>
        public static string GetNameFile(string file)
        {
            string name = "";
            bool sw = true;
            int i = file.Length;

            while (sw)
            {
                i--;
                if (file[i] == '\\') sw = false;
                else name = file[i] + name;
            }
            
            return name;
        }

        /// <summary>
        /// Se crea una copia (local) de los elementos a mostrar
        /// </summary>
        public void SetList(List<string> lista)
        {
            temp = new List<string>();
            for (int i = 0; i < lista.Count; i++) temp.Add((string)lista[i]);
        }

        private void lstElementos_DoubleClick(object sender, EventArgs e)
        {
            trackElementos.Value = lstElementos.SelectedIndex + 1;            
        }

        private void CheckForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarCheckForm();
        }

        private void lblTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void lblTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        /// <summary>
        /// Centra el Form en medio del MDI parent
        /// </summary>
        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void CheckForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid); 
        }

        public void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
