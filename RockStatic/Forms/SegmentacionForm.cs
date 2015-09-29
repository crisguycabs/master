using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        /// Area a hacer zoom de la imagen del dicom
        /// </summary>
        System.Drawing.Point mouseLoc;

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
        /// Lapiz para dibujar el area del centro del zoom
        /// </summary>
        Pen pen3;

        /// <summary>
        /// variable de control para evitar que se lance el evento Paint de pictCore
        /// </summary>
        bool controlPaint;

        /// <summary>
        /// Lista dinamica de elementos a dibujar en pantalla
        /// </summary>
        public List<CCuadrado> elementosScreen;

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
            pen3 = new System.Drawing.Pen(Color.Red,1F);

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

            this.Invalidate();
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

        /// <summary>
        /// Se resetea el PictureBox principal
        /// </summary>
        private void ResetPictBox()
        {
            this.trackElementos_ValueChanged(new object(),new EventArgs());
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
            if (radManual.Checked)
            {
                this.Cursor = Cursors.Cross;                
            }
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
                if (i == lstElementos.SelectedIndex) e.Graphics.DrawEllipse(pen2, elementosScreen[i].x - elementosScreen[i].width, elementosScreen[i].y - elementosScreen[i].width, 2 * elementosScreen[i].width, 2 * elementosScreen[i].width);
                else e.Graphics.DrawEllipse(pen1, elementosScreen[i].x - elementosScreen[i].width, elementosScreen[i].y - elementosScreen[i].width, 2 * elementosScreen[i].width, 2 * elementosScreen[i].width);
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

        private void pictElemento_MouseMove(object sender, MouseEventArgs e)
        {
            if (radManual.Checked == true)
            {
                // se prepara la imagen a mostrar en el zoom
                mouseLoc = pictElemento.PointToClient(Cursor.Position);
                
                // se dibuja la imagen en zoom
                // An empty bitmap which will hold the cropped image
                Bitmap bmp = new Bitmap(pictSmall.Height, pictSmall.Height);

                Graphics g = Graphics.FromImage(bmp);

                // si el puntero.x es menor que pictSmall.width se empieza a dibujar en una coordenada x>0
                // si el puntero.y es menor que pictSmall.width se empieza a dibujar en una coordenada y>0
                // si el borde derecho o inferior del area seleccionada para zoom indica un punto fuera de la imagen original
                // entonces se selecciona un area mas pequeña

                Rectangle selectedArea = new Rectangle();
                selectedArea.Width = selectedArea.Height = pictSmall.Height;
                selectedArea.X = MainForm.CorregirPictBox2Original(mouseLoc.X, pictElemento.Image.Height, pictElemento.Height) - (pictSmall.Height/2);
                selectedArea.Y = MainForm.CorregirPictBox2Original(mouseLoc.Y, pictElemento.Image.Height, pictElemento.Height) - (pictSmall.Height/2);

                int iniX = 0;
                int iniY = 0;

                if (selectedArea.Left < 0)
                {
                    iniX = Math.Abs(selectedArea.Left);
                    selectedArea.Width = pictSmall.Height - iniX;
                    selectedArea.X = 0;
                }
                if (selectedArea.Top < 0)
                {
                    iniY = Math.Abs(selectedArea.Top);
                    selectedArea.Height = pictSmall.Height - iniY;
                    selectedArea.Y = 0;
                }

                g.DrawImage(pictElemento.Image, iniX, iniY, selectedArea, GraphicsUnit.Pixel);
                g.DrawEllipse(pen3, 58, 58, 8, 8);
                pictSmall.Image = bmp;
            }
        }

        private void pictSmall_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid); 
        }

        private void SegmentacionForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Green, ButtonBorderStyle.Solid); 
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            if (elementosScreen.Count < 4)
            {
                MessageBox.Show("No se han seleccionado todos los elementos del slide.\n\nNo se puede proceder a previsualizar.","Error al previsualizar!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else if (elementosScreen.Count > 4)
            {
                MessageBox.Show("Se han seleccionado demasiados elementos en el slide.\n\nNo se puede proceder a previsualizar.", "Error al previsualizar!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.padre.previewSegForm = new PreviewSegForm();
            this.padre.abiertoPreviewSegForm = true;
            this.padre.previewSegForm.padre = this.padre;

            // se cortan las imagenes y se envian a PreviewSegForm

            // se prepara un List de Bitmap
            List<Bitmap> cortes = new List<Bitmap>();
            
            // primero se hace una copia del List elementosScreen con las coordenadas corregidas
            List<CCuadrado> tempElementos = new List<CCuadrado>();
            for (int i = 0; i < elementosScreen.Count; i++)
            {
                CCuadrado temp = new CCuadrado(elementosScreen[i]);
                tempElementos.Add(MainForm.CorregirPictBox2Original(temp, pictElemento.Image.Height, pictElemento.Height));
            }
            
            // se ordena el tempElementos de radio mas grande a mas pequeño para sacar el CORE
            // se ordenan segun el tamano del lado de cuadrado, de mas grande a mas pequeno, y se toma el Core como el elemento mas grande
            tempElementos.Sort(delegate(CCuadrado x, CCuadrado y)
            {
                return y.width.CompareTo(x.width);
            });

            Bitmap aEnviar = new Bitmap(pictElemento.Image);
            cortes.Add(MainForm.CropCirle(aEnviar, tempElementos[0]));
            padre.previewSegForm.core = cortes[0];
            tempElementos.RemoveAt(0);
            
            // se ordenan de izquierda a derecha
            // se ordenan segun la coordenada X, de izquierda a derecha. Los phantom P1 P2 y P3 se ordenan de izquierda a derecha
            tempElementos.Sort(delegate(CCuadrado x, CCuadrado y)
            {
                return x.x.CompareTo(y.x);
            });
            for (int i = 0; i < tempElementos.Count;i++)
            {
                cortes.Add(MainForm.CropCirle(aEnviar, tempElementos[i]));
            }
            padre.previewSegForm.p1 = cortes[1];
            padre.previewSegForm.p2 = cortes[2];
            padre.previewSegForm.p3 = cortes[3];  
            
            // se invoca como un cuadro de dialogo modal, no MDIchild
            this.padre.previewSegForm.ShowDialog();
        }

        /// <summary>
        /// Este metodo toma la imagen en pantalla, realiza una copia, y detecta las areas circulares 
        /// Las areas circulares se dibujan en la imagen en pantalla
        /// </summary>
        /// <returns>Vector Rectangle[] con las areas encontradas</returns>
        public Rectangle[] DetectarBordes()
        {
            // se reestablece la imagen
            ResetPictBox();

            // se realiza una copia de la imagen
            Bitmap slide = FormatToAForge(new Bitmap(pictElemento.Image));
            slide.Save("slide.jpg");

            // lock image
            BitmapData bitmapData = slide.LockBits(
                new Rectangle(0, 0, slide.Width, slide.Height),
                ImageLockMode.ReadWrite, slide.PixelFormat);

            // se realiza el primer barrido, dividiendo la imagen usando el primer treshold
            ColorFiltering colorFilter = new ColorFiltering();

            int valorThreshold = Convert.ToInt32(track1.Value);

            colorFilter.Red = new IntRange(0, valorThreshold);
            colorFilter.Green = new IntRange(0, valorThreshold);
            colorFilter.Blue = new IntRange(0, valorThreshold);
            colorFilter.FillOutsideRange = false;

            colorFilter.ApplyInPlace(bitmapData);

            // se localizan los elementos circulares
            BlobCounter blobCounter = new BlobCounter();

            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = 5;
            blobCounter.MinWidth = 5;

            blobCounter.ProcessImage(bitmapData);
            Blob[] blobsTemp = blobCounter.GetObjectsInformation();
            slide.UnlockBits(bitmapData);

            // se eliminan los blobs no cuadrados, aquellos cuya relacion Abs(1-width/heigh) es mayor a 0.2, ademas de reordenar los blobs de menor a mayor area
            List<Blob> blobs = new List<Blob>();

            // se hace una copia
            for (int i = 0; i < blobsTemp.Length; i++) blobs.Add(blobsTemp[i]);

            // se eliminan los rectangulares
            for (int i = 0; i < blobs.Count; i++)
            {
                if (Math.Abs(1 - (blobs[i].Rectangle.Height / blobs[i].Rectangle.Width)) > 0.2) blobs.RemoveAt(i);
            }

            // se ordenan los blobs de mayor area a menor area
            blobs.Sort(delegate(Blob x, Blob y)
            {
                return (y.Rectangle.Height * y.Rectangle.Width).CompareTo(x.Rectangle.Height * x.Rectangle.Width);
            });
            
            // solo se toman los 4 blobs mas grandes
            while (blobs.Count > 4) blobs.RemoveAt(blobs.Count - 1);

            // se prepara el vector de salida
            Rectangle[] areas = new Rectangle[blobs.Count];
            for (int i = 0; i < blobs.Count; i++) areas[i] = blobs[i].Rectangle;

            /*///
            Bitmap imagen = new Bitmap(pictElemento.Image);
            Graphics g = Graphics.FromImage(imagen);
            for (int i = 0; i < blobs.Count; i++) g.DrawEllipse(pen1, blobs[i].Rectangle);
            pictElemento.Image = imagen;
            ///*/

            return areas;
            

            /*
            // se extrae el elemento mas grande
            try
            {
                Bitmap imagen = new Bitmap(pictElemento.Image);

                Bitmap subslide = imagen.Clone(blobs[0].Rectangle, imagen.PixelFormat);
                subslide = FormatToAForge(subslide);
                subslide.Save("subslide.jpg");
                
                // sobre la seccion del elemento mas grade se realiza el mismo proceso
                // pero usando el segundo valor de threshold

                // primero se hace una copia
                Bitmap subslide2 = FormatToAForge(subslide);

                // lock image
                BitmapData bitmapData2 = subslide2.LockBits(
                    new Rectangle(0, 0, subslide2.Width, subslide2.Height),
                    ImageLockMode.ReadWrite, subslide2.PixelFormat);

                // se realiza el primer barrido, dividiendo la imagen usando el primer treshold
                colorFilter = new ColorFiltering();

                valorThreshold = Convert.ToInt32(track2.Value);

                colorFilter.Red = new IntRange(0, valorThreshold);
                colorFilter.Green = new IntRange(0, valorThreshold);
                colorFilter.Blue = new IntRange(0, valorThreshold);
                colorFilter.FillOutsideRange = false;

                colorFilter.ApplyInPlace(bitmapData2);

                // se localizan los elementos circulares
                BlobCounter blobCounter2 = new BlobCounter();

                blobCounter.FilterBlobs = true;
                blobCounter.MinHeight = 5;
                blobCounter.MinWidth = 5;

                blobCounter.ProcessImage(bitmapData2);
                Blob[] blobs2 = blobCounter.GetObjectsInformation();
                subslide2.UnlockBits(bitmapData2);

                // se enmarcan los elementos encontrados
                try
                {
                    // ahora se dibuja en la imagen principal las zonas
                    // primero se obtiene la posicion del ultimo recuadro pero en funcion de la imagen principal
                    Rectangle[] areas = new Rectangle[4];
                    areas[0] = new Rectangle(blobs[0].Rectangle.X + blobs2[1].Rectangle.X,
                        blobs[0].Rectangle.Y + blobs2[1].Rectangle.Y,
                        blobs2[1].Rectangle.Width,
                        blobs2[1].Rectangle.Height);

                    // se copian el resto de los elementos
                    areas[1] = blobs[1].Rectangle;
                    areas[2] = blobs[2].Rectangle;
                    areas[3] = blobs[3].Rectangle;

                    // se incrementa un poco el encuadre
                    for (int i = 0; i < areas.Length; i++)
                    {
                        areas[i].X = areas[i].X - 2;
                        areas[i].Y = areas[i].Y - 2;
                        areas[i].Width = areas[i].Width + 4;
                        areas[i].Height = areas[i].Height + 4;
                    }

                    ///
                    Graphics g = Graphics.FromImage(imagen);
                    for (int i = 0; i < areas.Length; i++) g.DrawRectangle(pen1, areas[i]);
                    pictElemento.Image = imagen;
                    ///

                    return areas;
                }
                catch
                {
                    return null;
                }
            }
            catch { return null; }     
             */
        }

        public void DeteccionAutomatica()
        {
            // se limpian todas las areas seleccionadas previamente
            btnClean_Click(new object(),new EventArgs());
            
            // se ejecuta la deteccion de bordes
            Rectangle[] areas=DetectarBordes();
            
            if (areas != null)
            {
                CCuadrado temp;
                for (int i = 0; i < areas.Length; i++)
                {
                    countAreas++;
                    temp = new CCuadrado();
                    temp.width = (int)(areas[i].Width / 2);
                    temp.x = areas[i].X + temp.width;
                    temp.y = areas[i].Y + temp.width;
                    temp.nombre = "Area" + countAreas;
                    
                    elementosScreen.Add(MainForm.CorregirOriginal2PictBox(temp,pictElemento.Image.Height,pictElemento.Height));
                    // el metodo de correccion de tamaño de coordenadas modifica el CCuadrado temp
                    AddList(temp);                    
                }

                // se obliga al PictureBox que se resetee y dibuje todos los elementos que hayan en memoria
                controlPaint = true;
                pictElemento.Invalidate();

                btnClean.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                // no hacer nada, no se detectaron los bordes
            }
        }

        private void num1_ValueChanged(object sender, EventArgs e)
        {
            track1.Value = (int)num1.Value;
            DeteccionAutomatica();
        }

        private void num2_ValueChanged(object sender, EventArgs e)
        {
            track2.Value = (int)num2.Value;
            DeteccionAutomatica();
        }

        private void track1_ValueChanged(object sender, EventArgs e)
        {
            num1.Value = track1.Value;
            DeteccionAutomatica();
        }

        private void track2_ValueChanged(object sender, EventArgs e)
        {
            num2.Value = track2.Value;
            DeteccionAutomatica();
        }
    }
}
