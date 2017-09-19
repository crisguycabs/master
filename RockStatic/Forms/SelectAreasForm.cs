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
    /// Ventana para la selección de las areas de interes en la segmentaciones transversales
    /// </summary>
    public partial class SelectAreasForm : Form
    {
        #region variables de disenador

        Point lastClick;    

        /// <summary>
        /// Lapiz para dibujar el circulo seleccionado
        /// </summary>
        Pen pen2;

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// variable de control para evitar que se lance el evento Paint de pictCore
        /// </summary>
        bool controlPaint;

        /// <summary>
        /// Array que guarda las coordenadas de los clicks necesarios para dibujar un circulo
        /// </summary>
        CCirculo[] tempClicksCore;

        /// <summary>
        /// control de cambios
        /// </summary>
        bool changes;

        /// <summary>
        /// Contador para clicks
        /// </summary>
        int countClickCore;

        /// <summary>
        /// Contador de areas de interes creadas para agregar al nombre
        /// </summary>
        int countAreas;

        /// <summary>
        /// control de presionar el trackElementos
        /// </summary>
        bool buttonDown = false;

        #endregion

        public SelectAreasForm()
        {
            InitializeComponent();
        }

        public void CentrarForm()
        {
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) *0.1/ 2));
        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            CentrarForm();            
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void btnCerrar_MouseEnter(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.White;
        }

        private void btnCerrar_MouseLeave(object sender, EventArgs e)
        {
            ((System.Windows.Forms.Button)(sender)).ForeColor = Color.Black;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            padre.actual.areasCore = new List<CAreaInteres>();
            this.padre.CerrarSelectAreasForm();
        }

        private void SelectAreasForm_Load(object sender, EventArgs e)
        {
            tempClicksCore= new CCirculo[3];
            countClickCore=0;
            
            // lapiz
            float[] dashValues = { 10, 3, 5, 3 };
            pen2 = new System.Drawing.Pen(Color.Red, 2F);
            pen2.DashPattern = dashValues;
            
            // control de cambios
            //changes = true;
            changes = false;

            padre.actual.datacuboHigh.widthSegCore = padre.actual.areaCore.width;
            padre.actual.datacuboLow.widthSegCore = padre.actual.areaCore.width;

     

            //// voy a modificar 

            if (padre.actual.phantomEnDicom)
            {

                this.padre.ShowWaiting("Por favor espere mientras RockStatic realiza la segmentacion de los elementos");

 
                this.padre.actual.datacuboHigh.SegCircThread(new CCuadrado(padre.actual.areaCore.x , padre.actual.areaCore.y , padre.actual.areaCore.width), "core");
                this.padre.actual.datacuboLow.SegCircThread(new CCuadrado(padre.actual.areaCore.x, padre.actual.areaCore.y, padre.actual.areaCore.width), "core");
                padre.actual.datacuboHigh.SegCircThread(padre.actual.areaPhantom1, "p1");
                padre.actual.datacuboLow.SegCircThread(padre.actual.areaPhantom1, "p1");
                padre.actual.datacuboHigh.SegCircThread(padre.actual.areaPhantom2, "p2");
                padre.actual.datacuboLow.SegCircThread(padre.actual.areaPhantom2, "p2");
                padre.actual.datacuboHigh.SegCircThread(padre.actual.areaPhantom3, "p3");
                padre.actual.datacuboLow.SegCircThread(padre.actual.areaPhantom3, "p3");

                // se generan los cortes longitudinales
                this.padre.actual.datacuboHigh.GenerarCoresHorizontales();
                this.padre.actual.datacuboHigh.GenerarCoresVerticales();
                this.padre.actual.datacuboLow.GenerarCoresHorizontales();
                this.padre.actual.datacuboLow.GenerarCoresVerticales();

                ////-----------------------esto es modificado --------------------------------
                padre.actual.datacuboHigh.widthSegP1 = padre.actual.areaPhantom1.width;
                padre.actual.datacuboHigh.widthSegP2 = padre.actual.areaPhantom2.width;
                padre.actual.datacuboHigh.widthSegP3 = padre.actual.areaPhantom3.width;
                this.padre.actual.datacuboHigh.GeneraPhanton1Horizonales();
                this.padre.actual.datacuboHigh.GeneraPhanton2Horizonales();
                this.padre.actual.datacuboHigh.GeneraPhanton3Horizonales();
                ////-----------------------esto es modificado --------------------------------
                this.padre.CloseWaiting();
            }
            else
            {
                // se recortan los core y phantom para cada elemento HIGH y LOW
                this.padre.ShowWaiting("Por favor espere mientras RockStatic realiza la segmentacion de los elementos");

                // se genera la segmentacion transversal

                this.padre.actual.datacuboHigh.SegCircThread(padre.actual.areaCore, "core");
                this.padre.actual.datacuboLow.SegCircThread(padre.actual.areaCore, "core");

                // se generan los cortes longitudinales
                this.padre.actual.datacuboHigh.GenerarCoresHorizontales();
                this.padre.actual.datacuboHigh.GenerarCoresVerticales();
                this.padre.actual.datacuboLow.GenerarCoresHorizontales();
                this.padre.actual.datacuboLow.GenerarCoresVerticales();

                this.padre.CloseWaiting();
            }

            // se prepara la barra
            this.trackElementos.Minimum = 1;
            this.trackElementos.Maximum = padre.actual.datacuboHigh.dataCube.Count;
            this.trackElementos.TickFrequency = (int)(padre.actual.datacuboHigh.dataCube.Count / 10);
            this.trackElementos.Value = 1;

            // -----------------------------------------------------------------------------------------------------

            this.pictCore.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segCore, padre.actual.areaCore.width, padre.actual.areaCore.width);

            // si existe informacion de phantoms en los DICOM
            
            if (padre.actual.phantomEnDicom)
            {
                //this.pictP1.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom1, padre.actual.areaPhantom1.width * 2, padre.actual.areaPhantom1.width * 2);
                //this.pictP2.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom2, padre.actual.areaPhantom2.width * 2, padre.actual.areaPhantom2.width * 2);
                //this.pictP3.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom3, padre.actual.areaPhantom3.width * 2, padre.actual.areaPhantom3.width * 2);

                int minNormalizacion = Convert.ToInt32(padre.actual.datacuboHigh.dataCube[0].pixelData.Min());
                int maxNormalizacion = Convert.ToInt32(padre.actual.datacuboHigh.dataCube[0].pixelData.Max());

                this.pictP1.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom1, padre.actual.areaPhantom1.width , padre.actual.areaPhantom1.width, minNormalizacion, maxNormalizacion );
                this.pictP2.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom2, padre.actual.areaPhantom2.width, padre.actual.areaPhantom2.width, minNormalizacion, maxNormalizacion);
                this.pictP3.Image = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[0].segPhantom3, padre.actual.areaPhantom3.width , padre.actual.areaPhantom3.width, minNormalizacion, maxNormalizacion );

                /*this.pictP1.Image = MainForm.Byte2image(elementosP1[0]);
                this.pictP2.Image = MainForm.Byte2image(elementosP2[0]);
                this.pictP3.Image = MainForm.Byte2image(elementosP3[0]);*/
            }
            else
            {
                // no hay informacion de phantoms en dicom
                grpPhantoms.Enabled = false;
            }

            // se preparan los numericUpDown
            numActual.Minimum = 1;
            numActual.Maximum = padre.actual.datacuboHigh.dataCube.Count;
            changes = false;
            numActual.Value = 1;

            numFrom.Minimum = 1;
            numFrom.Maximum = padre.actual.datacuboHigh.dataCube.Count;
            changes = false;
            numFrom.Value = 1;

            numUntil.Minimum = 1;
            numUntil.Maximum = padre.actual.datacuboHigh.dataCube.Count;
            changes = false;
            numUntil.Value = padre.actual.datacuboHigh.dataCube.Count;
            
            countAreas = 0;
            if (padre.actual.areasDone) countAreas = padre.actual.areasCore.Count;

            LlenarListAreas();
            pictCore.Invalidate();

            changes = true;
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            int n = trackElementos.Value;

            if(changes)
            {
                changes = false;
                
                numActual.Value = n;

                changes = true;
            }

            Filtrar(n);

            // si existe informacion de phantoms en los DICOM
            if (padre.actual.phantomEnDicom)
            {
                //this.pictP1.Image = MainForm.Byte2image(elementosP1[n - 1]);
                //this.pictP2.Image = MainForm.Byte2image(elementosP2[n - 1]);
                //this.pictP3.Image = MainForm.Byte2image(elementosP3[n - 1]);
            }

            // se cambia el area seleccionada en el listbox. Se busca el area que corresponde con el slide actual
            for (int i = 0; i < padre.actual.areasCore.Count; i++)
            {
                if ((trackElementos.Value >= padre.actual.areasCore[i].ini) & (trackElementos.Value <= padre.actual.areasCore[i].fin))
                {
                    lstAreas.SelectedIndex = i;
                    break;
                }
            }

            pictCore.Invalidate();
            padre.selecAreas2Form.pictCore.Invalidate();
        }

        private void numActual_ValueChanged(object sender, EventArgs e)
        {
            if(changes)
            {
                changes = false;

                trackElementos.Value = Convert.ToInt32(numActual.Value);

                changes = true;
            }
        }

        /// <summary>
        /// Se calcula el nuevo elemento usando los 3-clicks realizados sobre la imagen, y se agrega al List
        /// </summary>
        public void AddElemento(CCirculo[] tempClicks, string elemento)
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

            // calculate centre - where the lines perpendicular to the centre of segments a and b intersect
            punto.x = Convert.ToInt32((grad_a * grad_b * (tempClicks[0].y - tempClicks[2].y) + grad_b * (tempClicks[0].x + tempClicks[1].x) - grad_a * (tempClicks[1].x + tempClicks[2].x)) / (2 * (grad_b - grad_a)));
            punto.y = Convert.ToInt32(((tempClicks[0].x + tempClicks[1].x) / 2 - punto.x) / grad_a + (tempClicks[0].y + tempClicks[1].y) / 2);
            punto.width = Convert.ToInt32(Math.Sqrt(Math.Pow(punto.x - tempClicks[0].x, 2) + Math.Pow(punto.y - tempClicks[0].y, 2)));

            // si no existen areas dibujadas entonces se agrega la primera a la lista
            // si la nueva area dibujada está al inicio de un area previamente creada entonces la nueva area modifica el area creada

            if (padre.actual.areasCore.Count < 1)
            {
                // se crea la primera area
                AddArea(punto, elemento);
            }
            else
            {
                // hay areas creadas, se verifica si se debe sobre escribir o se crea una segunda area
                if (trackElementos.Value == padre.actual.areasCore[lstAreas.SelectedIndex].ini)
                {
                    // se cambia el punto obtenido a las coordenadas originales, considerando que el tamaño del pictCore y de la imagen son diferentes
                    CCuadrado corregido = new CCuadrado(punto);
                    corregido = MainForm.CorregirPictBox2Original(corregido, padre.actual.datacuboHigh.widthSegCore, pictCore.Height);

                    padre.actual.areasCore[lstAreas.SelectedIndex].x = corregido.x;
                    padre.actual.areasCore[lstAreas.SelectedIndex].y = corregido.y;
                    padre.actual.areasCore[lstAreas.SelectedIndex].width = corregido.width;

                    // se pintan las areas el pictCore
                    controlPaint = true;
                    pictCore.Invalidate();
                }
                else
                {
                    // se crea una nueva area
                    AddArea(punto, elemento);
                }
            }

            btnClear.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void AddArea(CCuadrado punto, string elemento)
        {
            // se incrementa el contador de areas
            countAreas++;

            // se cambia el punto obtenido a las coordenadas originales, considerando que el tamaño del pictCore y de la imagen son diferentes
            CCuadrado corregido = new CCuadrado(punto);
            corregido = MainForm.CorregirPictBox2Original(corregido, padre.actual.datacuboHigh.widthSegCore, pictCore.Height);
            CAreaInteres area = new CAreaInteres(corregido.x, corregido.y, corregido.width, "Area" + countAreas.ToString(), Convert.ToInt32(numActual.Value), padre.actual.datacuboHigh.dataCube.Count);

            switch (elemento)
            {
                case "core":

                    padre.actual.areasCore.Add(area);

                    // se ordenan los elementos de principio a fin
                    this.padre.actual.areasCore.Sort(delegate(CAreaInteres a, CAreaInteres b)
                    {
                        return a.ini.CompareTo(b.ini);
                    });

                    // se verifica que no se crucen las areas entre si
                    if (this.padre.actual.areasCore.Count > 1)
                    {
                        // solo se verifica que no hayan cruces si y solo si hay mas de una area agregada a la lista
                        for (int i = 1; i < this.padre.actual.areasCore.Count; i++)
                        {
                            if (this.padre.actual.areasCore[i - 1].fin > this.padre.actual.areasCore[i].ini)
                                this.padre.actual.areasCore[i - 1].fin = this.padre.actual.areasCore[i].ini - 1;
                        }
                    }

                    // se llena el ListBox
                    LlenarListAreas();

                    // se busca que indice del listbox se debe seleccionar, en funcion del slide en el que se encuentra
                    for (int i = 0; i < this.padre.actual.areasCore.Count; i++)
                    {
                        if ((trackElementos.Value >= this.padre.actual.areasCore[i].ini) & (trackElementos.Value <= this.padre.actual.areasCore[i].fin))
                            lstAreas.SelectedIndex = i;
                    }

                    // se pintan las areas el pictCore
                    controlPaint = true;
                    pictCore.Invalidate();

                    // se envia el list de areas de interes a la SelectAreas2Form
                    //padre.selecAreas2Form.GetAreasCore(areasCore, pictCore.Image.Width);
                    break;
            }
        }

        /// <summary>
        /// Se llena el listbox de las areas de interes
        /// </summary>
        private void LlenarListAreas()
        {
            lstAreas.Items.Clear();

            // se debe verificar que hayan areas de interes en memoria
            if (padre.actual.areasCore.Count > 0)
            {
                for (int i = 0; i < padre.actual.areasCore.Count; i++)
                    lstAreas.Items.Add(padre.actual.areasCore[i].nombre);

                btnClear.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void pictCore_MouseClick(object sender, MouseEventArgs e)
        {
            // solo se cuentan los clicks en modo manual
            // se asegura que el PictureBox obtenga el foco
            pictCore.Focus();

            // se guarda la coordenada del click, y se aumenta el contador
            tempClicksCore[countClickCore] = new CCirculo(e.X, e.Y, 0);
            countClickCore++;

            switch (countClickCore)
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
                    AddElemento(tempClicksCore,"core");
                    ResetCountClickCore();
                    break;
            }
        }

        private void pictCore_MouseEnter(object sender, EventArgs e)
        {
            // se cambia el cursor si se ha definido el MODO MANUAL
            this.Cursor = Cursors.Cross;            
        }

        private void pictCore_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Se resetea el conteo de clicks
        /// </summary>
        public void ResetCountClickCore()
        {
            countClickCore = 0;
            tempClicksCore = new CCirculo[3];

            lblPunto1.Visible = false;
            lblPunto2.Visible = false;
            btnCancel.Enabled = false;
        }

        private void pictCore_Paint(object sender, PaintEventArgs e)
        {
            // se busca si existe un elemento en la lista areasCore que se corresponda con el slide actual, y se pinta el area
            for (int i = 0; i < this.padre.actual.areasCore.Count; i++)
            {
                if ((trackElementos.Value >= this.padre.actual.areasCore[i].ini) & (trackElementos.Value <= this.padre.actual.areasCore[i].fin))
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    CCuadrado corregido = new CCuadrado(padre.actual.areasCore[i].x, padre.actual.areasCore[i].y, padre.actual.areasCore[i].width);
                    corregido = MainForm.CorregirOriginal2PictBox(corregido, padre.actual.datacuboHigh.widthSegCore, pictCore.Height);

                    e.Graphics.DrawEllipse(pen2, corregido.x - corregido.width, corregido.y - corregido.width, 2 * corregido.width, 2 * corregido.width);
                    //e.Graphics.DrawEllipse(pen2, this.padre.actual.areasCore[i].x - this.padre.actual.areasCore[i].width, this.padre.actual.areasCore[i].y - this.padre.actual.areasCore[i].width, 2 * this.padre.actual.areasCore[i].width, 2 * this.padre.actual.areasCore[i].width);
                    
                    return; // se encontro el elemento areaInteres buscado, no es necesario buscar los otros elementos en la lista
                }
            }
        }        

        private void SelectAreasForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }

        /// <summary>
        /// Modifica el brillo y contraste de la imagen que aparece en pantalla
        /// </summary>
        private void Filtrar(int n)
        {
            Bitmap temp = MyDicom.CrearBitmap(padre.actual.datacuboHigh.dataCube[n - 1].segCore, padre.actual.areaCore.width, padre.actual.areaCore.width);                          
            
            AForge.Imaging.Filters.BrightnessCorrection brillo = new AForge.Imaging.Filters.BrightnessCorrection(Convert.ToInt32(trackBrillo.Value));
            AForge.Imaging.Filters.ContrastCorrection contraste = new AForge.Imaging.Filters.ContrastCorrection(Convert.ToInt32(trackContraste.Value));

            contraste.ApplyInPlace(temp);
            brillo.ApplyInPlace(temp);

            pictCore.Image = temp;

            //pictElemento.Invalidate();
        }

        private void trackBrillo_Scroll(object sender, EventArgs e)
        {
            int n = trackElementos.Value;
            Filtrar(n);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int n = trackElementos.Value;
            trackBrillo.Value = 0;
            trackContraste.Value = 0;

            Filtrar(n);
        }

        private void lstAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!buttonDown)
            {
                int nactual = lstAreas.SelectedIndex;

                // se busca el INI del area de interes seleccionada y se mueve el trackbar a esa posicion
                trackElementos.Value = padre.actual.areasCore[nactual].ini;

                numFrom.Value = padre.actual.areasCore[nactual].ini;
                numUntil.Value = padre.actual.areasCore[nactual].fin;
                numRad.Value = padre.actual.areasCore[nactual].width;
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                padre.actual.areasCore[this.lstAreas.SelectedIndex].y--;
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
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
                padre.actual.areasCore[this.lstAreas.SelectedIndex].x++;
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
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
                padre.actual.areasCore[this.lstAreas.SelectedIndex].x--;
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
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
                padre.actual.areasCore[this.lstAreas.SelectedIndex].y++;
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
            }
            catch
            {
            }
        }

        private void numFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                padre.actual.areasCore[this.lstAreas.SelectedIndex].ini=Convert.ToInt32(numFrom.Value);
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
            }
            catch
            {
            }
        }

        private void numUntil_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                padre.actual.areasCore[this.lstAreas.SelectedIndex].fin = Convert.ToInt32(numUntil.Value);
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
            }
            catch
            {
            }
        }

        private void numRad_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int ielemento = lstElementos.SelectedIndex;
                padre.actual.areasCore[this.lstAreas.SelectedIndex].width = Convert.ToInt32(numRad.Value);
                controlPaint = true;
                pictCore.Invalidate();

                padre.selecAreas2Form.Pintar();
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetCountClick();
        }

        /// <summary>
        /// Se resetea el conteo de clicks
        /// </summary>
        public void ResetCountClick()
        {
            countClickCore = 0;
            tempClicksCore = new CCirculo[3];

            lblPunto1.Visible = false;
            lblPunto2.Visible = false;
            btnCancel.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstAreas.SelectedIndex < 0) return;

            padre.actual.areasCore.RemoveAt(lstAreas.SelectedIndex);
            LlenarListAreas();

            controlPaint = true;
            pictCore.Invalidate();

            padre.selecAreas2Form.Pintar();

            if (padre.actual.areasCore.Count < 1)
            {
                btnClear.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            padre.actual.areasCore.Clear();
            LlenarListAreas();

            controlPaint = true;
            pictCore.Invalidate();

            padre.selecAreas2Form.Pintar();

            btnClear.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// se crea un area que englobe todo la segmentacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelall_Click(object sender, EventArgs e)
        {
            // si existen areas previamente seleccionadas se borran
            if (padre.actual.areasCore.Count > 0)
            {
                padre.actual.areasCore.Clear();
                lstAreas.Items.Clear();
            }

            // se crea una area que englobe todo
            padre.actual.areasCore.Add(new CAreaInteres(Convert.ToInt32(padre.actual.datacuboHigh.widthSegCore/2),Convert.ToInt32(padre.actual.datacuboHigh.widthSegCore/2),Convert.ToInt32(padre.actual.datacuboHigh.widthSegCore/2),"area"+(countAreas++).ToString(),1,padre.actual.datacuboHigh.dataCube.Count));

            // se ajusta el radio a el ancho menos 1
            padre.actual.areasCore[0].width--;

            LlenarListAreas();
            lstAreas.SelectedIndex = 0;

            controlPaint = true;
            pictCore.Invalidate();

            btnClear.Enabled = true;
            btnDelete.Enabled = true;

            padre.selecAreas2Form.Pintar();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // se verifica que se hallan creado al menos un area
            if (padre.actual.areasCore.Count < 1)
            {
                MessageBox.Show("No existen areas seleccionadas a guardar", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            this.padre.actual.areasDone = true;

            // se guarda en disco
            this.padre.actual.Salvar();

            // se marca la segmentacion como completa
            this.padre.proyectoForm.SetForm();

            this.padre.CerrarSelectAreasForm();            
        }

        private void btnSelLong_Click(object sender, EventArgs e)
        {
            padre.selecAreas2Form.Select();
        }

        private void grpPhantoms_Enter(object sender, EventArgs e)
        {

        }

        private void trackElementos_MouseDown(object sender, MouseEventArgs e)
        {
            buttonDown = true;
        }

        private void trackElementos_MouseUp(object sender, MouseEventArgs e)
        {
            buttonDown = false;
        }        
    }
}
