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

namespace DocumentParser.DocumentLoading.Resume.Books
{
	/// <summary>
	/// IBook is an abstract class that should
	/// be implemented by another class that
	/// will give the book it's type.
	/// </summary>
	/// <typeparam name="TEntry">This is the type that will be processed by the many collections and stores in this method.</typeparam>
	public abstract class IBook<TEntry> : BaseBook
	{
		#region Constants

		// Directives.
		public const string USA = "US";

		// Types.
		// // String
		public const string PERSONAL = "Personal";
		public const string MOBILE = "Mobile";
		public const string WORK = "Work";
		public const string ALTERNATE = "Alternate";
		public const string NULL = "NULL";

		// // Int
		public const int PERSONAL_CODE = 0;
		public const int MOBILE_CODE = 1;
		public const int WORK_CODE = 2;
		public const int ALTERNATE_CODE = 3;
		public const int NULL_CODE = -1;

		// // Static Attributes
		public static Dictionary<int, string> TYPES;
		public static Dictionary<string, int> CODES;
		public static bool isInitialized = false;

		#endregion

		#region Attributes

		// References.
		protected Dictionary<int, TEntry> _directory;
		protected Dictionary<string, TEntry> _book;

		// Collections.
		protected List<int> _types;
		protected List<TEntry> _collection;

		// Flags.
		protected bool _empty;

		#endregion

		#region Properties

		/// <summary>
		/// Directory is a store of a particular
		/// object, organized by its type key.
		/// </summary>
		public virtual Dictionary<int, TEntry> Directory
		{
			get { return this._directory; }
		}

		/// <summary>
		/// Types is the collection of type keys.
		/// This is a list of all of the
		/// currently registered ones.
		/// </summary>
		public virtual List<int> Types
		{
			get { return this._types; }
		}

		/// <summary>
		/// Collection is the collection
		/// of objects that this generic
		/// object type adopts.
		/// </summary>
		public virtual List<TEntry> Collection
		{
			get { return this._collection; }
		}

		/// <summary>
		/// IsEmpty is a reference to the private
		/// empty field that will tell the user
		/// if the Generic object is empty.
		/// 
		/// GenericBook objects cannot be removed,
		/// meaning, we do not need to count
		/// the lists every time we want to check.
		/// 
		/// Instead, we use a simple,
		/// true-false flag.
		/// 
		/// A boolean.
		/// </summary>
		public virtual bool Empty
		{
			get { return this._empty; }
		}

		/// <summary>
		/// Size returns the Count size of our
		/// collection. As all of the Dictionary
		/// and List objects here are parallel
		/// to each other, we need not worry
		/// about counting the others.
		/// </summary>
		public virtual int Size
		{
			get { return this._collection.Count; }
		}

		#endregion

		#region Constructor
	
		/// <summary>
		/// IBook() is an empty constructor
		/// that runs the initialization
		/// method to facilitate modification
		/// of its internal fields later on.
		/// </summary>
		public IBook()
		{
			_init();
		}

		/// <summary>
		/// IBook(int, string) is a constructor
		/// that takes an entry.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		public IBook(int type, string input)
		{
			_init();
			TryAddEntry(type, input);
		}
				
		/// <summary>
		/// IBook(string, string) is a constructor
		/// that takes an entry.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		public IBook(string type, string input)
		{
			_init();
			TryAddEntry(type, input);
		}
		
		/// <summary>
		/// IBook(int, TEntry) is a constructor
		/// that takes an entry via direct pass of the object.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		public IBook(int type, TEntry input)
		{
			_init();
			TryAddEntry(type, input);
		}

		/// <summary>
		/// IBook(string, TEntry) is a constructor
		/// that takes an entry via direct pass of the object.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		public IBook(string type, TEntry input)
		{
			_init();
			TryAddEntry(type, input);
		}

		/// <summary>
		/// _init() is a void method
		/// called by the constructors above. It is used by the constructor in order
		/// to set the values up to the default settings.
		/// 
		/// <para>Rather than rewrite code, we use it in order
		/// to streamline the construction of different
		/// Constructor methods.</para>
		/// </summary>
		protected virtual void _init()
		{
			_directory = new Dictionary<int, TEntry>();
			_book = new Dictionary<string, TEntry>();
			_types = new List<int>();
			_collection = new List<TEntry>();
			_empty = true;

			if (!isInitialized)
			{
				// Create the objects.
				TYPES = new Dictionary<int, string>();
				CODES = new Dictionary<string, int>();

				// Fill Types dictionary.
				CODES.Add(PERSONAL, PERSONAL_CODE);
				CODES.Add(MOBILE, MOBILE_CODE);
				CODES.Add(WORK, WORK_CODE);
				CODES.Add(ALTERNATE, ALTERNATE_CODE);
				CODES.Add(NULL, NULL_CODE);

				// Fill Codes dictionary.
				TYPES.Add(PERSONAL_CODE, PERSONAL);
				TYPES.Add(MOBILE_CODE, MOBILE);
				TYPES.Add(WORK_CODE, WORK);
				TYPES.Add(ALTERNATE_CODE, ALTERNATE);
				TYPES.Add(NULL_CODE, NULL);

				isInitialized = true;
			}
		}

		#endregion

		#region Methods

		#region Static Methods.

		/// <summary>
		/// Merge(IBook, IBook) takes two books and merges
		/// so that any empty holes in the first are
		/// filled by the second.
		/// </summary>
		/// <param name="baseBook">Book being merged to.</param>
		/// <param name="newBook">Book with the new data to merge. </param>
		/// <param name="overwrite">Overwrite flag.</param>
		public static void Merge(IBook<TEntry> baseBook, IBook<TEntry> newBook, bool overwrite)
		{
			if (overwrite)
			{
				Overwrite(baseBook, newBook);
				return;
			}
			else
			{
				foreach (int t in baseBook.Types)
				{
					if (!baseBook.HasType(t) && newBook.HasType(t))
					{
						baseBook.AddEntry(t, newBook.Directory[t]);
					}
				}
				return;
			}
		}

		/// <summary>
		/// Overwrite(IBook, IBook) takes two books of 
		/// the same type and merges them, 
		/// overwriting all entries from the latter,
		/// into the new one.
		/// </summary>
		/// <param name="baseBook">Book being overwritten.</param>
		/// <param name="newBook">Book with the new data to overwrite.</param>
		public static void Overwrite(IBook<TEntry> baseBook, IBook<TEntry> newBook)
		{
			foreach (int t in baseBook.Types)
			{
				if (baseBook.HasType(t) && newBook.HasType(t))
				{
					baseBook.Directory[t] = newBook.Directory[t];
				}
			}
		}

		#endregion

		#region AddEntry Methods

		/// <summary>
		/// TryAddEntry(int, string) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public virtual bool TryAddEntry(int type, string input)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, input);
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
		/// AddEntry(int, string) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		protected virtual void AddEntry(int type, string input)
		{
			if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else if (String.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("The input value for the entry string is null.");
			}
			else
			{
				TEntry entry = GetAsEntry(input);

				// Attempt to add the entry to the registry.
				if (!IsValidEntry(entry))
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
		/// TryAddEntry(string, string) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public virtual bool TryAddEntry(string type, string input)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, input);
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
		/// AddEntry(string, string) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		protected virtual void AddEntry(string type, string input)
		{
			if (String.IsNullOrEmpty(type))
			{
				throw new ArgumentException("No type was provided for the entry.");
			}
			else if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else if (String.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException("The input value for the entry string is null.");
			}
			else
			{
				TEntry entry = GetAsEntry(input);

				// Attempt to add the entry to the registry.
				if (!IsValidEntry(entry))
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

		/// <summary>
		/// TryAddEntry(int, TEntry) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public virtual bool TryAddEntry(int type, TEntry input)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, input);
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
		/// AddEntry(int, TEntry) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		protected virtual void AddEntry(int type, TEntry entry)
		{
			if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else if (entry == null)
			{
				throw new ArgumentNullException("The input value for the entry object is null.");
			}
			else
			{
				// Attempt to add the entry to the registry.
				if (!IsValidEntry(entry))
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
		/// TryAddEntry(string, TEntry) attempts
		/// to add the input parameter into the
		/// book, avoiding overwrites.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		/// <returns>Returns true if operation successful, false if there is an overwrite or exception.</returns>
		public virtual bool TryAddEntry(string type, TEntry input)
		{
			bool response = false;

			try
			{
				this.AddEntry(type, input);
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
		/// AddEntry(string, TEntry) puts the
		/// type and input into the proper
		/// places and sets the flag _empty
		/// to false.
		/// </summary>
		/// <param name="type">The type seeking to be registered.</param>
		/// <param name="input">The input seeking to be registered.</param>
		protected virtual void AddEntry(string type, TEntry entry)
		{
			if (String.IsNullOrEmpty(type))
			{
				throw new ArgumentException("No type was provided for the entry.");
			}
			else if (HasType(type))
			{
				throw new ArgumentException("The entry's type already exists in the book.");
			}
			else if (!IsValidType(type))
			{
				throw new ArgumentException("The entry's type is invalid and cannot be recognized.");
			}
			else if (entry == null)
			{
				throw new ArgumentNullException("The input value for the entry object is null.");
			}
			else
			{
				// Attempt to add the entry to the registry.
				if (!IsValidEntry(entry))
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

		#region HasType Methods

		/// <summary>
		/// HasType(int) returns a value
		/// based on the assessment of
		/// whewther or not there is a
		/// given type in any of the 
		/// appropriate collections.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <returns></returns>
		protected virtual bool HasType(int type)
		{
			if (_types.Count > 0)
			{
				if (!IsValidType(type))
				{
					return false;
				}
				else
				{
					if (_directory.ContainsKey(type))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// HasType(string) returns a value
		/// based on the assessment of
		/// whewther or not there is a
		/// given type in any of the 
		/// appropriate collections.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <returns></returns>
		protected virtual bool HasType(string type)
		{
			if (_types.Count > 0)
			{
				if (!IsValidType(type))
				{
					return false;
				}
				else
				{
					if (_book.ContainsKey(type))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// HasEntry(string) returns a value
		/// based on the assessment of
		/// whewther or not there is a
		/// given entry in any of the 
		/// appropriate collections.
		/// </summary>
		/// <param name="entry">Entry to be searched for.</param>
		/// <returns></returns>
		protected virtual bool HasEntry(string entry)
		{
			return HasEntry(GetAsEntry(entry));
		}

		/// <summary>
		/// HasEntry(string) returns a value
		/// based on the assessment of
		/// whewther or not there is a
		/// given entry in any of the 
		/// appropriate collections.
		/// </summary>
		/// <param name="entry">Entry to be searched for.</param>
		/// <returns>True if it exists. False if it is new.</returns>
		protected virtual bool HasEntry(TEntry entry)
		{
			if (Size > 0)
			{
				if (!IsValidEntry(entry))
				{
					return false;
				}
				else
				{
					if (_directory.ContainsValue(entry) || _book.ContainsValue(entry))
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}

		#endregion

		#region Accessor Methods
		
		/// <summary>
		/// GetAsEntry(string) returns the
		/// equivalent object that matches
		/// the string, within the context
		/// of the implementation class.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exception if the entry does not exist. Returns object otherwise.</returns>
		protected abstract TEntry GetAsEntry(string entry);

		/// <summary>
		/// GetAsEntry(int) returns the
		/// equivalent object that matches
		/// the int, in special cases, within the context
		/// of the implementation class.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exception if the entry does not exist. Returns object otherwise.</returns>
		protected abstract TEntry GetAsEntry(int entry);

		/// <summary>
		/// GetType(string) takes a string and outputs
		/// an associated value.
		/// </summary>
		/// <param name="type">The type being searched for.</param>
		/// <returns>Returns the null marker if a valid integer cannot be returned.</returns>
		protected virtual int GetType(string type)
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
		/// GetType(string) takes an integer and outputs
		/// an associated value.
		/// </summary>
		/// <param name="type">The type being searched for.</param>
		/// <returns>Returns the null marker if a valid string cannot be returned.</returns>
		protected virtual string GetType(int type)
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
		/// GetType(TEntry) takes an entry and outputs
		/// an associated value.
		/// </summary>
		/// <param name="entry">Gets the type this entry object has.</param>
		/// <returns>Throws an exception if the entry does not exist. Returns an integer otherwise.</returns>
		protected virtual int GetType(TEntry entry)
		{
			if (HasEntry(entry))
			{
				for (int i = 0; i < _collection.Count; i++)
				{
					TEntry compare = _collection[i];
					if (entry.Equals(compare))
					{
						return _types[i];
					}
				}

				// If we reach this point then there is no contained entry.
				throw new ArgumentException("There was an error getting the type from the entry.");
			}
			else
			{
				throw new ArgumentException("This is not a stored entry.");
			}
		}


		/// <summary>
		/// GetEntry(int) takes a type via parameter
		/// and attempts to return the given entry
		/// for that type.
		/// </summary>
		/// <param name="type">Type of entry to return.</param>
		/// <returns>Returns entry if valid. If null, throws, ArgumentException.</returns>
		protected virtual TEntry GetEntry(int type)
		{
			// Check if the type is valid.
			if (IsValidType(type))
			{
				if (!HasType(type) || !_directory.ContainsKey(type))
				{
					throw new ArgumentException("This type is not currently registered with an entry.");
				}
				else
				{
					return _directory[type];
				}
			}
			else
			{
				throw new ArgumentException("The input type is invalid.");
			}
		}


		/// <summary>
		/// GetEntry(string) takes a type via parameter
		/// and attempts to return the given entry
		/// for that type.
		/// </summary>
		/// <param name="type">Type of entry to return.</param>
		/// <returns>Returns entry if valid. If null, throws, ArgumentException.</returns>
		protected virtual TEntry GetEntry(string type)
		{
			// Check if the string has content.
			if (!String.IsNullOrEmpty(type.Trim()))
			{
				// Check if the type is valid.
				if (IsValidType(type))
				{
					if (!HasType(type.Trim()) || !_book.ContainsKey(type.Trim()))
					{
						throw new ArgumentException("This type is not currently registered with an entry.");
					}
					else
					{
						return _book[type.Trim()];
					}
				}
				else
				{
					throw new ArgumentException("The input type is invalid.");
				}
			}
			else
			{
				throw new ArgumentNullException("The input type statement was empty.");
			}
		}

		#endregion

		#region Validation Methods

		/// <summary>
		/// IsValidEntry(TEntry) takes a generic
		/// object and checks it against a
		/// particular set of criteria.
		/// In this case, it returns false
		/// if the entry is null.
		/// </summary>
		/// <param name="entry">Entry being evaluated.</param>
		/// <returns>Returns true if valid entry. Returns false if invalid entry is checked.</returns>
		protected virtual bool IsValidEntry(TEntry entry)
		{
			if (entry == null)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// IsValidEntry(string) takes a generic
		/// string and checks it against a
		/// particular set of criteria.
		/// In this case, it returns false
		/// if the entry is null.
		/// </summary>
		/// <param name="entry">Entry being evaluated.</param>
		/// <returns>Returns true if valid entry. Returns false if invalid entry is checked.</returns>
		protected virtual bool IsValidEntry(string entry)
		{
			if (!String.IsNullOrEmpty(entry.Trim()))
			{
				return IsValidEntry(GetAsEntry(entry.Trim()));
			}
			return false;
		}


		/// <summary>
		/// IsValidType(int) takes the type
		/// and checks it against a
		/// particular set of criteria.
		/// In this case, it returns false
		/// if the type is null.
		/// </summary>
		/// <param name="type">Type being evaluated.</param>
		/// <returns>Returns true if valid type. Returns false if invalid type is checked.</returns>
		protected virtual bool IsValidType(int type)
		{
			return ( (TYPES.ContainsKey(type)) && (type != NULL_CODE) );
		}

		/// <summary>
		/// IsValidType(string) takes the type
		/// and checks it against a
		/// particular set of criteria.
		/// In this case, it returns false
		/// if the type is null.
		/// </summary>
		/// <param name="type">Type being evaluated.</param>
		/// <returns>Returns true if valid type. Returns false if invalid type is checked.</returns>
		protected virtual bool IsValidType(string type)
		{
			if (!String.IsNullOrEmpty(type.Trim()))
			{
				return ((CODES.ContainsKey(type)) && (type != NULL));
			}

			return false;
		}

		#endregion

		#endregion
	}
}
