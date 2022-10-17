using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Count_Down_Timer
{
    public partial class mainForm : Form
    {
        private int counter = 24;
        private int fontSize = 250;
        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LabelDecor();
        }

        private void LabelDecor()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.digital_display_tfb.Length;
            byte[] fontdata = Properties.Resources.digital_display_tfb;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);            

            label1.Font = new Font(pfc.Families[0], fontSize);
            label1.Text = counter.ToString("D2");

            label1.ForeColor = Color.Red;
            label1.Location = new Point((SystemInformation.PrimaryMonitorSize.Width / 2) - (label1.Width / 2), (SystemInformation.PrimaryMonitorSize.Height / 2) - (label1.Height / 2));

        }

        private void systemTimer_Tick(object sender, EventArgs e)
        {
            var soundPlayer = new System.Media.SoundPlayer();

            if (counter >= 0)
            {
                label1.Text = counter.ToString("D2");
                counter--;

                //soundPlayer.SoundLocation = "long-beepy.wav";
                //soundPlayer.PlaySync();
                if(counter == -1)
                {
                    soundPlayer.SoundLocation = "long-buzzer.wav";
                    soundPlayer.Play();
                }
                else if (counter >= 0 && counter <= 4)
                {
                    //soundPlayer.SoundLocation = "loud-beepy.wav";
                    //soundPlayer.Play();
                }
            }
            else if(counter == 0)
            {
                soundPlayer.Stop();
            }
        }
        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                //Set timer to 24
                counter = 24;
            }
            else if (e.KeyCode == Keys.S)
            {
                //Set timer to 14
                counter = 14;
            }
            else if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                //font size increase
                fontSize += 20;
                LabelDecor();
            }
            else if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Subtract)
            {
                //font size decrease
                fontSize -= 20;
                LabelDecor();
            }
            else if (e.KeyCode == Keys.Space)
            {
                //timer start or stop
                if(systemTimer.Enabled == true)
                {
                    systemTimer.Stop();
                }
                else
                {
                    systemTimer.Start();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                counter++;
            }
            else if (e.KeyCode == Keys.Down)
            {
                counter--;
            }
            else if (e.KeyCode == Keys.P)
            {
                systemTimer.Stop();
            }
            else if (e.KeyCode == Keys.O)
            {
                systemTimer.Start();
            }
            else if (e.KeyCode == Keys.T || e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

        }
    }
}
