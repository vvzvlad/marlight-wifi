using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;


namespace Marlight_test
{
    public partial class Form1 : Form
    {
        readonly Marlight_lib.Marlight marlight = new Marlight_lib.Marlight();

        string path = Application.StartupPath + "\\Settings.ini";

        bool RGB = false;
        int Flag = 0;
        int i = 0;

        string IP = "127.0.0.1";
        string Port = "50000";

        private Bitmap myBitmap;

        public Form1()
        {
            InitializeComponent();

            Win32.GetPrivateProfileString("Main", "IP", "192.168.88.188", IP, 100, path);
            Win32.GetPrivateProfileString("Main", "Port", "50000", Port, 100, path);

            textBoxIP.Text = IP;
            textBoxPort.Text = Port;

            textBox1.Text = IP;
            textBox2.Text = Port;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            marlight.AllOn();
            toolStripStatusLabel1.Text = "Выбраны все зоны";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            marlight.AllOff();
            toolStripStatusLabel1.Text = "Все лампы отключены";
            timer1.Enabled = false;
            RGB = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            marlight.LampOn(1);
            toolStripStatusLabel1.Text = "Выбраны зона № 1";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            marlight.LampOn(2);
            toolStripStatusLabel1.Text = "Выбраны зона № 2";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            marlight.LampOn(3);
            toolStripStatusLabel1.Text = "Выбраны зона № 3";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            marlight.LampOn(4);
            toolStripStatusLabel1.Text = "Выбраны зона № 4";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            marlight.LampOff(1);
            RGB = false;
            toolStripStatusLabel1.Text = "Зона № 1 отключена";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            marlight.LampOff(2);
            RGB = false;
            toolStripStatusLabel1.Text = "Зона № 2 отключена";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            marlight.LampOff(3);
            RGB = false;
            toolStripStatusLabel1.Text = "Зона № 3 отключена";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            marlight.LampOff(4);
            RGB = false;
            toolStripStatusLabel1.Text = "Зона № 4 отключена";
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

        private void button16_Click(object sender, EventArgs e)
        {
            if(RGB)
                marlight.RGBBrightUp();
            else
                marlight.BrightUp();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if(RGB)
                marlight.RGBBrightDown();
            else
                marlight.BrightDown();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            RGB = true;
            marlight.RGBModeOn();
            toolStripStatusLabel1.Text = "Включен режим управления цветом";
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


        private void button24_Click(object sender, EventArgs e)
        {
            Flag = 3;
            toolStripStatusLabel1.Text = "Создана задача 'Сигнал' на " + maskedTextBox1.Text;
            Cheker.Enabled = true;

            label10.Text = "Сигнал по таймеру";
            label11.Text = maskedTextBox1.Text;
            label12.Text = "Активна";
        }


        private void button27_Click(object sender, EventArgs e)
        {
            marlight.Init(textBoxIP.Text, Convert.ToInt32(textBoxPort.Text));
            toolStripStatusLabel1.Text = "Подключение по "+textBoxIP.Text+":"+textBoxPort.Text;   
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            marlight.TempColder();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            marlight.TempWarmer();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            RGB = false;
            marlight.SetDefaultTempAndBright();
            toolStripStatusLabel1.Text = "Управление цветом отключено";
            label1.Text = "R: 0";
            label2.Text = "G: 0";
            label3.Text = "B: 0";
            timer1.Enabled = false;
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Enabled = false;
            else
                timer1.Enabled = true;
            toolStripStatusLabel1.Text = "Режим постоянной смены цветов";

         //   marlight.RGBModeOn();
         //   marlight.RGBSetColor(myColor.R, myColor.G, myColor.B);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            marlight.PresetMode();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            marlight.LampOn(1);
            Thread.Sleep(900);
            marlight.RGBModeOn();
            marlight.RGBSetColor(255, 0, 0);
            Thread.Sleep(900);

            marlight.LampOn(2);
            Thread.Sleep(900);
            marlight.RGBModeOn();
            marlight.RGBSetColor(0, 255, 0);
            Thread.Sleep(900);

            marlight.LampOn(3);
            Thread.Sleep(900);
            marlight.RGBModeOn();
            marlight.RGBSetColor(0, 0, 255);
            Thread.Sleep(900);

            marlight.LampOn(4);
            Thread.Sleep(900);
            marlight.RGBModeOn();
            marlight.RGBSetColor(255, 255, 0);

            marlight.AllOn();

        }

        private void button13_Click_1(object sender, EventArgs e)  // ВКЛ
        {
            Flag = 1;
            Cheker.Enabled=true;
            toolStripStatusLabel1.Text = "Создана задача включения на "+maskedTextBox1.Text;

            label10.Text = "Включение";
            label11.Text = maskedTextBox1.Text;
            label12.Text = "Активна";
        }

        private void Cheker_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.ToString("HH:mm") == maskedTextBox1.Text)
            {

                if (Flag==2)
                {
                    marlight.AllOff();
                    toolStripStatusLabel1.Text = "Все лампы отключены в " + DateTime.Now.ToString("HH:mm");
                    label12.Text = "Завершено";
                }
                if(Flag==1)
                {
                    marlight.AllOn();
                    toolStripStatusLabel1.Text = "Все лампы включены в " + DateTime.Now.ToString("HH:mm");
                    label12.Text = "Завершено";
                }
                if (Flag == 3)
                {
                    Alarmer.Enabled = true;

                }
                timer1.Enabled = false;
                Flag = 0;
            }  
        }

        private void button14_Click_1(object sender, EventArgs e)  // ВЫКЛ
        {
            Flag = 2;
            Cheker.Enabled = true;
            toolStripStatusLabel1.Text = "Создана задача отключения на " + maskedTextBox1.Text;

            label10.Text = "Отключение";
            label11.Text = maskedTextBox1.Text;
            label12.Text = "Активна";
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            Cheker.Enabled = false;
            toolStripStatusLabel1.Text = "Задача отменена!";

            label10.Text = "Отсутствует";
            label11.Text = "Не назначено";
            label12.Text = "Ожидание задачи";
        }



        private void Alarmer_Tick(object sender, EventArgs e)
        {
            if (i != 4)
            {
                marlight.PresetAlarm(Convert.ToByte("00"), Convert.ToByte("00"));
                i++;
            }
            else
            {
                i = 0;
                Alarmer.Enabled = false;
                label12.Text = "Завершено";
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\Settings.ini"; // возвращает текущую директорию запущенного файла и добавляет к ней папку и имя файла.
         //   string s = "text"; // эта переменная обязательно должна быть не пустой.
            // нельзя задать её просто string s; Почему? понятия не имею, но если переменная изначально пустая,
            // функция фозвращает длину ключа а не его значение.
            Win32.WritePrivateProfileString("Main", "IP", textBoxIP.Text, path);
            Win32.WritePrivateProfileString("Main", "Port", textBoxPort.Text, path);
            toolStripStatusLabel1.Text = "Настройки успешно сохранены";
            MessageBox.Show("Настройки подключения изменены");
        }

        private void Connector_Tick(object sender, EventArgs e)
        {
            marlight.Init(textBox1.Text, Convert.ToInt32(textBox2.Text));
            toolStripStatusLabel1.Text = "Подключение по " + textBox1.Text + ":" + textBox2.Text;
            Connector.Enabled = false;
        }

        public class Win32
        {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int MessageBox(int hWnd, String text,
                String caption, uint type);
            [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetPrivateProfileString(String sSection, String sKey, String sDefault,
                String sString, int iSize, String sFile);
            [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
            public static extern bool WritePrivateProfileString(String sSection, String sKey, String sString, String sFile);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
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
                timer1.Enabled = false;
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
           
                RGB = true;
                marlight.RGBModeOn();
            
                timer1.Enabled = false;
                myBitmap = Win32APICall.GetDesktop();
                Color myColor = myBitmap.GetPixel(MousePosition.X, MousePosition.Y);
             
                toolStripStatusLabel1.Text = "Режим изменения цвета";
                marlight.RGBSetColor(myColor.R, myColor.G, myColor.B);

                label1.Text = "R: " + myColor.R;
                label1.Refresh();
                label2.Text = "G: " + myColor.G;
                label2.Refresh();
                label3.Text = "B: " + myColor.B;
                label3.Refresh();
        }

    }
}