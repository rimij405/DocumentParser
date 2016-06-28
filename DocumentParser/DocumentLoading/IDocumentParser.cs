using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading
{
	public abstract class IDocumentParser : IDisposable
	{
		// What does the IDocumentParser require of its implementations?

		/*
		 First, consider it's purpose:

		IDocumentParser is an interface representing
		all the different files that need to be loaded.

		Second, consider it's initial and end states:

		Initially, IDocumentParsers should require a
		file to be loaded.

		Finally, IDocumentParsers should contain an indexed
		collection of information that can be
		searched by the main program to provide
		accurate data.

		What is this data that needs to be parsed?

		Full Name: "John Doe"
		Telephone: "888 888-8888" OR "(888) 888-8888" OR "888.888.8888" OR "888.888.8888 ext. 8"
		Address: "X [NAME] [ROAD/STREET/AVENUE/ETC.] [CITY], [STATE] [ZIP-CODE]"
		Email: "xyz@xyz.com" - the minimum character count is 3.
		Objective: "Objective" OR "Summary" OR "Mission"
		Experience: "Experience" OR "Work Experience" OR "Employment"
		Skills: "Areas of Expertise" OR "Skills"
		Achievements: "Achievements" OR "Key achievements"
		Other: "Other Relevant Experience"
		Education: "Education"		

		 */

		#region Properties

		// Debugging fields.
		private bool _initialized;
		private Stack<string> _errors;
		private List<string> _log;

		// Stopwatch methods.
		private Stopwatch _global;
		private Stopwatch _local;

		#endregion

		#region Flags

		/// <summary>
		/// IsInitialized is a boolean flag
		/// relaying the status of the
		/// _initialized field. If false,
		/// the program has not yet been
		/// initialized.
		/// </summary>
		public bool IsInitialized
		{
			get
			{
				return this._initialized;
			}
		}

		/// <summary>
		/// IsRunning is a boolean flag
		/// relaying the status of the
		/// _global diagnostics stopwatch.
		/// If false, it is not running.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				
				return this._global.IsRunning;
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// HasErrors is a boolean property
		/// that relays whether or not
		/// the error stack is null or empty.
		/// </summary>
		public bool HasErrors
		{
			get { return !(this._errors == null || this._errors.Count <= 0); }
		}

		/// <summary>
		/// HasMessages is a boolean property
		/// that relays whether or not
		/// the log list is null or empty.
		/// </summary>
		public bool HasMessages
		{
			get { return !(this._log == null || this._log.Count <= 0); }
		}

		/// <summary>
		/// PeekErrors returns the latest
		/// entry into the error stack
		/// without removing it.
		/// </summary>
		public string PeekErrors
		{
			get { return this._errors.Peek(); }
		}

		/// <summary>
		/// ErrorMessage returns the latest
		/// entry into the error stack,
		/// removing it in consequence.
		/// </summary>
		public string ErrorMessage
		{
			get { return this._errors.Pop(); }
			set { this._errors.Push(value); }
		}

		/// <summary>
		/// LogMessages returns the logged messages.
		/// </summary>
		public List<string> LogMessages
		{
			get { return this._log; }
		}

		/// <summary>
		/// Errors compiles the errors from the stack
		/// into a list, removing them.
		/// This functionality means you must store
		/// it after your first call. It allows us
		/// to have errors be, "read," and disposed.
		/// </summary>
		public List<string> Errors
		{
			get
			{
				List<string> toReturn = new List<string>();

				while (HasErrors)
				{
					toReturn.Add(ErrorMessage);
				}

				return toReturn;
			}
		}

		#endregion
		
		#region Methods
		/// <summary>
		/// IDocumentParser() is the default
		/// constructor for IDocumentParser
		/// and its implementations.
		/// </summary>

		public IDocumentParser()
		{
			_initialized = false;
		}


		/// <summary>
		/// This method contains any functions
		/// that need to be run at the start 
		/// of the class call. This
		/// switches the "isInitialized"
		/// flag to "true".
		/// </summary>
		public virtual void Init()
		{

			// Perform required base initialization operations, here. //
			_errors = new Stack<string>();
			_log = new List<string>();
			_global = new Stopwatch();
			Start();

			_initialized = true;
		}
		
		/// <summary>
		/// Run(Action) takes a delegate method
		/// and invokes it. This method allows
		/// us to track diagnostics regarding
		/// performance of an given method, 
		/// without having to worry about
		/// invoking the timer methods individually
		/// inside each particular method.
		/// </summary>
		/// <param name="delegateMethod">The targeted method.</param>
		public virtual void Run(Action delegateMethod)
		{
			// Check to see if program has been initialized.
			if (IsInitialized)
			{
				// Notify log that this method is starting.			
				Log("Beginning invocation for: \"" + delegateMethod.Method.Name + "\".",
					"\nTarget: \"" + delegateMethod.Target.ToString() + "\".",
					"\nMethod Info Reference: \"" + delegateMethod.Method.ToString() + "\".");

				// Start the local time clock.
				LogStart();

				// Invoke the method.
				delegateMethod.Invoke();

				// 
				LogEnd();

				// Notify log that this method has ended.
				Log("Completed invocation of: \"" + delegateMethod.Method.Name + "\",",
					"\n" + LogTimePrint() + " - method runtime.");

				// Check for errors.
				if (HasErrors)
				{
					List<string> errs = Errors;

					// Ask user if they would like to see the errors.
					if (Program.Prompt("There are " + errs.Count + " unviewed errors. Would you like to see them? {y}/{n}"))
					{
						if (Program.Prompt("Would you like to see errors by latest? {y}/{n}"))
						{
							int count = 0;
							foreach (string err in errs)
							{
								Program.Write("Error #" + count + ": " + err);
							}
						}
						else
						{
							for (int index = (errs.Count - 1); index >= 0; index++)
							{
								int count = (index + 1);
								Program.Write("Error #" + count + ": " + errs[index]);
							}
						}
					}
				}
				else
				{
					Log("Invocation completed with no errors.");
				}

				if (HasMessages)
				{
					// Ask user if they would like to see the log.
					if (Program.Prompt("Would you like to see the log? {y}/{n}"))
					{
						foreach (string log in LogMessages)
						{
							Program.Write(log);
						}
					}

					// Ask user if they would like to reset the log/timers.
					if (Program.Prompt("Would you like to reset the log? {y}/{n}"))
					{
						Reset();
					}
				}
				else
				{
					Log("No messages contained in the log.");
				}
			}
			else
			{
				Program.Pause("The parser has not been initialized. Please initialize it first.");
			}
		}

		/// <summary>
		/// LoadDocument(string) takes the full path
		/// of the given file, checks to see
		/// if the path leads to a valid
		/// file, and returns a boolean indicating
		/// the success of the operation. 
		/// </summary>
		/// <param name="path">The file path of the file to be loaded.</param>
		/// <returns>Returns true on file load success.</returns>
		protected abstract bool LoadDocument(string path);

		/// <summary>
		/// ValidateDocument(string) takes the full path
		/// of the given file and returns
		/// a boolean indicating the validity
		/// of the file at the end of that path.
		/// </summary>
		/// <param name="path">The file path of the file to be loaded.</param>
		/// <param name="extensions">The collection of valid extensions to check for.</param>
		/// <returns>Returns true on file validity. Returns false if file does not exist.</returns>
		protected virtual bool ValidateDocument(string path, params string[] extensions)
		{
			FileInfo file = new FileInfo(path);
			string fileName = file.Name;
			string dirName = file.Directory.FullName;

			// Check if the directory exists.
			if (Directory.Exists(dirName))
			{
				string filePath = file.FullName;

				// Check if the file exists.
				if (File.Exists(filePath))
				{

					// Check if the file has the correct extension.
					bool extMatch = false;

					foreach (string ext in extensions)
					{
						extMatch = (file.Extension == ext);
						if (extMatch) { break; }
					}

					if (!extMatch)
					{
						Log("Document validation was unsuccessful.");
						Error("FileNotFoundException: The file - " + fileName + " - exists, but has the incorrect extension.", false);
						return false;
					}
					else
					{
						Log("Document successfully validated.");
						return true;
					}

				}
				else
				{
					Log("Document validation was unsuccessful.");
					Error("FileNotFoundException: The file - " + fileName + " - does not exist.", false);
					return false;
				}

			}
			else
			{
				Log("Document validation was unsuccessful.");
				Error("DirectoryNotFoundException: The directory for " + fileName + " was not found.", false);
				return false;
			}
		}

		/// <summary>
		/// ValidateDocument(string) takes the full path
		/// of the given file and returns
		/// a boolean indicating the validity
		/// of the file at the end of that path.
		/// </summary>
		/// <param name="path">The file path of the file to be loaded.</param>
		/// <param name="extension">The single extension to check for.</param>
		/// <returns>Returns true on file validity. Returns false if file does not exist.</returns>
		protected virtual bool ValidateDocument(string path, string extension)
		{
			FileInfo file = new FileInfo(path);
			string fileName = file.Name;
			string dirName = file.Directory.FullName;

			// Check if the directory exists.
			if (Directory.Exists(dirName))
			{
				string filePath = file.FullName;

				// Check if the file exists.
				if (File.Exists(filePath))
				{
					if (file.Extension != extension)
					{
						Log("Document validation was unsuccessful.");
						Error("FileNotFoundException: The file - " + fileName + " - exists, but has the incorrect extension.", false);
						return false;
					}
					else
					{
						Log("Document successfully validated.");
						return true;
					}

				}
				else
				{
					Log("Document validation was unsuccessful.");
					Error("FileNotFoundException: The file - " + fileName + " - does not exist.", false);
					return false;
				}

			}
			else
			{
				Log("Document validation was unsuccessful.");
				Error("DirectoryNotFoundException: The directory for " + fileName + " was not found.", false);
				return false;
			}
		}

		#region Debugging Methods

		/// <summary>
		/// Log(string) adds a time-stamped message to the list of messages.
		/// This does not print to the console.
		/// </summary>
		/// <param name="message">Message to be added to the log.</param>
		protected virtual void Log(string message) 
		{
			if (String.IsNullOrEmpty(message))
			{
				Error("An empty message was pushed to the log.", true);
			}
			else
			{
				_log.Add(TimePrint() + ": " + message);
			}
		}

		/// <summary>
		/// Log(params string[]) adds a time-stamped message to the list of messages.
		/// This does not print to the console.
		/// </summary>
		/// <param name="messageChunks">Message chunks to be concatenated and added to the log.</param>
		protected virtual void Log(params string[] messageChunks)
		{
			if (messageChunks == null || messageChunks.Length <= 0)
			{
				Error("An empty message was pushed to the log.", true);
			}
			else
			{
				string packet = "";

				foreach (string message in messageChunks)
				{
					// As long as the statement isn't empty, add to the error packet.
					if (!String.IsNullOrEmpty(message))
					{
						packet += message + " ";
					}
				}

				Error(packet);
			}
		}

		/// <summary>
		/// Error(string) pushes the latest error message
		/// to the top of the stack.
		/// </summary>
		/// <param name="message">Error message to push to the stack.</param>
		protected virtual void Error(string message)
		{
			if (String.IsNullOrEmpty(message))
			{
				Error("An error has occurred.", false);
			}
			else
			{
				Error(message, false);
			}
		}

		/// <summary>
		/// Error(string, bool) pushes the latest error message
		/// to the top of the stack.
		/// </summary>
		/// <param name="message">Error message to push to the stack.</param>
		/// <param name="warning">Flag determining which warning text to use. True for "Warning:" and false for "Error:".</param>
		protected virtual void Error(string message, bool warning)
		{
			string warningText = "Warning";
			if (!warning) { warningText = "Error"; }

			if (String.IsNullOrEmpty(message))
			{
				_errors.Push(TimePrint() + ": " + warningText + "An error has occurred.");
			}
			else
			{
				_errors.Push(TimePrint() + ": " + warningText + message);
			}
		}

		/// <summary>
		/// Error(string[]) pushes the latest error message
		/// to the top of the stack.
		/// </summary>
		/// <param name="messageChunks">Message chunks to be concatenated and added to the log.</param>
		protected virtual void Error(params string[] messageChunks)
		{
			if (messageChunks == null || messageChunks.Length <= 0)
			{
				Error("An error has occured.");
			}
			else
			{
				string packet = "";

				foreach (string message in messageChunks)
				{
					// As long as the statement isn't empty, add to the error packet.
					if (!String.IsNullOrEmpty(message))
					{
						packet += message + " ";
					}
				}

				Error(packet);
			}
		}

		/// <summary>
		/// Error(bool, params string[]) pushes the latest error message
		/// to the top of the stack.
		/// </summary>
		/// <param name="warning">Flag determining which warning text to use. True for "Warning:" and false for "Error:".</param>
		/// <param name="messageChunks">Message chunks to be concatenated and added to the log.</param>
		protected virtual void Error(bool warning, params string[] messageChunks)
		{
			if (messageChunks == null || messageChunks.Length <= 0)
			{
				Error("An error has occured.", warning);
			}
			else
			{
				string packet = "";

				foreach (string message in messageChunks)
				{
					// As long as the statement isn't empty, add to the error packet.
					if (!String.IsNullOrEmpty(message))
					{
						packet += message + " ";
					}
				}

				Error(packet, warning);
			}
		}

		/// <summary>
		/// Reset() clears the log, the error stack, and stops the global clock.
		/// </summary>
		protected virtual void Reset()
		{
			_log = new List<string>();
			_errors = new Stack<string>();
			Log("DocumentParser has been reset. Log cleared. Clock reset.");
			_global.Restart();
			_local.Stop();
		}

		/// <summary>
		/// LogStart() starts the local clock.
		/// Used for local, in methods.
		/// </summary>
		private void LogStart()
		{
			_local.Start();
		}

		/// <summary>
		/// LogEnd() stops the local clock.
		/// Used for local, in methods.
		/// </summary>
		private void LogEnd()
		{
			_local.Stop();
		}

		/// <summary>
		/// LogTime() returns a TimeSpan object
		/// holding the elapsed time
		/// given by the _local stopwatch.
		/// </summary>
		/// <returns>Returns the elapsed local-time TimeSpan object.</returns>
		protected virtual TimeSpan LogTime()
		{
			return _local.Elapsed;
		}

		/// <summary>
		/// LogTimePrint() provides a local-time
		/// timestamp of elapsed time.
		/// </summary>
		/// <returns>Return elapsed local-time time in milliseconds.</returns>
		protected virtual string LogTimePrint()
		{
			return "[Est. Duration (" + LogTime().TotalMilliseconds + " ms)]";
		}

		/// <summary>
		/// Start() begins the global
		/// clock's ticking.
		/// </summary>
		protected virtual void Start()
		{
			_global.Start();
		}

		/// <summary>
		/// End() ends the global
		/// clock's ticking.
		/// </summary>
		protected virtual void End()
		{
			_global.Stop();
		}

		/// <summary>
		/// Time() returns a TimeSpan object
		/// holding the elapsed time
		/// given by the _global stopwatch.
		/// </summary>
		/// <returns>Returns the elapsed global-time TimeSpan object.</returns>
		protected virtual TimeSpan Time()
		{
			return _global.Elapsed;
		}

		protected virtual string TimePrint()
		{
			return "[Global Time Elapsed: (" + Time().TotalMilliseconds + " ms)]";
		}

		/// <summary>
		/// TimeStamp() returns the current time
		/// via the DateTime functions provided by
		/// Microsoft's System.dll.
		/// </summary>
		/// <returns>Returns the current time as a string.</returns>
		protected virtual string TimeStamp()
		{
			DateTime dt = DateTime.Now;
			int day, month, year, hour, minute, second, millisecond;

			day = dt.Day;
			month = dt.Month;
			year = dt.Year;
			hour = dt.Hour;
			minute = dt.Minute;
			second = dt.Second;
			millisecond = dt.Millisecond;

			return Program.Format("[({1}/{2}/{3}) ({4}:{5}:{6}:{7})]", month.ToString(),
				day.ToString(), year.ToString(), hour.ToString(),
				minute.ToString(), second.ToString(), millisecond.ToString());
		}

		/// <summary>
		/// TimeStamp(bool) returns the current time
		/// via the DateTime functions provided by
		/// Microsoft's System.dll.
		/// </summary>
		/// <param name="includeDayOfWeek">This specification, when false, removes the day of the week from being printed.</param>
		/// <returns>Returns the current time as a string.</returns>
		protected virtual string TimeStamp(bool includeDayOfWeek)
		{
			if (includeDayOfWeek)
			{
				DateTime dt = DateTime.Now;
				string dayOfWeek;
				int day, month, year, hour, minute, second, millisecond;

				dayOfWeek = dt.DayOfWeek.ToString();
				day = dt.Day;
				month = dt.Month;
				year = dt.Year;
				hour = dt.Hour;
				minute = dt.Minute;
				second = dt.Second;
				millisecond = dt.Millisecond;

				return Program.Format("[({0}, {1}/{2}/{3}) ({4}:{5}:{6}:{7})]", dayOfWeek, month.ToString(),
					day.ToString(), year.ToString(), hour.ToString(),
					minute.ToString(), second.ToString(), millisecond.ToString());
			}
			else
			{
				return TimeStamp();
			}
		}

		/// <summary>
		/// Dispose() is implemented from the IDisposable
		/// interface. Dispose allows
		/// IDocumentParser objects to be
		/// implemented via the "using" command.
		/// Dispose also ends the global clock,
		/// creating a final log to be collected
		/// from the object, should the 
		/// program utilize this.
		/// </summary>
		public virtual void Dispose()
		{
			// Dispose
			Log("Object " + this.ToString() + " has been disposed of.");
		}

		/// <summary>
		/// ToString() returns the timestamped identity of this document parser.
		/// </summary>
		/// <returns>Returns the string that identifies a particular IDocumentParser.</returns>
		public override string ToString()
		{
			string identity = "\t{" + TimeStamp() + ": |IDocumentParser.cs|}";
			return base.ToString() + identity;
		}

		#endregion

		#endregion

	}
}
