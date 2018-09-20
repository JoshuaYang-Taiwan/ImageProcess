using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.DebuggerVisualizers;
using Emgu.Util;

namespace WindowsFormsApplication1
{

    public static class imageProcessing
    {

        public const string Red = "Red";
        public const string Green = "Green";
        public const string Blue = "Blue";

        public const string Horizontal = "Horizontal";
        public const string Vertical = "Vertical";



        public static Bitmap grayscale(Bitmap bitmap)
        {
            Bitmap bitmapGray = new Bitmap(bitmap.Width,bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    bitmapGray.SetPixel(x, y, newColor);
                }
            }
            return bitmapGray;
        }

        public static Bitmap colorNegative(Bitmap bitmap)
        {
            Bitmap bitmapColorNegative = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    Color newColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    bitmapColorNegative.SetPixel(x, y, newColor);
                }
            }
            return bitmapColorNegative;
        }

        public static Bitmap grayNegative(Bitmap bitmap)
        {
            Bitmap bitmapGrayNegative = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(255 - gray, 255 - gray, 255 - gray);
                    bitmapGrayNegative.SetPixel(x, y, newColor);
                }
            }
            return bitmapGrayNegative;
        }

        public static Bitmap binarization(Bitmap bitmap)
        {
            Bitmap bitmapBnarization = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int BW = color.R + color.G + color.B;
                    if (BW >= 128)
                        BW = 255;
                    else
                        BW = 0;
                    Color colorBW = Color.FromArgb(BW, BW, BW);
                    bitmapBnarization.SetPixel(x, y, colorBW);
                }
            }
            return bitmapBnarization;
        }

        public static Bitmap Contrast(Bitmap bitmap)
        {
            Bitmap bitmapContrast = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int R = color.R;
                    int G = color.G;
                    int B = color.B;
                    
                    R = R >= 128 ?　255 : 0;
                    G = G >= 128 ?　255 : 0;
                    B = B >= 128 ?　255 : 0;

                    Color colorContrast = Color.FromArgb(R, G, B);
                    bitmapContrast.SetPixel(x, y, colorContrast);
                }
            }
            return bitmapContrast;
        }

        public static Bitmap ColorPart(Bitmap bitmap,String RGB)
        {
            Bitmap bitmapRedPart = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    Color color2 = Color.White;
                    switch(RGB)
                    {
                        case "Red" :
                            color2 = Color.FromArgb(color.R, 0, 0);
                            break;
                        case "Green" :
                            color2 = Color.FromArgb(0, color.G, 0);
                            break;
                        case "Blue":
                            color2 = Color.FromArgb(0, 0, color.B);
                            break;
                    }
                    bitmapRedPart.SetPixel(x, y,color2);
                }
            }
            return bitmapRedPart;
        }

        public static Bitmap Quantify(Bitmap bitmap, int value)
        {
            Bitmap bitmapQuantify = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    int range = 256 / value;
                    int newGray = range * (gray / range);
                    Color colorQuantify = Color.FromArgb(newGray, newGray, newGray);
                    bitmapQuantify.SetPixel(x, y, colorQuantify);
                }
            }
            return bitmapQuantify;
        }

        public static Bitmap Power(Bitmap bitmap, int value)
        {
            Bitmap bitmapPower = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int R = color.R + value;
                    int G = color.G + value;
                    int B = color.B + value;
                    R = R > 255 ? 255 : R;
                    G = G > 255 ? 255 : G;
                    B = B > 255 ? 255 : B;
                    Color colorPower = Color.FromArgb(R, G, B);
                    bitmapPower.SetPixel(x, y, colorPower);
                }
            }

            return bitmapPower;
        }

        public static Bitmap Smooth(Bitmap bitmap)
        {
            Bitmap bitmapSmooth = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;

                    int sum = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            color = bitmap.GetPixel(i, j);
                            gray = (color.R + color.G + color.B) / 3;
                            sum = sum + gray;
                        }
                    }
                    int newGray = sum / 9;
                    Color newColor = Color.FromArgb(newGray, newGray, newGray);
                    bitmapSmooth.SetPixel(x, y, newColor);
                }
            }
            return bitmapSmooth;
        }

        public static Bitmap Median(Bitmap bitmap)
        {
            Bitmap bitmapMedian = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);

                    int count = 0;
                    int[] grayArray = new int[9];
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            color = bitmap.GetPixel(i, j);
                            gray = (color.R + color.G + color.B) / 3;
                            grayArray[count] = gray;
                            count++;
                        }
                    }
                    Array.Sort(grayArray);
                    newColor = Color.FromArgb(grayArray[4], grayArray[4], grayArray[4]);
                    bitmapMedian.SetPixel(x, y, newColor);
                }
            }
            return bitmapMedian;
        }

        public static Bitmap Sobel(Bitmap bitmap, String direction)
        {
            Bitmap BitmapSobel = new Bitmap(bitmap.Width, bitmap.Height);
            int[] sobel = new int[9];
            switch(direction)
            {
                case "Horizontal" :
                    sobel = new int[9]{ -1, 0, 1, -2, 0, 2, -1, 0, 1 };
                    break;
                case "Vertical" :
                    sobel = new int[9] { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
                    break;
            }
            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);
    
                    int sum = 0, count = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            color = bitmap.GetPixel(i, j);
                            gray = (color.R + color.G + color.B) / 3;
                            sum = sum + (gray * sobel[count]);
                            count++;
                        }
                   }
                   int newGray = sum;
                   if (newGray > 255) newGray = 255;
                   if (newGray < 0) newGray = 0;
                   newColor = Color.FromArgb(newGray, newGray, newGray);
                   BitmapSobel.SetPixel(x, y, newColor);
                }
            }
            return BitmapSobel;
        }

        public static Bitmap Sobel(Bitmap bitmap)
        {
            Bitmap bitmapSobel = new Bitmap(bitmap.Width, bitmap.Height); ;
            int[] sobel1 = new int[9] { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
            int[] sobel2 = new int[9] { -1, 0, 1, -2, 0, 2, -1, 0, 1 };

            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);

                    int sum1 = 0, sum2 = 0, count = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            color = bitmap.GetPixel(i, j);
                            gray = (color.R + color.G + color.B) / 3;
                            sum1 = sum1 + (gray * sobel1[count]);
                            sum2 = sum2 + (gray * sobel2[count]);
                            count++;
                        }
                    }
                    int newGray = sum1 + sum2;
                    if (newGray > 255) newGray = 255;
                    if (newGray < 0) newGray = 0;
                    newColor = Color.FromArgb(newGray, newGray, newGray);
                    bitmapSobel.SetPixel(x, y, newColor);
                }
            }
            return bitmapSobel;

        }

        public static Bitmap HistogramEqualization(Bitmap bitmap)
        {
            Bitmap bitmapHE = new Bitmap(bitmap.Width, bitmap.Height);

            int[] histogram = new int[256];
            int[] cdf = new int[256];

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int gray = (color.R + color.G + color.B) / 3;
                    histogram[gray]++;
                    Color newColor = Color.FromArgb(gray, gray, gray);
                }
            }
            cdf[0] = histogram[0];
            for (int x = 1; x < 255; x++)
            {
                cdf[x] = cdf[x - 1] + histogram[x];
            }

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;

                    int HE = (int)(((double)(cdf[gray] - cdf.Min()) / ((bitmap.Width * bitmap.Height) - cdf.Min())) * 255);

                    HE = HE > 255 ? 255 : HE;
                    HE = HE < 0 ? 0 : HE;

                    Color colorHE = Color.FromArgb(HE, HE, HE);
                    bitmapHE.SetPixel(x, y, colorHE);
                }
            }
            return bitmapHE;
        }

        public static Bitmap Mirror(Bitmap bitmap, String direction)
        {
            Bitmap bitmapMirror = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    switch (direction)
                    {
                        case "Vertical":
                            bitmapMirror.SetPixel(bitmap.Width - 1 - x, y, color);
                            break;
                        case "Horizontal":
                            bitmapMirror.SetPixel(x, bitmap.Height - 1 - y, color);
                            break;
                    }
                }
            }
            return bitmapMirror;
        }

        public static Bitmap Translation(Bitmap bitmap, String direction)
        {
            Bitmap bitmapTranslation = new Bitmap(bitmap.Width, bitmap.Height);
            int shift = 0;
            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    switch (direction)
                    {
                        case "Horizontal":
                            shift = bitmap.Height / 2;
                            int newY = (y + shift) % bitmap.Height;
                            bitmapTranslation.SetPixel(x, newY, color);
                            break;
                        case "Vertical":
                            shift = bitmap.Width / 2;
                            int newX = (x + shift) % bitmap.Width;
                            bitmapTranslation.SetPixel(newX, y, color);
                            break;
                    }
                }
            }
            return bitmapTranslation;
        }

        public static Bitmap Dilate(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            Image<Gray, Byte> dilate = new Image<Gray, Byte>(image.Width, image.Height);
            StructuringElementEx EX9x9 = new StructuringElementEx(15, 31, 7,15, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            CvInvoke.cvDilate(image, dilate, EX9x9, 1);
            return dilate.ToBitmap();
        }

        public static Bitmap Erose(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            Image<Gray, Byte> erose = new Image<Gray, Byte>(image.Width, image.Height);
            StructuringElementEx EX9x9 = new StructuringElementEx(15, 31, 7, 15, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            CvInvoke.cvErode(image, erose, EX9x9, 1);
            return erose.ToBitmap();
        }

        public static Bitmap absDiff(Bitmap bitmap)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "圖檔(*.JPG)|*.JPG";

            Image<Gray, Byte> bitmap1 = new Image<Gray, Byte>(bitmap);
            
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bitmap1 = new Image<Gray, Byte>(openFileDialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show("讀取錯誤!");
            }

            Image<Gray, Byte> bitmap2 = new Image<Gray, Byte>(bitmap);
            Image<Gray, Byte> bitmap3 = new Image<Gray, Byte>(bitmap2.Width, bitmap2.Height);
            CvInvoke.cvAbsDiff(bitmap1, bitmap2, bitmap3);
            return bitmap3.ToBitmap();
        }

        public static Bitmap Subtract(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            Image<Gray, Byte> erose = new Image<Gray, Byte>(image.Width, image.Height);
            Image<Gray, Byte> dilate = new Image<Gray, Byte>(image.Width, image.Height);
            StructuringElementEx EX9x9 = new StructuringElementEx(5, 5, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            CvInvoke.cvErode(image, erose, EX9x9, 1);
            CvInvoke.cvDilate(image, dilate, EX9x9, 1);
            CvInvoke.cvAbsDiff(erose, dilate, image);
            return image.ToBitmap();
        }

        public static Bitmap Opening(Bitmap bitmap)
        {
            return Dilate(Erose(bitmap));
        }

        public static Bitmap Closing(Bitmap bitmap)
         {
             return Erose(Dilate(bitmap));
         }
        
        public static Bitmap CvGaussBGStatModelParams(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            CvInvoke.cvSmooth(image, image, Emgu.CV.CvEnum.SMOOTH_TYPE.CV_GAUSSIAN, 5, 5, 1.5, 0);
            return image.ToBitmap();
        }

        public static Bitmap cvSobel(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            CvInvoke.cvSobel(image, image, 1, 1, 1);
            return image.ToBitmap();

            
        }

        public static Bitmap cvHistogramEqualization(Bitmap bitmap)
        {
            Image<Gray, Byte> image = new Image<Gray, Byte>(bitmap);
            image._EqualizeHist();
            return image.ToBitmap();
        }

        public static Bitmap 絕對領域(Bitmap bitmap)
        {
            Bitmap bitmap2 = new Bitmap(bitmap.Width/25*12, bitmap.Height/10*5);
            Color color=new Color();
            /* for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {

                    if(x>=(bitmap.Width / 25) * 8 || x<=(bitmap.Width / 5) * 4)
                        if(y>=(bitmap.Height / 10) * 3|| y < (bitmap.Height / 5) * 4){
                          color = bitmap.GetPixel(x, y);
                          bitmap2.SetPixel(x , y, color);}
                        else
                           bitmap2.SetPixel(x , y, );
                }
            }*/
            for (int x = (bitmap.Width / 25) * 8; x < (bitmap.Width / 25) * 20; x++)
            {
                for (int y = (bitmap.Height / 10) * 3; y < (bitmap.Height / 10) * 8; y++)
                {
                    color = bitmap.GetPixel(x, y);
                    bitmap2.SetPixel(x-(bitmap.Width / 25) * 8  , y-(bitmap.Height / 10) * 3, color);
                }
            }
            return bitmap2;
        }

        public static Bitmap Prewitt(Bitmap bitmap)
        {
            Bitmap BitmapPrewitt = new Bitmap(bitmap.Width, bitmap.Height);
            int[] sobel = new int[9] { -1, -1, -1, 0, 0, 0, 1, 1, 1 };

            for (int x = 1; x < bitmap.Width - 1; x++)
            {
                for (int y = 1; y < bitmap.Height - 1; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.FromArgb(gray, gray, gray);

                    int sum1 = 0, count = 0;
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            color = bitmap.GetPixel(i, j);
                            gray = (color.R + color.G + color.B) / 3;
                            sum1 = sum1 + (gray * sobel[count]);
                            count++;
                        }
                    }
                    int newGray = sum1;
                    if (newGray > 255) newGray = 255;
                    if (newGray < 0) newGray = 0;
                    newColor = Color.FromArgb(newGray, newGray, newGray);
                    BitmapPrewitt.SetPixel(x, y, newColor);
                }
            }
            return BitmapPrewitt;
        }

        public static Bitmap revision(Bitmap bitmap)
        {
            Bitmap bitmapGray = new Bitmap(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);

                    int revision = (int)(0.08 * color.R + 0.22 * color.G + 0.7 * color.B);
                    Color newColor = Color.FromArgb(revision, revision, revision);
                    bitmapGray.SetPixel(x, y, newColor);
                }
            }
            return bitmapGray;
        }

        public static Bitmap choose(Bitmap bitmap)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "圖檔(*.JPG)|*.JPG";

            Bitmap bitmap1 = new Bitmap(bitmap);

            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    bitmap1 = new Bitmap(openFileDialog.FileName);
                }
            }
            catch
            {
                MessageBox.Show("讀取錯誤!");
            }

            Bitmap bitmap2 = new Bitmap(bitmap);
            Bitmap bitmap3 = new Bitmap(bitmap1.Width, bitmap1.Height);

            for (int x = 0; x < bitmap2.Width; x++)
            {
                for (int y = 0; y < bitmap2.Height; y++)
                {
                    Color c1 = bitmap2.GetPixel(x, y);
                    Color c2 = bitmap1.GetPixel(x, y);
                    int cc1 = c1.R + c1.G + c1.B;
                    if (cc1 != 0)
                    {
                        bitmap3.SetPixel(x, y, c2);
                    }
                }
            }
            return bitmap3;
        }

        public static Bitmap label(Bitmap bitmap)
        {
            Bitmap binaryBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            Bitmap labelingBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            int threshold = 50;
            int labelNum = 1;
            int[,] labeling = new int[bitmap.Width, bitmap.Height];
            List<Point> equTable = new List<Point>();
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (color.R + color.G + color.B) / 3;
                    Color newColor = Color.White;
                    if (gray < threshold) newColor = Color.Black;
                    binaryBitmap.SetPixel(x, y, newColor);

                    if (x - 1 >= 0 && y - 1 >= 0 && gray < threshold)
                    {
                        if (labeling[x - 1, y] > 0 && labeling[x, y - 1] > 0)
                        {
                            labeling[x, y] = labeling[x - 1, y];
                            if (labeling[x - 1, y] != labeling[x, y - 1])
                            {
                                Point point;
                                if (labeling[x - 1, y] > labeling[x, y - 1]) point = new Point(labeling[x, y - 1], labeling[x - 1, y]);
                                else point = new Point(labeling[x - 1, y], labeling[x, y - 1]);

                                if (!equTable.Exists(p => p == point)) equTable.Add(point);
                            }
                        }
                        else if (labeling[x - 1, y] > 0) labeling[x, y] = labeling[x - 1, y];
                        else if (labeling[x, y - 1] > 0) labeling[x, y] = labeling[x, y - 1];
                        else
                        {
                            labeling[x, y] = labelNum;
                            labelNum++;
                        }
                    }
                }
            }

            for (int i = 0; i < equTable.Count; i++)
            {
                for (int j = 0; j < equTable.Count; j++)
                {
                    if (i != j)
                    {
                        if (equTable[i].Y == equTable[j].X)
                        {
                            equTable[j] = new Point(equTable[i].X, equTable[j].Y);
                        }
                        if (equTable[i].Y == equTable[j].Y)
                        {
                            equTable[j] = new Point(equTable[j].X, equTable[i].X);
                        }
                    }
                }
            }

            Random rand = new Random();
            List<Color> colorTable = new List<Color>();
            List<int> totalLabel = new List<int>();
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int i = 0; i < equTable.Count; i++)
                    {
                        if (labeling[x, y] == equTable[i].Y) labeling[x, y] = equTable[i].X;
                    }

                    int label = labeling[x, y];
                    if (!totalLabel.Exists(k => k == label))
                    {
                        totalLabel.Add(label);
                        Color randColor = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
                        colorTable.Add(randColor);
                    }

                    int index = totalLabel.FindIndex(k => k == label);
                    labelingBitmap.SetPixel(x, y, colorTable[index]);
                }
            }
            return labelingBitmap;
        }

    }
}
