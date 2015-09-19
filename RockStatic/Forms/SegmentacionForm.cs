using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;
using AForge.Math;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Imaging.Textures;
using AForge.Math.Geometry;

namespace RockStatic
{
    public partial class SegmentacionForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Contador para clicks
        /// </summary>
        int countClick;

        /// <summary>
        /// Array que guarda las coordenadas de los clicks necesarios para dibujar un circulo
        /// </summary>
        CCirculo[] tempClicks;

        /// <summary>
        /// Instancia que hace referencia a la imagen cargada en pantalla para modificarla
        /// </summary>
        Graphics g;

        /// <summary>
        /// Lapiz con el que se dibujan los circulos
        /// </summary>
        Pen pen1;

        /// <summary>
        /// Lapiz para dibujar el circulo seleccionado
        /// </summary>
        Pen pen2;

        /// <summary>
        /// variable de control para evitar que se lance el evento Paint de pictCore
        /// </summary>
        bool controlPaint;

        /// <summary>
        /// Lista dinamica de elementos a dibujar en pantalla
        /// </summary>
        List<CCuadrado> elementosScreen;

        /// <summary>
        /// contador de areas creadas
        /// </summary>
        int countAreas;

        #endregion

        System.Drawing.Point lastClick;

        public SegmentacionForm()
        {
            InitializeComponent();
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            SetForm();

            ResetCountClick();

            // instancia de la imagen
            g = pictElemento.CreateGraphics();

            // se prepara el lapiz con el que se va a pintar
            pen1 = new System.Drawing.Pen(Color.Red, 2F);
            float[] dashValues = { 10, 3, 5, 3 };
            pen1.DashPattern = dashValues;
            pen2 = new System.Drawing.Pen(Color.LawnGreen, 2F);
            pen2.DashPattern = dashValues;

            // se prepara la lista dinamica
            elementosScreen = new List<CCuadrado>();

            controlPaint = false;

            countAreas = 0;

            // se cargan los elementos de areas de Core y Phantom si existen
            if (this.lblTitulo.Text.Contains("HIGH"))
            {
                // se cargan los elementos HIGH si existen
                if (padre.actual.GetAreasHigh())
                {
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetCoreHigh(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom1High(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom2High(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom3High(), pictElemento.Image.Height, pictElemento.Height));

                    LlenarList();
                    controlPaint = true;
                    pictElemento.Invalidate();
                }
            }
            else
            {
                // se cargan los elementos LOW si existen
                if (padre.actual.GetAreasLow())
                {
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetCoreLow(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom1Low(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom2Low(), pictElemento.Image.Height, pictElemento.Height));
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(padre.actual.GetPhantom3Low(), pictElemento.Image.Height, pictElemento.Height));

                    LlenarList();
                    controlPaint = true;
                    pictElemento.Invalidate();
                }
            }
        }

        public void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se llena el ListBox, se reestablece el TrackBar
        /// </summary>
        public void SetForm()
        {
            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = padre.actual.GetHigh().Count;
            trackElementos.Value = 1;
            
            // se pinta la primera imagen
            pictElemento.Image = MainForm.Byte2image(padre.actual.GetHigh()[0]);

            CentrarForm();
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            pictElemento.Image = MainForm.Byte2image(padre.actual.GetHigh()[trackElementos.Value - 1]);
            controlPaint = true;
            pictElemento.Invalidate();     
        }

        /// <summary>
        /// Se pasa una ruta completa y se extrae el nombre del archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
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

        private void SegmentacionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarSegmentacionForm();
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

        public void CentrarForm()
        {
            // dado que ahora el area de trabajo es TODO el MdiParent, entonces solo se debe tomar el area del MdiParent para calcular la posicion inicial del MdiChild
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 1 / 2) - 38);
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

        /// <summary>
        /// Se resetea el conteo de clicks
        /// </summary>
        public void ResetCountClick()
        {
            countClick = 0;
            tempClicks = new CCirculo[3];

            lblPunto1.Visible = false;
            lblPunto2.Visible = false;
            btnCancel.Enabled = false;            
        }

        private void radAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (radAuto.Checked) radAuto.ForeColor = Color.White;
            else radAuto.ForeColor = Color.Green;

            grpAuto.Enabled = radAuto.Checked;
            ResetCountClick();
        }

        private void radManual_CheckedChanged(object sender, EventArgs e)
        {
            if (radManual.Checked) radManual.ForeColor = Color.White;
            else radManual.ForeColor = Color.Green;

            grpManual.Enabled = radManual.Checked;
            ResetCountClick();
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {
            CentrarForm();
        }

        /// <summary>
        /// Formatea una imagen para poder ser usada por la libreria AForge
        /// </summary>
        /// <param name="original">Imagen original</param>
        /// <returns>Imagen formateada para AForge</returns>
        public System.Drawing.Bitmap FormatToAForge(System.Drawing.Bitmap original)
        {
            original = AForge.Imaging.Image.Clone(original);

            // check pixel format
            if ((original.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale) ||
                 (Bitmap.GetPixelFormatSize(original.PixelFormat) > 32))
            {
                MessageBox.Show("The demo application supports only color images.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // free image
                original.Dispose();
                original = null;
            }
            else
            {
                // make sure the image has 24 bpp format
                if (original.PixelFormat != System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    Bitmap temp = AForge.Imaging.Image.Clone(original, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                    original.Dispose();
                    original = temp;
                }
            }

            return original;
        }

        private void pictElemento_MouseEnter(object sender, EventArgs e)
        {
            // se cambia el cursor si se ha definido el MODO MANUAL
            if (radManual.Checked) this.Cursor = Cursors.Cross;
        }

        private void pictElemento_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCountClick();
        }

        private void pictElemento_MouseClick(object sender, MouseEventArgs e)
        {
            if (radManual.Checked)
            {
                // solo se cuentan los clicks en modo manual
                // se asegura que el PictureBox obtenga el foco
                pictElemento.Focus();

                // se guarda la coordenada del click, y se aumenta el contador
                tempClicks[countClick] = new CCirculo(e.X, e.Y, 0);
                countClick++;

                switch (countClick)
                {
                    case 1:
                        lblPunto1.Visible = true;
                        btnCancel.Enabled = true;
                        break;
                    case 2:
                        lblPunto2.Visible = true;
                        break;
                    case 3:
                        // con tres clicks se dibuja el circulo
                        AddElemento();
                        ResetCountClick();
                        break;
                }
            }
        }

        /// <summary>
        /// Se calcula el nuevo elemento usando los 3-clicks realizados sobre la imagen, y se agrega al List
        /// </summary>
        public void AddElemento()
        {
            // tomado del script calcCircle.m

            CCuadrado punto = new CCuadrado();

            double epsilon = 0.000000001;

            bool ax_is_0 = (Math.Abs(tempClicks[1].x - tempClicks[0].x) <= epsilon);
            bool bx_is_0 = (Math.Abs(tempClicks[2].x - tempClicks[1].x) <= epsilon);

            // check whether both lines are vertical - collinear
            if (ax_is_0 && bx_is_0)
            {
                MessageBox.Show("Los puntos ingresados pertenecen a una misma linea recta", "Error al dibujar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int delta_a_x = tempClicks[1].x - tempClicks[0].x;
            int delta_a_y = tempClicks[1].y - tempClicks[0].y;
            int delta_b_x = tempClicks[2].x - tempClicks[1].x;
            int delta_b_y = tempClicks[2].y - tempClicks[1].y;

            // make sure delta gradients are not vertical
            // swap points to change deltas
            if (ax_is_0)
            {
                int temp;
                temp = tempClicks[1].x;
                tempClicks[1].x = tempClicks[2].x;
                tempClicks[2].x = temp;
                temp = tempClicks[1].y;
                tempClicks[1].y = tempClicks[2].y;
                tempClicks[2].y = temp;
                delta_a_x = tempClicks[1].x - tempClicks[0].x;
                delta_a_y = tempClicks[1].y - tempClicks[0].y;
            }

            if (bx_is_0)
            {
                int temp;
                temp = tempClicks[0].x;
                tempClicks[0].x = tempClicks[1].x;
                tempClicks[1].x = temp;
                temp = tempClicks[0].y;
                tempClicks[0].y = tempClicks[1].y;
                tempClicks[1].y = temp;
                delta_b_x = tempClicks[2].x - tempClicks[1].x;
                delta_b_y = tempClicks[2].y - tempClicks[1].y;
            }

            double grad_a = (double)delta_a_y / (double)delta_a_x;
            double grad_b = (double)delta_b_y / (double)delta_b_x;

            // check whether the given points are collinear
            if (Math.Abs(grad_a - grad_b) <= epsilon)
            {
                MessageBox.Show("Los puntos ingresados pertenecen a una misma linea recta", "Error al dibujar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // swap grads and points if grad_a is 0
            if (Math.Abs(grad_a) <= epsilon)
            {
                double temp2;
                temp2 = grad_a;
                grad_a = grad_b;
                grad_b = temp2;
                int temp;
                temp = tempClicks[0].x;
                tempClicks[0].x = tempClicks[2].x;
                tempClicks[2].x = temp;
                temp = tempClicks[0].y;
                tempClicks[0].y = tempClicks[2].y;
                tempClicks[2].y = temp;
            }

            countAreas++;

            // calculate centre - where the lines perpendicular to the centre of segments a and b intersect
            punto.x = Convert.ToInt32((grad_a * grad_b * (tempClicks[0].y - tempClicks[2].y) + grad_b * (tempClicks[0].x + tempClicks[1].x) - grad_a * (tempClicks[1].x + tempClicks[2].x)) / (2 * (grad_b - grad_a)));
            punto.y = Convert.ToInt32(((tempClicks[0].x + tempClicks[1].x) / 2 - punto.x) / grad_a + (tempClicks[0].y + tempClicks[1].y) / 2);
            punto.width = Convert.ToInt32(Math.Sqrt(Math.Pow(punto.x - tempClicks[0].x, 2) + Math.Pow(punto.y - tempClicks[0].y, 2)));
            punto.nombre = "Area " + countAreas;

            elementosScreen.Add(punto);
            AddList(punto);
            // se obliga al PictureBox que se resetee y dibuje todos los elementos que hayan en memoria
            controlPaint = true;
            pictElemento.Invalidate();

            btnClean.Enabled = true;
            btnDelete.Enabled = true;
        }

        /// <summary>
        /// Se agrega un elemento (CCuadrado) al ListBox
        /// </summary>
        private void AddList(CCuadrado punto)
        {
            string elemento = punto.nombre;
            lstElementos.Items.Add(elemento);
            lstElementos.SelectedIndex = lstElementos.Items.Count - 1;
        }

        /// <summary>
        /// Se llena por completo el ListBox con toda la informacion que halla en memoria
        /// </summary>
        public void LlenarList()
        {
            lstElementos.Items.Clear();
            for (int i = 0; i < elementosScreen.Count; i++) AddList(elementosScreen[i]);
            lstElementos.SelectedIndex = 0;
        }

        /// <summary>
        /// El evento es el encargado de pintar todos los elementos en el PictureBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictElemento_Paint(object sender, PaintEventArgs e)
        {
            if (!controlPaint) return;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            for (int i = 0; i < elementosScreen.Count; i++)
            {
                if (i == lstElementos.SelectedIndex) e.Graphics.DrawRectangle(pen2, elementosScreen[i].x - elementosScreen[i].width, elementosScreen[i].y - elementosScreen[i].width, 2 * elementosScreen[i].width, 2 * elementosScreen[i].width);
                else e.Graphics.DrawRectangle(pen1, elementosScreen[i].x - elementosScreen[i].width, elementosScreen[i].y - elementosScreen[i].width, 2 * elementosScreen[i].width, 2 * elementosScreen[i].width);
                //if (i == lstElementos.SelectedIndex) e.Graphics.DrawEllipse(pen2, elementosScreen[i].x - elementosScreen[i].r, elementosScreen[i].y - elementosScreen[i].r, 2 * elementosScreen[i].r, 2 * elementosScreen[i].r);
                //else e.Graphics.DrawEllipse(pen1, elementosScreen[i].x - elementosScreen[i].r, elementosScreen[i].y - elementosScreen[i].r, 2 * elementosScreen[i].r, 2 * elementosScreen[i].r);
            }
            controlPaint = false;
        }

        /// <summary>
        /// Se vacia la lista y se llena con la informacion en memoria
        /// </summary>
        public void ResetList()
        {
            this.lstElementos.Items.Clear();
            for (int i = 0; i < elementosScreen.Count; i++) AddList(elementosScreen[i]);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            elementosScreen.Clear();
            ResetList();
            controlPaint = true;
            pictElemento.Invalidate();

            numRadio.Value = 0;

            btnDelete.Enabled = false;
            btnClean.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // se borra el elemento de la lista y de la memoria
                int ielemento = lstElementos.SelectedIndex;
                lstElementos.Items.RemoveAt(ielemento);
                elementosScreen.RemoveAt(ielemento);

                numRadio.Value = 0;

                // se vuelve a pintar con los elementos que quedaron
                controlPaint = true;
                pictElemento.Invalidate();

                btnDelete.Enabled = false;
                if (elementosScreen.Count == 0) btnClean.Enabled = false; // no quedan elementos en memoria
            }
            catch
            {
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                elementosScreen[ielemento].y--;
                controlPaint = true;
                pictElemento.Invalidate();
            }
            catch
            {
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                elementosScreen[ielemento].y++;
                controlPaint = true;
                pictElemento.Invalidate();
            }
            catch
            {
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                elementosScreen[ielemento].x--;
                controlPaint = true;
                pictElemento.Invalidate();
            }
            catch
            {
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                elementosScreen[ielemento].x++;
                controlPaint = true;
                pictElemento.Invalidate();
            }
            catch
            {
            }
        }

        private void lstElementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // se selecciona el radio y se muestra en pantalla
                controlPaint = false;
                int ielemento = lstElementos.SelectedIndex;
                numRadio.Value = elementosScreen[ielemento].width;
                controlPaint = true;
                pictElemento.Invalidate();
                btnDelete.Enabled = true;
            }
            catch
            {

            }
        }

        private void numRadio_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                elementosScreen[lstElementos.SelectedIndex].width = Convert.ToInt32(numRadio.Value);
                controlPaint = true;
                pictElemento.Invalidate();
            }
            catch
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // se verifica que existan los 4 elementos en el listBox lstElementos
            if (lstElementos.Items.Count == 4)
            {
                // existen 4 elementos, se puede proceder a guardarlos, segun se hayan cargado los elementos HIGH o LOW

                // se ordenan segun el tamano del lado de cuadrado, de mas grande a mas pequeno, y se toma el Core como el elemento mas grande
                elementosScreen.Sort(delegate(CCuadrado x, CCuadrado y)
                {
                    return y.width.CompareTo(x.width);
                });

                if (this.lblTitulo.Text.Contains("HIGH")) this.padre.actual.SetCoreHigh(MainForm.CorregirPictBox2Original(elementosScreen[0], pictElemento.Image.Height, pictElemento.Height));
                else this.padre.actual.SetCoreLow(MainForm.CorregirPictBox2Original(elementosScreen[0], pictElemento.Image.Height, pictElemento.Height));

                elementosScreen.RemoveAt(0);

                // se ordenan segun la coordenada X, de izquierda a derecha. Los phantom P1 P2 y P3 se ordenan de izquierda a derecha
                elementosScreen.Sort(delegate(CCuadrado x, CCuadrado y)
                {
                    return x.x.CompareTo(y.x);
                });
                if (this.lblTitulo.Text.Contains("HIGH"))
                {
                    this.padre.actual.SetPhantom1High(MainForm.CorregirPictBox2Original(elementosScreen[0], pictElemento.Image.Height, pictElemento.Height));
                    this.padre.actual.SetPhantom2High(MainForm.CorregirPictBox2Original(elementosScreen[1], pictElemento.Image.Height, pictElemento.Height));
                    this.padre.actual.SetPhantom3High(MainForm.CorregirPictBox2Original(elementosScreen[2], pictElemento.Image.Height, pictElemento.Height));
                }
                else
                {
                    this.padre.actual.SetPhantom1Low(MainForm.CorregirPictBox2Original(elementosScreen[0], pictElemento.Image.Height, pictElemento.Height));
                    this.padre.actual.SetPhantom2Low(MainForm.CorregirPictBox2Original(elementosScreen[1], pictElemento.Image.Height, pictElemento.Height));
                    this.padre.actual.SetPhantom3Low(MainForm.CorregirPictBox2Original(elementosScreen[2], pictElemento.Image.Height, pictElemento.Height));
                }

                // se modifica la variable de control de la segmentacion dependiendo si se han cargado los elementos HIGH o LOW
                // se cambia la imagen de segmentacion HIGH o LOW
                if (this.lblTitulo.Text.Contains("HIGH"))
                {
                    this.padre.actual.SetAreasHigh(true);
                    this.padre.proyectoForm.pictSegHigh.Image = Properties.Resources.greenTick;
                }
                else
                {
                    this.padre.actual.SetAreasLow(true);
                    this.padre.proyectoForm.pictSegLow.Image = Properties.Resources.greenTick;
                }                

                this.Close();
            }
            else if (lstElementos.Items.Count < 4)
            {
                // existen menos de 4 elementos, no se pueden guardar y se le avisa al usuario
                MessageBox.Show("No se han marcado todas las areas necesarias.\n\nNo se puede proceder a guardar.", "Error al guardar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lstElementos.Items.Count > 4)
            {
                // existen mas de 4 elementos, no se pueden guardar y se le avisa al usuario
                MessageBox.Show("Se han marcado mas de 4 areas. Por favor, vuelva a la pantalla y elimine las areas sobrantes", "Error al guardar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
