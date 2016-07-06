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
using AddressReader = AddressParser.AddressParser;
using Address = AddressParser.AddressParseResult;

namespace DocumentParser.DocumentLoading.Resume.Books
{
	/// <summary>
	/// AddressBook is a collection of Addresses
	/// via the IBook abstract class.
	/// </summary>
	public class AddressBook : IBook<Address>
	{
		#region Properties

		/// <summary>
		/// Addresses is a list of all the 
		/// Address objects being stored.
		/// </summary>
		public List<Address> Addresses
		{
			get { return this.Collection; }
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// GetFormattedAddress(string) returns a single formatted
		/// string containing the numbers alone, formatted
		/// in the US National method.
		/// </summary>
		/// <param name="input">Input string to be parsed.</param>
		/// <returns>Returns a formatted address.</returns>
		public static string GetFormattedAddress(string input)
		{
			if (String.IsNullOrEmpty(input.Trim()))
			{
				return "";
			}
			else
			{
				Address result = AddressBook.GetAddress(input.Trim());

				if (result == null)
				{
					return "";
				}
				else
				{
					return result.ToString();
				}
			}
		}

		/// <summary>
		/// GetAddress(string) returns a Address object
		/// </summary>
		/// <param name="input">Input string to be parsed.</param>
		/// <returns>Returns an Address object.</returns>
		public static Address GetAddress(string input)
		{
			if (String.IsNullOrEmpty(input)) { return null; }

			AddressReader addressUtility = new AddressReader();

			try
			{
				Address result = addressUtility.ParseAddress(input.Trim());
				return result;
			}
			catch (Exception e)
			{
				Program.Write("There was an error parsing this address.");
				Program.Write("The exception is as follows: " + e.Message);
				Program.Write(e.Source);
				Program.Write(e.StackTrace);
				return null;
			}
		}

		#endregion
		
		#region Constructors

		/// <summary>
		/// AddressBook() is an empty constructor
		/// that sets up the storage and collection
		/// objects for future use, as well as
		/// flagging the _empty flag with it's
		/// default value.
		/// </summary>
		public AddressBook() : base()
		{
		}

		/// <summary>
		/// AddressBook(int, string) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="address">Address to be parsed.</param>
		public AddressBook(int type, string address) 
			: base(type, address)
		{
		}

		/// <summary>
		/// AddressBook(string, string) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="address">Address to be parsed.</param>
		public AddressBook(string type, string address) 
			: base(type, address)
		{
		}

		/// <summary>
		/// AddressBook(int, Address) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="address">Address to be parsed.</param>
		public AddressBook(int type, Address address) 
			: base(type, address)
		{
		}

		/// <summary>
		/// AddressBook(string, Address) takes a type
		/// and an address.
		/// </summary>
		/// <param name="type">Type the address will be registered as.</param>
		/// <param name="address">Address to be parsed.</param>
		public AddressBook(string type, Address address)
			: base(type, address)
		{
		}

		#endregion

		#region Methods

		#region GetAsEntry Implementations
		
		/// <summary>
		/// GetAsEntry(string) returns the
		/// the Address that matches
		/// the string.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override Address GetAsEntry(string entry)
		{
			if (String.IsNullOrEmpty(entry.Trim()))
			{
				throw new ArgumentNullException("The entry value is invalid because it was null.");
			}
			else
			{
				try
				{
					AddressReader addressUtility = new AddressReader();
					Address address = addressUtility.ParseAddress(entry.Trim());

					// Check if it's possible.
					if (address == null)
					{
						throw new ArgumentNullException("There was an error processing the address.");
					}
					else
					// If it's possible and valid, check Dictionary reference status.
					{
						// Does the address, itself, already exist?
						if (_directory.ContainsValue(address) || _book.ContainsValue(address))
						{
							throw new ArgumentException("This address has already been recorded!");
						}
						else
						{

							// If all formms of validation have succeeded, safely pass the queried entry.
							return address;

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
		/// the Address that matches
		/// the number, if possible.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override Address GetAsEntry(int entry)
		{
			if (entry < 0)
			{
				throw new NotImplementedException();
			}
			else
			{
				return GetAsEntry(entry.ToString().Trim());
			}
		}

		#endregion

		#region Miscellaneous Methods

		/// <summary>
		/// GetAddress(int) allows users to retrieve a single address.
		/// </summary>
		/// <param name="type">Type of address required.</param>
		/// <returns>Returns an Address object.</returns>
		public Address GetAddress(int type)
		{
			return GetEntry(type);
		}

		#endregion

		#endregion
	}
}
