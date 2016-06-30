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
using Email = System.Net.Mail.MailAddress;

namespace DocumentParser.DocumentLoading.Resume
{
	public class EmailBook : IBook<Email>
	{
		#region Properties

		/// <summary>
		/// Emails is a list of all the 
		/// Email objects being stored.
		/// </summary>
		public List<Email> Emails
		{
			get { return this.Collection; }
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// GetFormattedEmailAddress(string) returns a single formatted
		/// string containing a parsed email address, if possible.
		/// </summary>
		/// <param name="input">Input string to be parsed.</param>
		/// <returns>Returns a formatted email address.</returns>
		public static string GetFormattedEmail(string input)
		{
			if (String.IsNullOrEmpty(input.Trim()))
			{
				return "";
			}
			else
			{
				try
				{
					Email result = new Email(input.Trim());

					if (result == null)
					{
						return "";
					}
					else
					{
						return result.ToString();
					}					
				}
				catch (Exception e)
				{
					Program.Write("There was an error parsing this email.");
					Program.Write("The exception is as follows: " + e.Message);
					Program.Write(e.Source);
					Program.Write(e.StackTrace);
					return null;
				}
			}
		}

		/// <summary>
		/// GetEmail(string) returns a Email object
		/// </summary>
		/// <param name="input">Input string to be parsed.</param>
		/// <returns>Returns an Email object.</returns>
		public static Email GetEmail(string input)
		{
			if (String.IsNullOrEmpty(input)) { return null; }
			
			try
			{
				Email result = new Email(input.Trim());
				return result;
			}
			catch (Exception e)
			{
				Program.Write("There was an error parsing this email.");
				Program.Write("The exception is as follows: " + e.Message);
				Program.Write(e.Source);
				Program.Write(e.StackTrace);
				return null;
			}
		}


		#endregion

		#region Constructors

		/// <summary>
		/// EmailBook() is an empty constructor
		/// that sets up the storage and collection
		/// objects for future use, as well as
		/// flagging the _empty flag with it's
		/// default value.
		/// </summary>
		public EmailBook() : base()
		{
		}

		/// <summary>
		/// EmailBook(int, string) takes a type
		/// and an email.
		/// </summary>
		/// <param name="type">Type the email address will be registered as.</param>
		/// <param name="email">Email address to be parsed.</param>
		public EmailBook(int type, string email) : base(type, email)
		{
		}

		/// <summary>
		/// EmailBook(string, string) takes a type
		/// and an email.
		/// </summary>
		/// <param name="type">Type the email address will be registered as.</param>
		/// <param name="email">Email address to be parsed.</param>
		public EmailBook(string type, string email) : base(type, email)
		{
		}

		/// <summary>
		/// EmailBook(int, Email) takes a type
		/// and an email.
		/// </summary>
		/// <param name="type">Type the email address will be registered as.</param>
		/// <param name="email">Email address to be parsed.</param>
		public EmailBook(int type, Email email) : base(type, email)
		{
		}

		/// <summary>
		/// EmailBook(string, Email) takes a type
		/// and an email.
		/// </summary>
		/// <param name="type">Type the email address will be registered as.</param>
		/// <param name="email">Email address to be parsed.</param>
		public EmailBook(string type, Email email) : base(type, email)
		{
		}

		#endregion

		#region Methods

		#region GetAsEntry Implementations

		/// <summary>
		/// GetAsEntry(string) returns the
		/// the Email that matches
		/// the string.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override Email GetAsEntry(string entry)
		{
			if (String.IsNullOrEmpty(entry.Trim()))
			{
				throw new ArgumentNullException("The entry value is invalid because it was null.");
			}
			else
			{
				try
				{
					Email email = new Email(entry);

					// Check if it's possible.
					if (email == null)
					{
						throw new ArgumentNullException("There was an error processing the address.");
					}
					else
					// If it's possible and valid, check Dictionary reference status.
					{
						// Does the address, itself, already exist?
						if (_directory.ContainsValue(email) || _book.ContainsValue(email))
						{
							throw new ArgumentException("This address has already been recorded!");
						}
						else
						{

							// If all formms of validation have succeeded, safely pass the queried entry.
							return email;

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
		/// the Email that matches
		/// the number, if possible.
		/// </summary>
		/// <param name="entry">The entry being searched for.</param>
		/// <returns>Throws exceptions if the entry does not exist or cannot be parsed. Returns object otherwise.</returns>
		protected override Email GetAsEntry(int entry)
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
		/// GetEmail(int) allows users to retrieve a single email address.
		/// </summary>
		/// <param name="type">Type of email address required.</param>
		/// <returns>Returns an Email object.</returns>
		public Email GetEmail(int type)
		{
			return GetEntry(type);
		}

		#endregion

		#endregion
	}
}
