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

        public double valorCTo=0;

        public double valorCTw=0;

        #endregion

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
            sw.WriteLine("CTr");
            sw.WriteLine(this.valorCTw.ToString("#.000"));
            sw.WriteLine("");
            sw.WriteLine("DATACUBOSTEMPORALES");
            for (int i = 0; i < datacubos.Count; i++)
                sw.WriteLine("T" + (i + 1).ToString());
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
    }
}
