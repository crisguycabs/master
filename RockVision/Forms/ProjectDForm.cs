using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace RockVision
{
    public partial class ProjectDForm : Form
    {
        #region variables de disenador

        public MainForm padre;

        System.Drawing.Point lastClick;

        #endregion

        public ProjectDForm()
        {
            InitializeComponent();
        }

        private void ProjectDForm_Load(object sender, EventArgs e)
        {
            /*
            padre.actualD.datacubos[0].CrearBitmapThread();
            pictureBox1.Image = padre.actualD.datacubos[0].dataCube[0].bmp; */

            this.label4.Text = this.Text = this.padre.actualD.name.ToUpper();
            
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            padre.AbrirHomeForm();

            this.Close();           
        }

        private void ProjectDForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            padre.CerrarProyectoDForm();
        }

        private void btnPorosidad_Click(object sender, EventArgs e)
        {
            padre.ShowWaiting("Por favor espere mientras RockVision realiza las estimaciones...");
            if(this.padre.actualD.EstimarPorosidad())
            {
                chartPorosidad.Series[0].Points.Clear();
                for (int i = 0; i < padre.actualD.porosidad.Length; i++) chartPorosidad.Series[0].Points.AddXY(i + 1, padre.actualD.porosidad[i]);

                chartPorosidad.ChartAreas[0].AxisY.Minimum = padre.actualD.porosidad.Min() - 1;
                chartPorosidad.ChartAreas[0].AxisY.Maximum = padre.actualD.porosidad.Max() + 1;

                if (chartPorosidad.ChartAreas[0].AxisY.Minimum < 0) chartPorosidad.ChartAreas[0].AxisY.Minimum = 0;
                if (chartPorosidad.ChartAreas[0].AxisY.Maximum > 100) chartPorosidad.ChartAreas[0].AxisY.Maximum = 100;

                chartPorosidad.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
                chartPorosidad.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";

                tabControl.SelectedIndex = 0;

                padre.CloseWaiting();
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar la porosidad usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }

        private void btnSo_Click(object sender, EventArgs e)
        {
            if (padre.actualD.EstimarSo())
            {
                var r = new Random();

                chartSo.Series.Add(new HeatSeries
                {
                    Values = new ChartValues<HeatPoint>
                    {
                        //X means sales man
                        //Y is the day
                        //"Jeremy Swanson"
                        new HeatPoint(0, 0, r.Next(0, 10)),
                        new HeatPoint(0, 1, r.Next(0, 10)),
                        new HeatPoint(0, 2, r.Next(0, 10)),
                        new HeatPoint(0, 3, r.Next(0, 10)),
                        new HeatPoint(0, 4, r.Next(0, 10)),
                        new HeatPoint(0, 5, r.Next(0, 10)),
                        new HeatPoint(0, 6, r.Next(0, 10)),
                        //"Lorena Hoffman"
                        new HeatPoint(1, 0, r.Next(0, 10)),
                        new HeatPoint(1, 1, r.Next(0, 10)),
                        new HeatPoint(1, 2, r.Next(0, 10)),
                        new HeatPoint(1, 3, r.Next(0, 10)),
                        new HeatPoint(1, 4, r.Next(0, 10)),
                        new HeatPoint(1, 5, r.Next(0, 10)),
                        new HeatPoint(1, 6, r.Next(0, 10)),
                        //"Robyn Williamson"
                        new HeatPoint(2, 0, r.Next(0, 10)),
                        new HeatPoint(2, 1, r.Next(0, 10)),
                        new HeatPoint(2, 2, r.Next(0, 10)),
                        new HeatPoint(2, 3, r.Next(0, 10)),
                        new HeatPoint(2, 4, r.Next(0, 10)),
                        new HeatPoint(2, 5, r.Next(0, 10)),
                        new HeatPoint(2, 6, r.Next(0, 10)),
                        //"Carole Haynes"
                        new HeatPoint(3, 0, r.Next(0, 10)),
                        new HeatPoint(3, 1, r.Next(0, 10)),
                        new HeatPoint(3, 2, r.Next(0, 10)),
                        new HeatPoint(3, 3, r.Next(0, 10)),
                        new HeatPoint(3, 4, r.Next(0, 10)),
                        new HeatPoint(3, 5, r.Next(0, 10)),
                        new HeatPoint(3, 6, r.Next(0, 10)),
                        //"Essie Nelson"
                        new HeatPoint(4, 0, r.Next(0, 10)),
                        new HeatPoint(4, 1, r.Next(0, 10)),
                        new HeatPoint(4, 2, r.Next(0, 10)),
                        new HeatPoint(4, 3, r.Next(0, 10)),
                        new HeatPoint(4, 4, r.Next(0, 10)),
                        new HeatPoint(4, 5, r.Next(0, 10)),
                        new HeatPoint(4, 6, r.Next(0, 10))
                    },
                    DataLabels = true,
 
                    //The GradientStopCollection is optional
                    //If you do not set this property, LiveCharts will set a gradient
                    GradientStopCollection = new GradientStopCollection
                    {
                        new GradientStop(System.Windows.Media.Color.FromRgb(0, 0, 255), 0),
                        new GradientStop(System.Windows.Media.Color.FromRgb(0, 63, 192), .25),
                        new GradientStop(System.Windows.Media.Color.FromRgb(0, 127, 128), .5),
                        new GradientStop(System.Windows.Media.Color.FromRgb(0, 191, 64), .75),
                        new GradientStop(System.Windows.Media.Color.FromRgb(0, 255, 0), 1)
                    }
                });

                chartSo.AxisX.Add(new Axis
                {
                    LabelsRotation = -15,
                    Labels = new[]
                    {
                        "Jeremy Wanson",
                        "Lorena Hoffman",
                        "Robyn Williamson",
                        "Carole Haynes",
                        "Essie Nelson"
                    },
                    Separator = new Separator { Step = 1 }
                });

                chartSo.AxisY.Add(new Axis
                {
                    Labels = new[]
                    {
                        "Monday",
                        "Tuesday",
                        "Wednesday",
                        "Thursday",
                        "Friday",
                        "Saturday",
                        "Sunday"
                    }
                });
            
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnSw_Click(object sender, EventArgs e)
        {
            if (padre.actualD.EstimarSw())
            {
                
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar la saturacion de agua usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnVo_Click(object sender, EventArgs e)
        {
            padre.ShowWaiting("Por favor espere mientras RockVision realiza las estimaciones...");
            if (!padre.actualD.porosidadEstimada)
            {
                if (this.padre.actualD.EstimarPorosidad())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la porosidad usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!padre.actualD.satOestimada)
            {
                if (this.padre.actualD.EstimarSo())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (this.padre.actualD.EstimarVo())
            {
                chartVo.Series[0].Points.Clear();
                for (int i = 0; i < padre.actualD.vo.Count; i++) chartVo.Series[0].Points.AddXY(this.padre.actualD.diferenciasT[i], padre.actualD.vo[i]);

                chartVo.ChartAreas[0].AxisY.Minimum = padre.actualD.vo.Min() - 1;
                chartVo.ChartAreas[0].AxisY.Maximum = padre.actualD.vo.Max() + 1;

                chartVo.ChartAreas[0].AxisX.Minimum = 0;

                //if (chartVo.ChartAreas[0].AxisY.Minimum < 0) chartVo.ChartAreas[0].AxisY.Minimum = padre.actualD.vo.Min() - 1;
                //if (chartVo.ChartAreas[0].AxisY.Maximum > 100) chartVo.ChartAreas[0].AxisY.Maximum = padre.actualD.vo.Max() + 1;

                chartVo.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
                chartVo.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";

                tabControl.SelectedIndex = 4;

                padre.CloseWaiting();                
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar el volumen de crudo atrapado usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnFrente_Click(object sender, EventArgs e)
        {
            padre.ShowWaiting("Por favor espere mientras RockVision realiza las estimaciones...");
            if (!padre.actualD.satOestimada)
            {
                if (!this.padre.actualD.EstimarSo())
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (padre.actualD.EstimarFrenteAvance())
            {
                MessageBox.Show("Frente estimado");
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            padre.CloseWaiting();
        }

        private void btnVw_Click(object sender, EventArgs e)
        {

            padre.ShowWaiting("Por favor espere mientras RockVision realiza las estimaciones...");
            if (!padre.actualD.porosidadEstimada)
            {
                if (this.padre.actualD.EstimarPorosidad())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la porosidad usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!padre.actualD.satWestimada)
            {
                if (this.padre.actualD.EstimarSw())
                {
                }
                else 
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (this.padre.actualD.EstimarVw())
            {
                chartVw.Series[0].Points.Clear();
                for (int i = 0; i < padre.actualD.vw.Count; i++) chartVw.Series[0].Points.AddXY(this.padre.actualD.diferenciasT[i], padre.actualD.vw[i]);

                chartVw.ChartAreas[0].AxisY.Minimum = padre.actualD.vw.Min() - 1;
                chartVw.ChartAreas[0].AxisY.Maximum = padre.actualD.vw.Max() + 1;

                chartVw.ChartAreas[0].AxisX.Minimum = 0;

                //if (chartVo.ChartAreas[0].AxisY.Minimum < 0) chartVo.ChartAreas[0].AxisY.Minimum = padre.actualD.vo.Min() - 1;
                //if (chartVo.ChartAreas[0].AxisY.Maximum > 100) chartVo.ChartAreas[0].AxisY.Maximum = padre.actualD.vo.Max() + 1;

                chartVw.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
                chartVw.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";

                tabControl.SelectedIndex = 5;

                padre.CloseWaiting();
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar el volumen de crudo atrapado usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnFactor_Click(object sender, EventArgs e)
        {
            padre.ShowWaiting("Por favor espere mientras RockVision realiza las estimaciones...");
            if (!padre.actualD.porosidadEstimada)
            {
                if (this.padre.actualD.EstimarPorosidad())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la porosidad usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!padre.actualD.satOestimada)
            {
                if (this.padre.actualD.EstimarSo())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar la saturacion de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (!padre.actualD.voestimado)
            {
                if (this.padre.actualD.EstimarVo())
                {
                }
                else
                {
                    padre.CloseWaiting();
                    MessageBox.Show("No fue posible estimar el volumen de crudo usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (this.padre.actualD.EstimarFr())
            {
                chartFr.Series[0].Points.Clear();
                for (int i = 0; i < padre.actualD.fr.Count; i++) chartFr.Series[0].Points.AddXY(this.padre.actualD.diferenciasT[i], padre.actualD.fr[i]);

                chartFr.ChartAreas[0].AxisY.Minimum = padre.actualD.fr.Min();
                chartFr.ChartAreas[0].AxisY.Maximum = padre.actualD.fr.Max();

                chartFr.ChartAreas[0].AxisX.Minimum = 0;

                //if (chartVo.ChartAreas[0].AxisY.Minimum < 0) chartVo.ChartAreas[0].AxisY.Minimum = padre.actualD.vo.Min() - 1;
                //if (chartVo.ChartAreas[0].AxisY.Maximum > 100) chartVo.ChartAreas[0].AxisY.Maximum = padre.actualD.vo.Max() + 1;

                chartFr.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
                chartFr.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";

                tabControl.SelectedIndex = 6;

                padre.CloseWaiting();
            }
            else
            {
                padre.CloseWaiting();
                MessageBox.Show("No fue posible estimar el factor de recobro usando los cubos de datos seleccionados", "Error al estimar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnGetRMN_Click(object sender, EventArgs e)
        {
            if (padre.abiertoGetRMNform)
            {
                // esta abierto

                padre.getRMNform.Select();
            }
            else
            {
                // esta cerrado, se crea nuevo
                padre.getRMNform = new GetRMN();
                padre.getRMNform.padre = this.padre;
                padre.getRMNform.MdiParent = this.padre;

                padre.abiertoGetRMNform = true;

                padre.getRMNform.Show();
            }
        }

        public void DibujarPorosidadRMN()
        {
            chartPorosidad.Series[1].Points.Clear();
            
            chartPorosidad.Series[1].Points.AddXY(1,padre.porRMN);
            chartPorosidad.Series[1].Points.AddXY(padre.actualD.datacubos[0].meanCT.Count, padre.porRMN);

            chartPorosidad.ChartAreas[0].AxisX.Minimum = 0;
            chartPorosidad.ChartAreas[0].AxisX.Maximum = padre.actualD.datacubos[0].meanCT.Count+1;

            chartPorosidad.ChartAreas[0].AxisX.LabelStyle.Format = "#.##";
            chartPorosidad.ChartAreas[0].AxisY.LabelStyle.Format = "#.##";
        }
    }
}
