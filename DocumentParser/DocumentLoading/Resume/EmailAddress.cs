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
