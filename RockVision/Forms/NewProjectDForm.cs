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
    public partial class NewProjectDForm : Form
    {
        #region variables de diseñador

        Point lastClick;

        public MainForm padre;

        #endregion

        public NewProjectDForm()
        {
            InitializeComponent();
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

        private void NewProjectDForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.padre.CerrarNewProjectDForm();
        }

        private void btnSelCTRo_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (Directory.GetFiles(fbd.SelectedPath, "*.dcm").Length == 0)
                {
                    MessageBox.Show("La ruta carpeta no contiene archivos DICOM.\r\n\r\nPor favor, escoga otra carpeta.", "Error de lectura de DICOMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtCTRo.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void btnSelCTRw_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (Directory.GetFiles(fbd.SelectedPath, "*.dcm").Length == 0)
                {
                    MessageBox.Show("La ruta carpeta no contiene archivos DICOM.\r\n\r\nPor favor, escoga otra carpeta.", "Error de lectura de DICOMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    txtCTRw.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        private void btnAddCTtemp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (Directory.GetFiles(fbd.SelectedPath, "*.dcm").Length == 0)
                {
                    MessageBox.Show("La ruta carpeta no contiene archivos DICOM.\r\n\r\nPor favor, escoga otra carpeta.", "Error de lectura de DICOMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    lstCTtemp.Items.Add(fbd.SelectedPath.ToString());
                    lstCTtemp.SelectedIndex = lstCTtemp.Items.Count - 1;
                }
            }
        }

        private void btnDelCTtemp_Click(object sender, EventArgs e)
        {
            try
            {
                lstCTtemp.Items.RemoveAt(lstCTtemp.SelectedIndex);
                lstCTtemp.SelectedIndex = lstCTtemp.Items.Count - 1;
            }
            catch
            {
            }
        }

        private void btnUpCTtemp_Click(object sender, EventArgs e)
        {
            try
            {
                int pos = lstCTtemp.SelectedIndex;
                string temp = lstCTtemp.SelectedItem.ToString();

                lstCTtemp.Items.Insert(pos - 1, temp);
                lstCTtemp.Items.RemoveAt(pos + 1);

                lstCTtemp.SelectedIndex = pos - 1;
            }
            catch
            {
            }
        }

        private void btnDownCTtemp_Click(object sender, EventArgs e)
        {
            try
            {
                int pos = lstCTtemp.SelectedIndex;
                string temp = lstCTtemp.SelectedItem.ToString();

                lstCTtemp.Items.Insert(pos + 2, temp);
                lstCTtemp.Items.RemoveAt(pos);

                lstCTtemp.SelectedIndex = pos +1;
            }
            catch
            {
            }
        }
    }
}
