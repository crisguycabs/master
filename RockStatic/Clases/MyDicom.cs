﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvilDICOM.Core;
using EvilDICOM.Core.Element;
using EvilDICOM.Core.Selection;
using System.Drawing;
using System.Drawing.Imaging;
 
namespace RockStatic
{
    /// <summary>
    /// Wrapper para la libreria EvilDICOM (Rex Cardan, http://www.rexcardan.com/evildicom/) con modificaciones propias para ser manejadas por la suite RockUIS
    /// Un objeto MyDicom maneja un objeto de DICOM según la estructura de EvilDicom. Se incluyen los metodos para modificar el DICOM y guardarlo en disco
    /// </summary>
    public class MyDicom
    {
        #region variables de clase

        /// <summary>
        /// Minimo valor del DICOM. Es necesario mantener este valor para efectos de codificación de los pixel_data y escritura en disco
        /// </summary>
        public int minPixVal = 0;
 
        /// <summary>
        /// Indica si el DICOM contienen signo o no. Es necesario mantener este valor para efectos de codificación de los pixel_data y escritura en disco
        /// </summary>
        public bool signedImage = false;
 
        /// <summary>
        /// Imagen normalizada asociadas al objeto DICOM
        /// </summary>
        public Bitmap bmp = null;
 
        /// <summary>
        /// List que contiene la informacion de pixeles CT (ushort) para el DICOM cargado        
        /// </summary>
        public List<ushort> pixelData = null;
 
        /// <summary>
        /// List que contiene los pixeles CT de la segmentacion transversal del core del DICOM
        /// </summary>
        public List<ushort> segCore = null;

        /// <summary>
        /// List que contiene los pixeles CT de la segmentacion transversal del phantom1 del DICOM
        /// </summary>
        public List<ushort> segPhantom1 = null;

        /// <summary>
        /// List que contiene los pixeles CT de la segmentacion transversal del phantom2 del DICOM
        /// </summary>
        public List<ushort> segPhantom2 = null;

        /// <summary>
        /// List que contiene los pixeles CT de la segmentacion transversal del phantom3 del DICOM
        /// </summary>
        public List<ushort> segPhantom3 = null;
 
        /// <summary>
        /// Objeto DICOM cargado
        /// </summary>
        public DICOMObject dcm = null;
 
        /// <summary>
        /// Selector del objeto DICOM cargado
        /// </summary>
        public DICOMSelector selector = null;

        /// <summary>
        /// Guarda la informacion del histograma del slide
        /// </summary>
        public uint[] histograma = null;

        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MyDicom()
        {
            minPixVal = 0;
            signedImage = false;
            bmp = null;
            pixelData = new List<ushort>();
            dcm = null;
            selector = null;
        }
 
        /// <summary>
        /// Constructor con asignación. Lee la ruta del DICOM, lo lee y decodifica la informacion de pixeles. NO NORMALIZA
        /// </summary>
        /// <param name="ruta">Ruta del DICOM a leer</param>
        public MyDicom(string ruta)
        {
            dcm = DICOMObject.Read(@ruta);
            selector = dcm.GetSelector();
            pixelData = Byte2Pixels16((List<byte>)selector.PixelData.Data_, Convert.ToInt16(selector.Columns.Data), Convert.ToInt16(selector.Rows.Data), Convert.ToInt16(selector.PixelRepresentation.Data), Convert.ToDouble(selector.RescaleSlope.Data), Convert.ToDouble(selector.RescaleIntercept.Data), Convert.ToString(selector.PhotometricInterpretation.Data));
        }

        

        /// <summary>
        /// Decodifica la informacion binaria de PIXEL_DATA en numeros CT
        /// </summary>
        /// <param name="pixelValues">List obtenido de EvilDICOM con la informacion binaria de pixeles</param>
        /// <param name="width">Valor COLUMNS obtenido de EvilDICOM</param>
        /// <param name="height">Valor ROWS obtenido de EvilDICOM</param>
        /// <param name="pixelRepresentation">Valor PIXEL_REPRESENTATION obtenido de EvilDICOM</param>
        /// <param name="rescaleSlope">Valor RESCALE_SLOPE obtenido de EvilDICOM</param>
        /// <param name="rescaleIntercept">Valor RESCALE_INTERCEPT obtenido de EvilDICOM</param>
        /// <param name="photoInterpretation">Valor PHOTO_INTERPRETATION obtenido de EvilDICOM</param>
        /// <returns></returns>
        private List<ushort> Byte2Pixels16(List<byte> pixelValues, int width, int height, int pixelRepresentation, double rescaleSlope, double rescaleIntercept, string photoInterpretation)
        {
            int i1 = 0;
            ushort unsignedS;
            int numPixels = width * height;
            int max16 = ushort.MaxValue;
            int pixVal = 0;
            List<int> pixels16Int = new List<int>();
            List<ushort> pixels16 = new List<ushort>();
            byte[] signedData = new byte[2];
 
            for (int i = 0; i < numPixels; ++i)
            {
                i1 = i * 2;
 
                if (pixelRepresentation == 0) // Unsigned
                {
                    unsignedS = Convert.ToUInt16((pixelValues[i1 + 1] << 8) + pixelValues[i1]);
                    pixVal = (int)(unsignedS * rescaleSlope + rescaleIntercept);
                    if (photoInterpretation == "MONOCHROME1")
                        pixVal = max16 - pixVal;
                }
                else  // Pixel representation is 1, indicating a 2s complement image
                {
                    signedData[0] = pixelValues[i1];
                    signedData[1] = pixelValues[i1 + 1];
                    short sVal = System.BitConverter.ToInt16(signedData, 0);
 
                    // Need to consider rescale slope and intercepts to compute the final pixel value
                    pixVal = (int)(sVal * rescaleSlope + rescaleIntercept);
                    if (photoInterpretation == "MONOCHROME1")
                        pixVal = max16 - pixVal;
                }
                pixels16Int.Add(pixVal);
            }
 
            minPixVal = pixels16Int.Min();
            signedImage = false;
            if (minPixVal < 0) signedImage = true;
 
            // Use the above pixel data to populate the list pixels16 
            foreach (int pixel in pixels16Int)
            {
                // We internally convert all 16-bit images to the range 0 - 65535
                if (signedImage)
                    pixels16.Add((ushort)(pixel - minPixVal)); //pixels16.Add((ushort)(pixel - min16));
                else
                    pixels16.Add((ushort)(pixel));
            }
 
            pixels16Int.Clear();
 
            return pixels16;
        }
 
        /// <summary>
        /// Convierte la información de pixeles (cropped o no) en CT a bytes
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="pixelRepresentation">Valor PIXEL_REPRESENTATION obtenido de EvilDICOM</param>
        /// <param name="rescaleSlope">Valor RESCALE_SLOPE obtenido de EvilDICOM</param>
        /// <param name="rescaleIntercept">Valor RESCALE_INTERCEPT obtenido de EvilDICOM</param>
        /// <param name="photoInterpretation">Valor PHOTO_INTERPRETATION obtenido de EvilDICOM</param>
        /// <returns>List de bytes codificado</returns>
        public List<byte> Pixels162Byte(List<ushort> pixels16, int pixelRepresentation, double rescaleSlope, double rescaleIntercept, string photoInterpretation)
        {
            List<byte> pixelValues = new List<byte>();
            int max16 = ushort.MaxValue;
            int pixVal;
            int sval;
            List<int> pixels16Int = new List<int>();
            byte[] b0;
 
            for (int i = 0; i < pixels16.Count; i++)
            {
                if (signedImage)
                    pixels16Int.Add(pixels16[i] + minPixVal);
            }
 
            for (int i = 0; i < pixels16.Count; i++)
            {
                pixVal = pixels16Int[i];
                if (photoInterpretation == "MONOCHROME1")
                    pixVal = pixVal - max16;
 
                sval = (int)((pixVal - rescaleIntercept) / rescaleSlope);
                b0 = System.BitConverter.GetBytes(sval);
                pixelValues.Add(b0[0]);
                pixelValues.Add(b0[1]);
            }
 
            return pixelValues;
        }
 
        /// <summary>
        /// Toma el List de ushort, que se mapea a una imagen BMP, y se recorta los elementos que se mapean en un area cuadrada de centro (xcenter,ycenter) de ancho 2*anchoRect y de largo 2*largoRect
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="altoRect">Distancia del centro al borde superior del rectangulo</param>
        /// <param name="anchoRect">Distancia del centro al borde lateral del rectangulo</param>
        /// <param name="width">Ancho de la imagen original</param>
        /// <param name="height">Alto de la imagen original</param>
        /// <returns></returns>
        public void CropCTRectangle(List<ushort> pixels16, int xcenter, int ycenter, int altoRect, int anchoRect, int width, int height)
        {
            List<ushort> pixelsCrop = new List<ushort>();
            int k = 0;
 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= (xcenter - anchoRect)) && (i < (xcenter + anchoRect)) && (j >= (ycenter - altoRect)) && (j < (ycenter + altoRect)))
                        pixelsCrop.Add(pixels16[k]);
                    k++;
                }
            }
 
            this.segCore = new List<ushort>();
            segCore.Clear();
            for (int i = 0; i < pixelsCrop.Count; i++)
            {
                segCore.Add(pixelsCrop[i]);
            }
 
        }
 
        /// <summary>
        /// Toma el List de ushort, que se mapea a una imagen BMP, y recorta los elementos que se mapean un area circular de centro (xcenter,ycenter) de radio rad
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="rad">Radio del circulo a extraer</param>
        /// <param name="width">Ancho de la imagen original</param>
        /// <param name="height">Alto de la imagen original</param>
        public void CropCTCircle(List<ushort> pixels16, int xcenter, int ycenter, int rad, int width, int height)
        {
            List<ushort> pixelsCrop = new List<ushort>();
            int k = 0;
            double dist;
 
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= (xcenter - rad)) && (i < (xcenter + rad)) && (j >= (ycenter - rad)) && (j < (ycenter + rad)))
                    {
                        // si esta dentro del area cuadrada
                        int dx = i - xcenter;
                        int dy = j - ycenter;
                        dist = Math.Sqrt(dx * dx + dy * dy);
 
                        if (dist <= rad)
                        {
                            // si esta dentro del circulo
                            pixelsCrop.Add(pixels16[k]);
                        }
                        else
                        {
                            // si esta dentro del area cuadrado pero fuera del circulo
                            pixelsCrop.Add(0);
                        }
 
                    }
                    k++;
                }
            }
 
            this.segCore = new List<ushort>();
            segCore.Clear();
            for (int i = 0; i < pixelsCrop.Count; i++)
            {
                segCore.Add(pixelsCrop[i]);
            }
        }

        /// <summary>
        /// Toma el List de ushort, que se mapea a una imagen BMP, y recorta los elementos que se mapean un area circular de centro (xcenter,ycenter) de radio rad
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="rad">Radio del circulo a extraer</param>
        /// <param name="width">Ancho de la imagen original</param>
        /// <param name="height">Alto de la imagen original</param>
        public List<ushort> CropCTCircle(int xcenter, int ycenter, int rad, int width, int height)
        {
            List<ushort> pixelsCrop = new List<ushort>();
            int k = 0;
            double dist;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int dx = i - xcenter;
                    int dy = j - ycenter;
                    dist = Math.Sqrt(dx * dx + dy * dy);

                    if (dist <= rad)
                    {
                        // si esta dentro del circulo
                        pixelsCrop.Add(this.pixelData[k]);
                    }
                    else
                    {
                        // si esta dentro del area cuadrado pero fuera del circulo
                        pixelsCrop.Add(0);
                    }
                    k++;
                    /*
                    if ((i >= (xcenter - rad)) && (i < (xcenter + rad)) && (j >= (ycenter - rad)) && (j < (ycenter + rad)))
                    {
                        // si esta dentro del area cuadrada
                        int dx = i - xcenter;
                        int dy = j - ycenter;
                        dist = Math.Sqrt(dx * dx + dy * dy);

                        if (dist <= rad)
                        {
                            // si esta dentro del circulo
                            pixelsCrop.Add(this.pixelData[k]);
                        }
                        else
                        {
                            // si esta dentro del area cuadrado pero fuera del circulo
                            pixelsCrop.Add(0);
                        }

                    }
                    else
                    {
                        // esta fuera del area cuadrado 
                        pixelsCrop.Add(0);
                    }
                    */
                }
            }

            return pixelsCrop;
        }

        /// <summary>
        /// Toma el List de ushort, que se mapea a una imagen BMP, y recorta los elementos que se mapean un area circular de centro (xcenter,ycenter) de radio rad. Version RV
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="rad">Radio del circulo a extraer</param>
        /// <param name="width">Ancho de la imagen original</param>
        /// <param name="height">Alto de la imagen original</param>
        public List<ushort> CropCTCircleRV(int xcenter, int ycenter, int rad, int width, int height)
        {
            List<ushort> pixelsCrop = new List<ushort>();
            double dist;
            double xesquina = xcenter - rad;
            double yesquina = ycenter - rad;
            double diametro = 2 * rad;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= xesquina) && (i < (xesquina + diametro)) && (j >= yesquina) && (j < (yesquina + diametro)))
                    {
                        // si esta dentro del area cuadrada
                        double dx = (double)i - xcenter;
                        double dy = (double)j - ycenter;
                        dist = Math.Sqrt(dx * dx + dy * dy);

                        if (dist <= Convert.ToDouble(diametro) / 2)
                        {
                            // si esta dentro del circulo
                            int pos = ((j - 1) * width) + i;
                            pixelsCrop.Add(this.pixelData[pos]);
                        }
                        else
                        {
                            // si esta dentro del area cuadrado pero fuera del circulo
                            pixelsCrop.Add(0);
                        }
                    }
                }
            }

            return pixelsCrop;
        }

        /// <summary>
        /// Toma el List de ushort, que se mapea a una imagen BMP, y recorta los elementos que se mapean un area circular de centro (xcenter,ycenter) de radio rad, y devuelve el valor medio de la segmentacion. Uso en RV-D
        /// </summary>
        /// <param name="pixels16">List con la información CT en cada pixel</param>
        /// <param name="xcenter">Coordenadas cartesianas del centro X, con cero en la esquina superior izquierda</param>
        /// <param name="ycenter">Coordenadas cartesianas del centro Y, con cero en la esquina superior izquierda</param>
        /// <param name="rad">Radio del circulo a extraer</param>
        /// <param name="width">Ancho de la imagen original</param>
        /// <param name="height">Alto de la imagen original</param>
        public double CropMeanCTRVD(int xcenter, int ycenter, int rad, int width, int height)
        {
            double dist;
            double xesquina = xcenter - rad;
            double yesquina = ycenter - rad;
            double diametro = 2 * rad;

            double acumulador=0;
            double count=0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= xesquina) && (i < (xesquina + diametro)) && (j >= yesquina) && (j < (yesquina + diametro)))
                    {
                        // si esta dentro del area cuadrada
                        double dx = (double)i - xcenter;
                        double dy = (double)j - ycenter;
                        dist = Math.Sqrt(dx * dx + dy * dy);

                        if (dist <= Convert.ToDouble(diametro) / 2)
                        {
                            // si esta dentro del circulo
                            int pos = ((j - 1) * width) + i;
                            acumulador += Convert.ToDouble(this.pixelData[pos]);
                            count++;
                        }                        
                    }
                }
            }

            return acumulador/count;
        }
 
        /// <summary>
        /// Crea un Bitmap a partir de la informacion de pixeles, en CT
        /// </summary>
        /// <param name="pixels16">List de shorts con la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen</param>
        /// <param name="height">Alto deseado de la imagen</param>
        /// <returns>Imagen resultante</returns>
        public static Bitmap CrearBitmap(List<ushort> pixels16, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
 
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);

            double maximoValor = pixels16.Max();
            double minimoValor = pixels16.Min();
            double range = maximoValor - minimoValor;
            double color;
 
            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;
 
                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;
 
                    for (j = 0; j < bmd.Width; ++j)
                    {
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;
 
                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;
 
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
 
            bmp.UnlockBits(bmd);
 
            return bmp;
        }
 
        /// <summary>
        /// Crea un Bitmap a partir de la informacion de pixeles, en CT, y guarda la imagen en la instancia bmp de la clase
        /// </summary>
        /// <param name="pixels16">List de shorts con la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen</param>
        /// <param name="height">Alto deseado de la imagen</param>
        public void CreateBitmap(List<ushort> pixels16, int width, int height)
        {
            bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
 
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
 
            double maximoValor = pixels16.Max();
            double minimoValor = pixels16.Min();
            double range = maximoValor - minimoValor;
            double color;
 
            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;
 
                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;
 
                    for (j = 0; j < bmd.Width; ++j)
                    {
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;
 
                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;
 
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
 
            bmp.UnlockBits(bmd);
        }
 
        /// <summary>
        /// Crea un Bitmap a partir de la informacion de pixeles, en CT, normalizando segun los límites que se le pasan, y guarda la imagen en la instancia bmp de la clase
        /// </summary>
        /// <param name="pixels16">List de shorts con la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen</param>
        /// <param name="height">Alto deseado de la imagen</param>
        /// <param name="minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="maxNormalizacion">Valor maximo CT de la normalizacion</param>
        public void CreateBitmap(List<ushort> pixels16, int width, int height, int minNormalizacion, int maxNormalizacion)
        {
            bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
 
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
 
            double maximoValor = maxNormalizacion;
            double minimoValor = minNormalizacion;
            double range = maximoValor - minimoValor;
            double color;
 
            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;
 
                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;
 
                    for (j = 0; j < bmd.Width; ++j)
                    {
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;
 
                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;
 
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
 
            bmp.UnlockBits(bmd);
        }
 
        /// <summary>
        /// Crea un Bitmap a partir de la informacion de pixeles, en CT, normalizando segun los límites que se le pasan, y guarda la imagen en la instancia bmp de la clase
        /// </summary>
        /// <param name="pixels16">List de shorts con la informacion CT de cada pixel</param>
        /// <param name="width">Ancho deseado de la imagen</param>
        /// <param name="height">Alto deseado de la imagen</param>
        /// <param name="minNormalizacion">Valor minimo CT de la normalizacion</param>
        /// <param name="maxNormalizacion">Valor maximo CT de la normalizacion</param>
        public static Bitmap CrearBitmap(List<ushort> pixels16, int width, int height, int minNormalizacion, int maxNormalizacion)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
 
            BitmapData bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
 
            double maximoValor = maxNormalizacion;
            double minimoValor = minNormalizacion;
            double range = maximoValor - minimoValor;
            double color;
 
            unsafe
            {
                int pixelSize = 3;
                int i, j, j1, i1;
                byte b;
 
                for (i = 0; i < bmd.Height; ++i)
                {
                    byte* row = (byte*)bmd.Scan0 + (i * bmd.Stride);
                    i1 = i * bmd.Width;
 
                    for (j = 0; j < bmd.Width; ++j)
                    {
                        // se normaliza de 0 a 255
                        color = Convert.ToInt32(Convert.ToDouble(pixels16[i * bmd.Width + j] - minimoValor) * ((double)255) / range);
                        if (color < 0) color = 0;
                        if (color > 255) color = 255;
 
                        // se convierte el color gris a byte
                        b = Convert.ToByte(color);
                        j1 = j * pixelSize;
 
                        row[j1] = b;            // Red
                        row[j1 + 1] = b;        // Green
                        row[j1 + 2] = b;        // Blue
                    }
                }
            }
 
            bmp.UnlockBits(bmd);
 
            return bmp;
        }
    }
}