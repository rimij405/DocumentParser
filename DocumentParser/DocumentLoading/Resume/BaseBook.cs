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

namespace DocumentParser.DocumentLoading.Resume
{
	/// <summary>
	/// BaseBook is a base class that identifies
	/// all classes that implement IBook,
	/// as of the same core type of object.
	/// </summary>
	public class BaseBook
	{
		/// <summary>
		/// Returns the BaseBook object,
		/// explicitly casted as a BaseBook.
		/// </summary>
		/// <returns>Returns the object as a BaseBook.</returns>
		public BaseBook GetSelf()
		{
			return this as BaseBook;
		}

		public Name GetAsName()
		{
			return this as Name;
		}

		public AddressBook GetAsAddressBook()
		{
			return this as AddressBook;
		}

		public PhoneBook GetAsPhoneBook()
		{
			return this as PhoneBook;
		}

		public EmailBook GetAsEmailBook()
		{
			return this as EmailBook;
		}

		public bool IsEmpty()
		{
			if (this is Name)
			{
				return this.GetAsName().Empty;
			}

			if (this is AddressBook)
			{
				return this.GetAsAddressBook().Empty;
			}

			if (this is PhoneBook)
			{
				return this.GetAsPhoneBook().Empty;
			}

			if (this is EmailBook)
			{
				return this.GetAsEmailBook().Empty;
			}

			if (this is BaseBook)
			{
				return true;
			}

			return true;
		}

		public static BaseBook GetSelf(object obj)
		{
			if(obj is BaseBook) { return obj as BaseBook; }
			if(obj is Name || obj is AddressBook || obj is EmailBook || obj is PhoneBook) { return (BaseBook)obj; }
			else { throw new ArgumentException("This is not a BaseBook object."); }
		}
	}
}
