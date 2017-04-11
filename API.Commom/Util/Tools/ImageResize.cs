using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;

namespace WinstonChurchill.API.Common.Util.Tools
{
    public class ImageResize
    {
        public enum Dimensions
        {
            Width,
            Height
        }

        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }

        public static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image ConstrainProportions(byte[] bytesImagem, int Size, Dimensions Dimension)
        {
            MemoryStream ms = new MemoryStream(bytesImagem);
            Image novaImagem = Image.FromStream(ms);
            Image imagem;

            if (novaImagem.Height > novaImagem.Width && novaImagem.Width < 200)
            {
                imagem = ConstrainProportions(novaImagem, Size, Dimension);
                imagem = Crop(imagem, Size, imagem.Width - 38, AnchorPosition.Center);
            }
            else
                imagem = ConstrainProportions(novaImagem, Size, Dimensions.Width);

            return imagem;
        }

        public static byte[] ConstrainProportions(byte[] bytesImagem, int Size, bool thumb)
        {
            MemoryStream ms = new MemoryStream(bytesImagem);
            Image novaImagem = Image.FromStream(ms);
            Image imagem;

            if (novaImagem.Height > novaImagem.Width && thumb)
            {
                imagem = ConstrainProportions(novaImagem, Size, Dimensions.Width);
                imagem = Crop(imagem, Size, imagem.Width - 38, AnchorPosition.Top);
            }
            else
                imagem = ConstrainProportions(novaImagem, Size, Dimensions.Width);

            return ConvertFileByte(imagem);
        }

        public static Image ConvertByteToImage(byte[] byteImagem)
        {
            MemoryStream ms = new MemoryStream(byteImagem);
            Image imagem = Image.FromStream(ms);
            return imagem;
        }

        public static Image ConstrainProportions(Image imgPhoto, int Size, Dimensions Dimension)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;
            float nPercent = 0;

            switch (Dimension)
            {
                case Dimensions.Width:
                    nPercent = ((float)Size / (float)sourceWidth);
                    break;
                default:
                    nPercent = ((float)Size / (float)sourceHeight);
                    break;
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            if (sourceWidth > destWidth && sourceHeight > destHeight)
            {

                Bitmap bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.Clear(Color.Transparent);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingMode = CompositingMode.SourceCopy;
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX-1, destY-1, destWidth+1, destHeight+1),
                new Rectangle(sourceX-1, sourceY-1, sourceWidth+1, sourceHeight+1),
                GraphicsUnit.Pixel);

                grPhoto.Dispose();

                return bmPhoto;
            }
            else
                return imgPhoto;
        }

        public static Image FixedSize(Image imgPhoto, int Width, int Height)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)((Width - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)((Height - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static Image Crop(Image imgPhoto, int Width, int Height, AnchorPosition Anchor)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (Anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)(Height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)((Height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (Anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)(Width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)((Width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX-1, destY-1, destWidth+1, destHeight+1),
                new Rectangle(sourceX-1, sourceY-1, sourceWidth+1, sourceHeight+1),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        /// <summary>
        /// Lista as imagens recortadas verticalmente, a partir da imagem original
        /// </summary>
        /// <param name="imgPhoto">Imagem de origem</param>
        /// <param name="Width">Largura da imagem</param>
        /// <param name="Height">Altura que sera recortada cada imagem</param>
        /// <returns>Lista com imagens recortadas</returns>
        public static List<Image> ListCroppedImagesVertical(Image imgPhoto, int Width, int Height)
        {
            List<Image> lstRetorno = new List<Image>();
           
            int  QtdPaginas = Convert.ToInt32(Math.Ceiling((double)imgPhoto.Height/(double)Height));
            
            for (int i = 0; i <  QtdPaginas; i++)
            {
                Rectangle section = new Rectangle(new Point(0, Height * i), new Size(Width, Height));
                                
                Bitmap bmPhoto = new Bitmap(section.Width, section.Height);
                bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
                
                Graphics grPhoto = Graphics.FromImage(bmPhoto);
                grPhoto.Clear(Color.Transparent);
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

                grPhoto.DrawImage(imgPhoto, 0, 0, section, GraphicsUnit.Pixel);

                lstRetorno.Add(bmPhoto);
                grPhoto.Dispose();    
            }
            return lstRetorno;
        }

        /// <summary>
        /// Converte um Arquivo Fisico em Bite[] para gravar no Banco
        /// </summary>
        /// <param name="filename">Caminho do Arquivo</param>
        /// <returns>Bytes Convertidos</returns>
        public static byte[] ConvertFileByte(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] ImageData = new byte[fs.Length];
            fs.Read(ImageData, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            return ImageData;
        }

        /// <summary>
        /// Converte uma imagem Bite[] para gravar no Banco
        /// </summary>
        /// <param name="Image">Imagem</param>
        /// <returns>Arquivo em Byte[]</returns>
        public static byte[] ConvertFileByte(Image imageToConvert)
        {
            byte[] Ret;
            try
            {
                MemoryStream ms = new MemoryStream();

                imageToConvert.Save(ms, ImageFormat.Jpeg);
                Ret = ms.ToArray();

                ms.Close();
            }
            catch (Exception) { throw; }
            return Ret;
        }

        /// <summary>
        /// Converte texto em imagem
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="textColor"></param>
        /// <param name="backColor"></param>
        /// <returns>Retorna string em base64</returns>
        public static Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;

        }

        public static byte[] CropWhiteSpaceByte(byte[] byteImage, int largura)
        {
            Image imagem = ConvertByteToImage(byteImage);

            imagem = CropWhiteSpace(imagem, largura);

            return ConvertFileByte(imagem);
        }

        public static Image CropWhiteSpace(byte[] byteImage)
        {
            Image imagem = ConvertByteToImage(byteImage);

            return CropWhiteSpace(imagem);
        }


        /// <summary>
        /// Remove espaços de uma imagem que estão na cor branca #FFFFFF
        /// </summary>
        /// <param name="img">Imagem a ser tratada</param>
        /// <returns>Image já recortada</returns>
        public static Image CropWhiteSpace(Image img, int largura = 0)
        {

            Bitmap bmp = new Bitmap(img);

            int w = bmp.Width, h = bmp.Height;

            Func<int, bool> allWhiteRow = row =>
            {
                for (int i = 0; i < w; ++i)
                    if (bmp.GetPixel(i, row).R != 255)
                        return false;
                return true;
            };

            Func<int, bool> allWhiteColumn = col =>
            {
                for (int i = 0; i < h; ++i)
                    if (bmp.GetPixel(col, i).R != 255)
                        return false;
                return true;
            };

            int topmost = 0;
            for (int row = 0; row < h; ++row)
            {
                if (allWhiteRow(row))
                    topmost = row;
                else break;
            }

            int bottommost = 0;
            for (int row = h - 1; row >= 0; --row)
            {
                if (allWhiteRow(row))
                    bottommost = row;
                else break;
            }

            int leftmost = 0, rightmost = 0;
            for (int col = 0; col < w; ++col)
            {
                if (allWhiteColumn(col))
                    leftmost = col;
                else
                    break;
            }

            for (int col = w - 1; col >= 0; --col)
            {
                if (allWhiteColumn(col))
                    rightmost = col;
                else
                    break;
            }

            int croppedWidth = (rightmost - leftmost);
            if (largura > 0) croppedWidth = largura;

            int croppedHeight = (bottommost - topmost);

            if (croppedWidth <= 0) croppedWidth = 985;
            if (croppedHeight <= 0) croppedHeight = 1000;

            try
            {
                Bitmap target = new Bitmap(croppedWidth, croppedHeight);
                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(bmp,
                      new RectangleF(0, 0, croppedWidth, croppedHeight),
                      new RectangleF(leftmost, topmost, croppedWidth, croppedHeight),
                      GraphicsUnit.Pixel);
                }
                return target;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro para os valores topmost={0} btm={1} left={2} right={3}", topmost, bottommost, leftmost, rightmost), ex);
            }
        }

       
    }
}
