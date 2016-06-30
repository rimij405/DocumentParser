using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume
{
	/// <summary>
	/// ContactSheet.cs is a class wrapper
	/// for contact information that can
	/// generally be stripped from a
	/// resume.
	/// 
	/// A Name, AddressBook,
	/// EmailAddressBook, and Telephone
	/// are associated with every,
	/// one ContactSheet.
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

		#region Constructor

		public ContactSheet()
		{

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
