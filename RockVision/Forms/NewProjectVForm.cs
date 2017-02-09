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

        /// <summary>
        /// guarda el centro X del circulo de segmentacion
        /// </summary>
        public int centroX = 0;

        /// <summary>
        /// guarda el centro Y del circulo de segmentacion
        /// </summary>
        public int centroY = 0;

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

            // tamaño por defecto del circulo de segmentacion
            numRadio.Value = Convert.ToInt32((this.pictElemento.Width / 2)) - 10;

            // centro del circulo de segmentacion
            centroX = pictElemento.Width / 2;
            centroY = pictElemento.Height / 2;

            // se pinta el circulo de segmentacion
            pictElemento.Invalidate();

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

                // se convierte el centro y radio del circulo de segmentacion a coordenadas de la imagen

                double imgWidth = pictElemento.Image.Width;
                double imgHeight = pictElemento.Image.Height;
                double boxWidth = pictElemento.Size.Width;
                double boxHeight = pictElemento.Size.Height;

                double scale;
                double ycero = 0;
                double xcero = 0;

                if (imgWidth / imgHeight > boxWidth / boxHeight)
                {
                    //If true, that means that the image is stretched through the width of the control.
                    //'In other words: the image is limited by the width.

                    //The scale of the image in the Picture Box.
                    scale = boxWidth / imgWidth;

                    //Since the image is in the middle, this code is used to determinate the empty space in the height
                    //'by getting the difference between the box height and the image actual displayed height and dividing it by 2.
                    ycero = (boxHeight - scale * imgHeight) / 2;
                }
                else
                {
                    //If false, that means that the image is stretched through the height of the control.
                    //'In other words: the image is limited by the height.

                    //The scale of the image in the Picture Box.
                    scale = boxHeight / imgHeight;

                    //Since the image is in the middle, this code is used to determinate the empty space in the width
                    //'by getting the difference between the box width and the image actual displayed width and dividing it by 2.
                    xcero = (boxWidth - scale * imgWidth) / 2;
                }

                centroX = Convert.ToInt32((Convert.ToDouble(centroX) - xcero) / scale);
                centroY = Convert.ToInt32((Convert.ToDouble(centroY) - ycero) / scale);
                int radio = Convert.ToInt32(Convert.ToDouble(numRadio.Value) / scale);

                padre.actualV = new CProyectoV(saveFile.FileName, centroX, centroY, radio, elementos);                

                // se cierra la ventana de espera
                padre.CloseWaiting();

                // se cierra la ventana
                this.Close();

                // se abre la ventana de visualizacion
                this.padre.AbrirVisualForm();
            }
        }

        private void pictElemento_Paint(object sender, PaintEventArgs e)
        {
            // se dibuja el circulo de la segmentacion
            
            Pen brocha2 = new Pen(Color.FromArgb(128, 0, 255, 0));
            brocha2.Width = 2;

            int r=Convert.ToInt32(numRadio.Value);

            e.Graphics.DrawEllipse(brocha2, centroX - r, centroY - r, 2 * r, 2 * r);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            centroY--;
            pictElemento.Invalidate();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            centroY++;
            pictElemento.Invalidate();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            centroX--;
            pictElemento.Invalidate();
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            centroX++;
            pictElemento.Invalidate();
        }

        private void numRadio_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Invalidate();
        }
    }
}
