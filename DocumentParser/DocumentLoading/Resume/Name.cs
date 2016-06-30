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

namespace DocumentParser.DocumentLoading.Resume
{
	/// <summary>
	/// The Name.cs class holds three things:
	/// The first, middle, and last name
	/// of a given applicant.
	/// </summary>
	public class Name : BaseBook
	{
		#region Constants

		// // String types.
		public const string PREFIX = "Prefix";
		public const string FIRST_NAME = "First Name";
		public const string MIDDLE_NAME = "Middle Name";
		public const string LAST_NAME = "Last Name";
		public const string SUFFIX = "Suffix";
		public const string FULL_NAME = "Full Name";
		public const string NULL = "NULL";

		// // Int codes.
		public const int PREFIX_CODE = 0;
		public const int FIRST_NAME_CODE = 1;
		public const int MIDDLE_NAME_CODE = 2;
		public const int LAST_NAME_CODE = 3;
		public const int SUFFIX_CODE = 4;
		public const int FULL_NAME_CODE = 5;
		public const int NULL_CODE = -1;

		public static Dictionary<int, string> TYPES;
		public static Dictionary<string, int> CODES;
		public static bool isInitialized = false;

		#endregion

		#region Attributes
		
		private Dictionary<string, string> _name;
		private bool _empty;

		#endregion

		#region Properties

		/// <summary>
		/// FullName returns the entire
		/// full name utilizing whatever
		/// resources are not empty or null.
		/// </summary>
		public string FullName
		{
			get { return _name[FULL_NAME]; }
		}

		/// <summary>
		/// Prefix returns the
		/// prefix portion of
		/// the name.
		/// </summary>
		public string Prefix
		{
			get { return _name[PREFIX]; }
		}

		/// <summary>
		/// FirstName returns the
		/// first name portion of
		/// the name.
		/// </summary>
		public string FirstName
		{
			get { return _name[FIRST_NAME]; }
		}

		/// <summary>
		/// MiddleName returns the
		/// middle name portion of
		/// the name.
		/// </summary>
		public string MiddleName
		{
			get { return _name[MIDDLE_NAME]; }
		}

		/// <summary>
		/// LastName returns the
		/// last name portion of
		/// the name.
		/// </summary>
		public string LastName
		{

			get { return _name[LAST_NAME]; }
		}

		/// <summary>
		/// Suffix returns the
		/// suffix portion of
		/// the name.
		/// </summary>
		public string Suffix
		{
			get { return _name[SUFFIX]; }
		}
		
		/// <summary>
		/// HasPrefix determines if
		/// this name has a given
		/// prefix.
		/// </summary>
		public bool HasPrefix
		{
			get
			{
				string temp = Prefix;
				return (temp != NULL && !IsEmptyOrNull(temp));
			}
		}

		/// <summary>
		/// HasFirstName determines if
		/// this name has a given
		/// first name.
		/// </summary>
		public bool HasFirstName
		{
			get
			{
				string temp = FirstName;
				return (temp != NULL && !IsEmptyOrNull(temp));
			}
		}

		/// <summary>
		/// HasMiddleName determines if
		/// this name has a given
		/// middle name.
		/// </summary>
		public bool HasMiddleName
		{
			get
			{
				string temp = MiddleName;
				return (temp != NULL && !IsEmptyOrNull(temp));
			}
		}
		
		/// <summary>
		/// HasLastName determines if
		/// this name has a given
		/// last name.
		/// </summary>
		public bool HasLastName
		{
			get
			{
				string temp = LastName;
				return (temp != NULL && !IsEmptyOrNull(temp));
			}
		}
		
		/// <summary>
		/// HasSuffix determines if
		/// this name has a given
		/// suffix.
		/// </summary>
		public bool HasSuffix
		{
			get
			{
				string temp = Suffix;
				return (temp != NULL && !IsEmptyOrNull(temp));
			}
		}

		/// <summary>
		/// IsEmpty returns the
		/// _empty flag.
		/// </summary>
		public bool Empty
		{
			get { return this._empty; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Name() is an empty constructor.
		/// </summary>
		public Name()
		{
			_init();
		}

		/// <summary>
		/// Name(string, string, string, string, string) is a constructor for 
		/// Name that takes five paramter values and stores an applicant's
		/// name as best as it can.
		/// </summary>
		/// <param name="prefix">Applicant's prefix.</param>
		/// <param name="firstName">The appicant's first name.</param>
		/// <param name="middleName">The appicant's middle name.</param>
		/// <param name="lastName">The appicant's last name.</param>
		/// <param name="suffix">Applicant's suffix.</param>
		public Name(string prefix, string firstName, string middleName, string lastName, string suffix)
		{
			_init();
			AssignUnlessEmpty(PREFIX, prefix);
			AssignUnlessEmpty(FIRST_NAME, firstName);
			AssignUnlessEmpty(MIDDLE_NAME, middleName);
			AssignUnlessEmpty(LAST_NAME, lastName);
			AssignUnlessEmpty(SUFFIX, suffix);
		}

		/// <summary>
		/// _init() is an initialization
		/// method called by the above constructors.
		/// </summary>
		public void _init()
		{
			this._name = new Dictionary<string, string>();
			this._empty = true;

			_name.Add(PREFIX, NULL);
			_name.Add(FIRST_NAME, NULL);
			_name.Add(MIDDLE_NAME, NULL);
			_name.Add(LAST_NAME, NULL);
			_name.Add(SUFFIX, NULL);
			_name.Add(FULL_NAME, NULL);
			_name.Add(NULL, NULL);

			if (!isInitialized)
			{
				TYPES = new Dictionary<int, string>();
				CODES = new Dictionary<string, int>();

				TYPES.Add(PREFIX_CODE, PREFIX);
				TYPES.Add(FIRST_NAME_CODE, FIRST_NAME);
				TYPES.Add(MIDDLE_NAME_CODE, MIDDLE_NAME);
				TYPES.Add(LAST_NAME_CODE, LAST_NAME);
				TYPES.Add(SUFFIX_CODE, SUFFIX);
				TYPES.Add(FULL_NAME_CODE, FULL_NAME);
				TYPES.Add(NULL_CODE, NULL);

				CODES.Add(PREFIX, PREFIX_CODE);
				CODES.Add(FIRST_NAME, FIRST_NAME_CODE);
				CODES.Add(MIDDLE_NAME, MIDDLE_NAME_CODE);
				CODES.Add(LAST_NAME, LAST_NAME_CODE);
				CODES.Add(SUFFIX, SUFFIX_CODE);
				CODES.Add(FULL_NAME, FULL_NAME_CODE);
				CODES.Add(NULL, NULL_CODE);

				isInitialized = true;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// UpdateFullName() updates
		/// the full name being stored
		/// in the _name Dictionary.
		/// </summary>
		private void UpdateFullName()
		{
			string fullName = "";

			if (HasPrefix)
			{ fullName += _name[PREFIX] + " "; }

			if (HasFirstName)
			{ fullName += _name[FIRST_NAME] + " "; }

			if (HasMiddleName)
			{ fullName += _name[MIDDLE_NAME] + " "; }

			if (HasLastName)
			{ fullName += _name[LAST_NAME] + " "; }

			if (HasSuffix)
			{ fullName += _name[SUFFIX] + " "; }

			_name[FULL_NAME] = fullName.Trim();
		}

		/// <summary>
		/// IsEmpty(string) determines
		/// if the input string matches
		/// the "EMPTY" code provided
		/// within this class. See <see cref="Name.NULL"/>
		/// </summary>
		/// <param name="input">String input for determination of emptiness.</param>
		/// <returns>Returns true if the string is empty or null; returns false if otherwise.</returns>
		private bool IsEmptyOrNull(string input)
		{
			return (input.Trim() == NULL || String.IsNullOrEmpty(input.Trim()));
		}

		/// <summary>
		/// AssignUnlessEmpty(string, string) assigns the second
		/// parameter's value to the first, as long as it isn't empty.
		/// </summary>
		/// <param name="assignType">Type the assignee is to be registered to.</param>
		/// <param name="assignee">The value to give to the type.</param>
		public void AssignUnlessEmpty(string assignType, string assignee)
		{
			if (IsValidType(assignType) && !IsEmptyOrNull(assignee))
			{
				string value = _name[assignType];
				value = assignee;
				_name[assignType] = value;
				UpdateFullName();
			}
		}

		#region GetType methods.

		/// <summary>
		/// GetType(int) returns
		/// the string conversion
		/// of the given type.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns the appropriate value for type.</returns>
		public string GetType(int type)
		{
			if (IsValidType(type))
			{
				return TYPES[type];
			}
			else
			{
				return TYPES[NULL_CODE];
			}
		}

		/// <summary>
		/// GetType(string) returns
		/// the string conversion
		/// of the given type.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns the appropriate value for type.</returns>
		public int GetType(string type)
		{
			if (IsValidType(type))
			{
				return CODES[type];
			}
			else
			{
				return CODES[NULL];
			}
		}

		#endregion

		#region Validation methods.

		/// <summary>
		/// IsValidType(int) returns
		/// the validity status of
		/// the parameter.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns true if valid and not null. Returns false, if otherwise.</returns>
		public bool IsValidType(int type)
		{
			return ((TYPES.ContainsKey(type)) && (type != NULL_CODE));
		}

		/// <summary>
		/// IsValidType(string) returns
		/// the validity status of
		/// the parameter.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns true if valid and not null. Returns false, if otherwise.</returns>
		public bool IsValidType(string type)
		{
			if (!IsEmptyOrNull(type))
			{
				return ((CODES.ContainsKey(type)) && (type != NULL));
			}
			else
			{
				return false;
			}
		}

		#endregion

		#endregion
	}
}
