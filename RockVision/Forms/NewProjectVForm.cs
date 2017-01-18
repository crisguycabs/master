using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockVision
{
    public partial class NewProjectVForm : Form
    {
        #region variales de diseñador

        /// <summary>
        /// Referencia al MainForm padre
        /// </summary>
        public MainForm padre;

        /// <summary>
        /// lista de los nombres de los dicom que se van a mostrar en pantalla
        /// </summary>
        public List<string> elementos=null;

        #endregion

        public NewProjectVForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Se establece el contenido de la forma
        /// </summary>
        public void SetForm()
        {
            // se limpa el combobox
            lstElementos.Items.Clear();

            // se limpia la imagen del pictbox
            pictElemento.Image = null;

            // se llena el combobox
            for (int i = 0; i < elementos.Count; i++) lstElementos.Items.Add((string)elementos[i]);

            lstElementos.SelectedIndex = 0;
        }
    }
}
