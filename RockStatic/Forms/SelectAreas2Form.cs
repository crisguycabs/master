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
    /// Ventana para presentar la reconstruccion longitudinal 2D del volumen seleccionado (areas de interes)
    /// </summary>
    public partial class SelectAreas2Form : Form
    {
        #region variables de disenador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        Point lastClick;

        /// <summary>
        /// brocha con color semitransparente
        /// </summary>
        Brush brocha;

        /// <summary>
        /// Pen que toma el color de Brocha
        /// </summary>
        Pen lapiz;

        /// <summary>
        /// brocha con color semitransparente
        /// </summary>
        Brush brocha2;

        /// <summary>
        /// Pen que toma el color de Brocha
        /// </summary>
        Pen lapiz2;

        /// <summary>
        /// Brocha con color verde
        /// </summary>
        Brush brocha3;

        /// <summary>
        /// Pen que toma el color de brocha3
        /// </summary>
        Pen lapiz3;

        /// <summary>
        /// Guarda el menor valor CT de todo el datacubo
        /// </summary>
        int minimo;

        /// <summary>
        /// Guarda el mayor valor CT de todo el datacuto
        /// </summary>
        int maximo;

        /// <summary>
        /// Establece el factor de escalado de ancho de las imagenes de corte generadas
        /// </summary>
        int factor;

        #endregion
        
        public SelectAreas2Form()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.padre.CerrarSelectAreasForm();
        }

        private void SelectAreas2Form_Load(object sender, EventArgs e)
        {
            //slideActual = 0;

            //int nelemento=Convert.ToInt32(padre.actual.datacuboHigh.widthSeg/2);
            int nelemento = Convert.ToInt32(padre.actual.datacuboHigh.widthSeg/2 );
            minimo = padre.actual.datacuboHigh.GetMinimo();
            maximo = padre.actual.datacuboHigh.GetMaximo();
            double resZ = Convert.ToDouble(padre.actual.datacuboHigh.dataCube[0].selector.SliceThickness.Data);
            double resXY = Convert.ToDouble(padre.actual.datacuboHigh.dataCube[0].selector.PixelSpacing.Data_[0]);
            factor = Convert.ToInt32(resZ / resXY);

            Bitmap corte;
            corte = padre.actual.datacuboHigh.CreateBitmapCorte(padre.actual.datacuboHigh.coresHorizontal[0], padre.actual.datacuboHigh.dataCube.Count * factor, padre.actual.datacuboHigh.widthSeg, minimo, maximo);
            
            int width = corte.Width;
            int height = corte.Height;

            // se cambia el tamano de los PictureBox y del RangeBar
            // pictCore.Width = pictPhantom1.Width = pictPhantom2.Width = pictPhantom3.Width = rangeBar.Width = width;
            // if(height<pictCore.Height) pictCore.Height = height;

            pictCore.Image = corte;

            // se prepara el color de la brocha y lapiz
            brocha = new SolidBrush(Color.Red);
            lapiz = new Pen(brocha, 3F);

            brocha2 = new SolidBrush(Color.FromArgb(128,0,255,0));
            lapiz2 = new Pen(brocha2);

            brocha3 = new SolidBrush(Color.Green);
            lapiz3 = new Pen(brocha3);
            lapiz3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            if (!padre.actual.phantomEnDicom)
                grpPhantoms.Enabled = false;

            trackCortes.Minimum = 1;
            trackCortes.Maximum = corte.Height;
            trackCortes.Value = nelemento;

            trackCortes.TickFrequency = (int)(corte.Height / 10);

            numActual.Minimum = 1;
            numActual.Maximum = corte.Height;
            numActual.Value = nelemento;
            
            pictCore.Invalidate();
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
        /// Se invalidan los pictBox para repintar las areas seleccionadas
        /// </summary>
        public void Pintar()
        {
            pictCore.Invalidate();
        }

        public void pictCore_Paint(object sender, PaintEventArgs e)
        {
            double ancho;
            double dif;
            
            // se pintan las lineas de Cabeza y Cola
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // se pintan los cuadrados que se hallan seleccionado en la ventana SelectAreasForm
            // se recorren las areas, y si existe una !=null se escala el ancho del area al tamaño del plano
            if (padre.actual.areasCore.Count < 1) return;

            for (int i = 0; i < padre.actual.areasCore.Count; i++)
            {
                dif = Math.Abs(padre.actual.areasCore[i].y - Convert.ToDouble(numActual.Value));
                ancho = Math.Sqrt(padre.actual.areasCore[i].width * padre.actual.areasCore[i].width - dif * dif);

                double imgWidth = pictCore.Image.Width;
                double imgHeight = pictCore.Image.Height;
                double boxWidth = pictCore.Size.Width;
                double boxHeight = pictCore.Size.Height;

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

                float height = (float)(ancho * scale * 2);
                float width = (float)((padre.actual.areasCore[i].fin - padre.actual.areasCore[i].ini + 1) * factor * scale);
                float x = (float)(((padre.actual.areasCore[i].ini - 1) * scale * factor) + xcero);
                float y = (float)(((padre.actual.areasCore[i].y - ancho) * scale) + ycero);
                e.Graphics.FillRectangle(brocha2, x, y, width, height);

                /*if (areasCore[i] != null)
                {
                    // float x = (areasCore[i].x - areasCore[i].width) * pictCore.Image.Height / anchoOriginal;
                    float x = ((float)i * pictCore.Image.Width / areasCore.Count);
                    float y = (areasCore[i].y - areasCore[i].width) * pictCore.Image.Height / anchoOriginal;
                    float width = pictCore.Image.Width / areasCore.Count;
                    float height = 2*areasCore[i].width*pictCore.Image.Height/anchoOriginal;
                    e.Graphics.FillRectangle(brocha2, x, y, width, height);
                    
                    /*
                    using (Graphics g = Graphics.FromImage(pictCore.Image))
                    {
                        g.FillRectangle(brocha2, (areasCore[i].x - areasCore[i].width) * pictCore.Image.Height / anchoOriginal, (areasCore[i].y - areasCore[i].width) * pictCore.Image.Height / anchoOriginal, Convert.ToInt32(pictCore.Image.Width/areasCore.Count), 2*areasCore[i].width*pictCore.Image.Height/anchoOriginal);                        
                    }
                    */
                //}
            }
        }

        private void SelectAreas2Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }

        private void trackCortes_Scroll(object sender, EventArgs e)
        {
            pictCore.Image = padre.actual.datacuboHigh.CreateBitmapCorte(padre.actual.datacuboHigh.coresHorizontal[trackCortes.Value - 1], padre.actual.datacuboHigh.dataCube.Count * factor, padre.actual.datacuboHigh.widthSeg, minimo, maximo);
            numActual.Value = trackCortes.Value;
        }

        private void radHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            trackCortes_Scroll(sender, e);
        }

        private void numActual_ValueChanged(object sender, EventArgs e)
        {
            trackCortes.Value = Convert.ToInt32(numActual.Value);
            trackCortes_Scroll(sender, e);
        }

        private void btnSelTrans_Click(object sender, EventArgs e)
        {
            padre.selecAreasForm.Select();
        }              
    }
}
