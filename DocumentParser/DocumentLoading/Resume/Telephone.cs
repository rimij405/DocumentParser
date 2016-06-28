/*****************************************************************************
   * 
   * <Resume Scraper> Copyright (C) 2016  Ian A. Effendi 
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
using PhoneNumbers;

namespace DocumentParser.DocumentLoading.Resume
{
	public class Telephone
	{

		#region Constants

		public const string USA = "US";
		public const string PERSONAL = "Personal Number";
		public const string MOBILE = "Mobile Number";
		public const string WORK = "Work Number";
		public const string ALTERNATE = "Alternate Number";
		public const int PERSONAL_TYPE = 0;
		public const int MOBILE_TYPE = 1;
		public const int WORK_TYPE = 2;
		public const int ALTERNATE_TYPE = 3;

		#endregion


		#region Attributes

		private List<int> _telephoneTypes;
		private List<PhoneNumber> _telephone;
		private bool _empty;

		#endregion

		#region Properties



		#endregion


		#region Constructor

		public Telephone()
		{

		}


		/// <summary>
		/// Telephone(string, string) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(string type, string number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number, USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Telephone(int, string) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(int type, string number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number, USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetValidType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Telephone(string, int) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(string type, int number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number.ToString(), USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Telephone(int, int) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(int type, int number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number.ToString(), USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetValidType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Telephone(int, PhoneNumber) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(int type, PhoneNumber number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				if (phoneUtility.IsPossibleNumber(number) && phoneUtility.IsValidNumber(number))
				{
					_telephone.Add(number);
					_telephoneTypes.Add(GetValidType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// Telephone(string, PhoneNumber) takes
		/// a type (personal, mobile, work, etc.) and a
		/// phone number to assign to the contact.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		public Telephone(string type, PhoneNumber number)
		{
			_telephoneTypes = new List<int>();
			_telephone = new List<PhoneNumber>();

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				if (phoneUtility.IsPossibleNumber(number) && phoneUtility.IsValidNumber(number))
				{
					_telephone.Add(number);
					_telephoneTypes.Add(GetType(type));
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}
		
		#endregion

		#region Methods

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

		/// <summary>
		/// GetNumber(int) allows users to retrieve a single address.
		/// </summary>
		/// <param name="type">Type of number required.</param>
		/// <returns>Returns a PhoneNumber object.</returns>
		public PhoneNumber GetNumber(int type)
		{
			PhoneNumber number = null;

			if (HasType(type))
			{
				return _telephone[_telephoneTypes.IndexOf(type)];
			}

			return number;
		}

		/// <summary>
		/// GetNumber(string) allows users to retrieve a single address.
		/// </summary>
		/// <param name="type">Type of number required.</param>
		/// <returns>Returns a PhoneNumber object.</returns>
		public PhoneNumber GetNumber(string type)
		{
			PhoneNumber number = null;

			if (HasType(type))
			{
				return _telephone[_telephoneTypes.IndexOf(GetType(type))];
			}

			return number;
		}

		/// <summary>
		/// HasType(int) determines
		/// if the type List has the
		/// appropriate type.
		/// </summary>
		/// <param name="type">Type being searched for.</param>
		/// <returns>Returns true if the type list contains the appropriate type.</returns>
		public bool HasType(int type)
		{
			return _telephoneTypes.Contains(type);
		}

		/// <summary>
		/// HasType(string) determines
		/// if the type List has the
		/// appropriate type.
		/// </summary>
		/// <param name="type">Type being searched for.</param>
		/// <returns>Returns true if the type list contains the appropriate type.</returns>
		public bool HasType(string type)
		{
			return _telephoneTypes.Contains(GetType(type));
		}

		/// <summary>
		/// AddNumber(string, string) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(string type, string number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number, USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// AddNumber(int, int) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(int type, int number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number.ToString(), USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetValidType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// AddNumber(int, string) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(int type, string number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number, USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetValidType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// AddNumber(string, int) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(string type, int number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				PhoneNumber phoneNum = phoneUtility.Parse(number.ToString(), USA);

				if (phoneUtility.IsPossibleNumber(phoneNum) && phoneUtility.IsValidNumber(phoneNum))
				{
					_telephone.Add(phoneNum);
					_telephoneTypes.Add(GetType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// AddNumber(string, PhoneNumber) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(string type, PhoneNumber number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				if (phoneUtility.IsPossibleNumber(number) && phoneUtility.IsValidNumber(number))
				{
					_telephone.Add(number);
					_telephoneTypes.Add(GetType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// AddNumber(int, PhoneNumber) adds a number,
		/// and assigns a type to it.
		/// </summary>
		/// <param name="type">Type to be assigned.</param>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns true on operation success. Otherwise throws an exception.</returns>
		public bool AddNumber(int type, PhoneNumber number)
		{
			if (HasType(GetValidType(type)))
			{
				return false;  // Eg. Already has a personal number on record; can't be overwritten.
			}

			PhoneNumberUtil phoneUtility = PhoneNumberUtil.GetInstance();

			try
			{
				if (phoneUtility.IsPossibleNumber(number) && phoneUtility.IsValidNumber(number))
				{
					_telephone.Add(number);
					_telephoneTypes.Add(GetValidType(type));
					return true;
				}
				else
				{
					throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number is not valid or possible.");
				}
			}
			catch (NumberParseException e)
			{
				throw e;
			}
		}

		/// <summary>
		/// GetType(number) gets the type
		/// associated with the existing number
		/// in the list.
		/// </summary>
		/// <param name="number">Number to be parsed.</param>
		/// <returns>Returns the integer value of the Telephone type. If invalid, throws a NumberParseException.</returns>
		public int GetType(PhoneNumber number)
		{
			if (_telephone.Contains(number))
			{
				return _telephoneTypes[_telephone.IndexOf(number)];
			}

			throw new NumberParseException(ErrorType.NOT_A_NUMBER, "This number isn't stored by the program and, as such, as no type.");
		}

		/// <summary>
		/// GetType(string) returns the type
		/// in integer form. It's a type of parser.
		/// Any non-existent type will be returned
		/// as "alternate".
		/// </summary>
		/// <param name="type">Type to be returned.</param>
		/// <returns>Returns type as an integer.</returns>
		public int GetType(string type)
		{
			switch (type)
			{
				case PERSONAL:
					return PERSONAL_TYPE;
				case MOBILE:
					return MOBILE_TYPE;
				case WORK:
					return WORK_TYPE;
				case ALTERNATE:
				default:
					return ALTERNATE_TYPE;
			}
		}


		/// <summary>
		/// GetValidType(int) returns the type
		/// in string form. It's a type of parser.
		/// Any non-existent type will be returned
		/// as "alternate".
		/// </summary>
		/// <param name="type">Type to be returned.</param>
		/// <returns>Returns type as an int.</returns>
		public string GetValidType(string type)
		{
			switch (type)
			{
				case PERSONAL:
				case MOBILE:
				case WORK:
				case ALTERNATE:
					return type;
				default:
					return ALTERNATE;
			}
		}

		/// <summary>
		/// GetType(int) returns the type
		/// in string form. It's a type of parser.
		/// Any non-existent type will be returned
		/// as "alternate".
		/// </summary>
		/// <param name="type">Type to be returned.</param>
		/// <returns>Returns type as an string.</returns>
		public string GetType(int type)
		{
			switch (type)
			{
				case PERSONAL_TYPE:
					return PERSONAL;
				case MOBILE_TYPE:
					return MOBILE;
				case WORK_TYPE:
					return WORK;
				case ALTERNATE_TYPE:
				default:
					return ALTERNATE;
			}
		}

		/// <summary>
		/// GetValidType(int) returns the type
		/// in string form. It's a type of parser.
		/// Any non-existent type will be returned
		/// as "alternate".
		/// </summary>
		/// <param name="type">Type to be returned.</param>
		/// <returns>Returns type as an int.</returns>
		public int GetValidType(int type)
		{
			switch (type)
			{
				case PERSONAL_TYPE:
				case MOBILE_TYPE:
				case WORK_TYPE:
				case ALTERNATE_TYPE:
					return type;
				default:
					return ALTERNATE_TYPE;
			}
		}


		#endregion
	}
}
