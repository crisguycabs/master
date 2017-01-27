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

namespace RockVision
{
    public partial class NewProjectVForm : Form
    {
        #region variales de diseñador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        Point lastClick;

        /// <summary>
        /// lista de los nombres de los dicom que se van a mostrar en pantalla
        /// </summary>
        public List<string> elementos=null;

        /// <summary>
        /// datacubo temporal
        /// </summary>
        public RockStatic.MyDataCube tempDicom = null;

        #endregion

        public NewProjectVForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Se establece el contenido de la forma
        /// </summary>
        public void SetForm()
        {
            // se muestra la ventana de espera
            padre.ShowWaiting("Espere mientras RockVision carga los elementos seleccionados...");

            // se limpa el combobox y se llena el combobox
            lstElementos.Items.Clear();
            for (int i = 0; i < elementos.Count; i++) lstElementos.Items.Add((string)elementos[i]);
            
            // se limpia la imagen del pictbox
            pictElemento.Image = null;

            // se crean las imagenes del datacubo
            tempDicom = new RockStatic.MyDataCube(elementos);
            tempDicom.CrearBitmapThread();

            lstElementos.SelectedIndex = 0;

            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = tempDicom.dataCube.Count;
            trackElementos.Value = 1;
            lstElementos.SelectedIndex = 0;

            // se pinta la primera imagen
            pictElemento.Image = tempDicom.dataCube[0].bmp;

            // se genera el texto del counter
            txtCounter.Text = "1 de " + elementos.Count.ToString();

            // se cierra la ventana de espera
            padre.CloseWaiting();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // se toma el indice seleccionado y se borra de la lista temporal, asi como de los dicom
            int indice = lstElementos.SelectedIndex;
            elementos.RemoveAt(indice);
            tempDicom.dataCube.RemoveAt(indice);

            // se vuelve a pintar el ListBox
            lstElementos.Items.Clear();
            for (int i = 0; i < elementos.Count; i++) lstElementos.Items.Add((string)elementos[i]);

            trackElementos.Maximum = elementos.Count;

            if (indice >= elementos.Count)
                indice--;

            trackElementos.Value = indice + 1;
            lstElementos.SelectedIndex = indice;

            txtCounter.Text = (indice + 1).ToString() + " de " + elementos.Count.ToString();

            this.trackElementos_ValueChanged(sender, e);
        }

        private void NewProjectVForm_Load(object sender, EventArgs e)
        {
            SetForm();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // se cancelo la creacion de un nuevo proyecto
            this.Close();
            
            // se abre de nuevo la ventana de HomeForm
            padre.AbrirHomeForm();
        }

        private void NewProjectVForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarNewProjectVForm();
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

        private void NewProjectVForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.RoyalBlue, 2), this.DisplayRectangle); 
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            pictElemento.Image = tempDicom.dataCube[trackElementos.Value - 1].bmp;
            lstElementos.ClearSelected();
            lstElementos.SelectedIndex = trackElementos.Value - 1;
            txtCounter.Text = trackElementos.Value.ToString() + " de " + elementos.Count.ToString();
        }

        private void lstElementos_DoubleClick(object sender, EventArgs e)
        {
            trackElementos.Value = lstElementos.SelectedIndex + 1;
            lstElementos.SelectedIndex = trackElementos.Value - 1;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // se escoge el lugar donde se va a guardar el proyecto
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Proyecto RockVision (*.rvv)|*.rvv";
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                // se verifica que no exista una carpeta con el mismo nombre
                string previous = System.IO.Path.GetDirectoryName(saveFile.FileName) + "\\" + Path.GetFileNameWithoutExtension(saveFile.FileName);
                if (Directory.Exists(previous))
                {
                    MessageBox.Show("Ya existe un proyecto con el nombre " + Path.GetFileNameWithoutExtension(saveFile.FileName) + ". Cambie el nombre del proyecto o escoga otra ruta donde guardarlo.", "Error de duplicado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // se muestra la ventana de espera
                padre.ShowWaiting("Espere mientras RockVision crea el nuevo proyecto...");

                padre.actualV = new CProyectoV(saveFile.FileName, elementos);                

                // se cierra la ventana de espera
                padre.CloseWaiting();

                // se cierra la ventana
                this.Close();

                // se abre la ventana de visualizacion
                this.padre.AbrirVisualForm();
            }
        }
    }
}
