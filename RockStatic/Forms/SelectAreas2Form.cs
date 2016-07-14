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
    public partial class SelectAreas2Form : Form
    {
        #region variables de disenador

        int anchoOriginal;

        List<CCuadrado> areasCore;

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        #endregion

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
        /// Guarda el slide actual en SelectAreasForm
        /// </summary>
        //float slideActual;

        public SelectAreas2Form()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.padre.CerrarSelectAreasForm();
        }

        public void SetRange(int head, int tail)
        {
            rangeBar.RangeMinimum = head;
            rangeBar.RangeMaximum = tail;
            pictCore.Invalidate();
        }

        private void SelectAreas2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void SelectAreas2Form_Load(object sender, EventArgs e)
        {
            //slideActual = 0;

            // se cambia el tamano de los PictureBox y del RangeBar
            Bitmap corte = padre.actual.datacuboHigh.dataCube[0].bmp;
            int width = corte.Width;
            int height = corte.Height;

            pictCore.Width = pictPhantom1.Width = pictPhantom2.Width = pictPhantom3.Width = rangeBar.Width = width;
            if(height<pictCore.Height) pictCore.Height = height;

            rangeBar.TotalMaximum = padre.actual.datacuboHigh.dataCube.Count;
            rangeBar.TotalMinimum = 1;
            rangeBar.RangeMinimum = 1;
            rangeBar.RangeMaximum = padre.actual.datacuboHigh.dataCube.Count;

            // se prepara el color de la brocha y lapiz
            brocha = new SolidBrush(Color.Red);
            lapiz = new Pen(brocha, 2F);

            brocha2 = new SolidBrush(Color.FromArgb(128,0,255,0));
            lapiz2 = new Pen(brocha2);

            brocha3 = new SolidBrush(Color.Green);
            lapiz3 = new Pen(brocha3);
            lapiz3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
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

        public void InvalidarPictCore(float slide)
        {
            //slideActual = slide-1;
            //pictCore.Invalidate();
        }

        public void pictCore_Paint(object sender, PaintEventArgs e)
        {
            // se pintan las lineas de Cabeza y Cola
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int head = pictCore.Width * rangeBar.RangeMinimum / rangeBar.TotalMaximum;
            int tail = (pictCore.Width * rangeBar.RangeMaximum / rangeBar.TotalMaximum)-2;

            e.Graphics.DrawLine(lapiz, head, 0, head, pictCore.Height);
            e.Graphics.DrawLine(lapiz, tail, 0, tail, pictCore.Height);
            
            //float xSlide=slideActual* pictCore.Image.Width / areasCore.Count;
            //e.Graphics.DrawLine(lapiz3, xSlide, 0, xSlide, pictCore.Height);
            
            // se pintan los cuadrados que se hallan seleccionado en la ventana SelectAreasForm
            // se recorren las areas, y si existe una !=null se escala el ancho del area al tamaño del plano
            if (areasCore == null) return;
            for (int i = 0; i < areasCore.Count; i++)
            {
                if (areasCore[i] != null)
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
                }
            }
        }

        private void rangeBar_RangeChanging(object sender, EventArgs e)
        {
            pictCore.Invalidate();
            padre.selecAreasForm.SetRange(rangeBar.RangeMinimum, rangeBar.RangeMaximum);
        }

        public void GetAreasCore(List<CCuadrado> areas,int ancho)
        {
            this.areasCore = areas;
            anchoOriginal = ancho;
            pictCore.Invalidate();
        }

        private void SelectAreas2Form_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Green, 2), this.DisplayRectangle);       
        }
    }
}
