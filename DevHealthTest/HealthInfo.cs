using System;

using System.Collections.Generic;
using System.Text;

using System.IO;

using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

using System.Xml.Linq;
using System.Reflection;

namespace DevHealth
{

    class HealthInfos
    {
        public class healthInfo
        {
            public static string AppPath
            {
                get
                {
                    string _AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    if (!_AppPath.EndsWith(@"\"))
                        _AppPath += @"\";
                    Uri uri = new Uri(_AppPath);
                    _AppPath = uri.AbsolutePath;
                    _AppPath = _AppPath.Replace("/", "\\");
                    return _AppPath;
                }
            }
            /// <summary>
            /// full qualified name of the info
            /// </summary>
            public string sPath;
            public string sType;
            public eTypes eType;
            /// <summary>
            /// short name
            /// </summary>
            public string sShortName;
            public enum eTypes
            {
                @Integer = 0,
                @String,
                @Float,
                @Boolean,
                unknown = -1
            }
            /// <summary>
            /// create a new healthInfo
            /// </summary>
            /// <param name="sP">full path</param>
            /// <param name="sT">type descriptor</param>
            public healthInfo(string sP, string sT)
            {
                sPath = sP;
                sType = sT;
                if (sT == "Integer")
                    eType = eTypes.Integer;
                else if (sT == "String")
                    eType = eTypes.String;
                else if (sT == "Float")
                    eType = eTypes.Float;
                else if (sT == "Boolean")
                    eType = eTypes.Boolean;
                else
                    eType = eTypes.unknown;
                string[] sTemp = sP.Split('\\');
                sShortName = sTemp[sTemp.Length - 1];
            }
            public override string ToString()
            {
                return sShortName;
            }
            public string getXML()
            {
                string ssHWconfig = "<Subsystem Name=\"Device Monitor\">\r\n";
                ssHWconfig += "<Group Name=\"ITCHealth\">\r\n";
                ssHWconfig += "<Field Name=\"" + this.sPath + "\"></Field>\r\n";
                ssHWconfig += "</Group></Subsystem>";
                return ssHWconfig;
            }
        }
        public const string sDevHealthSysXML = "SystemHealth.xml";
        public const string sDevHealthNetXML = "NetworkHealth.xml";

        public List<healthInfo> _Infos;
        public HealthInfos()
        {
            _Infos = new List<healthInfo>();
            //readSystem();
            //readNetwork();
            readHealthXML(sDevHealthSysXML);
            readHealthXML(sDevHealthNetXML);
        }

        public string getInfo(healthInfo hiInfo, ref uint uError)
        {
            string sReturn = "";
            string ssHWconfig = "<Subsystem Name=\"Device Monitor\">\r\n";
            ssHWconfig += "<Group Name=\"ITCHealth\">\r\n";
            ssHWconfig += "<Field Name=\""+ hiInfo.sPath + "\"></Field>\r\n"; //removed {0} before </Field> too
            ssHWconfig += "</Group></Subsystem>";

            Intermec.DeviceManagement.SmartSystem.ITCSSApi ssAPI = new Intermec.DeviceManagement.SmartSystem.ITCSSApi();
            
            uint uiRet = 0;
            StringBuilder sbRetData = new StringBuilder(1024);
            int iLen = 1024;

            uiRet = ssAPI.Get(ssHWconfig, sbRetData, ref iLen, 0);
            uError = uiRet;
            if (uiRet == Intermec.DeviceManagement.SmartSystem.ITCSSErrors.E_SS_SUCCESS)
            {
                string sVal = sbRetData.ToString();
                string sSearch = hiInfo.sPath;// "FirmwareVersion\">"; // "<Field Name=\"System\\Info\\LastBoot\">"
                int iIndex;
                iIndex = sVal.IndexOf(sSearch);
                if (iIndex >= 0)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(sVal);
                    XmlNodeList xNodes = xDoc.GetElementsByTagName("Field");
                    XmlNode root = xDoc.FirstChild;
                    if (root!=null)
                    {
                        sReturn = root.InnerText;
                    }
                }
            }
            else
            {
                string sVal = sbRetData.ToString();
                int iIndex = sVal.IndexOf("Error=\"");
                string sErr = "";
                if (iIndex >= 0)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(sVal);
                    //XmlNode xNode = xDoc.SelectSingleNode("/Subsystem/Group[1]/Field[1]");// "//Field[@Name='" + hiInfo.sPath + "']");
                    XmlNodeList xNodes = xDoc.GetElementsByTagName("Field");
                    for (int j = 0; j < xNodes.Count; j++)
                    {
                        System.Diagnostics.Debug.WriteLine(xNodes[j].InnerXml);
                        foreach (XmlAttribute xa in xNodes[j].Attributes)
                        {
                            System.Diagnostics.Debug.WriteLine(xa.Name);
                            if (xa.Name.Equals("Error"))
                            {
                                System.Diagnostics.Debug.WriteLine(xa.InnerXml);
                                sErr = xa.InnerXml;
                            }
                        }
                    }

                }
                uiRet = uint.Parse(sErr, System.Globalization.NumberStyles.AllowHexSpecifier);
                
                System.Diagnostics.Debug.WriteLine(uiRet.ToString("X8") + "\r\n" + sbRetData);
                System.Diagnostics.Debug.WriteLine(SSerrorStrings.getErrorString("0x" + uiRet.ToString("X8")));
                System.Diagnostics.Debug.WriteLine(SSAPIerrors.getErrString(uiRet));
                uError = uiRet;
            }
            ssAPI = null;
            return sReturn;
        }

        void readHealthXML(string sDevXML)
        {
            Stream _stream;
            if (File.Exists(healthInfo.AppPath + sDevXML))
                _stream = new FileStream(healthInfo.AppPath + sDevXML, FileMode.Open);
            else
            {
                string sNameSpace = Assembly.GetExecutingAssembly().FullName.Split(new char[]{','})[0];
                _stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(sNameSpace+"."+sDevXML);
            }

            System.Diagnostics.Debug.Assert(_stream != null);

            XDocument xd = new XDocument();
            XmlReader xr = XmlReader.Create(_stream);
            xd = XDocument.Load(xr);

            //we have only two levels
            foreach (XElement xn in xd.Elements())
            {
                //System.Diagnostics.Debug.WriteLine(xn.Name.ToString());
                foreach (XElement xSub in xn.Elements())
                {
                    //System.Diagnostics.Debug.WriteLine(xSub.Name.ToString());
                    foreach (XElement xField in xSub.Elements())
                    {
                        //System.Diagnostics.Debug.WriteLine(xField.Name.ToString());
                        if (xField.Name == "Field")
                        {
                            if (!xField.Attribute("Name").Value.Contains("AttachedDevice"))//exclude unreadable OneWire infos
                            {
                                //System.Diagnostics.Debug.WriteLine(xField.Attribute("Name").Value);
                                string sName = xField.Attribute("Name").Value;
                                string sType = "";
                                if (xField.Attribute("Type") != null)
                                {
                                    sType = xField.Attribute("Type").Value;
                                }
                                _Infos.Add(new healthInfo(sName, sType));
                            }
                        }
                    }
                }
            }
        }//ReadHealthXML
    }
}
