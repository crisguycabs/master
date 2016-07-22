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
using System.Drawing.Imaging;

namespace RockStatic
{
    /// <summary>
    /// Form principal. Contiene los tipos de datos globales y métodos estáticos
    /// </summary>
    public partial class MainForm : Form
    {
        #region Variables de Diseñador

        public List<CUsuario> usuarios;

        /// <summary>
        /// Instancia del Form NewProjectForm
        /// </summary>
        public NewProjectForm nuevoProyectoForm;

        /// <summary>
        /// Variable que indica si la ventana NewProjectForm esta abiera o no
        /// </summary>
        public bool abiertoNuevoProyectoForm;

        /// <summary>
        /// Instancia del Form CheckForm
        /// </summary>
        public CheckForm checkForm;

        /// <summary>
        /// Instancia del Form PhantomForm
        /// </summary>
        public PhantomsForm phantomForm;

        /// <summary>
        /// Variable que indica si la ventana CheckForm esta abierta o no
        /// </summary>
        public bool abiertoCheckForm;

        /// <summary>
        /// Variable que indica si la ventana PhantomsForm esta abierta o no
        /// </summary>
        public bool abiertoPhantomsForm;

        /// <summary>
        /// Form para ingresar las densidades y Zeff de los phantoms, cuando estos se encuentran en los DICOM
        /// </summary>
        public Phantoms2Form phantoms2Form;

        /// <summary>
        /// Indica si el form Phantoms2Form esta abierto o no
        /// </summary>
        public bool abiertoPhantoms2Form;

        /// <summary>
        /// Instancia del Form HomeForm
        /// </summary>
        public HomeForm homeForm;

        /// <summary>
        /// Variable que indica si la ventana HomeForm esta abierta o no
        /// </summary>
        public bool abiertoHomeForm;

        /// <summary>
        /// Instancia del Form ProjectForm
        /// </summary>
        public ProjectForm proyectoForm;

        /// <summary>
        /// Variable que indica si la ventana ProjectForm esta abierta o no
        /// </summary>
        public bool abiertoProyectoForm;

        /// <summary>
        /// Instancia del Proyecto actual
        /// </summary>
        public CProyecto actual;

        /// <summary>
        /// Instancia del Form SegmentacionForm
        /// </summary>
        public SegmentacionForm segmentacionForm;

        /// <summary>
        /// Variable que indica si la ventana SegmentacionForm esta abierta o no
        /// </summary>
        public bool abiertoSegmentacionForm;

        /// <summary>
        /// Variable que indica si la ventana PreviewSegForm esta abierta o no
        /// </summary>
        public bool abiertoPreviewSegForm;

        /// <summary>
        /// Instancia del form PreviewSegForm
        /// </summary>
        public PreviewSegForm previewSegForm;

        /// <summary>
        /// Instancia del form WaitingForm
        /// </summary>
        public WaitingForm waitingForm;

        /// <summary>
        /// Variable que indica si la ventana WaitingForm esta abierta o no
        /// </summary>
        public bool abiertoWaitingForm;

        /// <summary>
        /// Instancia del form SelectAreasForm
        /// </summary>
        public SelectAreasForm selecAreasForm;

        /// <summary>
        /// Variable que indica si la ventana SelectAreasForm esta abierta o no
        /// </summary>
        public bool abiertoSelectAreasForm;

        /// <summary>
        /// Instancia del form SelectAreasForm
        /// </summary>
        public SelectAreas2Form selecAreas2Form;

        /// <summary>
        /// Variable que indica si la ventana SelectAreasForm esta abierta o no
        /// </summary>
        public bool abiertoSelectAreas2Form;

        #endregion

        Point lastClick;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void AbrirProjectForm()
        {
            this.proyectoForm = new ProjectForm();
            this.proyectoForm.padre = this;
            this.proyectoForm.MdiParent = this;
            this.abiertoProyectoForm = true;
            this.proyectoForm.Show();
        }

        /// <summary>
        /// Se muestra la ventana WaitingForm, con el mensaje que se muestra como argumento
        /// </summary>
        /// <param name="mensaje"></param>
        public void ShowWaiting(string mensaje)
        {
            if (!this.abiertoWaitingForm)
            {
                waitingForm = new WaitingForm();
                waitingForm.lblTexto.Text = mensaje;
                abiertoWaitingForm = true;
                waitingForm.Show();
                Application.DoEvents();
            }
            else
            {
                waitingForm.lblTexto.Text = mensaje;
                Application.DoEvents();
            }
        }

        public void CloseWaiting()
        {
            abiertoWaitingForm = false;
            waitingForm.Close();
            Application.DoEvents();
        }

        /// <summary>
        /// Toma un CCuadrado, con coordenadas segun el PictureBox, y las transforma segun la imagen original
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        /// <param name="heighto">Alto de la imagen original</param>
        /// <param name="heigths">Alto del PictureBox</param>
        /// <returns></returns>
        public static CCuadrado CorregirPictBox2Original(CCuadrado elemento, int heighto, int heigths)
        {
            // se pasa de un tamano de imagen pequeno a grande, por lo tanto la relacion es positiva
            double relacion = (double)heighto / (double)heigths;

            // nuevas coordenadas
            elemento.x = (int)(Math.Ceiling(elemento.x * relacion));
            elemento.y = (int)(Math.Ceiling(elemento.y * relacion));
            elemento.width = (int)(elemento.width * relacion);

            return elemento;
        }

        /// <summary>
        /// Toma un CCuadrado, con coordenadas segun el original, y las transforma segun el PictureBox
        /// </summary>
        /// <param name="elemento">CCuadrado que contiene la informacion del cuadrado</param>
        /// <param name="heighto">Alto de la imagen original</param>
        /// <param name="heigths">Alto del PictureBox</param>
        /// <returns></returns>
        public static CCuadrado CorregirOriginal2PictBox(CCuadrado elemento, int heighto, int heigths)
        {
            // se pasa de un tamano de imagen grande a pequeno, por lo tanto la relacion es positiva
            double relacion = (double)heigths / (double)heighto;

            // nuevas coordenadas
            elemento.x = (int)(Math.Ceiling(elemento.x * relacion)) - 1;
            elemento.y = (int)(Math.Ceiling(elemento.y * relacion)) - 1;
            elemento.width = (int)(elemento.width * relacion) + 1;

            return elemento;
        }

        /// <summary>
        /// Toma una imagen (el slide completo) y recorta un area circular delimitada por el elemento CCuadrado
        /// </summary>
        /// <param name="source">Imagen original de la que se extraera el corte circular</param>
        /// <param name="elemento">Area de corte</param>
        /// <returns></returns>
        public static Bitmap CropCirle(Bitmap srcImage, CCuadrado elemento)
        {
            // primero se extrae el area rectangular del slide que se pasa como argumento
            Bitmap bmp = new Bitmap(elemento.width * 2, elemento.width * 2);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle selectedArea = new Rectangle();
            selectedArea.X = elemento.x - elemento.width;
            selectedArea.Y = elemento.y - elemento.width;
            selectedArea.Width = selectedArea.Height = elemento.width * 2;
            g.DrawImage(srcImage, 0, 0, selectedArea, GraphicsUnit.Pixel);

            // la imagen bmp contiene el recorte rectangular
            // ahora se debe volver un circulo

            Bitmap dstImage = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);
            g = Graphics.FromImage(dstImage);
            using (Brush br = new SolidBrush(Color.Black))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
            g.SetClip(path);
            g.DrawImage(bmp, 0, 0);

            return dstImage;
        }


        public static Bitmap ColorToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width,
            h = bmp.Height,
            r, ic, oc, bmpStride, outputStride, bytesPerPixel;
            PixelFormat pfIn = bmp.PixelFormat;
            ColorPalette palette;
            Bitmap output;
            BitmapData bmpData, outputData;

            //Create the new bitmap
            output = new Bitmap(w, h, PixelFormat.Format8bppIndexed);

            //Build a grayscale color Palette
            palette = output.Palette;
            for (int i = 0; i < 256; i++)
            {
                Color tmp = Color.FromArgb(255, i, i, i);
                palette.Entries[i] = Color.FromArgb(255, i, i, i);
            }
            output.Palette = palette;

            //No need to convert formats if already in 8 bit
            if (pfIn == PixelFormat.Format8bppIndexed)
            {
                output = (Bitmap)bmp.Clone();

                //Make sure the palette is a grayscale palette and not some other
                //8-bit indexed palette
                output.Palette = palette;

                return output;
            }

            //Get the number of bytes per pixel
            switch (pfIn)
            {
                case PixelFormat.Format24bppRgb: bytesPerPixel = 3; break;
                case PixelFormat.Format32bppArgb: bytesPerPixel = 4; break;
                case PixelFormat.Format32bppRgb: bytesPerPixel = 4; break;
                default: throw new InvalidOperationException("Image format not supported");
            }

            //Lock the images
            bmpData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly,
            pfIn);
            outputData = output.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly,
            PixelFormat.Format8bppIndexed);
            bmpStride = bmpData.Stride;
            outputStride = outputData.Stride;

            //Traverse each pixel of the image
            unsafe
            {
                byte* bmpPtr = (byte*)bmpData.Scan0.ToPointer(),
                outputPtr = (byte*)outputData.Scan0.ToPointer();

                if (bytesPerPixel == 3)
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = .299*R + .587*G + .114*B
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 3, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                            (0.299f * bmpPtr[r * bmpStride + ic] +
                            0.587f * bmpPtr[r * bmpStride + ic + 1] +
                            0.114f * bmpPtr[r * bmpStride + ic + 2]);
                }
                else //bytesPerPixel == 4
                {
                    //Convert the pixel to it's luminance using the formula:
                    // L = alpha * (.299*R + .587*G + .114*B)
                    //Note that ic is the input column and oc is the output column
                    for (r = 0; r < h; r++)
                        for (ic = oc = 0; oc < w; ic += 4, ++oc)
                            outputPtr[r * outputStride + oc] = (byte)(int)
                            ((bmpPtr[r * bmpStride + ic] / 255.0f) *
                            (0.299f * bmpPtr[r * bmpStride + ic + 1] +
                            0.587f * bmpPtr[r * bmpStride + ic + 2] +
                            0.114f * bmpPtr[r * bmpStride + ic + 3]));
                }
            }

            //Unlock the images
            bmp.UnlockBits(bmpData);
            output.UnlockBits(outputData);

            return output;
        }

        /// <summary>
        /// Toma una coordenada, X o Y con coordenadas segun el PictureBox, y las transforma segun la imagen original
        /// </summary>
        /// <param name="coordenada">CCuadrado que contiene la informacion del cuadrado</param>
        /// <param name="heighto">Alto de la imagen original</param>
        /// <param name="heigths">Alto del PictureBox</param>
        /// <returns></returns>
        public static int CorregirPictBox2Original(int coordenada, int heighto, int heigths)
        {
            // se pasa de un tamano de imagen pequeno a grande, por lo tanto la relacion es positiva
            double relacion = (double)heighto / (double)heigths;

            // nueva coordenada
            coordenada = (int)(coordenada * relacion);

            return coordenada;
        }



        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Se crea un nuevo proyecto
        /// </summary>
        public void NuevoProyecto()
        {
            // si existe alguna ventana abierta, se cierra
            if (abiertoCheckForm) checkForm.Close();
            if (abiertoHomeForm) homeForm.Close();

            // Se abre el Form para seleccionar los archivos de imagenes/dycom
            if (!abiertoNuevoProyectoForm)
            {
                nuevoProyectoForm = new NewProjectForm();
                nuevoProyectoForm.MdiParent = this;
                nuevoProyectoForm.padre = this;
                this.abiertoNuevoProyectoForm = true;
                nuevoProyectoForm.Show();
            }
            else nuevoProyectoForm.Select();
        }

        private void nuevoProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevoProyecto();
        }

        public void CerrarNuevoProyectoForm()
        {
            this.abiertoNuevoProyectoForm = false;
            this.nuevoProyectoForm = null;
        }

        public void CerrarHomeForm()
        {
            this.abiertoHomeForm = false;
            this.homeForm = null;
        }

        public void AbrirHome()
        {
            homeForm = new HomeForm();
            homeForm.MdiParent = this;
            homeForm.padre = this;
            this.abiertoHomeForm = true;
            homeForm.Show();
        }

        /// <summary>
        /// Convierte una imagen en byte[]
        /// </summary>
        /// <param name="imageLocation">Ruta de la imagen en disco</param>
        /// <returns></returns>
        public static byte[] Img2byte(string imageLocation)
        {
            byte[] imageData = null;
            FileInfo fileInfo = new FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }

        /// <summary>
        /// Convierte una imagen en byte[]
        /// </summary>
        /// <param name="imageLocation">Ruta de la imagen en disco</param>
        /// <returns></returns>
        public static byte[] Img2byte(Bitmap srcImg)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(srcImg, typeof(byte[]));
            return xByte;
        }

        public static byte[] Img2byte(Image srcImg)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(srcImg, typeof(byte[]));
            return xByte;
        }

        /// <summary>
        /// Convierte un byte[] en imagen
        /// </summary>
        /// <param name="byteArrayIn">imagen en formato de byte[]</param>
        /// <returns></returns>
        public static Image Byte2image(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                Image returnImage = Image.FromStream(ms, true);
                return returnImage;
            }
            catch { return null; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            AbrirHome();

            // rutina de preparacion de la interfaz personalizada
            this.tableLayoutPanel1.CellPaint += new TableLayoutCellPaintEventHandler(tableLayoutPanel1_CellPaint);
            menuMain.BackColor = Color.FromArgb(255, 255, 255);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.Refresh();
        }

        private void cargarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // se cierra cualquier ventana que exista abierta
            if (abiertoCheckForm) checkForm.Close();
            if (abiertoHomeForm) homeForm.Close();
            if (abiertoNuevoProyectoForm) nuevoProyectoForm.Close();
            if (abiertoProyectoForm) proyectoForm.Close();

            SeleccionarProyecto();
        }

        /// <summary>
        /// Se selecciona la ruta donde se encuentra el archivo RSP
        /// </summary>
        public bool SeleccionarProyecto()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Archivos RSP (*.rsp)|*.rsp";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ShowWaiting("Por favor espere mientras RockStatic carga el proyecto " + openFile.SafeFileName);
                AbrirProyecto(openFile.FileName);
                CloseWaiting();
                
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Con la ruta del archivo RSP se procede a cargar en memoria el proyecto
        /// </summary>
        /// <param name="ruta"></param>
        public bool AbrirProyecto(string ruta)
        {
            StreamReader sr = new StreamReader(ruta);
            string line;
            string name = "";
            string path = "";
            int count = 0;
            bool phantoms = false;
            CCuadrado areaCore = new CCuadrado();
            CCuadrado areaP1 = new CCuadrado();
            CCuadrado areaP2 = new CCuadrado();
            CCuadrado areaP3 = new CCuadrado();
            bool segmentacionDone = false;
            int toRead = 0;
            int read = 0;
            double meanHigh = 0;
            double meanLow = 0;
            double desvHigh = 0;
            double desvLow = 0;
            double densidad = 0;
            double zeff = 0;
            CPhantom tempP1 = null;
            CPhantom tempP2 = null;
            CPhantom tempP3 = null;
            int x = 0;
            int y = 0;
            int width = 0;

            while ((line = sr.ReadLine()) != null)
            {
                switch (line)
                {
                    case "NAME":
                        name = sr.ReadLine();
                        break;
                    case "PATH":
                        path = sr.ReadLine();
                        break;
                    case "COUNT":
                        count = Convert.ToInt16(sr.ReadLine());
                        break;
                    case "PHANTOMS":
                        phantoms = Convert.ToBoolean(sr.ReadLine());

                        toRead = 3; // se deben leer 3 phantoms
                        read = 0;

                        while (read < toRead)
                        {
                            line = sr.ReadLine();
                            switch (line)
                            {
                                case "PHANTOM1":
                                    line = sr.ReadLine(); // densidad
                                    densidad = Convert.ToDouble(sr.ReadLine());
                                    line = sr.ReadLine(); // zeff
                                    zeff = Convert.ToDouble(sr.ReadLine());
                                    if (!phantoms)
                                    {
                                        line = sr.ReadLine(); // media high
                                        meanHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv high
                                        desvHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // media low
                                        meanLow = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv low
                                        desvLow = Convert.ToDouble(sr.ReadLine());
                                    }
                                    tempP1 = new CPhantom(meanHigh, desvHigh, meanLow, desvLow, densidad, zeff);
                                    read++;
                                    break;
                                case "PHANTOM2":
                                    line = sr.ReadLine(); // densidad
                                    densidad = Convert.ToDouble(sr.ReadLine());
                                    line = sr.ReadLine(); // zeff
                                    zeff = Convert.ToDouble(sr.ReadLine());
                                    if (!phantoms)
                                    {
                                        line = sr.ReadLine(); // media high
                                        meanHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv high
                                        desvHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // media low
                                        meanLow = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv low
                                        desvLow = Convert.ToDouble(sr.ReadLine());
                                    }
                                    tempP2 = new CPhantom(meanHigh, desvHigh, meanLow, desvLow, densidad, zeff);
                                    read++;
                                    break;
                                case "PHANTOM3":
                                    line = sr.ReadLine(); // densidad
                                    densidad = Convert.ToDouble(sr.ReadLine());
                                    line = sr.ReadLine(); // zeff
                                    zeff = Convert.ToDouble(sr.ReadLine());
                                    if (!phantoms)
                                    {
                                        line = sr.ReadLine(); // media high
                                        meanHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv high
                                        desvHigh = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // media low
                                        meanLow = Convert.ToDouble(sr.ReadLine());
                                        line = sr.ReadLine(); // desv low
                                        desvLow = Convert.ToDouble(sr.ReadLine());
                                    }
                                    tempP3 = new CPhantom(meanHigh, desvHigh, meanLow, desvLow, densidad, zeff);
                                    read++;
                                    break;
                            }
                        }
                        break;
                    case "SEGMENTACION":
                        segmentacionDone = Convert.ToBoolean(sr.ReadLine());
                        if (segmentacionDone)
                        {
                            read = 0;
                            
                            if (phantoms) toRead = 4; // si hay phantoms en los DICOM entonces se deben leer 4 segmentaciones
                            else toRead = 1; // sino, solo se lee la segmentacion del CORE
                            
                            while (read < toRead)
                            {
                                line = sr.ReadLine();
                                switch (line)
                                {
                                    case "CORE":
                                        line = sr.ReadLine(); // X
                                        x = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // Y
                                        y = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // width
                                        width = Convert.ToInt16(sr.ReadLine());
                                        areaCore = new CCuadrado(x, y, width);
                                        read++;
                                        break;
                                    case "PHANTOM1":
                                        line = sr.ReadLine(); // X
                                        x = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // Y
                                        y = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // width
                                        width = Convert.ToInt16(sr.ReadLine());
                                        areaP1 = new CCuadrado(x, y, width);
                                        read++;
                                        break;
                                    case "PHANTOM2":
                                        line = sr.ReadLine(); // X
                                        x = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // Y
                                        y = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // width
                                        width = Convert.ToInt16(sr.ReadLine());
                                        areaP2 = new CCuadrado(x, y, width);
                                        read++;
                                        break;
                                    case "PHANTOM3":
                                        line = sr.ReadLine(); // X
                                        x = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // Y
                                        y = Convert.ToInt16(sr.ReadLine());
                                        line = sr.ReadLine(); // width
                                        width = Convert.ToInt16(sr.ReadLine());
                                        areaP3 = new CCuadrado(x, y, width);
                                        read++;
                                        break;
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            // se prepara la lista de dicoms a cargar
            List<string> high = new List<string>();
            List<string> low = new List<string>();
            for (int i = 0; i < count; i++)
            {
                high.Add(path + "\\high\\" + i);
                low.Add(path + "\\high\\" + i);
            }

            // se crea el proyecto actual
            actual = new CProyecto(name, high, low, phantoms);
            actual.folderPath = path;
            actual.folderHigh = path + "\\high";
            actual.folderLow = path + "\\low";
            actual.phantomEnDicom = phantoms;
            actual.phantom1 = new CPhantom(tempP1);
            actual.phantom2 = new CPhantom(tempP2);
            actual.phantom3 = new CPhantom(tempP3);
            actual.segmentacionDone = segmentacionDone;
            if (segmentacionDone)
            {
                actual.areaCore = new CCuadrado(areaCore);
                actual.areaPhantom1 = new CCuadrado(areaP1);
                actual.areaPhantom2 = new CCuadrado(areaP2);
                actual.areaPhantom2 = new CCuadrado(areaP2);
            }

            this.proyectoForm = new ProjectForm();
            this.abiertoProyectoForm = true;
            this.proyectoForm.padre = this;
            this.proyectoForm.MdiParent = this;
            this.proyectoForm.Show();

            return true;
        }

        public void CerrarPreviewSegForm()
        {
            this.abiertoPreviewSegForm = false;
            this.previewSegForm = null;
        }

        public void CerrarCheckForm()
        {
            this.abiertoCheckForm = false;
            this.checkForm = null;
        }

        public void CerrarPhantomForm()
        {
            this.abiertoPhantomsForm = false;
            this.phantomForm = null;
        }

        public void CerrarPhantom2Form()
        {
            this.abiertoPhantoms2Form = false;
            this.phantoms2Form = null;
        }

        public void CerrarSegmentacionForm()
        {
            this.abiertoSegmentacionForm = false;
            this.segmentacionForm = null;
        }

        public void CerrarProjectForm()
        {
            this.abiertoProyectoForm = false;
            this.proyectoForm = null;
        }

        public void CerrarSelectAreasForm()
        {
            if (this.selecAreasForm != null)
            {
                this.selecAreasForm.Close();
                this.abiertoSelectAreasForm = false;
                this.selecAreasForm = null;
            }

            if (this.selecAreas2Form != null)
            {
                this.selecAreas2Form.Close();
                this.abiertoSelectAreas2Form = false;
                this.selecAreas2Form = null;
            }
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Row == 0 || e.Row == 0)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.Green, r);
            }
            if (e.Row == 1 || e.Row == 1)
            {
                Graphics g = e.Graphics;
                Rectangle r = e.CellBounds;
                g.FillRectangle(Brushes.White, r);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                btnMaximize.BackgroundImage = RockStatic.Properties.Resources.demaximize;

            }
            else
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.BackgroundImage = RockStatic.Properties.Resources.maximize;
            }

            // se centran todos los Form abiertos
            if (abiertoCheckForm) this.checkForm.CentrarForm();
            if (abiertoHomeForm) this.homeForm.CentrarForm();
            if (abiertoNuevoProyectoForm) this.nuevoProyectoForm.CentrarForm();
            if (abiertoProyectoForm) this.proyectoForm.CentrarForm();
            if (abiertoSegmentacionForm) this.segmentacionForm.CentrarForm();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            lastClick = e.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastClick.X;
                this.Top += e.Y - lastClick.Y;
            }
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            btnMaximize_Click(sender, e);
        }
    }
}
