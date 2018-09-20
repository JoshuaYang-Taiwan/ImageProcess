using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string FileName;
        private string method;

        public Form1()
        {
            InitializeComponent();
            this.panel1.Visible = false;
            ToolTip tooltip1 = new ToolTip();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\Users\tony\\Desktop";
            openFileDialog1.Filter = "圖檔(*.JPG)|*.JPG";
            saveFileDialog1.Filter = "圖檔(*.JPG)|*.JPG";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(FileName);
                    this.Text = FileName;
                }
            }
            catch
            {
                MessageBox.Show("讀取錯誤!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog1.FileName;
                    pictureBox2.Image.Save(FileName);
                    this.Text = FileName;
                }
            }
            catch
            {
                MessageBox.Show("存檔錯誤!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox2.Image;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox2.Image);
            choice(comboBox1.Text,bitmap);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            try
            {
                bitmap = new Bitmap(pictureBox1.Image);
            }
            catch
            {
                MessageBox.Show("請先開啟檔案!!");
                try
                {
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        FileName = openFileDialog1.FileName;
                        pictureBox1.Image = Image.FromFile(FileName);
                        this.Text = FileName;
                    }
                }
                catch
                {
                    MessageBox.Show("讀取錯誤!");
                }
                bitmap = new Bitmap(pictureBox1.Image);
            }

            choice(comboBox1.Text,bitmap);
            
            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            int value = 0;
            switch (this.method)
            {
                case "Quantify" :
                    value = (int)Math.Pow(2, trackBar1.Value);
                    pictureBox2.Image = imageProcessing.Quantify(bitmap, value);
                    break;
                case "Power" :
                    value = 50 * trackBar1.Value;
                    pictureBox2.Image = imageProcessing.Power(bitmap, value);
                    break;
            }
        }

        private void choice(String choose,Bitmap bitmap)
        {
            this.panel1.Visible = false;
            Form2 f2 = new Form2();
            
            switch (choose)
            {
                case "灰階圖片":
                    pictureBox2.Image = imageProcessing.grayscale(bitmap);
                    break;
                case "彩色負片":
                    pictureBox2.Image = imageProcessing.colorNegative(bitmap);
                    break;
                case "灰階負片":
                    pictureBox2.Image = imageProcessing.grayNegative(bitmap);
                    break;
                case "黑白效果":
                    pictureBox2.Image = imageProcessing.binarization(bitmap);
                    break;
                case "顯示原圖之紅色部分":
                    pictureBox2.Image = imageProcessing.ColorPart(bitmap, imageProcessing.Red);
                    break;
                case "顯示原圖之綠色部分":
                    pictureBox2.Image = imageProcessing.ColorPart(bitmap, imageProcessing.Green);
                    break;
                case "顯示原圖之藍色部分":
                    pictureBox2.Image = imageProcessing.ColorPart(bitmap, imageProcessing.Blue);
                    break;
                case "圖片量化":
                    this.panel1.Visible = true;
                    this.method = "Quantify";
                    this.trackBar1.SetRange(1, 8);
                    toolTip1.SetToolTip(this.trackBar1, "2, 4, 8, 16, 32, 64, 128, 256");
                    break;
                case "亮度增強":
                    this.panel1.Visible = true;
                    this.method = "Power";
                    this.trackBar1.SetRange(1, 4);
                    toolTip1.SetToolTip(this.trackBar1, "小增強, 增強, 大增強, 超級增強");
                    break;
                case "平滑濾波器":
                    pictureBox2.Image = imageProcessing.Smooth(bitmap);
                    break;
                case "中值濾波器":
                    pictureBox2.Image = imageProcessing.Median(bitmap);
                    break;
                case "Sobel":
                    pictureBox2.Image = imageProcessing.Sobel(bitmap);
                    break;
                case "Sobel  V":
                    pictureBox2.Image = imageProcessing.Sobel(bitmap, imageProcessing.Vertical);
                    break;
                case "Sobel  H":
                    pictureBox2.Image = imageProcessing.Sobel(bitmap, imageProcessing.Horizontal);
                    break;
                case "Histogram Equalization":
                    pictureBox2.Image = imageProcessing.HistogramEqualization(bitmap);
                    break;
                case "水平鏡射":
                    pictureBox2.Image = imageProcessing.Mirror(bitmap, imageProcessing.Vertical);
                    break;
                case "垂直鏡射":
                    pictureBox2.Image = imageProcessing.Mirror(bitmap, imageProcessing.Horizontal);
                    break;
                case "水平平移":
                    pictureBox2.Image = imageProcessing.Translation(bitmap, imageProcessing.Vertical);
                    break;
                case "垂直平移":
                    pictureBox2.Image = imageProcessing.Translation(bitmap, imageProcessing.Horizontal);
                    break;
                case "Dilate":
                    pictureBox2.Image = imageProcessing.Dilate(bitmap);
                    break;
                case "Erose":
                    pictureBox2.Image = imageProcessing.Erose(bitmap);
                    break;
                case "照片相減":
                    pictureBox2.Image = imageProcessing.absDiff(bitmap);
                    break;
                case "Subtract":
                    pictureBox2.Image = imageProcessing.Subtract(bitmap);
                    break;
                case "Opening":
                    pictureBox2.Image = imageProcessing.Opening(bitmap);
                    break;
                case "Closing":
                    pictureBox2.Image = imageProcessing.Closing(bitmap);
                    break;
                case "GAUSSIAN":
                    pictureBox2.Image = imageProcessing.CvGaussBGStatModelParams(bitmap);
                    break;
                case "cv Sobel":
                    pictureBox2.Image = imageProcessing.cvSobel(bitmap);
                    break;
                case "cv Histogram Equalization":
                    pictureBox2.Image = imageProcessing.cvHistogramEqualization(bitmap);
                    break;
                case "絕對領域":
                    pictureBox2.Image = imageProcessing.絕對領域(bitmap);
                    break;
                case "Contrast":
                    pictureBox2.Image = imageProcessing.Contrast(bitmap);
                    break;
                case "Prewitt":
                    pictureBox2.Image = imageProcessing.Prewitt(bitmap);
                    break;
                case "調整":
                    pictureBox2.Image = imageProcessing.revision(bitmap);
                    break;
                case "擷取":
                    pictureBox2.Image = imageProcessing.choose(bitmap);
                    break;
                case "label":
                    pictureBox1.Image = imageProcessing.label(bitmap);
                    break;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = int.Parse(textBox1.Text);
            pictureBox2.Width = int.Parse(textBox1.Text);
            pictureBox1.Height = int.Parse(textBox2.Text);
            pictureBox2.Height = int.Parse(textBox2.Text);
        }

        


        

    }

        
}

        

