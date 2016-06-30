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
using PhoneNumbers;

namespace DocumentParser.DocumentLoading.Resume
{
	public class PhoneBook : IBook<PhoneNumber>
	{
		#region Properties
				
		/// <summary>
		/// PhoneBook is a list of all the 
		/// PhoneNumber objects being stored.
		/// </summary>
		public List<PhoneNumber> PhoneNumbers
		{
			get { return this.Collection; }
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// GetFormattedNumbers(string) returns a collection
		/// of strings containing the numbers alone, formatted
		/// in the US National method.
		/// </summary>
		/// <param name="input">Input string to be searched.</param>
		/// <returns>Returns a List of numbers.</returns>
		public static List<string> GetFormattedNumbers(string input)
		{
			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			List<string> phoneNumbers = new List<string>();
			IEnumerable<PhoneNumberMatch> numbers = phoneUtility.FindNumbers(input, USA);

			foreach (PhoneNumberMatch match in numbers)
			{
				phoneNumbers.Add(phoneUtility.Format(match.Number, PhoneNumberFormat.NATIONAL));
			}

			return phoneNumbers;
		}

		/// <summary>
		/// GetNumbers(string) returns a collection
		/// of PhoneNumber objects.
		/// </summary>
		/// <param name="input">Input string to be searched.</param>
		/// <returns>Returns a List of numbers.</returns>
		public static List<PhoneNumber> GetNumbers(string input)
		{
			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			List<PhoneNumber> phoneNumber = new List<PhoneNumber>();
			IEnumerable<PhoneNumberMatch> numbers = phoneUtility.FindNumbers(input, USA);

			foreach (PhoneNumberMatch match in numbers)
			{
				phoneNumber.Add(match.Number);
			}

			return phoneNumber;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// PhoneBook() is an empty constructor
		/// that sets up the storage and collection
		/// objects for future use, as well as
		/// flagging the _empty flag with it's
		/// default value.
		/// </summary>
		public PhoneBook() : base()
		{
		}


		/// <summary>
		/// PhoneBook(string, string) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(string type, string number)
			: base (type, number)
		{
		}

		/// <summary>
		/// PhoneBook(int, string) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(int type, string number)
			: base(type, number)
		{
		}

		/// <summary>
		/// PhoneBook(string, int) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(string type, int number)
			: base()
		{
			TryAddEntry(type, number);
		}

		/// <summary>
		/// PhoneBook(int, int) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(int type, int number)
			: base()
		{
			TryAddEntry(type, number);
		}

		/// <summary>
		/// PhoneBook(int, PhoneNumber) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(int type, PhoneNumber number)
			: base (type, number)
		{
		}

		/// <summary>
		/// PhoneBook(string, PhoneNumber) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public PhoneBook(string type, PhoneNumber number)
			: base(type, number)
		{
		}
		
		#endregion
		
		#region Methods

		#region AddEntry Methods

		/// <summary>
		/// TryAddEntry(int, int) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="number">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public bool TryAddEntry(int type, int number)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, number);
				response = true;
			}
			catch (Exception e)
			{
				Program.Write("Exception handled! - " + e.Message);
				response = false;
			}
			finally
			{
				if (response)
				{
					Program.Write("No errors on entry addition.");
				}
			}

			return response;
		}

		/// <summary>
		/// AddEntry(int, int) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="number">The input seeking to be registered.</param>
		private void AddEntry(int type, int number)
		{
			if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else
			{
				PhoneNumber entry = GetAsEntry(number);

				// Attempt to add the entry to the registry. Validation attempts are performed first.

				if (entry == null)
				{
					throw new ArgumentNullException("The input value for the entry object is null.");
				}
				else if (!IsValidEntry(entry))
				{
					throw new ArgumentException("This entry is invalid and cannot be recognized.");
				}
				else
				{
					// All levels of validation have been passed.
					_directory.Add(type, entry);
					_book.Add(GetType(type), entry);
					_collection.Add(entry);
					_types.Add(type);
					_empty = false;
				}
			}
		}

		/// <summary>
		/// TryAddEntry(string, int) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="number">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public bool TryAddEntry(string type, int number)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, number);
				response = true;
			}
			catch (Exception ae)
			{
				Program.Write("Exception handled! - " + ae.Message);
				response = false;
			}
			finally
			{
				if (response)
				{
					Program.Write("No errors on entry addition.");
				}
			}

			return response;
		}

		/// <summary>
		/// AddEntry(string, int) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="number">The input seeking to be registered.</param>
		private void AddEntry(string type, int number)
		{
			if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else
			{
				PhoneNumber entry = GetAsEntry(number);

				// Attempt to add the entry to the registry. Validation attempts are performed first.

				if (entry == null)
				{
					throw new ArgumentException("The input value for the entry object is null.");
				}
				else if (!IsValidEntry(entry))
				{
					throw new ArgumentException("This entry is invalid and cannot be recognized.");
				}
				else
				{
					// All levels of validation have been passed.
					_directory.Add(GetType(type), entry);
					_book.Add(type, entry);
					_collection.Add(entry);
					_types.Add(GetType(type));
					_empty = false;
				}
			}
		}

		#endregion

		#region GetAsEntry implementations

		/// <summary>
		/// GetAsEntry(string) returns the
		/// the PhoneNumber that matches
		/// the string.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override PhoneNumber GetAsEntry(string entry)
		{
			if (String.IsNullOrEmpty(entry.Trim()))
			{
				throw new ArgumentNullException("The entry value is invalid because it was null.");
			}
			else
			{
				try
				{
					PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();
					PhoneNumber phoneNumber = phoneUtility.Parse(entry.Trim(), USA);

					// Check if it's possible.
					if (!phoneUtility.IsPossibleNumber(phoneNumber))
					{
						throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not possible.");
					}
					else
					// Check if it's valid.
					if (!phoneUtility.IsValidNumber(phoneNumber))
					{
						throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is invalid.");
					}
					else
					// If it's possible and valid, check Dictionary reference status.
					{
						// Does the phone number, itself, already exist?
						if (_directory.ContainsValue(phoneNumber) || _book.ContainsValue(phoneNumber))
						{
							throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This phone number has already been recorded!");
						}
						else
						{

							// If all formms of validation have succeeded, safely pass the queried entry.
							return phoneNumber;

						}
					}
				}
				catch (Exception npe)
				{
					throw npe;
				}
			}
		}

		/// <summary>
		/// GetAsEntry(int) returns the
		/// the PhoneNumber that matches
		/// the integer.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override PhoneNumber GetAsEntry(int entry)
		{
			if (String.IsNullOrEmpty(entry.ToString().Trim()))
			{
				throw new ArgumentNullException("The entry value is invalid because it was null.");
			}
			else
			{
				try
				{
					PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();
					PhoneNumber phoneNumber = phoneUtility.Parse(entry.ToString().Trim(), USA);

					// Check if it's possible.
					if (!phoneUtility.IsPossibleNumber(phoneNumber))
					{
						throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not possible.");
					}
					else
					// Check if it's valid.
					if (!phoneUtility.IsValidNumber(phoneNumber))
					{
						throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is invalid.");
					}
					else
					// If it's possible and valid, check Dictionary reference status.
					{
						// Does the phone number, itself, already exist?
						if (_directory.ContainsValue(phoneNumber) || _book.ContainsValue(phoneNumber))
						{
							throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This phone number has already been recorded!");
						}
						else
						{

							// If all formms of validation have succeeded, safely pass the queried entry.
							return phoneNumber;

						}
					}
				}
				catch (Exception npe)
				{
					throw npe;
				}
			}
		}

		#endregion

		#region Miscellaneous Methods

		/// <summary>
		/// GetNumber(int) allows users to retrieve a single phone number.
		/// </summary>
		/// <param name="type">Type of phone number required.</param>
		/// <returns>Returns an PhoneNumber object.</returns>
		public PhoneNumber GetNumber(int type)
		{
			return GetEntry(type);
		}

		#endregion

		#endregion
	}
}