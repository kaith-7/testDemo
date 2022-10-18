using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace winsorTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            splashPanel1.Visible = false;
            action += showSplash;
            CloseAction += CloseShow;
            sAction += ssfsdf;
            splashPanel1.Controls.Add(pictureBox1);
        }

        private int count = -1;
        private ArrayList images = new ArrayList();
        public Bitmap[] bitmap = new Bitmap[8];
        private int _value = 1;
        private Color _circleColor = Color.Gray ;
        private float _circleSize = 0.8f;

        public Color CircleColor
        {
            get { return _circleColor; }
            set { _circleColor = value; Invalidate(); }
        }

        public float CircleSize
        {
            get { return _circleSize; }
            set
            {
                if (value <= 0.0F) _circleSize = 0.05F;
                else _circleSize = value > 4.0F ? 4.0F : value; Invalidate();
            }
        }

        public Bitmap DrawCircle(int j)
        {
            const float angle = 360.0F / 8;
            Bitmap map = new Bitmap(150, 150);
            Graphics g = Graphics.FromImage(map);
            g.TranslateTransform(Width / 2.0F, Height / 2.0F);
            g.RotateTransform(angle * _value);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            int[] a = new int[8] { 25, 50, 75, 100, 125, 150, 175, 200 };
            for (int i = 1; i <= 8; i++)
            {
                int alpha = a[(i + j - 1) % 8];
                Color drawColor = Color.FromArgb(alpha, _circleColor);
                using (SolidBrush brush = new SolidBrush(drawColor))
                {
                    float sizeRate = 3.5F / _circleSize;
                    float size = Width / (6 * sizeRate);
                    float diff = (Width / 10.0F) - size;
                    float x = (Width / 80.0F) + diff;
                    float y = (Height / 80.0F) + diff;
                    g.FillEllipse(brush, x, y, size, size);
                    g.RotateTransform(angle);
                }
            }
            return map;
        }

        public void Draw()
        {
            for (int j = 0; j < 8; j++)
            {
                bitmap[7 - j] = DrawCircle(j);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            SetNewSize();
            base.OnResize(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            SetNewSize();
            base.OnSizeChanged(e);
        }
        private void SetNewSize()
        {
            int size = Math.Max(Width, Height);
            Size = new Size(size, size);
        }
        public void set()
        {
            for (int i = 0; i < 8; i++)
            {
                Draw();
                Bitmap map = new Bitmap((bitmap[i]), new Size(120, 110));
                images.Add(map);
            }
            pictureBox1.Image = (Image)images[0];
            pictureBox1.Size = pictureBox1.Image.Size;
        }
        private void pictureBox_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            base.Dispose();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            set();
            count = (count + 1) % 8;
            pictureBox1.Image = (Image)images[count];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            base.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
           
        }

        public Action action;
        public Action CloseAction;
        public Action sAction;

        private void button1_Click_1(object sender, EventArgs e)
        {
            action();
        }
        
        public void showSplash()
        {
             splashControl1.ShowSplash(false);
        }
        public void CloseShow()
        {
            splashControl1.HideSplash();
        }
        public void ssfsdf()
        {
            MessageBox.Show("hello world!");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            splashControl1.HideSplash();
        }
    }
}


