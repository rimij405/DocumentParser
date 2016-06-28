using DocumentParser.DocumentLoading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser
{

	public class Program
	{

		#region Attributes and Properties

		public static readonly string[] CONFIRMATION_COMMANDS =
		{
			"y", "yes", "yeah", "yup",
			"okay", "confirm", "positive", "+"
		};

		public static readonly string[] NEGATIVE_COMMANDS =
		{
			"n", "no", "nah", "nope",
			"cancel", "decline", "negative", "-"
		};

		public static readonly string[] QUIT_COMMANDS =
		{
			"q", "quit", "close", "exit",
			"alt+f4", "shutdown", "leave",
			"depart", "abandon", "break"
		};

		#region Methods

		private static Dictionary<string, int> commands;
		private static Dictionary<int, Action> delegates;
		private static Dictionary<int, Func<string, bool>> functions;
		private static int[] delegateValues = { TEST_PRINTER, TEST_DOC };

		#endregion

		private const int CONFIRMATION_CODE = int.MaxValue;
		private const int DECLINE_CODE = 0;
		private const int EXIT_CODE = -1;
		private const int NAN_CODE = int.MinValue;

		private const int TEST_PRINTER = 0;
		private const int TEST_DOC = 2;
		private const int PROMPT_CODE = 1;

		#endregion

		[STAThreadAttribute]
		public static void Main(string[] args)
		{
			init();

			try
			{

				while (TestingHub(delegates, functions))
				{
					// Anything that needs to be run after the above program.

				}

			}
			catch (Exception e)
			{
				Pause(e.Source);
				Pause(e.Message);
				Pause(e.StackTrace);
			}
			finally
			{

			}

			Exit(false);
		}

		#region Initialization and Service functions.

		private static void init()
		{
			delegates = new Dictionary<int, Action>();
			delegates.Add(TEST_PRINTER, TestPrintingAids);
			delegates.Add(TEST_DOC, DocumentTester.DocumentTesting);

			functions = new Dictionary<int, Func<string, bool>>();
			functions.Add(PROMPT_CODE, Prompt);

			commands = new Dictionary<string, int>();

			// Confirmation.
			foreach (string cmd in CONFIRMATION_COMMANDS)
			{
				commands.Add(cmd, CONFIRMATION_CODE);
			}

			// Decline.
			foreach (string cmd in NEGATIVE_COMMANDS)
			{
				commands.Add(cmd, DECLINE_CODE);
			}

			// Exit.
			foreach (string cmd in QUIT_COMMANDS)
			{
				commands.Add(cmd, EXIT_CODE);
			}
		}

		private static bool isLegal(string input)
		{
			switch (getCommandCode(input))
			{
				case CONFIRMATION_CODE:
				case DECLINE_CODE:
					return true;
				case EXIT_CODE:
					Exit();
					return false;
				case NAN_CODE:
				default:
					return false;
			}
		}

		private static bool isConfirmation(string input)
		{
			switch (getCommandCode(input))
			{
				case CONFIRMATION_CODE:
					return true;
				case DECLINE_CODE:
				case NAN_CODE:
					return false;
				case EXIT_CODE:
					Exit();
					return false;
				default:
					return false;
			}
		}

		private static bool isNegative(string input)
		{
			switch (getCommandCode(input))
			{
				case NAN_CODE:
				case CONFIRMATION_CODE:
					return false;
				case DECLINE_CODE:
					return true;
				case EXIT_CODE:
					Exit();
					return false;
				default:
					return false;
			}
		}

		private static int getCommandCode(string input)
		{
			return getCommandCode(input, false);
		}

		private static int getCommandCode(string input, bool matchCase)
		{
			if (!String.IsNullOrEmpty(input))
			{
				if (matchCase)
				{
					if (commands.ContainsKey(input.Trim()))
					{
						return commands[input.Trim()];
					}
				}
				else
				{
					if (commands.ContainsKey(input.ToLower().Trim()))
					{
						return commands[input.ToLower().Trim()];
					}
				}
			}

			return NAN_CODE;
		}
		
		#endregion

		#region Test functions.

		public static bool TestingHub(Dictionary<int, Action> tasks, Dictionary<int, Func<string, bool>> funcs)
		{
			Clear();

			foreach (int value in delegateValues)
			{
				// Invokes each existing method, in order.
				if (tasks.ContainsKey(value))
				{
					tasks[value].Invoke();
				}
			}
			
			return funcs[PROMPT_CODE].Invoke("Would you like to repeat the program? (y)/(n)");
		}

		public static bool Prompt(string message)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			string user = "";
			long timeOut = 30000;

			do
			{
				long time = sw.ElapsedMilliseconds;

				Write(message);
				user = GetKeyPrompt();

				if (isConfirmation(user))
				{
					sw.Stop();
					return true;
				}
				else if (isNegative(user))
				{
					sw.Stop();
					return false;
				}
				else
				{
					Write("Please enter valid command. (y) - for yes, (n) - for no, (q) - for quit. Time out in " + TimeSpan.FromMilliseconds(timeOut - time).Seconds + " seconds.");
				}

				if (time >= timeOut) // If we've reached the time out clock, force fail the prompt.
				{
					Pause("Your input timed out.");
					return false;
				}
			} while (!isLegal(user));
			
			sw.Stop();
			return false;
		}

		public static void Exit()
		{
			if (functions[PROMPT_CODE].Invoke("Would you like to exit the program? (y)/(n)"))
			{
				Exit(false);
			}

			return;
		}

		public static void Exit(bool prompt)
		{
			if (!prompt)
			{
				Write("Thank you for using ths program.");
				Pause("The program will now exit.");
				System.Environment.Exit(System.Environment.ExitCode);
			}
			else
			{
				Exit();
			}
		}

		public static void TestPrintingAids()
		{
			// This program should be testing out how to read a given document file using the NetOffice library.
			Pause("This program should be testing out how to read a given document file using the NetOffice library.");
			Pause();

			Pause("Testing the format function.");

			string toFormat = "Replaced data: {0}, {1}, {2}";
			string james = "James";
			string malcolm = "Malcolm";
			string stephanie = "Stephanie";

			Write("This data needs to be replaced by 3 names: {0}, {1}, {2}");
			Pause(Format(toFormat, james, malcolm, stephanie));
			Pause();
		}

		#endregion

		#region Printer aids.

		public static void Write()
		{
			Console.WriteLine("");
		}

		public static void Write(bool newline)
		{
			if (newline)
			{
				Console.WriteLine("");
			}
			else
			{
				Console.Write(" ");
			}
		}

		public static void Write(string message)
		{
			if (!String.IsNullOrEmpty(message))
			{
				Console.WriteLine(message);
			}
			else
			{
				Write();
			}
		}

		public static void Write(string message, bool newline)
		{
			if (!newline)
			{
				if (!String.IsNullOrEmpty(message))
				{
					Console.WriteLine(message);
				}
				else
				{
					Write();
				}
			}
			else
			{
				if (!String.IsNullOrEmpty(message))
				{
					Console.Write(message);
				}
				else
				{
					Write(false);
				}
			}
		}

		public static void Pause(string message)
		{
			Pause(message, false);
		}

		public static void Pause(string message, bool pauseMessage)
		{
			if (!String.IsNullOrEmpty(message))
			{
				Write(message);
			}

			if (pauseMessage)
			{
				Write("Press any key to continue...");
			}

			Console.ReadKey(true);
		}

		public static void Pause()
		{
			Pause("", true);
		}

		public static string Read()
		{
			return Console.ReadLine();
		}

		public static string Read(bool key)
		{
			if (key)
			{
				return Console.ReadKey(false).KeyChar.ToString();
			}
			else
			{
				return Console.ReadLine();
			}
		}

		public static string GetKeyPrompt(string message)
		{
			Write(message);
			return Read(false);
		}

		public static string GetKeyPrompt()
		{
			return Read(false);
		}

		public static bool IsKeyPrompt(string message, string key)
		{
			Write(message);
			return IsKey(key);
		}

		public static bool IsKeyPrompt(string message, params string[] keys)
		{
			bool isMatch = false;
			Write(message);

			foreach (string k in keys)
			{
				isMatch = IsKey(k[0]);
				if (isMatch) { return true; }
			}

			return false;
		}

		public static bool IsKeyPrompt(string message, bool matchCase, params string[] keys)
		{
			bool isMatch = false;
			Write(message);

			foreach (string k in keys)
			{
				isMatch = IsKey(k[0], matchCase);
				if (isMatch) { return true; }
			}

			return false;
		}

		public static string GetQueryPrompt(string message)
		{
			Write(message);
			return Read();
		}

		public static string GetQueryPrompt()
		{
			return Read();
		}

		public static bool IsQueryPrompt(string message, bool matchCase, params string[] queries)
		{
			bool isMatch = false;
			Write(message);

			foreach (string query in queries)
			{
				isMatch = IsQuery(query, matchCase);
				if (isMatch) { return true; }
			}

			return false;
		}

		public static bool IsQueryPrompt(string message, params string[] queries)
		{
			bool isMatch = false;
			Write(message);

			foreach (string query in queries)
			{
				isMatch = IsQuery(query);
				if (isMatch) { return true; }
			}

			return false;
		}

		public static bool IsQueryPrompt(string message, string query)
		{
			Write(message);
			return IsQuery(query);
		}

		public static bool IsQueryPrompt(string message, string query, bool matchCase)
		{
			Write(message);
			return IsQuery(query, matchCase);
		}

		public static bool IsKey(string keyToDetect, bool matchCase)
		{
			string userInput = Read(true);
			if (!matchCase)
			{
				return (userInput.ToLower().Trim() == keyToDetect.ToLower());
			}
			else
			{
				return (userInput.Trim() == keyToDetect);
			}
		}

		public static bool IsKey(char keyToDetect, bool matchCase)
		{
			string userInput = Read(true);
			if (!matchCase)
			{
				return (userInput[0].ToString().ToLower().Trim() == keyToDetect.ToString().ToLower());
			}
			else
			{
				return (userInput[0].ToString().Trim() == keyToDetect.ToString());
			}
		}

		public static bool IsKey(string keyToDetect)
		{
			return IsKey(keyToDetect[0]);	
		}

		public static bool IsKey(char keyToDetect)
		{
			return (keyToDetect == Console.ReadKey(false).KeyChar);
		}

		public static bool IsQuery(string queryToDetect, bool matchCase)
		{
			string userInput = Read();
			if (!matchCase)
			{
				return (userInput.ToLower().Trim() == queryToDetect.ToLower());
			}
			else
			{
				return (userInput.Trim() == queryToDetect);
			}
		}

		public static bool IsQuery(string queryToDetect)
		{
			return IsQuery(queryToDetect, false);
		}

		public static bool IsQuery(char queryToDetect)
		{
			return IsQuery(queryToDetect.ToString());
		}

		public static bool Matches(string key, string key2, bool matchCase)
		{
			key = key.Trim();
			key2 = key.Trim();

			if (matchCase)
			{
				return (key == key2);
			}
			else
			{
				return (key.ToLower() == key2.ToLower());
			}
		}

		public static bool Matches(char key, char key2, bool matchCase)
		{
			if (matchCase)
			{
				return (key == key2);
			}
			else
			{
				return (key.ToString().ToLower() == key2.ToString().ToLower());
			}
		}

		public static bool Matches(int key, int key2)
		{
			return (key == key2);
		}


		public static void Clear()
		{
			Console.Clear();
		}

		public static void SetBackgroundColor(ConsoleColor color)
		{
			Console.BackgroundColor = color;
			Console.Clear();
		}

		public static void SetForegroundColor(ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Clear();
		}

		public static void SetColors(ConsoleColor bgColor, ConsoleColor fontColor)
		{
			Console.BackgroundColor = bgColor;
			Console.ForegroundColor = fontColor;
			Console.Clear();
		}

		public static void SetBackgroundColor()
		{
			SetBackgroundColor(ConsoleColor.Black);
		}

		public static void SetForegroundColor()
		{
			SetForegroundColor(ConsoleColor.White);
		}

		public static void SetColors()
		{
			SetColors(ConsoleColor.Black, ConsoleColor.White);
		}

		public static string Format(string format, string toReplace, string value)
		{
			string toReturn = format.Replace(toReplace, value);
			return toReturn;
		}

		public static string Format(string format, string value)
		{
			string toReturn = format;

			if (!String.IsNullOrEmpty(format))
			{
				toReturn = Format(toReturn, "{0}", value);
			}

			return toReturn;
		}

		public static string Format(string format, params string[] values)
		{
			string toReturn = format;

			if (!String.IsNullOrEmpty(format))
			{
				for (int i = 0; i < values.Length; i++)
				{
					string replaceValue = "{" + i + "}";
					string result = Format(toReturn, replaceValue, values[i]);
					toReturn = result;
				}
			}

			return toReturn;
		}

		#endregion
	}

}
