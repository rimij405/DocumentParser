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
 ****************************************************************************/
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace DocumentParser.DocumentLoading.Resume
{
	public class EmailAddress
	{
		#region Constants

		public const string PERSONAL = "Personal Email";
		public const string ALTERNATE = "Alternate Email";
		public const string WORK = "Work Email";

		#endregion


		#region Attributes

		private Dictionary<string, MailAddress> _addresses;
		private MailAddress _personalAddress = null;
		private MailAddress _alternateAddress = null;
		private MailAddress _workAddress = null;

		#endregion

		#region Constructor

		/// <summary>
		/// EmailAddress(string, string) requires at least
		/// one set of a type and an email address, in order
		/// to be successfully constructed.
		/// 
		/// The dictionary can be utilized after construction.
		/// </summary>
		/// <param name="type">Type of email address: personal, work, or alternate.</param>
		/// <param name="address">The email address.</param>
		public EmailAddress(string type, string address)
		{
			_addresses = new Dictionary<string, MailAddress>();
			string referenceType = type;

			switch (type)
			{
				case PERSONAL:
					referenceType = PERSONAL;
					_personalAddress = new MailAddress(address);
					_addresses.Add(referenceType, _personalAddress);
					break;
				case WORK:
					referenceType = WORK;
					_workAddress = new MailAddress(address);
					_addresses.Add(referenceType, _workAddress);
					break;
				case ALTERNATE:
				default:
					referenceType = ALTERNATE;
					_alternateAddress = new MailAddress(address);
					_addresses.Add(referenceType, _alternateAddress);
					break;
			}
		}
		
		#endregion

		#region Methods

		/// <summary>
		/// AddEmail(string, string) takes a type of address
		/// and an email address. This allows a contact to have
		/// a maximum of 3 email addresses.
		/// </summary>
		/// <param name="type">Type of email address: personal, work, or alternate.</param>
		/// <param name="address">The email address.</param>
		/// <returns>Returns a true if successfully added; false if incorrectly added.</returns>
		public bool AddEmail(string type, string address)
		{
			if (!_addresses.ContainsKey(type))
			{
				string referenceType = type;

				if (type != PERSONAL && type != WORK && type != ALTERNATE) { return false; }

				switch (type)
				{
					case PERSONAL:
						referenceType = PERSONAL;
						_personalAddress = new MailAddress(address);
						_addresses.Add(referenceType, _personalAddress);
						return true;
					case WORK:
						referenceType = WORK;
						_workAddress = new MailAddress(address);
						_addresses.Add(referenceType, _workAddress);
						return true;
					case ALTERNATE:
					default:
						referenceType = ALTERNATE;
						_alternateAddress = new MailAddress(address);
						_addresses.Add(referenceType, _alternateAddress);
						return true;
				}
			}
			else
			{
				return false;
			}
			
		}

		#endregion
	}
}
