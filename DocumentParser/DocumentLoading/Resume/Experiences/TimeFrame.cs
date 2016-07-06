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
using Chronic;

namespace DocumentParser.DocumentLoading.Resume.Experiences
{

	// TODO: Add class descriptions and boilerplates to all classes.
	// TODO: Do not delete this or the above task until it is accomplished for ALL tasks.
	/// <summary>
	/// TimeFrame is a starting and ending period
	/// that allows the attempted parsing of user
	/// strings into times.
	/// </summary>
	public class TimeFrame : IComparable
	{
		#region Constants / Statics

		// TODO: Finalize constants.
		public readonly DateTime NOW = DateTime.Now;
		public readonly DateTime NULL = DateTime.MinValue;

		public static Parser DateParser = new Parser();

		// When checking against this dictionary,
		// always send the key to "to lowercase".
		public static Dictionary<string, string> Keywords = new Dictionary<string, string>()
		{
			{"present", "Present"},
			{"current", "Present"},
			{"now", "Present"},
			{"today", "Present"}
		};
		
		#endregion

		#region Attributes

		// TODO: Compete the Attributes section.

		// Private fields.
		private DateTime _start;
		private DateTime _end;

		// Private flags.
		private bool _empty;
		private bool _ranged;
		private bool _initialized;
		private bool _ongoing;

		#endregion

		#region Properties

		// TODO: Complete the Properties section.

		/// <summary>
		/// Empty returns the _empty boolean flag.
		/// </summary>
		public bool Empty
		{
			get { return this._empty; }
		}

		/// <summary>
		/// Ranged returns the flag _ranged.
		/// This dettermines whether or not
		/// the TimeFrame is one with a range.
		/// (A start AND a finish).
		/// </summary>
		public bool Ranged
		{
			get { return this._ranged; }
		}

		/// <summary>
		/// HasEnd allows us to check TimeFrames
		/// for an end. Used on ranged time frames
		/// to see if it is empty or not.
		/// </summary>
		public bool HasEnd
		{
			get
			{
				return (this._end == NULL);
			}
		}

		/// <summary>
		/// Period is a neat package of both
		/// the start DateTime and end DateTime.
		/// </summary>
		public Span Period
		{
			get
			{
				Span period = new Span(_start, _end);
				return period;
			}
		}

		/// <summary>
		/// Ongoing represents a flag
		/// that dictates whether or not
		/// the time frame extends to
		/// current day. ie. "Present".
		/// </summary>
		public bool Ongoing
		{
			get
			{
				return _ongoing;
			}
		}

		#endregion

		#region Constructor / Initialization

		// TODO: Complete the constructors.

		/// <summary>
		/// TimeFrame() is TimeFrame's
		/// empty constructor that registers
		/// as empty and unranged by default.
		/// </summary>
		public TimeFrame()
		{
			_init();
		}

		/// <summary>
		/// TimeFrame(string) is TimeFrame's
		/// constructor for unranged periods
		/// of time. It takes one date and
		/// assigns it, as best it can, to
		/// its _start field. If it cannot
		/// parse it, it will remain an empty
		/// TimeFrame.
		/// </summary>
		/// <param name="start"></param>
		public TimeFrame(string start)
		{
			_init();
			TrySetStart(start);
		}

		/// <summary>
		/// TimeFrame(string, string) is TimeFrame's
		/// constructor for ranged periods of time.
		/// It takes two dates and tries to parse
		/// both as best they can to the _start and
		/// _end fields, respectively.
		/// </summary>
		public TimeFrame(string start, string end)
		{
			_init();
			TrySetStart(start);
			TrySetEnd(end);
		}

		/// <summary>
		/// _init() is the initialization method
		/// used by the above constructors.
		/// </summary>
		private void _init()
		{
			this._empty = true;
			this._ranged = false;
			this._ongoing = false;
			this._start = NOW;
			this._end = NULL;

			this._initialized = true;
		}

		#endregion


		#region Methods

		// TODO: Complete static methods...

		#region Static Methods

		/// <summary>
		/// IsNullOrEmpty(TimeFrame)
		/// checks to see if a TimeFrame
		/// is either null or empty.
		/// </summary>
		/// <param name="testFrame">TimeFrame to test.</param>
		/// <returns>Returns true if null or empty. Returns false if not null or not empty.</returns>
		public static bool IsNullOrEmpty(TimeFrame testFrame)
		{
			// If null or empty.
			if (testFrame == null || testFrame.Empty) { return true; }

			// Check times to see if they're empty.
			if (testFrame.Ranged && !testFrame.HasEnd)
			{
				return true;
			}

			// If it passes the other tests, pass false.
			return false;
		}

		#endregion


		#region Methods

		#region Accessor Methods

		// TODO: Complete the Accessor Methods section.

		#endregion


		#region Mutator Methods
			
		/// <summary>
		/// TrySetStart(string) takes an input
		/// and attempts to assign a valid value
		/// (or no value, if not valid) to the 
		/// _start field.
		/// </summary>
		/// <param name="input">Input to attempt setting with.</param>
		/// <returns>Returns status of operation success. True for successful, false for unsuccessful.</returns>
		public bool TrySetStart(string input)
		{
			// Validate parameters.
			if (String.IsNullOrEmpty(input)) { return false; }

			try
			{
				// Parse the input.
				_start = DateParser.Parse(input.Trim()).ToTime();
			}
			catch (Exception e)
			{
				// e.StackTrace;
				return false;
			}

			if (_start != NULL)
			{
				_empty = false;
				return true;
			}

			return false;
		}
		/// <summary>
		/// TrySetEnd(string) takes an input
		/// and attempts to assign a valid value
		/// (or no value, if not valid) to the 
		/// _start field.
		/// </summary>
		/// <param name="input">Input to attempt setting with.</param>
		/// <returns>Returns status of operation success. True for successful, false for unsuccessful.</returns>
		public bool TrySetEnd(string input)
		{
			// Set ranged to on.
			_ranged = true;

			// Validate parameters.
			if (String.IsNullOrEmpty(input)) { return false; }

			// Check to see if it means current time.
			if (Keywords.ContainsKey(input.Trim().ToLower()))
			{
				_end = NOW;
				_empty = false;
				_ongoing = true;
				return true;
			}
			try
			{
				// Parse the input.
				_end = DateParser.Parse(input.Trim()).ToTime();
			}
			catch (Exception e)
			{
				// e.StackTrace;
				return false;
			}

			if (_end != NULL)
			{
				_empty = false;
				_ongoing = (_end == NOW);
				return true;
			}

			return false;
		}

		#endregion

		#region Service Methods

		/// <summary>
		/// IsGreaterThan(DateTime, DateTime) takes
		/// two dates and compares them. Returns true
		/// if one is greater than two. Returns false if
		/// one is equal to, or less than two.
		/// </summary>
		/// <param name="one">The context.</param>
		/// <param name="two">The comparator.</param>
		/// <returns>Returns true if one is greater than two.</returns>
		public bool IsGreaterThan(DateTime one, DateTime two)
		{
			if(one > two) { return true; }
			return false;
		}

		/// <summary>
		/// IsEqual(DateTime, DateTime) takes
		/// two dates and compares them. Returns true
		/// if one is equal to two. Returns false if
		/// one is greater than two, or less than two.
		/// </summary>
		/// <param name="one">The context.</param>
		/// <param name="two">The comparator.</param>
		/// <returns>Returns true if one is equal to two.</returns>
		public bool IsEqual(DateTime one, DateTime two)
		{
			if (IsLessThan(one, two) || IsGreaterThan(one, two)) { return false; }
			return true;
		}


		/// <summary>
		/// IsLessThan(DateTime, DateTime) takes
		/// two dates and compares them. Returns true
		/// if one is less than two. Returns false if
		/// one is equal to, or greater than two.
		/// </summary>
		/// <param name="one">The context.</param>
		/// <param name="two">The comparator.</param>
		/// <returns>Returns true if one is less than two.</returns>
		public bool IsLessThan(DateTime one, DateTime two)
		{
			if(one < two) { return true; }
			return false; 
		}

		/// <summary>
		/// CompareTo(obj) returns <see cref="int.MinValue"/>
		/// if it's not of the type <seealso cref="TimeFrame"/>
		/// or is not of the same ranged status,
		/// +1, if <see cref="this"/> is greater, -1, if it's
		/// lesser, and 0 if it's equal.
		/// </summary>
		/// <param name="obj">Object to compare.</param>
		/// <returns>Returns either <see cref="int.MinValue"/>, +1, -1, or 0.</returns>
		public int CompareTo(object obj)
		{
			if (obj is TimeFrame)
			{
				TimeFrame wrapper = obj as TimeFrame;

				if (this.Period.Width > wrapper.Period.Width)
				{
					return 1;
				}
				else if (this.Period.Width == wrapper.Period.Width)
				{
					return 0;
				}
				else
				{
					return -1;
				}
			}
			else
			{
				return int.MinValue;
			}
		}

		#endregion

		#endregion

		#endregion
	}
}
