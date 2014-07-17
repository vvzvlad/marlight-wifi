


using System;
using System.Threading;

namespace Marlight_lib
{
    /// <summary>
    /// Class for work with Marlight bulb thre WiFi receiver
    /// </summary>
    public class Marlight
    {
        /// <summary>
        /// valid IP address or host name
        /// </summary>
        public string IP = "localhost";

        /// <summary>
        /// port number on remote host
        /// </summary>
        public int Port = 50000;

        /// <summary>
        /// Dealy in milliseconds after any UDP command
        /// </summary>
        public int Delay = 0;

        /// <summary>
        /// Send message to remote host using threads
        /// If you use THREADS
        /// </summary>
        public bool UseThreads = true;

        /// <summary>
        /// Initilize library
        /// </summary>
        /// <param name="ip_host"></param>
        /// <param name="port"></param>
        /// <param name="delay"></param>
        /// <param name="use_threads"></param>
        public void Init(string ip_host, int port, int delay = 0, bool use_threads=true){
            IP = ip_host;
            Port = port;
            Delay = delay;
            UseThreads = use_threads;
        }

        #region Message Constats

        // ----------------------------- FIRST PAGE IPHONE APPLICATION  -----------------------------------
        readonly byte[] ALL_ON = new byte[] { 0x01, 0x55 };
        readonly byte[] ALL_OFF = new byte[] { 0x02, 0x55 };

        readonly byte[] BRIGHT_UP = new byte[] { 0x03, 0x55 };
        readonly byte[] BRIGHT_DOWN = new byte[] { 0x04, 0x55 };

        readonly byte[] TEMP_COLDER = new byte[] { 0x05, 0x55 };
        readonly byte[] TEMP_WARMER = new byte[] { 0x06, 0x55 };

        readonly byte[] BRIGHT_TEMP_DEFAULT = new byte[] { 0x07, 0x55 };

        readonly byte[] ON_1 = new byte[] { 0x08, 0x55 };
        readonly byte[] OFF_1 = new byte[] { 0x09, 0x55 };
        readonly byte[] ON_2 = new byte[] { 0x0A, 0x55 };
        readonly byte[] OFF_2 = new byte[] { 0x0B, 0x55 };
        readonly byte[] ON_3 = new byte[] { 0x0C, 0x55 };
        readonly byte[] OFF_3 = new byte[] { 0x0D, 0x55 };
        readonly byte[] ON_4 = new byte[] { 0x0E, 0x55 };
        readonly byte[] OFF_4 = new byte[] { 0x0F, 0x55 };

        // ----------------------------- SECOND PAGE IPHONE APPLICATION  (RGB) -----------------------------------
        readonly byte[] RGB_MODE_ON = new byte[] { 0x12, 0x55 };
        readonly byte[] RGB_MODE_BRIGHT_DOWN = new byte[] { 0x10, 0x55 };
        readonly byte[] RGB_MODE_BRIGHT_UP = new byte[] { 0x11, 0x55 };
        readonly byte[] RGB_MODE_SET_COLOR = new byte[] { 0x13, 0x00, 0x00, 0x00, 0x55 };

        // ----------------------------- THIRD PAGE IPHONE APPLICATION  (PRESETS) -----------------------------------
        readonly byte[] MODE_NIGHT = new byte[] { 0x14, 0x55 };
        readonly byte[] MODE_MEETING = new byte[] { 0x15, 0x55 };
        readonly byte[] MODE_READING = new byte[] { 0x16, 0x55 };
        readonly byte[] MODE_MODE = new byte[] { 0x17, 0x55 };                                        

        readonly byte[] MODE_TIMER = new byte[] { 0x18, 0x00, 0x00, 0x00, 0x00, 0x09, 0x14, 0x55 };    
        readonly byte[] MODE_ALARM = new byte[] { 0x19, 0x00, 0x00, 0x09, 0x14, 0x55 };                
        readonly byte[] MODE_SLEEP = new byte[] { 0x1A, 0x55 };                                        // ??????
        readonly byte[] MODE_RECREATION = new byte[] { 0x1B, 0x00, 0x00, 0x00, 0x55 };                  
        // ----------------------------- END -----------------------------------

        #endregion


        /// <summary>
        /// Send message to remote host
        /// </summary>
        /// <param name="message"></param>
        private void sendMsg(byte[] message)
        {

            if (UseThreads)
            {
                Thread t = new Thread(_sendMsg);
                t.Start(message);
            }
            else
            {
                _sendMsg(message);
            }
        }


        private void _sendMsg(object param)
        {
            byte[] message = (param as byte[]);
            using (System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient())
            {
                client.Connect(IP, Port);
                client.Send(message, message.Length);
                if (Delay != 0)
                    Thread.Sleep(Delay);
            }
        }


        /// <summary>
        /// Send custom message to remote host (for future use or undocument commands)
        /// </summary>
        /// <param name="custom_message"></param>
        public void SendCustomMsg(byte[] custom_message)
        {
            sendMsg(custom_message);
        }

        #region Simple commands
        /// <summary>
        /// Turn the lamp_num ON or make a lamp_num active by number
        /// Valid numbers is 1,2,3 and 4
        /// </summary>
        /// <param name="lamp_num"></param>
        public void LampOn(int lamp_num)
        {
            switch (lamp_num)
            {
                case 1:
                    sendMsg(ON_1);
                    break;
                case 2:
                    sendMsg(ON_2);
                    break;
                case 3:
                    sendMsg(ON_3);
                    break;
                case 4:
                    sendMsg(ON_4);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Turn the lamp_num OFF
        /// Valid numbers is 1,2,3 and 4
        /// </summary>
        /// <param name="lamp_num"></param>
        public void LampOff(int lamp_num)
        {
            switch (lamp_num)
            {
                case 1:
                    sendMsg(OFF_1);
                    break;
                case 2:
                    sendMsg(OFF_2);
                    break;
                case 3:
                    sendMsg(OFF_3);
                    break;
                case 4:
                    sendMsg(OFF_4);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Power on
        /// </summary>
        public void AllOn()
        {
            sendMsg(ALL_ON);
        }

        /// <summary>
        /// Power off
        /// </summary>
        public void AllOff()
        {
            sendMsg(ALL_OFF);
        }

        /// <summary>
        /// Bright Up on last lamp
        /// </summary>
        public void BrightUp()
        {
            sendMsg(BRIGHT_UP);
        }

        /// <summary>
        /// Bright Down on last lamp
        /// </summary>
        public void BrightDown()
        {
            sendMsg(BRIGHT_DOWN);
        }

        /// <summary>
        /// Set lamp temperature little bit colder
        /// </summary>
        public void TempColder()
        {
            sendMsg(TEMP_COLDER);
        }

        /// <summary>
        /// Set lamp temperature little bit warmer
        /// </summary>
        public void TempWarmer()
        {
            sendMsg(TEMP_WARMER);
        }

        /// <summary>
        /// Set the lamp to default color temperature and brightness
        /// </summary>
        public void SetDefaultTempAndBright()
        {
            sendMsg(BRIGHT_TEMP_DEFAULT);
        }
        #endregion

        #region RGB commands
        /// <summary>
        /// Set brightness little bit UP in RGB MODE
        /// </summary>
        public void RGBBrightUp()
        {
            sendMsg(RGB_MODE_BRIGHT_UP);
        }

        /// <summary>
        /// Set brightness little bit DOWN in RGB MODE
        /// </summary>
        public void RGBBrightDown()
        {
            sendMsg(RGB_MODE_BRIGHT_DOWN);
        }

        /// <summary>
        /// Turn ON RGB MODE
        /// </summary>
        public void RGBModeOn()
        {
            sendMsg(RGB_MODE_ON);
        }

        /// <summary>
        /// Set lamp color on RGB mode (RGB channels)
        /// </summary>
        /// <param name="R"> Red channel value</param>
        /// <param name="G"> Green channel value</param>
        /// <param name="B"> Blue channel value</param>
        public void RGBSetColor(byte R, byte G, byte B)
        {
            Byte[] color_msg = RGB_MODE_SET_COLOR;
            color_msg[1] = R;
            color_msg[2] = G;
            color_msg[3] = B;
            sendMsg(color_msg);
        }
        #endregion RGB commands

        #region Preset commands
        /// <summary>
        /// Preset a NIGHT MODE
        /// </summary>
        public void PresetNight()
        {
            sendMsg(MODE_NIGHT);
        }

        /// <summary>
        /// Preset a MEETING MODE
        /// </summary>
        public void PresetMeeting()
        {
            sendMsg(MODE_MEETING);
        }

        /// <summary>
        /// Preset a READING MODE
        /// </summary>
        public void PresetReading()
        {
            sendMsg(MODE_READING);
        }

        /// <summary>
        /// ????? just send a 2 byte to remote hoste (equal to MODE button oin iOS remote control application)
        /// </summary>
        public void PresetMode()
        {
            sendMsg(MODE_MODE);
        }

        /// <summary>
        /// Timer MODE
        /// </summary>
        /// <param name="on_hour"></param>
        /// <param name="on_minutes"></param>
        /// <param name="off_hour"></param>
        /// <param name="off_minutes"></param>
        public void PresetTimer(byte on_hour, byte on_minutes, byte off_hour, byte off_minutes)
        {
            byte[] timer_msg = MODE_TIMER;
            timer_msg[1] = on_hour;
            timer_msg[2] = on_minutes;
            timer_msg[3] = off_hour;
            timer_msg[4] = off_minutes;
            sendMsg(timer_msg);
        }

        /// <summary>
        /// emulate default send message on button TIMER on remote control on iOS
        /// </summary>
        public void PresetTimer()
        {
            sendMsg(MODE_TIMER);
        }

        /// <summary>
        /// Experemental ALARM MODE ???
        /// </summary>
        /// <param name="hours">??? hours ???</param>
        /// <param name="minutes">??? minutes ???</param>
        public void PresetAlarm(byte hours, byte minutes)
        {
            byte[] alarm_msg = MODE_ALARM;
            alarm_msg[1] = hours;
            alarm_msg[2] = minutes;
            sendMsg(alarm_msg);
        }

        /// <summary>
        /// emulate default send message on button SLEEP on remote control on iOS
        /// </summary>
        public void PresetSleep()
        {
            sendMsg(MODE_SLEEP);
        }

        /// <summary>
        /// Experemental RECREATION MODE ???
        /// </summary>
        /// <param name="byte_R">RGB red value</param>
        /// <param name="byte_G">RGB green value</param>
        /// <param name="byte_B">RGB blue value</param>
        public void PresetRecreation(byte byte_R, byte byte_G, byte byte_B)
        {
            byte[] recreation_msg = MODE_RECREATION;
            recreation_msg[1] = byte_R;
            recreation_msg[2] = byte_G;
            recreation_msg[3] = byte_B;
            sendMsg(recreation_msg);
        }
        #endregion Preset commands






    }
}
