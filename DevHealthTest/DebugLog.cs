		
		#region Namespace Import Declarations
		
			using System;
			
		#endregion
		
	public class DebugLog
	
	{
		
		#region Nested Types
		
			public enum LOGLEVEL
			
			{
				LogErrors = 0, 
				LogWarnings = 1, 
				LogDebug = 2, 
				LogMessages = 3, 
				LogAlways = 4, 
			}
			
		#endregion
		
		#region Constructors
		
			public DebugLog ()
			
			{
			}
			
		#endregion
		
		#region Methods
		
			public static void Write (LOGLEVEL logLevel, string logEntry, DateTime eventTime)
			
			{
                System.Diagnostics.Debug.WriteLine(eventTime.ToShortDateString() + " " + eventTime.ToShortTimeString() +" "+ logEntry);
			}
			
			public static void Write (LOGLEVEL logLevel, string logEntry)
			
			{
                System.Diagnostics.Debug.WriteLine(logEntry);
			}
			
			public static void Write (string logEntry)			
			{
                System.Diagnostics.Debug.WriteLine(logEntry);
			}
			
		#endregion
	}
	

