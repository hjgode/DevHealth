using System;
using System.Xml;
using System.Xml.Serialization;

//TCO_Client_GUI
namespace Intermec.SSAPIInterface

{
		
		#region Namespace Import Declarations
		
			using System;
			using System.Collections.Generic;
			using System.IO;
			using System.Reflection;
			using System.Runtime.InteropServices;
			using System.Text;
			using System.Xml;
			
		#endregion
		
	public class HealthModel:IDisposable
	
	{
        private static SSAPIInterface ssAPIInterface = new SSAPIInterface();
        public SSAPIInterface SSAPI
        {
            get
            {
                if (!ssAPIInterface.Connected)
                {
                    ssAPIInterface.Connect();
                }
                return ssAPIInterface;
            }
        }

		#region Fields
			private Dictionary<String, String> _healthData;
			private IntPtr hEvent;
		#endregion
		
		#region Nested Types
		
			private enum EVENTDEFS
			
			{
				EVENT_PULSE = 1, 
				EVENT_RESET = 2, 
				EVENT_SET = 3, 
			}
			
		#endregion
		
		#region Constructors

            public void Dispose()
            {
                if(hEvent!=IntPtr.Zero)
                    HealthModel.CloseHandle(this.hEvent);
                if (this.SSAPI.Connected)
                    this.SSAPI.Disconnect();
            }

            public string getBattInfo(ref uint uError)
            {
                string sRet = "";
                uint uiRet = 0;

                this.hEvent = IntPtr.Zero;
                StringBuilder stringBuilder1 = new StringBuilder();
                if ((!this.SSAPI.Connected) && (!this.SSAPI.Connect()))
                {
                    return "";
                }
                stringBuilder1.Length = 256000;
                if (this.hEvent == IntPtr.Zero)
                {
                    this.hEvent = HealthModel.CreateEvent(IntPtr.Zero, true, false, "SS_HEALTH_GUI_ACCESS");
                }
                #region batteryProps
                string[] sBattProp = { 
                        "BackupBatteryFlag",
                        "BackupBatteryFullLifeTime",
                        "BackupBatteryLifePercent",
                        "BackupBatteryLifeTime",
                        "BackupBatteryVoltage",
                        "BatteryAverageCurrent",
                        "BatteryAverageInterval",
                        "BatteryCPUUsage",
                        "BatteryChemistry",
                        "BatteryCurrent",
                        "BatteryHealth",
                        "BatteryFlag",
                        "BatteryFullLifeTime",
                        "BatteryIdleTimeout",
                        "BatteryLastChanged",
                        "BatteryLifePercent",
                        "BatteryLifeTime",
                        "BatteryPrevCPUUsage",
                        "BatteryTemperature",
                        "BatteryVoltage",
                        "BatterymAHourConsumed",
                        "ChargingTime",
                        "ExtremeTemperatureTime",
                        "HighTemperatureTime",
                        "LastFullCharge",
                        "LowTemperatureTime",
                        "PartNumber",
                        "SerialNumber",
                        "UsageTime",
                };
                #endregion
                string string1 = "<Subsystem Name=\"Device Monitor\"><Group Name=\"ITCHealth\">\r\n" ;
                foreach (string sProp in sBattProp)
                {
                    string1 += "<Field Name=\"System\\Power\\Battery\\" + sProp + "\" >0</Field>\r\n"; // Type=\"Integer\"
                }
                //string1 += "<Field Name=\"System\\Power\\Battery\\BackupBatteryFlag\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BackupBatteryFullLifeTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BackupBatteryLifePercent\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BackupBatteryLifeTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BackupBatteryVoltage\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryAverageCurrent\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryAverageInterval\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryCPUUsage\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryChemistry\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryCurrent\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryHealth\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryFlag\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryFullLifeTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryIdleTimeout\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryLastChanged\" Type=\"String\">Str</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryLifePercent\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryLifeTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryPrevCPUUsage\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryTemperature\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatteryVoltage\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\BatterymAHourConsumed\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\ChargingTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\ExtremeTemperatureTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\HighTemperatureTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\LastFullCharge\" Type=\"String\">Str</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\LowTemperatureTime\" Type=\"Integer\">0</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\PartNumber\" Type=\"String\">Str</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\SerialNumber\" Type=\"String\">Str</Field>\r\n";
                //string1 += "<Field Name=\"System\\Power\\Battery\\UsageTime\" Type=\"Integer\">0</Field>\r\n";
                string1 += "</Group></Subsystem>";

                uint uInt32_1 = 0;
                if (this.hEvent != IntPtr.Zero)
                {
                    bool b1 = HealthModel.EventModify(this.hEvent, 3);
                    uInt32_1 = this.SSAPI.Get(string1, ref stringBuilder1);
                    bool b2 = HealthModel.EventModify(this.hEvent, 2);
                    bool b3 = HealthModel.CloseHandle(this.hEvent);
                }
                DebugLog.Write(("Get Health data: " + stringBuilder1.ToString()));
                sRet = stringBuilder1.ToString();
                string sReturn = "";
                if (uInt32_1 == Intermec.DeviceManagement.SmartSystem.ITCSSErrors.E_SS_SUCCESS)
                {
                    ////unpack xml answer
                    //DevHealth.DevHealthUnpacker unpacker = new DevHealth.DevHealthUnpacker(sRet);
                    //System.Collections.ArrayList _infos = unpacker.HealthInfos;
                    sReturn = sRet;
                }
                else
                {
                    string sVal = stringBuilder1.ToString();
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
                    System.Diagnostics.Debug.WriteLine(uiRet.ToString("X8") + "\r\n" + stringBuilder1);
                    System.Diagnostics.Debug.WriteLine(DevHealth.SSerrorStrings.getErrorString("0x" + uiRet.ToString("X8")));
                    System.Diagnostics.Debug.WriteLine(DevHealth.SSAPIerrors.getErrString(uiRet));
                    uError = uiRet;
                }

                uError = uiRet;
                return sReturn;
            }

            public string getHealthInfo(string s, ref uint uError)
            {
                string sRet = "";
                uint uiRet = 0;

                this.hEvent = IntPtr.Zero;
                StringBuilder stringBuilder1 = new StringBuilder();
                if ((!this.SSAPI.Connected) && (!this.SSAPI.Connect()))
                {
                    return "";
                }
                stringBuilder1.Length = 256000;
                if (this.hEvent == IntPtr.Zero)
                {
                    this.hEvent = HealthModel.CreateEvent(IntPtr.Zero, true, false, "SS_HEALTH_GUI_ACCESS");
                }
                string string1 = "<Subsystem Name=\"Device Monitor\"><Group Name=\"ITCHealth\"><Field Name=\"" + s 
                    + "\"></Field></Group></Subsystem>";
                uint uInt32_1 = 0;
                if (this.hEvent != IntPtr.Zero)
                {
                    bool b1 = HealthModel.EventModify(this.hEvent, 3);
                    uInt32_1 = this.SSAPI.Get(string1, ref stringBuilder1);
                    bool b2 = HealthModel.EventModify(this.hEvent, 2);
                    bool b3 = HealthModel.CloseHandle(this.hEvent);
                }
                DebugLog.Write(("Get Health data: " + stringBuilder1.ToString()));
                sRet = stringBuilder1.ToString();
                if (uInt32_1 == Intermec.DeviceManagement.SmartSystem.ITCSSErrors.E_SS_SUCCESS)
                {

                    string sSearch = s;// "FirmwareVersion\">"; // "<Field Name=\"System\\Info\\LastBoot\">"
                    int iIndex;
                    iIndex = sRet.IndexOf(sSearch);
                    if (iIndex >= 0)
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(sRet);
                        XmlNodeList xNodes = xDoc.GetElementsByTagName("Field");
                        XmlNode root = xDoc.FirstChild;
                        if (root!=null)
                        {
                            sRet = root.InnerText;
                        }
                    }
                }
                else
                {
                    string sVal = stringBuilder1.ToString();
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
                    System.Diagnostics.Debug.WriteLine(uiRet.ToString("X8") + "\r\n" + stringBuilder1);
                    System.Diagnostics.Debug.WriteLine(DevHealth.SSerrorStrings.getErrorString("0x" + uiRet.ToString("X8")));
                    System.Diagnostics.Debug.WriteLine(DevHealth.SSAPIerrors.getErrString(uiRet));
                    uError = uiRet;
                }

                return sRet;
            }

			public HealthModel ()
			{
				int i1;
				XmlTextReader xmlTextReader1;
				string string3;
				Exception exception1;
				this.hEvent = IntPtr.Zero;
				this._healthData = new Dictionary<String, String> ();
				this._healthData["System\\TCOClientUIVersion"] = Assembly.GetExecutingAssembly ().GetName ().Version.ToString ();
				this._healthData["System\\TCOClientHealthDate"] = DateTime.Now.ToString ();
				StringBuilder stringBuilder1 = new StringBuilder ();
				string string1 = "<Subsystem Name=\"Device Monitor\"><Group Name=\"ITCHealth\"><Field Name=\"HealthInfo"
				+ "\"></Field></Group></Subsystem>";
				if ((! this.SSAPI.Connected) && (! this.SSAPI.Connect ()))
				{
					return;
				}
				stringBuilder1.Length = 256000;
				if (this.hEvent == IntPtr.Zero)
				{
					this.hEvent = HealthModel.CreateEvent (IntPtr.Zero, true, false, "SS_HEALTH_GUI_ACCESS");
				}
                string1 = "<Subsystem Name=\"Device Monitor\"><Group Name=\"ITCHealth\"><Field Name=\"System\\Info\\FirmwareVersion" 
                    + "\"></Field></Group></Subsystem>";
				if (this.hEvent != IntPtr.Zero)
				{
					bool b1 = HealthModel.EventModify (this.hEvent, 3);
					uint uInt32_1 = this.SSAPI.Get (string1, ref stringBuilder1);
					bool b2 = HealthModel.EventModify (this.hEvent, 2);
					bool b3 = HealthModel.CloseHandle (this.hEvent);
				}
				DebugLog.Write (("Get Health data: " + stringBuilder1.ToString ()));
				string string2 = stringBuilder1.ToString ();
				for (i1 = 0; (i1 < string2.Length); i1++)
				{
					if (string2[i1] < '\u0020')
					{
						string2 = (string2.Substring (0, i1) + " " + string2.Substring (((int) (i1 + 1))));
					}
				}
				if (string2.Length > 0)
				{
					try
					{
						xmlTextReader1 = new XmlTextReader (((TextReader) new StringReader (("<?xml version=\"1.0\"?>" + string2))));
						xmlTextReader1.WhitespaceHandling = WhitespaceHandling.None;
						while (xmlTextReader1.Read ())
						{
							if ((((xmlTextReader1.NodeType != XmlNodeType.Element) || (xmlTextReader1.Name != "Field")) || (! xmlTextReader1.MoveToAttribute ("Name"))) || (xmlTextReader1.Value != "HealthInfo"))
							{
								continue;
							}
							bool b4 = xmlTextReader1.MoveToElement ();
							while (xmlTextReader1.Read ())
							{
								if (xmlTextReader1.NodeType != XmlNodeType.Text)
								{
									if (xmlTextReader1.NodeType != XmlNodeType.CDATA)
									{
										continue;
									}
									this.ParseHealthData (xmlTextReader1.Value);
									break;
								}
								string3 = xmlTextReader1.Value;
								if (string3.StartsWith ("<![CDATA["))
								{
									string3 = string3.Substring (9);
									string3 = string3.Substring (0, ((int) (string3.Length - 3)));
								}
								this.ParseHealthData (string3);
							}
							break;
						}
					}
					catch (Exception exception2)
					{
						exception1 = exception2;
						DebugLog.Write (("HealthModel Exception:" + exception1.Message + ": " + exception1.StackTrace));
					}
				}
			}
			
		#endregion
		
		#region Properties
		
			public Dictionary<String, String> HealthData
			
			{
				get
				{
					return this._healthData;
				}
			}
			
		#endregion
		
		#region Methods
		
			[DllImportAttribute("coredll.dll", EntryPoint = "CloseHandle", SetLastError = true, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			private static extern bool CloseHandle (IntPtr hObject);
			[DllImportAttribute("coredll.dll", EntryPoint = "CreateEvent", SetLastError = true, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			private static extern IntPtr CreateEvent (IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);
			[DllImportAttribute("coredll.dll", EntryPoint = "EventModify", SetLastError = true, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			private static extern bool EventModify (IntPtr hObject, int func);
			private void ParseHealthData (string Data)
			
			{
				XmlTextReader xmlTextReader1;
				Exception exception1;
				string[] stringArray1;
				DebugLog.Write (("ParseHealthData: " + Data));
				string string1 = string.Empty;
				string string2 = string.Empty;
				string string3 = string.Empty;
				try
				{
					xmlTextReader1 = new XmlTextReader (((TextReader) new StringReader (Data)));
					xmlTextReader1.WhitespaceHandling = WhitespaceHandling.None;
					while (xmlTextReader1.Read ())
					{
						if (xmlTextReader1.NodeType == XmlNodeType.Element)
						{
							if (xmlTextReader1.Name != "Group")
							{
								if (xmlTextReader1.Name != "HGroup")
								{
									if (xmlTextReader1.Name != "Item")
									{
										continue;
									}
									if (xmlTextReader1.HasAttributes)
									{
										bool b3 = xmlTextReader1.MoveToAttribute ("Name");
										string3 = xmlTextReader1.Value;
									}
									bool b4 = xmlTextReader1.Read ();
									stringArray1 = new string[] { string1, "\\", string2, "\\", string3 };
									this._healthData[string.Concat (stringArray1)] = xmlTextReader1.Value;
									continue;
								}
								if (xmlTextReader1.HasAttributes)
								{
									bool b2 = xmlTextReader1.MoveToAttribute ("Name");
									string2 = xmlTextReader1.Value;
								}
								continue;
							}
							if (! xmlTextReader1.HasAttributes)
							{
								continue;
							}
							bool b1 = xmlTextReader1.MoveToAttribute ("Instance");
							string1 = xmlTextReader1.Value;
						}
					}
					xmlTextReader1.Close ();
				}
				catch (Exception exception2)
				{
					exception1 = exception2;
					DebugLog.Write (("Exception parsing health data: " + exception1.Message));
				}
			}
			
		#endregion
	}
	
}

