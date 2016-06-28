using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume
{
	/// <summary>
	/// The Name.cs class holds three things:
	/// The first, middle, and last name
	/// of a given applicant.
	/// </summary>
	public class Name
	{
		#region Constants

		public const string EMPTY = "{EMPTY}";

		#endregion

		#region Attributes

		private string _prefix = ""; // Dr., Mr., Mrs., Miss, etc.
		private string _firstName = ""; // First name. John, Jane, etc.
		private string _middleName = ""; // Middle name. Chris, Rose, etc.
		private string _lastName = ""; // Last name. Davidson, Davis, etc.
		private string _suffix = ""; // Suffix. M.D., M.A., Esq., PhD., Jr., III, etc.

		#endregion

		#region Properties

		/// <summary>
		/// FullName returns the entire
		/// full name utilizing whatever
		/// resources are not empty or null.
		/// </summary>
		public string FullName
		{
			get
			{
				string fullName = "";

				if (!IsEmpty(_prefix))
				{ fullName += _prefix + " "; }

				if (!IsEmpty(_firstName))
				{ fullName += _firstName + " "; }

				if (!IsEmpty(_middleName))
				{ fullName += _middleName + " "; }

				if (!IsEmpty(_lastName))
				{ fullName += _lastName + " "; }

				if (!IsEmpty(_suffix))
				{ fullName += _suffix + " "; }
				
				return fullName.Trim();
			}
		}

		/// <summary>
		/// Prefix returns the
		/// prefix portion of
		/// the name.
		/// </summary>
		public string Prefix
		{
			get
			{
				if (!IsEmpty(_prefix))
				{
					return _prefix;
				}
				else
				{
					return "";
				}
			}

		}

		/// <summary>
		/// FirstName returns the
		/// first name portion of
		/// the name.
		/// </summary>
		public string FirstName
		{
			get
			{
				if (!IsEmpty(_firstName))
				{
					return _firstName;
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>
		/// MiddleName returns the
		/// middle name portion of
		/// the name.
		/// </summary>
		public string MiddleName
		{
			get
			{
				if (!IsEmpty(_middleName))
				{
					return _middleName;
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>
		/// LastName returns the
		/// last name portion of
		/// the name.
		/// </summary>
		public string LastName
		{
			get
			{
				if (!IsEmpty(_lastName))
				{
					return _lastName;
				}
				else
				{
					return "";
				}
			}
		}

		/// <summary>
		/// Suffix returns the
		/// suffix portion of
		/// the name.
		/// </summary>
		public string Suffix
		{
			get
			{
				if (!IsEmpty(_suffix))
				{
					return _suffix;
				}
				else
				{
					return "";
				}
			}

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
				return (!IsEmpty(_prefix));
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
				return (!IsEmpty(_middleName));
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
				return (!IsEmpty(_suffix));
			}
		}

		#endregion

		#region Constructor

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
			AssignUnlessEmpty(ref this._prefix, prefix);
			AssignUnlessEmpty(ref this._firstName, firstName);
			AssignUnlessEmpty(ref this._middleName, middleName);
			AssignUnlessEmpty(ref this._lastName, lastName);
			AssignUnlessEmpty(ref this._suffix, suffix);
		}

		#endregion

		#region Methods

		/// <summary>
		/// IsEmpty(string) determines
		/// if the input string matches
		/// the "EMPTY" code provided
		/// within this class. See <see cref="Name.EMPTY"/>
		/// </summary>
		/// <param name="input">String input for determination of emptiness.</param>
		/// <returns>Returns true if the string is empty or null; returns false if otherwise.</returns>
		public bool IsEmpty(string input)
		{
			return (input == EMPTY || String.IsNullOrEmpty(input));
		}


		/// <summary>
		/// AssignUnlessEmpty(string, string) assigns the second
		/// parameter's value to the first, as long as 
		/// </summary>
		/// <param name="assignTo"></param>
		/// <param name="assignee"></param>
		public void AssignUnlessEmpty(ref string assignTo, string assignee)
		{
			if (IsEmpty(assignee))
			{
				assignTo = EMPTY;
			}
			else
			{
				assignTo = assignee;
			}
		}

		#endregion
	}
}
