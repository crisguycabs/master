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
    public partial class CheckForm : Form
    {
        #region variables de clase

        /// <summary>
        /// Copia local de los elementos a mostrar
        /// </summary>
        public List<string> temp;

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// Referencia que indica si se han cargado los archivos HIGH (true) o los archivos LOW (false)
        /// </summary>
        public bool filesHigh;

        /// <summary>
        /// Referencia al form NewProjectForm que invoca
        /// </summary>
        public NewProjectForm newProjectForm;

        #endregion

        Point lastClick;

        public CheckForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Se crea una copia (local) de los elementos a mostrar
        /// </summary>
        public void SetList(List<string> lista)
        {
            temp = new List<string>();
            for(int i=0;i<lista.Count;i++) temp.Add((string)lista[i]);            
        }

        private void CheckForm_Load(object sender, EventArgs e)
        {
            SetForm();
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
            // se llena el listbox
            lstElementos.Items.Clear();
            for (int i = 0; i < temp.Count; i++) lstElementos.Items.Add(GetNameFile((string)temp[i]));
            
            // se reestablece el TrackBar
            trackElementos.Minimum = 1;
            trackElementos.Maximum = temp.Count;
            trackElementos.Value = 1;
            lstElementos.SelectedIndex = 0;

            // se pinta la primera imagen
            pictElemento.Image=new Bitmap((string)temp[0]);
        }

        private void trackElementos_ValueChanged(object sender, EventArgs e)
        {
            pictElemento.Image = null;
            pictElemento.Image = new Bitmap((string)temp[trackElementos.Value-1]);
            lstElementos.ClearSelected();
            lstElementos.SelectedIndex = trackElementos.Value - 1;
        }

        /// <summary>
        /// Se pasa una ruta completa y se extrae el nombre del archivo
        /// </summary>
        /// <param name="file">Ruta completa del archivo</param>
        /// <returns>Nombre del archivo, con su extension</returns>
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

        private void lstElementos_DoubleClick(object sender, EventArgs e)
        {
            trackElementos.Value = lstElementos.SelectedIndex + 1;
        }

        public void btnGuardar_Click(object sender, EventArgs e)
        {
            newProjectForm.SetTemp(temp, filesHigh);
            this.Close();
        }

        private void CheckForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarCheckForm();
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = lstElementos.SelectedIndices;
            
            // se verifica que el primer, o unico elemento, seleccionado no sea el elemento 0
            if (indices[0] != 0)
            {
                List<string> temp2 = new List<string>();
                int primer = indices[0];

                // se CORTAN los elementos del List temp y se pasan al List temp2
                for (int i = 0; i < indices.Count; i++)
                {
                    temp2.Add((string)temp[primer]);
                    temp.RemoveAt(primer);
                }

                // se insertan los elementos CORTADOS una posicion mas arriba
                primer--;
                for (int i = 0; i < temp2.Count; i++)
                {
                    temp.Insert(primer, temp2[i]);
                    primer++;
                }

                primer = indices[0] - 1;
                SetForm();
                trackElementos.Value = primer+1;
            }
        }

        private void btnBajar_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection indices = lstElementos.SelectedIndices;
            
            // se verifica que el ultimo, o unico elemento, seleccionado no sea el ultimo elemento del ListBox
            if (indices[indices.Count-1] != lstElementos.Items.Count)
            {
                List<string> temp2 = new List<string>();
                int primer = indices[0];

                // se CORTAN los elementos del List temp y se pasan al List temp2
                for (int i = 0; i < indices.Count; i++)
                {
                    temp2.Add((string)temp[primer]);
                    temp.RemoveAt(primer);
                }

                // se insertan los elementos CORTADOS una posicion mas abajo
                primer++;
                for (int i = 0; i < temp2.Count; i++)
                {
                    temp.Insert(primer, temp2[i]);
                    primer++;
                }

                primer = indices[0] + 1;
                SetForm();
                trackElementos.Value = primer+1;
            }
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
            //this.Location = new System.Drawing.Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.Location = new System.Drawing.Point((MdiParent.Width - this.Width) / 2, (int)((MdiParent.Height - this.Height) * 0.8 / 2));
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
    }
}
