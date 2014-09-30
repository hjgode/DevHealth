using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevHealthAPI;

namespace DevHealthApiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getInfos();
        }

        string SystemInfoBase=@"System\Info\";
        string[] SystemInfos = new string[] {"FirmwareVersion", "HardwareCfgString", "ManufactureDate", "SerialNumber",
            "ServiceDate","UniqueId","UUID"};
        string SystemInfoBatteryBase = @"System\Power\Battery\";
        string[] BatteryInfos = new string[] { "PartNumber", "SerialNumber", "BatteryHealth", "BatteryLastChanged", "BatteryLifePercent", "BackupBatteryLifeTime" };

        void getInfos()
        {
            DevHealthAPI.SSAPIwrapper hm = new SSAPIwrapper();
            uint uError = 0;
            string sInfoXml="";
            StringBuilder sbInfo = new StringBuilder();
            foreach (string sQuery in SystemInfos)
            {
                sInfoXml = SystemInfoBase + sQuery;
                sbInfo.Append(sQuery + ": " + hm.getHealthInfo(sInfoXml, ref uError) + "\r\n");
            }
            foreach (string sQuery in BatteryInfos)
            {
                sInfoXml = SystemInfoBatteryBase + sQuery;
                sbInfo.Append(sQuery + ": " + hm.getHealthInfo(sInfoXml, ref uError) + "\r\n");
            }
            txtLog.Text = sbInfo.ToString();
        }
    }
}