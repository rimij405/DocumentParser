/*****************************************************************************
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
	 * This project makes use of the following, open-source, external libraries:
	 * 
	 * - libphonenumber-csharp (Under the Apache 2.0 License)
	 *	 <https://github.com/erezak/libphonenumber-csharp/blob/master/LICENSE>	
	 *	 This library is utilized by the Telephone.cs class to help
	 *	 facilitate the extraction and formation of telephone numbers
	 *	 in a variety of document settings. This particular library
	 *	 is a port of the original Java library.
	 *	 
	 * - US Address Parser (Under the GNU General Public License v. 2)
	 *   <https://usaddress.codeplex.com/>
	 *   This library is utilized by the AddressBook.cs class to help
	 *   facilitate the extraction and formation of street addresses
	 *   in a variety of documents and settings. This library is a
	 *   partial port of the original Perl module "GEO::StreetAddress:US"
	 *   written by Schuyler D. Erle. for CPAN.
	 * 
	 * - DocX (Under the Microsoft Public License)
	 *   <https://docx.codeplex.com>
	 *   This .Net library allows developers to manipulate Word documents
	 *   from Word 2007/2010/2013. It does not require Microsoft Word
	 *   or Office to be installed. It was written by Cathal Coffey.
	 *  
	 * - iTextSharp Library (Under the GNU Affero General Public License v. 3)
	 *   <http://itextpdf.com/>
	 *   This library helps automate the documentation process involving
	 *   PDF files. IText's license prevents this source code from being
	 *   developed for commercial purpose as a commercial waiver for the
	 *   limitations of the AGPLv3 license, has not been purchased at present.
	 *   Use of iTextSharp is permitted so long as this source remains
	 *   open source.
	 *   
	 * - PDFSharp Library (Under the MIT License)
	 *   <http://www.pdfsharp.net/>
	 *   PDFSharp is an Open Source .NET library that can easily
	 *   create and process PDF documents, on the fly, from any .NET
	 *   language. The smae drawing routines can be used to create
	 *   PDF documents, draw on the screen, or send output to any
	 *   printer.
	 *   
	 * - MigraDoc Foundation Library (Under the MIT License)
	 *   <http://www.pdfsharp.net/>
	 *   MigraDoc Foundation is an Open Source .NET library that
	 *   can easily create documents based on an object model
	 *   with paragraphs, tables, styles, etc., and render
	 *   them into PDF's or RTF's.
	 *   
	 * - NPOI Library (Under the Apache 2.0 License)
	 *   <https://npoi.codeplex.com/>
	 *   NPOI is the .NET version of the POI Java project at
	 *   <http://poi.apache.org/>. POI is an open source project
	 *   that can help you read/write *.xls, *.doc, and *.ppt files,
	 *   having a wide application.
	 *   
	 * - Newtonsoft Json.NET Library (Under the MIT License)
	 *   <http://www.newtonsoft.com/json>
	 *   A high-performance, world-class JSON Serializer library,
	 *   that was released as open-source under the MIT license.
	 *   Supports LINQ queries, XML conversion, and the .NET language
	 *   making it incredibly versatile, and invaluable.
	 *   
	 * - SharpZipLib Library (Under the MIT License)
	 *   <https://github.com/icsharpcode/SharpZipLib>
	 *   This library was previously released under the
	 *   GNU General Public License v. 2 (GPLv2), however,
	 *   it has since been re-released under the MIT License,
	 *   a simpler, more permissive, license.
	 * 
	 ****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using AddressParser;
using AddressReader = AddressParser.AddressParser;

namespace DocumentParser.DocumentLoading.Resume
{	
	public class AddressBook
	{
		#region Constants
		
		public const string PERSONAL = "HOME ADDRESS";
		public const string WORK = "WORK ADDRESS";
		public const string ALTERNATE = "ALTERNATE ADDRESS";
		public const int PERSONAL_TYPE = 0;
		public const int WORK_TYPE = 1;
		public const int ALTERNATE_TYPE = 3;

		#endregion

		#region Attributes

		private List<int> _addressTypes;
		private List<AddressParseResult> _addresses;
		
		#endregion

		#region Properties

		/// <summary>
		/// Types each address can be registered as.
		/// </summary>
		public List<int> Types
		{
			get { return this._addressTypes; }
		}

		/// <summary>
		/// Each of the addresses.
		/// </summary>
		public List<AddressParseResult> Addresses
		{
			get { return this._addresses; }
		}

		#endregion

		#region Constructor

		public AddressBook()
		{
			_addresses = new List<AddressParseResult>();
			_addressTypes = new List<int>();
		}

		/// <summary>
		/// AddressBook(int, string) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="input">Adress to be parsed.</param>
		public AddressBook(int type, string input)
		{
			_addresses = new List<AddressParseResult>();
			_addressTypes = new List<int>();

			this.AddAddress(type, input);
		}

		/// <summary>
		/// AddressBook(string, string) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="input">Adress to be parsed.</param>
		public AddressBook(string type, string input)
		{
			_addresses = new List<AddressParseResult>();
			_addressTypes = new List<int>();

			this.AddAddress(type, input);
		}

		/// <summary>
		/// AddressBook(int, AddressParseResult) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="input">Adress to be parsed.</param>
		public AddressBook(int type, AddressParseResult input)
		{
			_addresses = new List<AddressParseResult>();
			_addressTypes = new List<int>();

			this.AddAddress(type, input);
		}

		/// <summary>
		/// AddressBook(string, AddressParseResult) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="input">Adress to be parsed.</param>
		public AddressBook(string type, AddressParseResult input)
		{
			_addresses = new List<AddressParseResult>();
			_addressTypes = new List<int>();

			this.AddAddress(type, input);
		}

		#endregion

		#region Methods

		/// <summary>
		/// GetAddress(int) allows users to retrieve a single address.
		/// </summary>
		/// <param name="type">Type of address required.</param>
		/// <returns>Returns an AddressParseResult object.</returns>
		public AddressParseResult GetAddress(int type)
		{
			AddressParseResult address = null;

			if (HasType(type))
			{
				return _addresses[_addressTypes.IndexOf(type)];
			}

			return address;
		}

		/// <summary>
		/// GetAddress(string) allows users to retrieve a single address.
		/// </summary>
		/// <param name="type">Type of address required.</param>
		/// <returns>Returns an AddressParseResult object.</returns>
		public AddressParseResult GetAddress(string type)
		{
			AddressParseResult address = null;

			if (HasType(type))
			{
				return _addresses[_addressTypes.IndexOf(GetType(type))];
			}

			return address;
		}

		/// <summary>
		/// AddAddress(string, string) takes a type (personal,
		/// work, alternate, etc.) and an address, logging them into
		/// the appropriate collections. This variation
		/// requires parsing the address before inserting it.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <param name="input">The address result.</param>
		/// <returns>Returns true on operation success. Returns false if the type already exists. Throws an index out of range exception if the two collections are null.</returns>
		public bool AddAddress(string type, string input)
		{
			if (HasType(GetType(type)) || input == null || input.Length <= 0)
			{
				return false;  // Eg. Already has a personal address on record; can't be overwritten. OR the address is null.
			}

			try
			{
				AddressReader parser = new AddressReader();
				AddressParseResult address = parser.ParseAddress(input);

				if (address == null) { return false; }

				// Add the address and address type to the appropriate collections.
				_addresses.Add(address);
				_addressTypes.Add(GetType(type));
				return true;
			}
			catch (IndexOutOfRangeException ioe)
			{
				throw ioe;
			}
		}

		/// <summary>
		/// AddAddress(int, string) takes a type (personal,
		/// work, alternate, etc.) and an address, logging them into
		/// the appropriate collections. This variation
		/// requires parsing the address before inserting it.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <param name="input">The address result.</param>
		/// <returns>Returns true on operation success. Returns false if the type already exists. Throws an index out of range exception if the two collections are null.</returns>
		public bool AddAddress(int type, string input)
		{
			if (HasType(GetValidType(type)) || input == null || input.Length <= 0)
			{
				return false;  // Eg. Already has a personal address on record; can't be overwritten. OR the address is null.
			}

			try
			{
				AddressReader parser = new AddressReader();
				AddressParseResult address = parser.ParseAddress(input);

				if(address == null) { return false; }

				// Add the address and address type to the appropriate collections.
				_addresses.Add(address);
				_addressTypes.Add(GetValidType(type));
				return true;
			}
			catch (IndexOutOfRangeException ioe)
			{
				throw ioe;
			}
		}

		/// <summary>
		/// AddAddress(string, AddressParseResult) takes a type (personal,
		/// work, alternate, etc.) and an address, logging them into
		/// the appropriate collections.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <param name="address">The address result.</param>
		/// <returns>Returns true on operation success. Returns false if the type already exists. Throws an index out of range exception if the two collections are null.</returns>
		public bool AddAddress(string type, AddressParseResult address)
		{
			if (HasType(GetType(type)) || address == null)
			{
				return false;  // Eg. Already has a personal address on record; can't be overwritten. OR the address is null.
			}

			try
			{
				// Add the address and address type to the appropriate collections.
				_addresses.Add(address);
				_addressTypes.Add(GetType(type));
				return true;
			}
			catch (IndexOutOfRangeException ioe)
			{
				throw ioe;
			}
		}

		/// <summary>
		/// AddAddress(int, AddressParseResult) takes a type (personal,
		/// work, alternate, etc.) and an address, logging them into
		/// the appropriate collections.
		/// </summary>
		/// <param name="type">Type to be searched for.</param>
		/// <param name="address">The address result.</param>
		/// <returns>Returns true on operation success. Returns false if the type already exists. Throws an index out of range exception if the two collections are null.</returns>
		public bool AddAddress(int type, AddressParseResult address)
		{
			if (HasType(GetValidType(type)) || address == null)
			{
				return false;  // Eg. Already has a personal address on record; can't be overwritten. OR the address is null.
			}

			try
			{
				// Add the address and address type to the appropriate collections.
				_addresses.Add(address);
				_addressTypes.Add(GetValidType(type));
				return true;
			}
			catch (IndexOutOfRangeException ioe)
			{
				throw ioe;
			}
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
			return _addressTypes.Contains(type);
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
			return _addressTypes.Contains(GetType(type));
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
