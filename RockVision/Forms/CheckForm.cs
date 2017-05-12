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
using AForge;
using AForge.Math;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using AForge.Math.Geometry;

namespace RockVision
{
    public partial class CheckForm : Form
    {
        #region variables de diseñador

        System.Drawing.Point lastClick;

        /// <summary>
        /// guarda el centro X del circulo de segmentacions
        /// </summary>
        public int centroX = 0;

        /// <summary>
        /// guarda el centro Y del circulo de segmentacion
        /// </summary>
        public int centroY = 0;

        /// <summary>
        /// referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// ruta del datacubo de referencia saturado de crudo
        /// </summary>
        public string rutaCTRo="";

        /// <summary>
        /// ruta del datacubo de referencia saturado de agua
        /// </summary>
        public string rutaCTRw = "";

        /// <summary>
        /// rutas de los datacubos temporales
        /// </summary>
        public List<string> rutaCTtemp = null;

        /// <summary>
        /// datacubo temporal
        /// </summary>
        public RockStatic.MyDataCube tempDicom = null;

        /// <summary>
        /// valor CT del crudo
        /// </summary>
        public double valorCTo = 0;

        /// <summary>
        /// valor CT del agua
        /// </summary>
        public double valorCTw = 0;

        /// <summary>
        /// lista que guarda los nombres de los DICOM en la ruta de datacubo seleccionada
        /// </summary>
        string[] elementos = null;

        bool checkSegmentacion = false;

        #endregion

        public CheckForm()
        {
            InitializeComponent();
        }

        private void CheckForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.padre.CerrarCheckForm();
        }

        private void lblTitulo_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();
        }

        /// <summary>
        /// Centra el Form en medio del MDI parent
        /// </summary>
        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // se cancelo la creacion de un nuevo proyecto
            this.Close();

            // se abre de nuevo la ventana de HomeForm
            padre.AbrirHomeForm();
        }

        private void btnSubir_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnSubir_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            // se llena el combobox con los datacubos que se seleccionaron en la ventana NewProjectDForm
            cmbDatacubo.Items.Add("Referencia So=1");
            cmbDatacubo.Items.Add("Referencia Sw=1");
            for (int i = 0; i < this.rutaCTtemp.Count; i++) cmbDatacubo.Items.Add("Instante " + (i + 1).ToString());

            // se obtienen el numero de DICOMS en cada carpeta
            int[] elementosPorCarpeta = new int[this.rutaCTtemp.Count + 2];
            elementosPorCarpeta[0] = Directory.GetFiles(this.rutaCTRo, "*.dcm").Length;
            elementosPorCarpeta[1] = Directory.GetFiles(this.rutaCTRw, "*.dcm").Length;
            for (int i = 0; i < this.rutaCTtemp.Count; i++) elementosPorCarpeta[i + 2] = Directory.GetFiles(this.rutaCTtemp[i], "*.dcm").Length;

            numIni.Maximum = numFin.Maximum = elementosPorCarpeta.Min();

            numIni.Value = 1;
            numFin.Value = numFin.Maximum;

            if (elementosPorCarpeta.Min() != elementosPorCarpeta.Max()) MessageBox.Show("Las carpetas seleccionadas no tienen el mismo numero de elementos DICOM.\r\n\r\nSolo se tomaran los primeros " + numFin.Maximum.ToString() + " elementos DICOM.","Cantidad diferente de elementos DICOM",MessageBoxButtons.OK,MessageBoxIcon.Information);

            cmbDatacubo.SelectedIndex = 0;
        }

        private void cmbDatacubo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // dependiendo del datacubo seleccionado se leen y se cargan los DICOM y se muestran en la ventana
            elementos = null;

            switch(cmbDatacubo.SelectedIndex)
            {
                case 0:
                    elementos = Directory.GetFiles(this.rutaCTRo, "*.dcm");
                    break;
                case 1:
                    elementos = Directory.GetFiles(this.rutaCTRw, "*.dcm");
                    break;
                default:
                    elementos = Directory.GetFiles(this.rutaCTtemp[cmbDatacubo.SelectedIndex - 2], "*.dcm");                    
                    break;
            }

            // se muestra la ventana de espera
            padre.ShowWaiting("Espere mientras RockVision carga los elementos seleccionados...");

            // se limpia la imagen del pictbox
            pictElemento.Image = null;

            // se crean las imagenes del datacubo
            tempDicom = new RockStatic.MyDataCube(elementos);
            tempDicom.CrearBitmapThread();
            
            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = tempDicom.dataCube.Count;
            trackElementos.Value = 1;
            
            // se pinta la primera imagen
            pictElemento.Image = tempDicom.dataCube[0].bmp;

            // se genera el texto del counter
            txtCounter.Text = "1 de " + elementos.Length.ToString();

            if (!checkSegmentacion)
            {
                // tamaño por defecto del circulo de segmentacion
                numRadio.Value = Convert.ToInt32((this.pictElemento.Width / 2)) - 10;

                // centro del circulo de segmentacion
                centroX = pictElemento.Width / 2;
                centroY = pictElemento.Height / 2;

                checkSegmentacion = true;
            }

            // se pinta el circulo de segmentacion
            pictElemento.Invalidate();

            // se cierra la ventana de espera
            padre.CloseWaiting();
        }

        private void pictElemento_Paint(object sender, PaintEventArgs e)
        {
            // se dibuja el circulo de la segmentacion

            Pen brocha2 = new Pen(Color.FromArgb(128, 0, 255, 0));
            brocha2.Width = 2;

            int r = Convert.ToInt32(numRadio.Value);

            e.Graphics.DrawEllipse(brocha2, centroX - r, centroY - r, 2 * r, 2 * r);
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            pictElemento.Image = tempDicom.dataCube[trackElementos.Value - 1].bmp;
            txtCounter.Text = trackElementos.Value.ToString() + " de " + elementos.Length.ToString();
        }

        private void trackBrillo_Scroll(object sender, EventArgs e)
        {
            Filtrar();
            pictElemento.Invalidate();
        }

        private void trackContraste_Scroll(object sender, EventArgs e)
        {
            Filtrar();
            pictElemento.Invalidate();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            trackBrillo.Value = 0;
            trackContraste.Value = 0;

            Filtrar();
            pictElemento.Invalidate();
        }

        /// <summary>
        /// Modifica el brillo y contraste de la imagen que aparece en pantalla
        /// </summary>
        private void Filtrar()
        {
            Bitmap temp = new Bitmap(tempDicom.dataCube[trackElementos.Value - 1].bmp);

            AForge.Imaging.Filters.BrightnessCorrection brillo = new AForge.Imaging.Filters.BrightnessCorrection(Convert.ToInt32(trackBrillo.Value));
            AForge.Imaging.Filters.ContrastCorrection contraste = new AForge.Imaging.Filters.ContrastCorrection(Convert.ToInt32(trackContraste.Value));

            contraste.ApplyInPlace(temp);
            brillo.ApplyInPlace(temp);

            pictElemento.Image = temp;

            //pictElemento.Invalidate();
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // se escoge el lugar donde se va a guardar el proyecto
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Proyecto RockVision (*.rvd)|*.rvd";
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

                string[] rutaTemp2 = new string[rutaCTtemp.Count];
                for (int i = 0; i<rutaCTtemp.Count; i++) rutaTemp2[i] = rutaCTtemp[i];

                padre.actualD = new CProyectoD(saveFile.FileName, centroX, centroY, radio, this.rutaCTRo, this.rutaCTRw, rutaTemp2, this.valorCTo, this.valorCTw, Convert.ToInt32(numIni.Value - 1), Convert.ToInt32(numFin.Value - 1));

                // se cierra la ventana de espera
                padre.CloseWaiting();

                // se cierra la ventana
                this.Close();

                // se abre la ventana del proyecto de caracterizacion dinamica
                padre.AbrirProyectoDForm();
            }
        }
    }
}
