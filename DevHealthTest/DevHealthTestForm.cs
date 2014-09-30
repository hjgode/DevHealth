using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace DevHealthTest
{
    public partial class DevHealthTestForm : Form
    {
        public DevHealthTestForm()
        {
            InitializeComponent();
            DevHealth.HealthInfos hInfo = new DevHealth.HealthInfos();
            comboBox1.Items.Clear();
            foreach (DevHealth.HealthInfos.healthInfo hI in hInfo._Infos)
            {
                comboBox1.Items.Add(hI);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btnBattery_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                Intermec.SSAPIInterface.HealthModel health = new Intermec.SSAPIInterface.HealthModel();
                uint uErr = 0;
                string s = health.getBattInfo(ref uErr);

                string answer = "";
                //unpack xml answer
                DevHealth.DevHealthUnpacker unpacker = new DevHealth.DevHealthUnpacker(s);
                System.Collections.ArrayList _infos = unpacker.HealthInfos;

                foreach (DevHealth.DevHealthUnpacker.HealthInfo hi in _infos)
                {
                    System.Diagnostics.Debug.WriteLine(hi.printInfo());
                    answer += hi.printInfo() + "\r\n";
                }

                health.Dispose();
                if (uErr == 0)
                {
                    txtOut.Text = answer;
                }
                else
                {
                    txtOut.Text = DevHealth.SSerrorStrings.getSSAPIErrorString(uErr);
                }
            }
            catch (Exception ex)
            {
                txtOut.Text = ex.Message;
            }
            Cursor.Current = Cursors.Default;
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                return;
            
            DevHealth.HealthInfos.healthInfo hI = (DevHealth.HealthInfos.healthInfo)comboBox1.Items[comboBox1.SelectedIndex];

            DevHealth.HealthInfos hInfo = new DevHealth.HealthInfos();
            
            uint uErr = 0;
            string sInfo = hInfo.getInfo(hI, ref uErr);
            if (uErr == 0)
                txtOut.Text = sInfo;
            else
                txtOut.Text = DevHealth.SSerrorStrings.getSSAPIErrorString(uErr);// getErrorString("0x" + uErr.ToString("X8"));

        }
    }
}