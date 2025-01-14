﻿using System;
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

                chartPorosidad.ChartAreas[0].AxisY.Minimum = padre.actualD.porosidad.Min();
                chartPorosidad.ChartAreas[0].AxisY.Maximum = padre.actualD.porosidad.Max();

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
                tabControl.SelectedIndex = 1;

                chartSo.Series.Clear();
                chartSo.AxisX.Clear();
                chartSo.AxisY.Clear();

                HeatSeries saturacionO = new HeatSeries();
                ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();

                // se llena la serie con la información de la matriz
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < padre.actualD.datacubos[0].meanCT.Count; i++)
                    {
                        values.Add(new HeatPoint(i+1, j, padre.actualD.satO[j, i]));                        
                    }
                }
                saturacionO.Values = values;

                saturacionO.DataLabels = false;

                GradientStopCollection gradientes = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 0, 255), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 63, 192), .25),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 127, 128), .5),  
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 191, 64), .75),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 255, 0), 1),                        
                };
                saturacionO.GradientStopCollection = gradientes;

                chartSo.Series.Add(saturacionO);

                Axis ejeY = new Axis();
                List<string> labels = new List<string>();
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    labels.Add(padre.actualD.diferenciasT[j].ToString());
                }
                ejeY.Labels = labels;
                ejeY.Title = "Tiempo (min)";
                chartSo.AxisY.Add(ejeY);

                Axis ejeX = new Axis();
                ejeX.MinValue = 1;
                ejeX.MaxValue = padre.actualD.datacubos[0].meanCT.Count;
                ejeX.Title = "Corte Transversal";
                chartSo.AxisX.Add(ejeX);
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
                tabControl.SelectedIndex = 2;

                chartSw.Series.Clear();
                chartSw.AxisX.Clear();
                chartSw.AxisY.Clear();
                
                HeatSeries saturacionW = new HeatSeries();
                ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();

                // se llena la serie con la información de la matriz
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < padre.actualD.datacubos[0].meanCT.Count; i++)
                    {
                        values.Add(new HeatPoint(i + 1, j, padre.actualD.satW[j, i]));
                    }
                }
                saturacionW.Values = values;

                saturacionW.DataLabels = false;

                GradientStopCollection gradientes = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 0, 255), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 63, 192), .25),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 127, 128), .5),  
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 191, 64), .75),
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 255, 0), 1),                        
                };
                saturacionW.GradientStopCollection = gradientes;

                chartSw.Series.Add(saturacionW);

                Axis ejeY = new Axis();
                List<string> labels = new List<string>();
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    labels.Add(padre.actualD.diferenciasT[j].ToString());
                }
                ejeY.Labels = labels;
                ejeY.Title = "Tiempo (min)";
                chartSw.AxisY.Add(ejeY);

                Axis ejeX = new Axis();
                ejeX.MinValue = 1;
                ejeX.MaxValue = padre.actualD.datacubos[0].meanCT.Count;
                ejeX.Title = "Corte Transversal";
                chartSw.AxisX.Add(ejeX);

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
                tabControl.SelectedIndex = 3;

                chartFa.Series.Clear();
                chartFa.AxisX.Clear();
                chartFa.AxisY.Clear();

                HeatSeries frente = new HeatSeries();
                ChartValues<HeatPoint> values = new ChartValues<HeatPoint>();

                // se llena la serie con la información de la matriz
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < (padre.actualD.datacubos[0].meanCT.Count-1); i++)
                    {
                        values.Add(new HeatPoint(i + 1, j, padre.actualD.frente[j, i]));
                    }
                }
                frente.Values = values;

                frente.DataLabels = false;

                GradientStopCollection gradientes = new GradientStopCollection
                {
                    new GradientStop(System.Windows.Media.Color.FromRgb(0, 0, 255), 0),
                    new GradientStop(System.Windows.Media.Color.FromRgb(63, 0, 191), .25),
                    new GradientStop(System.Windows.Media.Color.FromRgb(127, 0, 127), .5),  
                    new GradientStop(System.Windows.Media.Color.FromRgb(191, 0, 63), .75),
                    new GradientStop(System.Windows.Media.Color.FromRgb(255, 0, 0), 1),                        
                };
                frente.GradientStopCollection = gradientes;

                chartFa.Series.Add(frente);

                Axis ejeY = new Axis();
                List<string> labels = new List<string>();
                for (int j = 0; j < padre.actualD.datacubos.Count - 2; j++)
                {
                    labels.Add(padre.actualD.diferenciasT[j].ToString());
                }
                ejeY.Labels = labels;
                ejeY.Title = "Tiempo (min)";
                chartFa.AxisY.Add(ejeY);

                Axis ejeX = new Axis();
                ejeX.MinValue = 1;
                ejeX.MaxValue = padre.actualD.datacubos[0].meanCT.Count-1;
                ejeX.Title = "Corte Transversal";
                chartFa.AxisX.Add(ejeX);
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

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
