using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace SpaceLinesAndPoints
{
    public partial class Form1 : Form
    {

        List<Point> points = new List<Point> { };
        Graphics G;
        FullScreen fullScreen;
        Brush b = new SolidBrush(Color.DarkGray);
        Brush r = new SolidBrush(Color.DarkRed);
        Pen y = new Pen(Color.DarkTurquoise, 1);
        bool drawing = false;
        int LineDistance = 160;
        int NumPoints = 120;
        public Form1()
        {
            InitializeComponent();
            Point.width = panel1.Width;
            Point.height = panel1.Height;
            CreatePoints();
            timer1.Start();
            fullScreen = new FullScreen(this,panel1);
            this.Text = "PRESS F11 TO GO FULL SCREEN";
        }
        public void CreatePoints()
        {
            Random rnd = new Random() { };
            for(int i = 0; i < NumPoints; i++)
            {
                int x = rnd.Next(panel1.Width);
                int y = rnd.Next(panel1.Height);
                points.Add(new Point(x, y));
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawing = true;
            G = e.Graphics;

            foreach(Point p1 in points)
            {
                p1.Update();
                int mCount = 1;
                foreach (Point p2 in points)
                {
                    if (p1 != p2)
                    {
                        double d = Distance(p1, p2);
                        if (d < LineDistance)
                        {
                            int s = 255 - Convert.ToInt32((d / LineDistance) * 255);
                            Color shade = Color.FromArgb(s, 255, 255, 255);
                            Pen pen = new Pen(shade, (float)(0.8 - (d / LineDistance)) * 10);
                            G.DrawLine(pen, (float)p1.x, (float)p1.y, (float)p2.x, (float)p2.y);
                            mCount++;
                        }
                        if(d <= 10)
                        {
                            double rb = new Random().NextDouble();
                            if (rb < 0.5)
                            {
                                p1.xv = -p2.xv * 1.1 + (rb / 10);
                                p2.yv = -p1.yv * 1.1 + (rb / 10);
                            }
                            else
                            {
                                p1.yv = -p2.yv * 1.1 + (rb / 10);
                                p2.xv = -p1.xv * 1.1 + (rb / 10);
                            }
                        }
                        if(d < 20)G.DrawEllipse(y, (float)p1.x - 20, (float)p1.y - 20, 40, 40);
                    }
                }
                //this.Text = mCount.ToString();
                p1.AddMoons(mCount);
            }

            foreach (Point p1 in points)
            {
                G.FillEllipse(b, (float)p1.x - 5, (float)p1.y - 5, 10, 10);
                foreach (Vector2 moon in p1.moons)
                {
                    G.FillEllipse(r, moon.X - 2.5f, moon.Y - 2.5f, 5, 5);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Invalidate();            
        }

        private double Distance(Point p1, Point p2)
        {
            double d = 0;
            d = Math.Sqrt(Math.Pow(p2.x - p1.x,2) + Math.Pow(p2.y - p1.y,2));
            return d;
        }
    }
    public class MyDisplay : Panel
    {
        public MyDisplay()
        {
            this.DoubleBuffered = true;
        }
    }

    class FullScreen
    {
        Form TargetForm;
        MyDisplay panel1;

        FormWindowState PreviousWindowState;

        public FullScreen(Form targetForm, MyDisplay panel1)
        {
            TargetForm = targetForm;
            TargetForm.KeyPreview = true;
            TargetForm.KeyDown += TargetForm_KeyDown;
            this.panel1 = panel1;
        }

        private void TargetForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F11)
            {
                Toggle();
            }
        }

        public void Toggle()
        {
            if (TargetForm.WindowState == FormWindowState.Maximized)
            {
                Leave();
            }
            else
            {
                Enter();
            }
        }

        public void Enter()
        {
            if (TargetForm.WindowState != FormWindowState.Maximized)
            {
                PreviousWindowState = TargetForm.WindowState;
                TargetForm.WindowState = FormWindowState.Normal;
                //TargetForm.FormBorderStyle = FormBorderStyle.None;
                TargetForm.WindowState = FormWindowState.Maximized;
                int w = TargetForm.Width - 40;
                int h = TargetForm.Height - 60;
                panel1.Width = w;
                panel1.Height = h;
                Point.width = w;
                Point.height = h;

            }
        }

        public void Leave()
        {
            TargetForm.FormBorderStyle = FormBorderStyle.Sizable;
            TargetForm.WindowState = PreviousWindowState;
        }
    }
}
