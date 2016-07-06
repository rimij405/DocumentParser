/*****************************************************************************
   * 
   * Resume Scraper Copyright (C) 2016  Ian A. Effendi 
   * 
   * This project has been created for the purpose of
   * scraping data and information from clients
   * from different documents and placing it into a separate
   * document guided by a series of design specifications
   * as designated by the code.
   *  
   * This program is free software: you can redistribute it and/or modify
   * it under the terms of the GNU Affero General Public License as
   * published by the Free Software Foundation, either version 3 of the
   * License, or (at your option) any later version.
   * 
   * This program is distributed in the hope that it will be useful,
   * but WITHOUT ANY WARRANTY; without even the implied warranty of
   * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   * GNU Affero General Public License for more details.
   * 
   * You should have received a copy of the GNU Affero General Public License
   * along with this program.  If not, see <http://www.gnu.org/licenses/>. 
   * 
   **************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume.Experiences
{
	/// <summary>
	/// Opportunity takes a time frame
	/// and some detailed information about
	/// a particular work experience for storage.
	/// </summary>
	public class Opportunity : IComparable
	{
		/*************************************
		 * Opportunity.cs
		 * 
		 * This class stores a time frame
		 * and functions to find an
		 * "earlier" occupation based
		 * on its date.
		 * 
		 * This class also stores the title
		 * of the opportunity and stores
		 * the time frame given for this
		 * opportunity.
		 *  
		 * This class also allows inheritors
		 * to take over a virtual class in
		 * which a particular title is
		 * standardized in a certain way.
		 * 
		 * (Such as taking "BFA" and turning
		 * it into, "Bachelor's of Fine Arts,"
		 * for education experiences.)
		 *  
		 * Opportunity is the base class
		 * for Work Experience, Education
		 * Experience, and Certification.
		 * 
		 * It also holds a series of bullet
		 * points to be printed via the
		 * Description object.
		 ************************************/

		#region Constants

		public static Dictionary<char, char> Capitalize
		{
			get
			{
				return new Dictionary<char, char>()
				{
					{'a', 'A'},
					{'b', 'B' },
					{'c', 'C' },
					{'d', 'D' },
					{'e', 'E' },
					{'f', 'F' },
					{'g', 'G' },
					{'h', 'H' },
					{'i', 'I' },
					{'j', 'J' },
					{'k', 'K' },
					{'l', 'L' },
					{'m', 'M' },
					{'n', 'N' },
					{'o', 'O' },
					{'p', 'P' },
					{'q', 'Q' },
					{'r', 'R' },
					{'s', 'S' },
					{'t', 'T' },
					{'u', 'U' },
					{'v', 'V' },
					{'w', 'S' },
					{'x', 'X' },
					{'y', 'Y' },
					{'z', 'Z' },
				};
			}
		}

		public static Dictionary<char, char> Downcase
		{
			get
			{
				return new Dictionary<char, char>()
				{
					{'A', 'a'},
					{'B', 'b' },
					{'C', 'c' },
					{'D', 'd' },
					{'E', 'e' },
					{'F', 'f' },
					{'G', 'g' },
					{'H', 'h' },
					{'I', 'i' },
					{'J', 'j' },
					{'K', 'k' },
					{'L', 'l' },
					{'M', 'm' },
					{'N', 'n' },
					{'O', 'o' },
					{'P', 'p' },
					{'Q', 'q' },
					{'R', 'r' },
					{'S', 's' },
					{'T', 't' },
					{'U', 'u' },
					{'V', 'v' },
					{'W', 's' },
					{'X', 'x' },
					{'Y', 'y' },
					{'Z', 'z' },
				};
			}
		}

		/// <summary>
		/// CharToUpper(char) takes
		/// a character and capitalizes it.
		/// </summary>
		/// <param name="c">Character to capitalize.</param>
		/// <returns>Returns a character as its uppercase version if possible.</returns>
		public static char CharToUpper(char c)
		{
			int nullStart = (int) 'A';
			int nullEnd = nullStart + 26;

			if ((int)c < nullEnd && (int)c >= nullStart)
			{
				// The character is already capitalized.
				return c;
			}
			else if(Capitalize.ContainsKey(c))
			{
				return Capitalize[c];
			}
			else
			{
				return c.ToString().ToUpper()[0]; // Test your luck via string.
			}
		}

		/// <summary>
		/// CharToLower(char) takes
		/// a character and downcases it.
		/// </summary>
		/// <param name="c">Character to turn to lowercase.</param>
		/// <returns>Returns a character as its lowercase version if possible.</returns>
		public static char CharToLower(char c)
		{
			int nullStart = (int) 'a';
			int nullEnd = nullStart + 26;

			if ((int)c < nullEnd && (int)c >= nullStart)
			{
				// The character is already capitalized.
				return c;
			}
			else if (Downcase.ContainsKey(c))
			{
				return Downcase[c];
			}
			else
			{
				return c.ToString().ToLower()[0]; // Test your luck via string.
			}
		}

		/// <summary>
		/// TitleCase(string) takes a string
		/// and title cases it like: "This"
		/// and "That".
		/// </summary>
		/// <param name="s">String to send to title case.</param>
		/// <returns>Returns a title-cased string.</returns>
		public static string TitleCase(string s)
		{
			if (String.IsNullOrEmpty(s.Trim()))
			{
				return "";
			}
			else if (s.Trim().Length == 1)
			{
				return CharToUpper(s.Trim()[0]).ToString();
			}
			else
			{
				string response = "";
				string trimmed = s.Trim();
				string[] series = s.Split(' ');

				foreach (string str in series)
				{
					// Check size of string.
					if (str.Length == 1) { response = TitleCase(str); break; }

					// Take the first letter of str if it's big enough.
					if (str.Length >= 2)
					{
						for (int i = 0; i < str.Length; i++)
						{
							if (i == 0)
							{
								response = CharToUpper(str[i]).ToString();
							}
							else
							{
								response += CharToLower(str[i]).ToString();
							}
						}
					}

					response += " ";
				}

				return response;
			}
		}

		#endregion

		#region Attributes

		protected TimeFrame _timeFrame;
		protected Description _description;
		protected string _title;
		protected bool _empty;

		#endregion

		#region Properties

		// TODO: Add comments to all properties and fields across all classes.
		// TODO: Remove redundancies.
		// TODO: Don't remove these until you've completed them for all other classes.
		// TODO: Ensure any third-party software no longer being used, is removed.

		public TimeFrame Time
		{
			get { return this._timeFrame; }
		}

		public Description Details
		{
			get { return this._description; }
		}

		public string Title
		{
			get { return this._title; }
		}

		public bool Empty
		{
			get { return this._empty; }
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// CompareTimeSpan(Opportunity, Opportunity)
		/// compares two opportunities and returns
		/// +1 if the first opportunity's time is
		/// earlier than the second, 0 if it is the
		/// same, and -1 if it is later than the second.
		/// </summary>
		/// <param name="one">Opportunity first.</param>
		/// <param name="two">Opportunity second.</param>
		/// <returns>Returns a 1, 0, or -1 depending on the results.</returns>
		public static int CompareTimeSpan(Opportunity one, Opportunity two)
		{

			// TODO: Implement this method.

			throw new NotImplementedException("Comparison of time spans is not yet implemented.");
		}

		#endregion

		#region Constructor / Initialization
		
		/// <summary>
		/// Opportunity(TimeFrame, Description, string)
		/// is the default constructor for any opportunity
		/// object. You need the time, the details for
		/// the opportunity, and the name of the opportunity
		/// in order to be able to export that data.
		/// </summary>
		/// <param name="time">A DateTime, usually in just Month, Year.</param>
		/// <param name="details">A series of bullet points.</param>
		/// <param name="name">The title for a given opportunity.</param>
		public Opportunity(TimeFrame time, Description details, string name)
		{
			this._empty = true;
			this._timeFrame = time;
			this._description = details;
			SetTitle(name);
		}

		#endregion

		#region Methods

		#region Mutator Methods

		/// <summary>
		/// SetTime(TimeFrame)
		/// sets the time.
		/// </summary>
		/// <param name="time">Time to set.</param>
		public void SetTime(TimeFrame time)
		{
			this._timeFrame = time;
		}

		/// <summary>
		/// SetDescription(Description)
		/// sets the description.
		/// </summary>
		/// <param name="details">Description to set.</param>
		public void SetDescription(Description details)
		{
			this._description = details;
		}

		/// <summary>
		/// SetTitle(string)
		/// sets the title.
		/// </summary>
		/// <param name="name">Title to set.</param>
		public void SetTitle(string name)
		{
			Standardize(name);
		}
		#endregion

		#region Accessor Methods

		// TODO: Implement method to get the earliest time.

		// TODO: Implement method to get the latest time.

		#endregion

		#region Service Methods

		/// <summary>
		/// Standardize(string)
		/// attempts to strip the title
		/// to get a standardized 
		/// set of information. (Such as
		/// capitalizing the title).
		/// </summary>
		/// <param name="nameToTest">Title to set.</param>
		protected virtual void Standardize(string nameToTest)
		{
			string finalizedString = "";

			if (!String.IsNullOrEmpty(nameToTest))
			{
				finalizedString = nameToTest;
			}
			else
			{
				this._title = finalizedString;
				this._empty = true;
				return;
			}

			// Series of standardizations: //
			// Title case the string.
			finalizedString = Opportunity.TitleCase(finalizedString);

			this._title = finalizedString;
			this._empty = false;
		}

		/// <summary>
		/// CompareTo(object) compares
		/// Opportunity to another
		/// Opportuntiy object and determines
		/// which is greater, equal, or, lesser.
		/// +1 for greater, 0 for equal, or -1,
		/// for lesser. Will return int.MinValue, if the
		/// input object doesn't match.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>Returns +1 for greater, 0 for equal, and -1 for lesser.</returns>
		public int CompareTo(object obj)
		{
			if (obj is Opportunity)
			{
				return Opportunity.CompareTimeSpan(this, obj as Opportunity);
			}
			else
			{
				return int.MinValue;
			}
		}

		#endregion

		#endregion

	}
}
