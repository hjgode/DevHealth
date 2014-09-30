namespace Intermec.SSAPIInterface
{
		
		#region Namespace Import Declarations
		
			using Intermec.DeviceManagement.SmartSystem;
			using System;
			using System.Diagnostics;
			using System.Runtime.InteropServices;
			using System.Text;
			
		#endregion
		
	public class SSAPIInterface
	
	{
		
		#region Fields
			private bool connected;
			private int hServerObj;
			private SS_ApiEx ssapi;
			private string threadID;
		#endregion
		
		#region Constructors
		
			public SSAPIInterface ()
			
			{
				this.connected = false;
			}
			
		#endregion
		
		#region Properties
		
			public bool Connected
			
			{
				get
				{
					return this.connected;
				}
			}
			
		#endregion
		
		#region Methods
		
			public bool Connect ()
			
			{
				byte[] byteArray1;
				uint uInt32_1;
				Exception exception1;
				if (this.connected)
				{
					return true;
				}
				else
				{
					DebugLog.Write ("SSAPIInterface Connect");
					this.threadID = Process.GetCurrentProcess ().Id.ToString ("x");
					this.ssapi = new SS_ApiEx (("TCOClnt" + this.threadID));
					try
					{
						byteArray1 = Encoding.UTF8.GetBytes ("09873-9069876950-90808-174789707");
						this.ssapi.Init (byteArray1);
						uInt32_1 = this.ssapi.Connect_W (uint.MinValue);
						this.LogResult ("Connect", uInt32_1);
						if (uInt32_1 == uint.MinValue)
						{
							this.connected = true;
						}
					}
					catch (Exception exception2)
					{
						exception1 = exception2;
						DebugLog.Write (("SSAPI::Connect Exception:" + exception1.Message));
					}
					DebugLog.Write (("SSAPIInterface Connect-" + this.connected.ToString ()));
					return this.connected;
				}
			}
			
			public void Disconnect ()
			
			{
				Exception exception1;
				try
				{
					uint uInt32_1 = this.ssapi.Disconnect ();
				}
				catch (Exception exception2)
				{
					exception1 = exception2;
					DebugLog.Write (("SSAPI::Disconnect Exception:" + exception1.Message));
				}
				this.connected = false;
			}
			
			public uint Get (string strXML, ref StringBuilder output)
			
			{
				uint uInt32_1;
				Exception exception1;
				if (! this.connected)
				{
					DebugLog.Write ("Get error: not connected");
					return ((uint) 0xC16E0027);
				}
				else
				{
					DebugLog.Write (("SSAPIInterface::Get :" + strXML));
					try
					{
						uInt32_1 = this.ssapi.DoAction (((string) null), "Get", strXML, output, ((string) null));
						this.LogResult ("Get", uInt32_1);
						return uInt32_1;
					}
					catch (Exception exception2)
					{
						exception1 = exception2;
						DebugLog.Write (("SSAPI::Get Exception:" + exception1.Message));
					}
					return ((uint) 0xC16E0021);
				}
			}
			
			private void LogResult (string function, uint resp)
			
			{
                System.Diagnostics.Debug.WriteLine("SSAPIInterface: " + function + " < " + resp.ToString());
			}
			
			public uint Set (string strXML)
			
			{
				string string1;
				uint uInt32_1;
				Exception exception1;
				if (! this.connected)
				{
					DebugLog.Write ("Set error: not connected");
					return ((uint) 0xC16E0027);
				}
				else
				{
					DebugLog.Write (("SSAPIInterface::Set :" + strXML));
					try
					{
						string1 = ((string) null);
						uInt32_1 = this.ssapi.DoAction (((string) null), "Set", strXML, string1);
						this.LogResult ("Set", uInt32_1);
						return uInt32_1;
					}
					catch (Exception exception2)
					{
						exception1 = exception2;
						DebugLog.Write (("SSAPI::Set Exception:" + exception1.Message));
					}
					return ((uint) 0xC16E0021);
				}
			}
			
			public bool SetServerNotVisible ()
			
			{
				bool b1 = true;
				DebugLog.Write ("SetServerVisible");
				if (SSAPIInterface.SS_ServerSetNotVisible (this.hServerObj) != uint.MinValue)
				{
					DebugLog.Write ("SS_ServerSetNotVisible Error");
					b1 = false;
				}
				if (SSAPIInterface.SS_DestroyObj (this.hServerObj) == uint.MinValue)
				{
					return b1;
				}
				else
				{
					DebugLog.Write ("SS_DestroyObj Error");
					return false;
				}
			}
			
			public bool SetServerVisible ()
			
			{
				uint uInt32_1;
				DebugLog.Write ("SetServerVisible");
				if ((uInt32_1 = SSAPIInterface.SS_GetServerObj (out this.hServerObj)) != uint.MinValue)
				{
					DebugLog.Write (("SS_GetServerObj ERROR: " + uInt32_1.ToString ()));
					return false;
				}
				else if (SSAPIInterface.SS_ServerSetVisible (this.hServerObj) == uint.MinValue)
				{
					return true;
				}
				else
				{
					DebugLog.Write ("SS_ServerSetVisible Error");
					return false;
				}
			}
			
			[DllImportAttribute("ssapi.dll", EntryPoint = "SS_DestroyObj", CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			public static extern uint SS_DestroyObj (int a_svrObj);
			[DllImportAttribute("ssapi.dll", EntryPoint = "SS_GetServerObj", CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			public static extern uint SS_GetServerObj ([Out] out int a_svrObj);
			[DllImportAttribute("ssapi.dll", EntryPoint = "SS_ServerSetNotVisible", CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			public static extern uint SS_ServerSetNotVisible (int a_svrObj);
			[DllImportAttribute("ssapi.dll", EntryPoint = "SS_ServerSetVisible", CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = true, CallingConvention = CallingConvention.Winapi)]
			[PreserveSigAttribute()]
			public static extern uint SS_ServerSetVisible (int a_svrObj);
		#endregion
	}
	
}

