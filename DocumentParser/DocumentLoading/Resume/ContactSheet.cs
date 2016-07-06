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
using DocumentParser.DocumentLoading.Resume.Books;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace DocumentParser.DocumentLoading.Resume
{
	/// <summary>
	/// ContactSheet.cs is a class wrapper
	/// for contact information that can
	/// generally be stripped from a
	/// resume.
	/// 
	/// <para>A Name, AddressBook,
	/// EmailAddressBook, and PhoneBook
	/// are associated with every,
	/// one ContactSheet.</para>
	/// </summary>
	public class ContactSheet
	{
		#region Constants

		public const string NAME = "Name";
		public const string EMAIL = "Email";
		public const string ADDRESS = "Address";
		public const string TELEPHONE = "Telephone";
		public const string NULL = "{NULL}";

		public const int NAME_CODE = 0;
		public const int EMAIL_CODE = 1;
		public const int ADDRESS_CODE = 2;
		public const int TELEPHONE_CODE = 3;
		public const int NULL_CODE = -1;

		public static Dictionary<int, string> TYPES;
		public static Dictionary<string, int> CODES;
		public static bool isInitialized = false;

		#endregion

		#region Attributes

		private Dictionary<string, BaseBook> _directory;

		#endregion

		#region Properties

		public Name Name
		{
			get { return (Name)this._directory[NAME]; }
		}

		public AddressBook AddressBook
		{
			get { return (AddressBook)this._directory[ADDRESS]; }
		}

		public PhoneBook Telephone
		{
			get { return (PhoneBook)this._directory[TELEPHONE]; }
		}

		public EmailBook EmailBook
		{
			get { return (EmailBook)this._directory[EMAIL]; }
		}

		#endregion

		#region Constructor / Initialization methods.

		/// <summary>
		/// ContactSheet() is an empty constructor
		/// for a ContactSheet object.
		/// 
		/// In theory, when you receive a paper
		/// form, it needs to be filled out.
		/// As such, ContactSheets are created
		/// "blank" and must be filled out
		/// by the user.
		/// </summary>
		public ContactSheet()
		{
			_init();
		}
		
		/// <summary>
		/// _init() is used by the above
		/// constructor methods in order
		/// to initialize the directory.
		/// </summary>
		private void _init()
		{
			_directory = new Dictionary<string, BaseBook>();
			Name _nameBook = new Name();
			AddressBook _addressBook = new AddressBook();
			EmailBook _emailBook = new EmailBook();
			PhoneBook _phoneBook = new PhoneBook();

			_directory.Add(NAME, _nameBook.GetSelf());
			_directory.Add(ADDRESS, _addressBook.GetSelf());
			_directory.Add(EMAIL, _emailBook.GetSelf());
			_directory.Add(TELEPHONE, _phoneBook.GetSelf());

			if (!isInitialized)
			{
				TYPES = new Dictionary<int, string>();
				CODES = new Dictionary<string, int>();

				TYPES.Add(NAME_CODE, NAME);
				TYPES.Add(ADDRESS_CODE, ADDRESS);
				TYPES.Add(EMAIL_CODE, EMAIL);
				TYPES.Add(TELEPHONE_CODE, TELEPHONE);
				TYPES.Add(NULL_CODE, NULL);
				
				CODES.Add(NAME, NAME_CODE);
				CODES.Add(ADDRESS, ADDRESS_CODE);
				CODES.Add(EMAIL, EMAIL_CODE);
				CODES.Add(TELEPHONE, TELEPHONE_CODE);
				CODES.Add(NULL, NULL_CODE);

				isInitialized = true;
			}
		}

		#region Add Methods.

		/// <summary>
		/// Add(Name entry) adds
		/// a new name to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(Name entry)
		{
			try
			{
				Name.Merge(_directory[NAME].GetAsName(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging names: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(string, string, string, string, string) adds
		/// a new name to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(string prefix, string firstName, string middleName, string lastName, string suffix)
		{
			try
			{
				Name entry = new Name(prefix, firstName, middleName, lastName, suffix);
				Name.Merge(_directory[NAME].GetAsName(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging names: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(AddressBook entry) adds
		/// a new address book to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(AddressBook entry)
		{
			try
			{
				AddressBook.Merge(_directory[ADDRESS].GetAsAddressBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging address book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(int, Address) adds
		/// a new address book to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(int type, AddressParser.AddressParseResult address)
		{
			try
			{
				AddressBook entry = new AddressBook(type, address);
				AddressBook.Merge(_directory[ADDRESS].GetAsAddressBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging address book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(EmailBook entry) adds
		/// a new email book to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(EmailBook entry)
		{
			try
			{
				EmailBook.Merge(_directory[EMAIL].GetAsEmailBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging email book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(int, Email) adds
		/// a new email book to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(int type, MailAddress email)
		{
			try
			{
				EmailBook entry = new EmailBook(type, email);
				EmailBook.Merge(_directory[EMAIL].GetAsEmailBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging email book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(PhoneBook entry) adds
		/// a new phonebook to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(PhoneBook entry)
		{
			try
			{
				PhoneBook.Merge(_directory[TELEPHONE].GetAsPhoneBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging email book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Add(int, PhoneNumber) adds
		/// a new phonebook to the contact sheet.
		/// </summary>
		/// <param name="entry">Value being inserted.</param>
		/// <returns>Returns true is operation was successful. Returns false, if otherwise.</returns>
		public bool Add(int type, PhoneNumbers.PhoneNumber number)
		{
			try
			{
				PhoneBook entry = new PhoneBook(type, number);
				PhoneBook.Merge(_directory[TELEPHONE].GetAsPhoneBook(), entry, true);
				return true;
			}
			catch (Exception e)
			{
				Program.Write("Error when merging email book: " + e.Message + "\n");
				Program.Write("Source: " + e.Source + "\n");
				Program.Write("Stack Trace: " + e.StackTrace);
				return false;
			}
		}

		#endregion

		#endregion

		#region Methods

		#region Book Status

		/// <summary>
		/// IsEmpty() determines if the current
		/// Contact sheet is empty.
		/// </summary>
		/// <returns>Returns true if empty. False if at least one BaseBook derived class has content.</returns>
		public bool IsEmpty()
		{
			bool emptyStatus = true;

			for (int i = 0; i < _directory.Count; i++)
			{
				emptyStatus = _directory[GetType(i)].IsEmpty();
				if (!emptyStatus) { return false; }
			}

			return emptyStatus;
		}

		#endregion

		#region Retrieving Books

		/// <summary>
		/// GetNames() returns the stored
		/// Name object.
		/// </summary>
		/// <returns>Returns the Name object.</returns>
		public Name GetNames()
		{
			return GetEntry(NAME).GetAsName();
		}

		/// <summary>
		/// GetAddressBook() returns the stored
		/// AddressBook.
		/// </summary>
		/// <returns>Returns the AddressBook.</returns>
		public AddressBook GetAddressBook()
		{
			return GetEntry(ADDRESS).GetAsAddressBook();
		}

		/// <summary>
		/// GetEmailBook() returns the stored
		/// EmailBook.
		/// </summary>
		/// <returns>Returns the EmailBook.</returns>
		public EmailBook GetEmailBook()
		{
			return GetEntry(EMAIL).GetAsEmailBook();
		}

		/// <summary>
		/// GetPhoneBook() returns the stored
		/// PhoneBook.
		/// </summary>
		/// <returns>Returns the PhoneBook.</returns>
		public PhoneBook GetPhoneBook()
		{
			return GetEntry(TELEPHONE).GetAsPhoneBook();
		}

		/// <summary>
		/// GetEntry(string) takes the type being
		/// sought out and returns the BaseBook
		/// form of that object.
		/// </summary>
		/// <param name="type">Type being searched for.</param>
		/// <returns>Returns a BaseBook wrapped object. Throws an ArgumentNullException if the input cannot be found.</returns>
		public BaseBook GetEntry(string type)
		{
			if (GetType(type) != NULL_CODE)
			{
				return _directory[type].GetSelf();
			}
			else
			{
				throw new ArgumentNullException("The input type can not be found.");
			}
		}

		#endregion

		#region Service Methods

		/// <summary>
		/// GetType(int) returns the
		/// string conversion of a
		/// given type.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns the appropriate type value.</returns>
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
		/// GetType(string) returns the
		/// integer converseion of a
		/// given type.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns the appropriate type value.</returns>
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

		/// <summary>
		/// IsValidType(int) determines
		/// if the type code input is
		/// a valid one.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns true if valid, false if invalid.</returns>
		public bool IsValidType(int type)
		{
			return ((TYPES.ContainsKey(type)) && (type != NULL_CODE));
		}

		/// <summary>
		/// IsValidType(string) determines
		/// if the type definition input is
		/// a valid one.
		/// </summary>
		/// <param name="type">Type being sought out.</param>
		/// <returns>Returns true if valid, false if invalid.</returns>
		public bool IsValidType(string type)
		{
			if (!String.IsNullOrEmpty(type.Trim()))
			{
				return ((CODES.ContainsKey(type.Trim())) && (type.Trim() != NULL));
			}

			return false;
		}

		#endregion

		#endregion
	}
}
