using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Marlight_test
{
    public partial class Form1 : Form
    {
        readonly Marlight_lib.Marlight marlight = new Marlight_lib.Marlight();

        private Bitmap myBitmap;

        public Form1()
        {
            InitializeComponent();
            marlight.Init("localhost", 50000);
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            marlight.AllOn();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            marlight.AllOff();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            marlight.LampOn(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            marlight.LampOn(2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            marlight.LampOn(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            marlight.LampOn(4);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            marlight.LampOff(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            marlight.LampOff(2);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            marlight.LampOff(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            marlight.LampOff(4);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            marlight.BrightUp();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            marlight.BrightDown();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            marlight.TempColder();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            marlight.TempWarmer();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            marlight.SetDefaultTempAndBright();
        }



        private static void DrawColorWheel(Graphics gr, Color outline_color, int xmin, int ymin, int wid, int hgt)
        {
            Rectangle rect = new Rectangle(xmin, ymin, wid, hgt);
            GraphicsPath wheel_path = new GraphicsPath();
            wheel_path.AddEllipse(rect);
            wheel_path.Flatten();

            float num_pts = (wheel_path.PointCount - 1) / 6;
            Color[] surround_colors = new Color[wheel_path.PointCount];

            int index = 0;
            InterpolateColors(surround_colors, ref index,
                1 * num_pts, 255, 255, 0, 0, 255, 255, 0, 255);
            InterpolateColors(surround_colors, ref index,
                2 * num_pts, 255, 255, 0, 255, 255, 0, 0, 255);
            InterpolateColors(surround_colors, ref index,
                3 * num_pts, 255, 0, 0, 255, 255, 0, 255, 255);
            InterpolateColors(surround_colors, ref index,
                4 * num_pts, 255, 0, 255, 255, 255, 0, 255, 0);
            InterpolateColors(surround_colors, ref index,
                5 * num_pts, 255, 0, 255, 0, 255, 255, 255, 0);
            InterpolateColors(surround_colors, ref index,
                wheel_path.PointCount, 255, 255, 255, 0, 255, 255, 0, 0);

            using (PathGradientBrush path_brush =
                new PathGradientBrush(wheel_path))
            {
                path_brush.CenterColor = Color.White;
                path_brush.SurroundColors = surround_colors;

                gr.FillPath(path_brush, wheel_path);

                // It looks better if we outline the wheel.
                using (Pen thick_pen = new Pen(outline_color, 2))
                {
                    gr.DrawPath(thick_pen, wheel_path);
                }
            }
        }

        // Fill in colors interpolating between the from and to values.
        private static void InterpolateColors(Color[] surround_colors,
            ref int index, float stop_pt,
            int from_a, int from_r, int from_g, int from_b,
            int to_a, int to_r, int to_g, int to_b)
        {
            int num_pts = (int)stop_pt - index;
            float a = from_a, r = from_r, g = from_g, b = from_b;
            float da = (to_a - from_a) / (num_pts - 1);
            float dr = (to_r - from_r) / (num_pts - 1);
            float dg = (to_g - from_g) / (num_pts - 1);
            float db = (to_b - from_b) / (num_pts - 1);

            for (int i = 0; i < num_pts; i++)
            {
                surround_colors[index++] =
                    Color.FromArgb((int)a, (int)r, (int)g, (int)b);
                a += da;
                r += dr;
                g += dg;
                b += db;
            }
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawColorWheel(e.Graphics, BackColor, 10, 50, 200, 200);
        
        }

        private void button16_Click(object sender, EventArgs e)
        {
            marlight.RGBBrightUp();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            marlight.RGBBrightDown();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            marlight.RGBModeOn();
        }


        private void tabPage2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                myBitmap = Win32APICall.GetDesktop();
                Color myColor = myBitmap.GetPixel(MousePosition.X, MousePosition.Y);

                marlight.RGBSetColor(myColor.R, myColor.G, myColor.B);

                label1.Text = "R: " + myColor.R;
                label1.Refresh();
                label2.Text = "G: " + myColor.G;
                label2.Refresh();
                label3.Text = "B: " + myColor.B;
                label3.Refresh();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            marlight.PresetNight();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            marlight.PresetMeeting();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            marlight.PresetReading();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            marlight.PresetMode();
        }


        private void button25_Click(object sender, EventArgs e)
        {
            marlight.PresetSleep();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            marlight.PresetTimer(Convert.ToByte(textBoxD.Text), Convert.ToByte(textBoxH.Text), Convert.ToByte(textBoxM.Text), Convert.ToByte(textBoxS.Text));
        }

        private void button24_Click(object sender, EventArgs e)
        {
            marlight.PresetAlarm(Convert.ToByte(textBoxH_alarm.Text), Convert.ToByte(textBoxM_alarm.Text));
        }

        private void button26_Click(object sender, EventArgs e)
        {
            marlight.PresetRecreation(Convert.ToByte(textBoxByte1.Text), Convert.ToByte(textBoxByte2.Text), Convert.ToByte(textBoxByte3.Text));
        }


        private void button27_Click(object sender, EventArgs e)
        {
            marlight.Init(textBoxIP.Text, Convert.ToInt32(textBoxPort.Text));
            MessageBox.Show("Remote host setting changed.");
            
            
        }

        

    }
}