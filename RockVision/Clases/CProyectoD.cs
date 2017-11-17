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
        /// indica si ya es estimo, o no, la saturacion de crudo
        /// </summary>
        public bool satOestimada = false;

        /// <summary>
        /// indica si ya es estimo, o no, la saturacion de agua
        /// </summary>
        public bool satWestimada = false;

        /// <summary>
        /// indica si ya es estimo, o no, el frente de avance
        /// </summary>
        public bool frenteEstimado = false;

        /// <summary>
        /// Primer slide a usar
        /// </summary>
        public int dcmInicio = 0;

        /// <summary>
        /// Ultimo slide a usar
        /// </summary>
        public int dcmFin = 0;

        /// <summary>
        /// indica si ya se estimo, o no, el volumen de crudo en cada instante de tiempo
        /// </summary>
        public bool voestimado = false;

        /// <summary>
        /// indica si ya se estimo, o no, el volumen de crudo en cada instante de tiempo
        /// </summary>
        public bool vwestimado = false;

        /// <summary>
        /// porosidad estimada
        /// </summary>
        public double[] porosidad = null;

        /// <summary>
        /// saturacion estimada de crudo al interior de la roca
        /// </summary>
        public double[,] satO = null;

        /// <summary>
        /// Espaciado en X del voxel
        /// </summary>
        public double voxelX = 0;

        /// <summary>
        /// Espaciado en Y del voxel
        /// </summary>
        public double voxelY = 0;

        /// <summary>
        /// Espaciado en Z del voxel
        /// </summary>
        public double voxelZ = 0;

        /// <summary>
        /// Numero de pixeles en el área segmentada
        /// </summary>
        public double nVoxel = 0;

        /// <summary>
        /// saturacion estimada de agua al interior de la roca
        /// </summary>
        public double[,] satW = null;

        public double[,] frente = null;

        public bool Soestimada = false;

        public bool Swestimada = false;

        public List<double> vo = null;

        public List<double> vw = null;

        public List<double> fr = null;

        public bool factorEstimado = false;

        public List<DateTime> marcasTiempo = null;

        public List<double> diferenciasT = null;

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

                    case "VoxelX":
                        this.voxelX = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "VoxelY":
                        this.voxelY = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "VoxelZ":
                        this.voxelZ = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "Nvoxel":
                        this.nVoxel = Convert.ToDouble(sr.ReadLine());
                        break;

                    case "DATACUBOSTEMPORALES":
                        while ((line = sr.ReadLine()) != "") datacubostemporales.Add(line);
                        break;

                    case "INICIO":
                        this.dcmInicio = Convert.ToInt32(sr.ReadLine());
                        break;

                    case "FIN":
                        this.dcmFin = Convert.ToInt32(sr.ReadLine());
                        break;
                }
            }

            sr.Close();

            // se leen todos y cada uno de los archivos dicom que estan en la carpeta CTRo
            string folder = System.IO.Path.GetDirectoryName(path) + "\\CTRo";
            string[] nfiles = System.IO.Directory.GetFiles(folder);

            this.datacubos = new List<RockStatic.MyDataCube>();
            // se cargan TODOS los dicom
            this.datacubos.Add(new RockStatic.MyDataCube(nfiles));
            // se obtiene el valor medio por dicom
            this.datacubos[this.datacubos.Count - 1].meanCT = new List<double>();

            double[] resultados = new double[2];
            for (int i = dcmInicio; i <= dcmFin; i++)
            {
                resultados = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropMeanCTRVD(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);
                this.datacubos[this.datacubos.Count - 1].meanCT.Add(resultados[0]);
            }
            this.nVoxel = resultados[1];

            // se borran los dicom
            this.datacubos[this.datacubos.Count - 1].dataCube = null;
            GC.Collect();

            // se segmentan los DICOM segun la informacion que se cargo desde el archivo
            // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
            //    this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);

            // la segmentacion transversal es TODO el DICOM
            // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubos[this.datacubos.Count - 1].widthSegCore = this.segR * 2;


            // se leen todos y cada uno de los archivos dicom que estan en la carpeta CTRw
            folder = System.IO.Path.GetDirectoryName(path) + "\\CTRw";
            nfiles = System.IO.Directory.GetFiles(folder);

            // se cargan TODOS los dicom
            this.datacubos.Add(new RockStatic.MyDataCube(nfiles));
            // se obtiene el valor medio por dicom
            this.datacubos[this.datacubos.Count - 1].meanCT = new List<double>();
            for (int i = dcmInicio; i <= dcmFin; i++)
            {
                resultados = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropMeanCTRVD(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);
                this.datacubos[this.datacubos.Count - 1].meanCT.Add(resultados[0]);
            }
            // se borran los dicom
            this.datacubos[this.datacubos.Count - 1].dataCube = null;
            GC.Collect();

            // se segmentan los DICOM segun la informacion que se cargo desde el archivo
            // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
            //    this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);

            // la segmentacion transversal es TODO el DICOM
            // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

            // hay tantos cortes horizontales como
            this.datacubos[this.datacubos.Count - 1].widthSegCore = this.segR * 2;

            this.marcasTiempo = new List<DateTime>();
            
            // se leen todos y cada uno de los archivos dicom que estan en las carpetas temporales
            for(int j=0;j<datacubostemporales.Count;j++)
            {
                folder = System.IO.Path.GetDirectoryName(path) + "\\" + datacubostemporales[j];
                nfiles = System.IO.Directory.GetFiles(folder);

                // se cargan TODOS los dicom
                this.datacubos.Add(new RockStatic.MyDataCube(nfiles));
                // se obtiene el valor medio por dicom
                this.datacubos[this.datacubos.Count - 1].meanCT = new List<double>();
                for (int i = dcmInicio; i <= dcmFin; i++) 
                {
                    resultados = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropMeanCTRVD(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);
                    this.datacubos[this.datacubos.Count - 1].meanCT.Add(resultados[0]);
                }

                this.marcasTiempo.Add(new DateTime(this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyDate.Data.Value.Year, this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyDate.Data.Value.Month, this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyDate.Data.Value.Day, this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyTime.Data.Value.Hour, this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyTime.Data.Value.Minute, this.datacubos[this.datacubos.Count - 1].dataCube[0].selector.StudyTime.Data.Value.Second));
                
                // se borran los dicom
                this.datacubos[this.datacubos.Count - 1].dataCube = null;
                GC.Collect();

                // se segmentan los DICOM segun la informacion que se cargo desde el archivo
                // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++)
                //    this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData = this.datacubos[this.datacubos.Count - 1].dataCube[i].CropCTCircle(segX, segY, segR, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Columns.Data, this.datacubos[this.datacubos.Count - 1].dataCube[i].selector.Rows.Data);
                
                // la segmentacion transversal es TODO el DICOM
                // for (int i = 0; i < this.datacubos[this.datacubos.Count - 1].dataCube.Count; i++) this.datacubos[this.datacubos.Count - 1].dataCube[i].segCore = this.datacubos[this.datacubos.Count - 1].dataCube[i].pixelData;

                // hay tantos cortes horizontales como
                this.datacubos[this.datacubos.Count - 1].widthSegCore = this.segR * 2;
            }

            this.diferenciasT=new List<double>();
            for (int i = 0; i < marcasTiempo.Count; i++) diferenciasT.Add(Math.Round(marcasTiempo[i].Subtract(marcasTiempo[0]).TotalMinutes));
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

            this.marcasTiempo = new List<DateTime>();

            if(!CropSave(rutaDicoms,rutaSo,segmentacionX,segmentacionY,radio,iniDicom,finDicom, true))
            {
                System.Windows.Forms.MessageBox.Show("No se pudo crear la carpeta con los DICOMS CTRo", "Error de escritura en disco", System.Windows.Forms.MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }            

            // se crea la carpeta de los dicom saturados de agua
            rutaDicoms = folder + "\\CTRw";
            System.IO.Directory.CreateDirectory(rutaDicoms);

            if (!CropSave(rutaDicoms, rutaSw, segmentacionX, segmentacionY, radio, iniDicom, finDicom, true))
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

                if (!CropSave(rutaDicoms, rutaTemp[i], segmentacionX, segmentacionY, radio, iniDicom, finDicom, false))
                {
                    System.Windows.Forms.MessageBox.Show("No se pudo crear la carpeta con los DICOMS del instante " + (i + 1).ToString(), "Error de escritura en disco", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }
            }

            this.diferenciasT = new List<double>();
            for (int i = 0; i < marcasTiempo.Count; i++) diferenciasT.Add(Math.Round(marcasTiempo[i].Subtract(marcasTiempo[0]).TotalMinutes));

            // informacion de la segmentacino
            segX = segmentacionX;
            segY = segmentacionY;
            segR = radio;

            this.valorCTo = valorCTo;
            this.valorCTw = valorCTw;

            this.dcmInicio = iniDicom;
            this.dcmFin = finDicom;

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
            sw.WriteLine("INICIO");
            sw.WriteLine(dcmInicio.ToString());
            sw.WriteLine("");
            sw.WriteLine("FIN");
            sw.WriteLine(dcmFin.ToString());
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
            sw.WriteLine("VoxelX");
            sw.WriteLine(this.voxelX.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("VoxelY");
            sw.WriteLine(this.voxelY.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("VoxelZ");
            sw.WriteLine(this.voxelZ.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("Nvoxel");
            sw.WriteLine(this.nVoxel.ToString("#.000"));
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
        public bool CropSave(string pathDestino, string pathOrigen, int segmentacionX, int segmentacionY, int radio, int iniDicom, int finDicom, bool datacuboReferencia)
        {
            try 
            {
                // se escogen solo los dicom en la carpeta entre el iniDicom y finDicom seleccionados en la ventana CheckForm
                string[] elementos = System.IO.Directory.GetFiles(pathOrigen, "*.dcm");
                List<string> elementos2 = new List<string>();
                for (int i = 0; i < elementos.Length; i++) elementos2.Add(elementos[i]);
                
                // se cargan los elementos copiados en el datacubo
                datacubos.Add(new RockStatic.MyDataCube(elementos2));

                int idc = datacubos.Count-1;
                this.datacubos[idc].meanCT = new List<double>();

                this.voxelX = datacubos[idc].dataCube[0].selector.PixelSpacing.Data_[0];
                this.voxelY = datacubos[idc].dataCube[0].selector.PixelSpacing.Data_[1];
                this.voxelZ = datacubos[idc].dataCube[0].selector.SliceThickness.Data;

                // se envian todos los dicom a disco duro
                for (int i = 0; i < this.datacubos[idc].dataCube.Count; i++)
                {
                    this.datacubos[idc].dataCube[i].dcm.Write(pathDestino +  "\\" + i.ToString("000000") + ".dcm");
                }

                if (!datacuboReferencia)
                {
                    this.marcasTiempo.Add(new DateTime(this.datacubos[idc].dataCube[0].selector.StudyDate.Data.Value.Year, this.datacubos[idc].dataCube[0].selector.StudyDate.Data.Value.Month, this.datacubos[idc].dataCube[0].selector.StudyDate.Data.Value.Day, this.datacubos[idc].dataCube[0].selector.StudyTime.Data.Value.Hour, this.datacubos[idc].dataCube[0].selector.StudyTime.Data.Value.Minute, this.datacubos[idc].dataCube[0].selector.StudyTime.Data.Value.Second));
                    // string date = "temp";                    
                }

                double[] resultado = new double[2];

                // se segmentan solo los dicom que se marcaron como requeridos
                for (int i = iniDicom; i <= finDicom; i++)
                {
                    resultado=this.datacubos[idc].dataCube[i].CropMeanCTRVD(segmentacionX, segmentacionY, radio, this.datacubos[idc].dataCube[i].selector.Columns.Data, this.datacubos[idc].dataCube[i].selector.Rows.Data);
                    this.datacubos[idc].meanCT.Add(resultado[0]);                    
                }
                this.nVoxel = resultado[1];

                // la segmentacion transversal es TODO el DICOM
                // for (int i = 0; i < this.datacubos[idc].dataCube.Count; i++) this.datacubos[idc].dataCube[i].segCore = this.datacubos[idc].dataCube[i].pixelData;

                this.datacubos[idc].diametroSegRV = radio * 2;

                // hay tantos cortes horizontales como
                this.datacubos[idc].widthSegCore = Convert.ToInt32(this.datacubos[idc].diametroSegRV);    

                // liberacion de memoria
                this.datacubos[idc].dataCube = null;
                GC.Collect();
       
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
                this.satO = new double[datacubos.Count - 2, datacubos[0].meanCT.Count];

                for (int j = 0; j < datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < datacubos[0].meanCT.Count; i++)
                    {
                        satO[j, i] = 100 * (datacubos[j+2].meanCT[i] - datacubos[1].meanCT[i]) / (valorCTo - valorCTw);
                    }
                }

                this.satOestimada=true;
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
                this.satW = new double[datacubos.Count - 2, datacubos[0].meanCT.Count];

                for (int j = 0; j < datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < datacubos[0].meanCT.Count; i++)
                    {
                        satW[j, i] = 100 * (datacubos[j + 2].meanCT[i] - datacubos[0].meanCT[i]) / (valorCTw - valorCTo);
                    }
                }

                this.satWestimada = true;
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
                this.porosidad = new double[datacubos[0].meanCT.Count];

                for (int i = 0; i < porosidad.Length; i++)
                {
                    porosidad[i] = 100 * (datacubos[0].meanCT[i] - datacubos[1].meanCT[i]) / (valorCTo - valorCTw);
                }

                this.porosidadEstimada = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Calcula la derivada ESPACIAL de la saturacion de crudo
        /// </summary>
        /// <returns></returns>
        public bool EstimarFrenteAvance()
        {
            try
            {
                this.frente = new double[datacubos.Count - 2, datacubos[0].meanCT.Count-1];

                for (int j = 0; j < datacubos.Count - 2; j++)
                {
                    for (int i = 0; i < (datacubos[0].meanCT.Count-1); i++)
                    {
                        frente[j, i] = Math.Abs(satO[j, i + 1] - satO[j, i]);
                    }
                }

                this.frenteEstimado = true;
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
                double volSlide = this.voxelX * this.voxelY * this.voxelZ * this.nVoxel;
                double vporSlide = 0;
                this.vo = new List<double>();
                
                for (int j = 0; j < (datacubos.Count-2); j++)
                {
                    double temp = 0;
                    for (int i = 0; i < datacubos[0].meanCT.Count; i++)
                    {
                        vporSlide = volSlide * porosidad[i]/100;

                        temp += vporSlide * satO[j, i]/100;
                    }
                    this.vo.Add(temp);
                }

                this.voestimado = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EstimarVw()
        {
            try
            {
                double volSlide = this.voxelX * this.voxelY * this.voxelZ * this.nVoxel;
                double vporSlide = 0;
                this.vw = new List<double>();

                for (int j = 0; j < (datacubos.Count - 2); j++)
                {
                    double temp = 0;
                    for (int i = 0; i < datacubos[0].meanCT.Count; i++)
                    {
                        vporSlide = volSlide * porosidad[i];

                        temp += vporSlide * satW[j, i]/100;
                    }
                    this.vw.Add(temp);
                }

                this.vwestimado = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EstimarFr()
        {
            try
            {
                this.fr = new List<double>();

                for (int j = 0; j < (datacubos.Count - 2); j++)
                {
                    fr.Add(100*(vo[0]-vo[j])/vo[0]);
                }

                this.factorEstimado = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
