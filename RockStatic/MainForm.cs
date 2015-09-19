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

namespace RockStatic
{
    /// <summary>
    /// Form principal. Contiene los tipos de datos globales y métodos estáticos
    /// </summary>
    public partial class MainForm : Form
    {
        #region Variables de Diseñador

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
        /// Variable que indica si la ventana CheckForm esta abierta o no
        /// </summary>
        public bool abiertoCheckForm;

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

        #endregion

        Point lastClick;

        public MainForm()
        {
            InitializeComponent();
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
        /// <param name="elemento"></param>
        /// <param name="heighto"></param>
        /// <param name="heigths"></param>
        /// <returns></returns>
        public static CCuadrado CorregirOriginal2PictBox(CCuadrado elemento, int heighto, int heigths)
        {
            // se pasa de un tamano de imagen grande a pequeno, por lo tanto la relacion es positiva
            double relacion = (double)heigths / (double)heighto;

            // nuevas coordenadas
            elemento.x = (int)(Math.Ceiling(elemento.x * relacion));
            elemento.y = (int)(Math.Ceiling(elemento.y * relacion));
            elemento.width = (int)(elemento.width * relacion);

            return elemento;
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
                AbrirProyecto(openFile.FileName);
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
            string name = Path.GetFileNameWithoutExtension(ruta);

            actual = new CProyecto();
            actual.name = name;
            
            string folder = Path.GetDirectoryName(ruta);
            actual.SetFolderPath(folder);

            string linea;

            // se lee el archivo RSP
            StreamReader sr = new StreamReader(ruta);
            
            while ((linea = sr.ReadLine()) != null)
            {
                switch (linea)
                {
                    case "SEGMENTACION HIGH":
                        actual.SetSegmentacionHigh(sr.ReadLine() == "True");
                        break;
                    case "SEGMENTACION LOW":
                        actual.SetSegmentacionLow(sr.ReadLine() == "True");
                        break;
                    case "AREAS HIGH":
                        actual.SetAreasHigh(sr.ReadLine() == "True");
                        break;
                    case "AREAS LOW":
                        actual.SetAreasLow(sr.ReadLine() == "True");
                        break;
                    case "COUNT":
                        actual.count = Convert.ToInt32(sr.ReadLine());
                        break;
                    default:
                        break;                        
                }
            }

            // se cierra el archivo SRP
            sr.Close();

            
            // primero se verifica que exista el archivo RSPH
            if (!File.Exists(actual.GetFolderPath() + name + ".rsph"))
            {
                MessageBox.Show("No existe el archivo " + name + ".rsph asociado al proyecto.\nNo se puede continuar con el proceso de carga.","Error al abrir el proyecto "+name,MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            // se verifica que exista el folder HIGH
            if(!Directory.Exists(actual.GetFolderHigh()))
            {
                MessageBox.Show("No existe la carpeta con las imagenes HIGH.\nVerifique la ruta indicada e intentelo de nuevo.\n\n" + actual.GetFolderHigh(), "Error al abrir el proyecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // se verifica que exista el archivo RSPL
            if (!File.Exists(actual.GetFolderPath() + name + ".rspl"))
            {
                MessageBox.Show("No existe el archivo " + name + ".rspl asociado al proyecto.\nNo se puede continuar con el proceso de carga.", "Error al abrir el proyecto " + name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // se verifica que exista el folder HIGH
            if (!Directory.Exists(actual.GetFolderLow()))
            {
                MessageBox.Show("No existe la carpeta con las imagenes LOW.\nVerifique la ruta indicada e intentelo de nuevo.\n\n" + actual.GetFolderLow(), "Error al abrir el proyecto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // se leen los elementos HIGH que estan en la carpeta HIGH, dentro de la ruta del proyect
            List<byte[]> files = new List<byte[]>();
            for (int i = 0; i < actual.count; i++) files.Add(File.ReadAllBytes(actual.GetFolderHigh() + i));
            actual.SetHigh(files);

            // se leen los elementos LOW que estan en la carpeta LOW, dentro de la ruta del proyect
            files = new List<byte[]>();
            for (int i = 0; i < actual.count; i++) files.Add(File.ReadAllBytes(actual.GetFolderLow() + i));
            actual.SetHigh(files);            

            this.proyectoForm = new ProjectForm();
            this.abiertoProyectoForm = true;
            this.proyectoForm.padre = this;
            this.proyectoForm.MdiParent = this;
            this.proyectoForm.Show();

            // se cargo satisfactoriamente el proyecto
            return true;
        }

        public void CerrarCheckForm()
        {
            this.abiertoCheckForm = false;
            this.checkForm = null;
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
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid, Color.DarkGreen, 2, ButtonBorderStyle.Solid);
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
