using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RockVision
{
    /// <summary>
    /// Esta clase permite el manejo de todos los dicom que componen el datacubo a procesar para la estimación de propiedades dinámicas
    /// </summary>
    public class CProyectoD
    {
        #region variables de clase

        /// <summary>
        /// centro X de la segmentacion
        /// </summary>
        public int segX = 0;

        /// <summary>
        /// centro Y de la segmentacion
        /// </summary>
        public int segY = 0;

        /// <summary>
        /// radio de la segmentacion
        /// </summary>
        public int segR = 0;

        /// <summary>
        /// nombre del proyectoV
        /// </summary>
        public string name = "";

        /// <summary>
        /// ruta en disco del archivo .RVD
        /// </summary>
        public string ruta = "";

        /// <summary>
        /// ruta de la carpeta del proyecto
        /// </summary>
        public string folder = "";

        /// <summary>
        /// datacubo que contiene los dicoms a visualizar
        /// </summary>
        public List<RockStatic.MyDataCube> datacubos = null;

        /// <summary>
        /// lista que contiene los limites de la segmentacion 2D
        /// </summary>
        public List<int> segmentacion2D = new List<int>();

        /// <summary>
        /// lista de colores para la segmentacion 2D
        /// </summary>
        public List<System.Drawing.Color> colorSeg2D = new List<System.Drawing.Color>();

        /// <summary>
        /// lista que contiene los limites de la segmentacion 3D
        /// </summary>
        public List<int> segmentacion3D = new List<int>();

        /// <summary>
        /// lista de colores para la segmentacion 3D
        /// </summary>
        public List<System.Drawing.Color> colorSeg3D = new List<System.Drawing.Color>();

        /// <summary>
        /// valor CT del crudo
        /// </summary>
        public double valorCTo=0;

        /// <summary>
        /// valor CT del agua dopada
        /// </summary>
        public double valorCTw=0;

        /// <summary>
        /// indica si ya es estimo, o no, la porosidad
        /// </summary>
        public bool porosidadEstimada = false;

        /// <summary>
        /// porosidad estimada
        /// </summary>
        public double[] porosidad = null;

        public double[] meanSo = null;

        public double[] meanSw = null;

        public double[] vPorosidadSlide = null;

        public List<double[]> mStmean = null;

        public List<double[]> mSato = null;

        public List<double[]> mSatw = null;

        public double[] mVo = null;

        public double[] mVw = null;

        public bool Soestimada = false;

        public bool Swestimada = false;

        public bool Voestimada = false;

        public bool Vwestimada = false;

        #endregion

        /// <summary>
        /// Constructor con asignacion para cargar un proyecto existente en disco
        /// </summary>
        /// <param name="path"></param>
        public CProyectoD(string path)
        {
            // se lee el archivo
            System.IO.StreamReader sr = new System.IO.StreamReader(path);

            string line="";
            
            this.ruta = path;

            List<string> datacubostemporales = new List<string>();

            while ((line = sr.ReadLine()) != null)
            {
                switch (line)
                {
                    case "NAME":
                        this.name = sr.ReadLine();
                        break;

                    case "SEGMENTACIONX":
                        this.segX = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACIONY":
                        this.segY = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACIONR":
                        this.segR = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "SEGMENTACION2D":
                        this.segmentacion2D = new List<int>();
                        while ((line = sr.ReadLine()) != "") this.segmentacion2D.Add(Convert.ToInt32(line));
                        break;

                    case "COLORSEG2D":
                        this.colorSeg2D = new List<System.Drawing.Color>();
                        while ((line = sr.ReadLine()) != "")
                        {
                            line=line.Replace("Color [", "");
                            line=line.Replace("]", "");
                            this.colorSeg2D.Add(System.Drawing.Color.FromName(line));
                        }
                        break;

                    case "SEGMENTACION3D":
                        this.segmentacion3D = new List<int>();
                        while ((line = sr.ReadLine()) != "") this.segmentacion3D.Add(Convert.ToInt32(line));
                        break;

                    case "COLORSEG3D":
                        this.colorSeg3D = new List<System.Drawing.Color>();
                        while ((line = sr.ReadLine()) != "") this.colorSeg3D.Add(System.Drawing.Color.FromName(line));
                        break;         
           
                    case "CTo":
                        this.valorCTo = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "CTw":
                        this.valorCTw = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "DATACUBOSTEMPORALES":
                        while ((line = sr.ReadLine()) != "") datacubostemporales.Add(line);
                        break;
                }
            }

            sr.Close();

            // se leen todos y cada uno de los archivos dicom que estan en la carpeta CTRo
            string folder = System.IO.Path.GetDirectoryName(path) + "\\CTRo";
            string[] nfiles = System.IO.Directory.GetFiles(folder);

            this.datacubos = new List<RockStatic.MyDataCube>();
            this.datacubos.Add(new RockStatic.MyDataCube(nfiles));

            // se segmentan los DICOM segun la informacion que se cargo desde el archivo
            for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
                this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);

            // la segmentacion transversal es TODO el DICOM
            for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubos[this.datacubos.Count - 1].widthSeg = Convert.ToInt32(this.datacubos[this.datacubos.Count-1].dataCube[0].selector.Rows.Data);


            // se leen todos y cada uno de los archivos dicom que estan en la carpeta CTRw
            folder = System.IO.Path.GetDirectoryName(path) + "\\CTRw";
            nfiles = System.IO.Directory.GetFiles(folder);

            this.datacubos.Add(new RockStatic.MyDataCube(nfiles));

            // se segmentan los DICOM segun la informacion que se cargo desde el archivo
            for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
                this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);

            // la segmentacion transversal es TODO el DICOM
            for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubos[this.datacubos.Count - 1].widthSeg = Convert.ToInt32(this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.Rows.Data);


            // se leen todos y cada uno de los archivos dicom que estan en las carpetas temporales
            for(int j=0;j<datacubostemporales.Count;j++)
            {
                folder = System.IO.Path.GetDirectoryName(path) + "\\" + datacubostemporales[j];
                nfiles = System.IO.Directory.GetFiles(folder);

                this.datacubos.Add(new RockStatic.MyDataCube(nfiles));

                // se segmentan los DICOM segun la informacion que se cargo desde el archivo
                for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
                    this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);
                
                // la segmentacion transversal es TODO el DICOM
                for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

                // hay tantos cortes horizontales como
                this.datacubos[this.datacubos.Count - 1].widthSeg = Convert.ToInt32(this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.Rows.Data);
            }
        }

        /// <summary>
        /// Constructor con asignacion para nuevos proyectos
        /// </summary>
        /// <param name="path"></param>
        /// <param name="segmentacionX"></param>
        /// <param name="segmentacionY"></param>
        /// <param name="radio"></param>
        /// <param name="rutaSo"></param>
        /// <param name="rutaSw"></param>
        /// <param name="rutaTemp"></param>
        /// <param name="valorCTo"></param>
        /// <param name="valorCTw"></param>
        /// <param name="iniDicom">basado en cero</param>
        /// <param name="finDicom">basado en cero</param>
        public CProyectoD(string path, int segmentacionX, int segmentacionY, int radio, string rutaSo, string rutaSw, string[] rutaTemp,double valorCTo, double valorCTw, int iniDicom, int finDicom)
        {
            // nombre del proyecto
            name = System.IO.Path.GetFileNameWithoutExtension(path);

            // ruta de la carpeta que contiene el proyecto
            folder = System.IO.Path.GetDirectoryName(path) + "\\" + name;

            // ruta del proyecto = ruta del folder + nombre
            ruta = folder + "\\" + name + ".rvd";

            // se crea la carpeta del proyecto
            System.IO.Directory.CreateDirectory(folder);

            // se instancia la lista de datacubos
            this.datacubos = new List<RockStatic.MyDataCube>();

            // se crea la carpeta de los dicom saturados de crudo
            string rutaDicoms = folder + "\\CTRo";
            System.IO.Directory.CreateDirectory(rutaDicoms);

            if(!CropSave(rutaDicoms,rutaSo,segmentacionX,segmentacionY,radio,iniDicom,finDicom))
            {
                System.Windows.Forms.MessageBox.Show("No se pudo crear la carpeta con los DICOMS CTRo", "Error de escritura en disco", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            // se crea la carpeta de los dicom saturados de agua
            rutaDicoms = folder + "\\CTRw";
            System.IO.Directory.CreateDirectory(rutaDicoms);

            if (!CropSave(rutaDicoms, rutaSw, segmentacionX, segmentacionY, radio, iniDicom, finDicom))
            {
                System.Windows.Forms.MessageBox.Show("No se pudo crear la carpeta con los DICOMS CTRw", "Error de escritura en disco", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }

            // se crean las carpetas de los dicoms temporales
            for (int i = 0; i < rutaTemp.Length;i++)
            {
                // se crea la carpeta de los dicom saturados de agua
                rutaDicoms = folder + "\\T" + (i+1).ToString();
                System.IO.Directory.CreateDirectory(rutaDicoms);

                if (!CropSave(rutaDicoms, rutaTemp[i], segmentacionX, segmentacionY, radio, iniDicom, finDicom))
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo crear la carpeta con los DICOMS del instante " + (i + 1).ToString(), "Error de escritura en disco", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }
            }

            // informacion de la segmentacino
            segX = segmentacionX;
            segY = segmentacionY;
            segR = radio;

            this.valorCTo = valorCTo;
            this.valorCTw = valorCTw;
            
            // se crea el archivo del proyecto, .RVD
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta, false);
            sw.Close();

            // se guarda la informacion del proyecto (info por derecto)
            Guardar();
        }

        /// <summary>
        /// Se guarda el archivo RVD con toda la información del proyecto de visualizacion
        /// </summary>
        public void Guardar()
        {
            // se abre el archivo
            System.IO.StreamWriter sw = new System.IO.StreamWriter(ruta, false);

            // se escribe el cabezote
            sw.WriteLine("PROYECTO DE ROCKVISION: VISUALIZACION DE MUESTRAS DE ROCAS");
            sw.WriteLine("COPYRIGHT CRISOSTOMO ALBERTO BARAJAS SOLANO");
            sw.WriteLine("HDSP, UIS, 2017");
            sw.WriteLine("");
            sw.WriteLine("SE PROHIBE LA MODIFICACION DE CUALQUIERA DE LOS ARCHIVOS RELACIONADOS");
            sw.WriteLine("CON EL SOFTWARE ROCKSTATIC SIN LA DEBIDA AUTORIZACION DEL AUTOR O DEL");
            sw.WriteLine("DIRECTOR DEL GRUPO HDSP");
            sw.WriteLine("=====================================================================");
            sw.WriteLine("");
            sw.WriteLine("NAME");
            sw.WriteLine(name);
            sw.WriteLine("RUTA");
            sw.WriteLine(ruta);
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONX");
            sw.WriteLine(segX.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONY");
            sw.WriteLine(segY.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACIONR");
            sw.WriteLine(segR.ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACION2D");
            for (int i = 0; i < segmentacion2D.Count; i++)
                sw.WriteLine(segmentacion2D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("COLORSEG2D");
            for (int i = 0; i < colorSeg2D.Count; i++)
                sw.WriteLine(colorSeg2D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("SEGMENTACION3D");
            for (int i = 0; i < segmentacion3D.Count; i++)
                sw.WriteLine(segmentacion3D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("COLORSEG3D");
            for (int i = 0; i < colorSeg3D.Count; i++)
                sw.WriteLine(colorSeg3D[i].ToString());
            sw.WriteLine("");
            sw.WriteLine("CTo");
            sw.WriteLine(this.valorCTo.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("CTw");
            sw.WriteLine(this.valorCTw.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("DATACUBOSTEMPORALES");
            for (int i = 2; i < datacubos.Count; i++)
                sw.WriteLine("T" + (i - 1).ToString());
            sw.WriteLine("");
            sw.Close();
        }

        /// <summary>
        /// Corta/segmenta los DICOM escogidos, los guarda en memoria y en disco
        /// </summary>
        /// <param name="pathDestino"></param>
        /// <param name="pathOrigen"></param>
        /// <param name="segmentacionX"></param>
        /// <param name="segmentacionY"></param>
        /// <param name="radio"></param>
        /// <param name="iniDicom"></param>
        /// <param name="finDicom"></param>
        /// <returns></returns>
        public bool CropSave(string pathDestino, string pathOrigen, int segmentacionX, int segmentacionY, int radio, int iniDicom, int finDicom)
        {
            try 
            {
                // se escogen solo los dicom en la carpeta entre el iniDicom y finDicom seleccionados en la ventana CheckForm
                string[] elementos = System.IO.Directory.GetFiles(pathOrigen, "*.dcm");
                List<string> elementos2 = new List<string>();
                for (int i = iniDicom; i <= finDicom; i++) elementos2.Add(elementos[i]);
                
                // se cargan los elementos copiados en el datacubo
                datacubos.Add(new RockStatic.MyDataCube(elementos2));

                int idc = datacubos.Count-1;

                // se realiza la segmentacion transversal y se guardan los dicom en disco en su nueva carpeta
                for (int i = 0; i < this.datacubos[idc].dataCube.Count; i++)
                {
                    this.datacubos[idc].dataCube[i].pixelData = this.datacubos[idc].dataCube[i].CropCTCircle(segmentacionX, segmentacionY, radio, this.datacubos[idc].dataCube[i].selector.Columns.Data, this.datacubos[idc].dataCube[i].selector.Rows.Data);
                    this.datacubos[idc].dataCube[i].dcm.Write(pathDestino +  "\\" + i.ToString("000000") + ".dcm");
                }

                // la segmentacion transversal es TODO el DICOM
                for (int i = 0; i < this.datacubos[idc].dataCube.Count; i++) this.datacubos[idc].dataCube[i].segCore = this.datacubos[idc].dataCube[i].pixelData;

                // hay tantos cortes horizontales como
                this.datacubos[idc].widthSeg = Convert.ToInt32(this.datacubos[idc].dataCube[0].selector.Rows.Data);                

                return true;
            }
            catch
            {
                return false;
            }            
        }

        /// <summary>
        /// Estima la saturacion de crudo
        /// </summary>
        /// <returns></returns>
        public bool EstimarSo()
        {
            try 
            {
                // se debe promediar el valor CT de cada slide de los datacubos temporales
                this.mStmean = new List<double[]>();
                this.mSato = new List<double[]>();

                for (int it = 2; it < datacubos.Count;it++)
                {
                    double[] Stmean = new double[datacubos[it].dataCube.Count];
                    
                    for(int i=0;i<datacubos[it].dataCube.Count;i++)
                    {
                        List<double> valoresSlide = new List<double>();

                        for(int j=0;j<datacubos[it].dataCube[i].pixelData.Count;j++)
                        {
                            if (datacubos[it].dataCube[i].pixelData[j] > 0) valoresSlide.Add(datacubos[it].dataCube[i].pixelData[j]);
                        }

                        Stmean[i] = valoresSlide.Average();
                    }

                    mStmean.Add(Stmean);

                    double[] Sato = new double[datacubos[it].dataCube.Count];
                    for (int i = 0; i < datacubos[it].dataCube.Count; i++)
                    {
                        // sato(i)=(Stmean(i)-Sw1mean(i))/(So1mean(i)-Sw1mean(i));
                        Sato[i] = (Stmean[i] - meanSw[i]) / (meanSo[i] - meanSw[i]);
                        if (Sato[i] > 1) Sato[i] = 1;
                        if (Sato[i] <0) Sato[i] = 0;
                    }

                    mSato.Add(Sato);
                }

                Soestimada = true;

                return true;
            }
            catch 
            {
                return false;
            }
        }

        /// <summary>
        /// Estima la saturacion de crudo
        /// </summary>
        /// <returns></returns>
        public bool EstimarSw()
        {
            try
            {
                // se debe promediar el valor CT de cada slide de los datacubos temporales
                this.mStmean = new List<double[]>();
                this.mSatw = new List<double[]>();

                for (int it = 2; it < datacubos.Count; it++)
                {
                    double[] Stmean = new double[datacubos[it].dataCube.Count];

                    for (int i = 0; i < datacubos[it].dataCube.Count; i++)
                    {
                        List<double> valoresSlide = new List<double>();

                        for (int j = 0; j < datacubos[it].dataCube[i].pixelData.Count; j++)
                        {
                            if (datacubos[it].dataCube[i].pixelData[j] > 0) valoresSlide.Add(datacubos[it].dataCube[i].pixelData[j]);
                        }

                        Stmean[i] = valoresSlide.Average();
                    }

                    mStmean.Add(Stmean);

                    double[] Satw = new double[datacubos[it].dataCube.Count];
                    for (int i = 0; i < datacubos[it].dataCube.Count; i++)
                    {
                        // sato(i)=(Stmean(i)-Sw1mean(i))/(So1mean(i)-Sw1mean(i));
                        Satw[i] = (Stmean[i] - meanSw[i]) / (meanSo[i] - meanSw[i]);
                        if (Satw[i] > 1) Satw[i] = 1;
                        if (Satw[i] < 0) Satw[i] = 0;
                    }

                    mSatw.Add(Satw);
                }

                Swestimada = true;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EstimarVo()
        {
            try
            {
                // se estima el volumen del voxel
                double vVoxel = datacubos[0].dataCube[0].selector.PixelSpacing.Data_[0] * datacubos[0].dataCube[0].selector.PixelSpacing.Data_[1] * datacubos[0].dataCube[0].selector.SliceThickness.Data;
                
                // el area es el de un circulo para cada slide
                double vSlide = Math.PI * Convert.ToDouble(this.segR * this.segR)*vVoxel;

                vPorosidadSlide = new double[porosidad.Length];

                for (int i = 0; i < porosidad.Length; i++) vPorosidadSlide[i] = vSlide * porosidad[i];

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Estima la porosidad efectiva usando los datacubos saturados de crudo y de agua
        /// </summary>
        /// <returns></returns>
        public bool EstimarPorosidad()
        {
            try
            {
                // se debe promediar el valor CT de cada slide del datacubo saturado de crudo y de agua
                this.porosidad = new double[datacubos[0].dataCube.Count];

                List<double> So;
                List<double> Sw;
                meanSo = new double[datacubos[0].dataCube.Count];
                meanSw = new double[datacubos[0].dataCube.Count];

                for (int i = 0; i < datacubos[0].dataCube.Count;i++)
                {
                    So = new List<double>();
                    Sw = new List<double>();

                    for (int j = 0; j < datacubos[0].dataCube[i].pixelData.Count; j++)
                    {
                        if(datacubos[0].dataCube[i].pixelData[j]>0)
                        {
                            try
                            {
                                //meanSo[i] += Convert.ToDouble(datacubos[0].dataCube[i].pixelData[j]);
                                //meanSw[i] += Convert.ToDouble(datacubos[1].dataCube[i].pixelData[j]);
                                //nPixel++;

                                So.Add(Convert.ToDouble(datacubos[0].dataCube[i].pixelData[j]));
                                Sw.Add(Convert.ToDouble(datacubos[1].dataCube[i].pixelData[j]));
                            }
                            catch 
                            {
                                MessageBox.Show("Error encontrado");
                            }
                        }
                    }

                    meanSo[i] = So.Average();
                    meanSw[i] = Sw.Average();
                    porosidad[i] = Math.Abs((So.Average() - Sw.Average()) / (valorCTo - valorCTw));
                }

                this.porosidadEstimada = true;
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
