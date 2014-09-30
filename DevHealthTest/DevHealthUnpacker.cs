using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DevHealth
{
    /*
    <Subsystem Name="Device Monitor">
    <Group Name="ITCHealth">
        <Field Name="System\\Power\\Battery\\BackupBatteryFlag" Type="Integer">8
        </Field>
        <Field Name="System\\Power\\Battery\\BackupBatteryFullLifeTime" Type="Integer">720000
        </Field>
        <Field Name="System\\Power\\Battery\\BackupBatteryLifePercent" Type="Integer">58
        </Field>
        <Field Name="System\\Power\\Battery\\BackupBatteryLifeTime" Type="Integer">417600
        </Field>
        <Field Name="System\\Power\\Battery\\BackupBatteryVoltage" Type="Integer">2564
        </Field>
        <Field Name="System\\Power\\Battery\\BatteryAverageCurrent" Type="Integer">458
        </Field>
        <Field Name="System\\Power\\Battery\\BatteryAverageInterval" Type="Integer">458
        </Field>
    </Group>
    </Subsystem>
*/
    class DevHealthUnpacker
    {
        public class HealthInfo
        {
            public HealthInfo()
            {
                _FieldName = "";
                _shortName = "";
                _categoryMain = "";
                _CategorySub = "";
                this.Type = "";
                Value = "";
            }
            private string _shortName = "";     //ie BackupBatteryFlag
            private string _FieldName;          //System\Power\Battery\BackupBatteryFlag
            private string _categoryMain="";
            public string CategoryMain
            {
                get { return _categoryMain; }
            }
            private string _CategorySub = "";   //ie Battery
            public string FieldName
            {
                set
                {
                    System.Diagnostics.Debug.WriteLine("Adding " + value);
                    _FieldName = value;
                    string[] s = _FieldName.Split(new char[] { '\\' });
                    if(s.Length>1)
                        _shortName = s[s.Length-1];
                    if (s.Length > 2)
                        _CategorySub = s[s.Length - 2];
                    if (s.Length > 3)
                        _categoryMain = s[s.Length - 3];
                }
                get
                {
                    return _FieldName;
                }
            }
            public string Type;
            public string Error;
            public string Value;
            public string ShortName
            {
                get { return _shortName; }
            }
            public string printInfo()
            {
                string s = "";
                if(this.Error=="")
                    s = this.ShortName + ": " + this.Value;
                else
                    s = this.ShortName + ": Error: " + this.Error;
                return s;
            }
        }
        ArrayList _healthInfos=null;        
        
        public DevHealthUnpacker(string sXML)
        {
            _healthInfos = new ArrayList(1);

            TextReader tr = new StringReader(sXML);
            XmlTextReader xtr = new XmlTextReader(tr);

            xtr.WhitespaceHandling = WhitespaceHandling.None;
            xtr.Read(); // read the XML declaration node, advance to <suite> tag

            while (!xtr.EOF) //load loop
            {
                if (xtr.Name == "Subsystem" && !xtr.IsStartElement()) 
                    break;

                while (xtr.Name != "Field" || !xtr.IsStartElement())
                    xtr.Read(); // advance to <Field> tag

                HealthInfo tc = new HealthInfo();
                tc.FieldName = xtr.GetAttribute("Name");
                tc.Type = xtr.GetAttribute("Type");
                
                tc.Error = xtr.GetAttribute("Error");
                if (tc.Error == null)
                    tc.Error = "";

                tc.Value = xtr.ReadElementString("Field"); // consumes the </arg1> tag
                // we are now at an </testcase> tag

                _healthInfos.Add(tc);

                xtr.Read(); // and now either at <testcase> tag or </suite> tag
            } // load loop

            xtr.Close();
        }
        
        public ArrayList HealthInfos
        {
            get { return _healthInfos; }
        }

        public static string sXmlTest = "    <Subsystem Name=\"Device Monitor\">\r\n" +
                            "    <Group Name=\"ITCHealth\">\r\n" +
                            "        <Field Name=\"System\\Power\\Battery\\BackupBatteryFlag\" Type=\"Integer\">8" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BackupBatteryFullLifeTime\" Type=\"Integer\">720000" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BackupBatteryLifePercent\" Type=\"Integer\">58" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BackupBatteryLifeTime\" Type=\"Integer\">417600" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BackupBatteryVoltage\" Type=\"Integer\">2564" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BatteryAverageCurrent\" Type=\"Integer\">458" +
                            "        </Field>" +
                            "        <Field Name=\"System\\Power\\Battery\\BatteryAverageInterval\" Type=\"Integer\">458" +
                            "        </Field>\r\n" +
                            "    </Group>\r\n" +
                            "    </Subsystem>\r\n";
    }
}
