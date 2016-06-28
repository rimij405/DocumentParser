using DocumentParser.DocumentLoading.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading
{
	/// <summary>
	/// ResumeDTO is a Data Transfer Object.
	/// It contains no actionable functions,
	/// instead opting to be a simple
	/// data wrapper that holds appropriate
	/// data.
	/// </summary>
	public class ResumeDTO
	{
		/* Let's take the PDFObject and make it a Data Transfer Object,
		   taking notes from the idea of APO development.
		   For now, instead of naming it "PDFObject", we don't want to waste time:
		   Let's go straight to the problem and create the
		   ResumeDTO.*/

		#region Attributes

		// What does this file need to hold?

		/* This file needs:
		 * - Full name of the applicant.
		 * - Applicant's street address. <Prioritize residence if available.>
		 * - Applicant's phone number. <Prioritize mobile if available.>
		 * - Applicant's email address. <Prioritize personal if available.>
		 * - Work experience <Variable>
		 * - - Company <string>, Location of Employment <city, state, country>, Duration <Month, Year> - <Month, Year>
		 * - Education <Variable> / Certification <Variable, Optional>
		 * - - University <string>, Location of Study <city, state, country>, Duration <Month, Year> - <Month, Year>, Degree <BS in ______>, Certification <>
		 * - Other <Variable, Optional>
		 * - - Information goes here.
		 */

		private Name _fullName;
		private List<AddressBook> _streetAddresses;
		private List<Telephone> _phoneNumbers;
		private List<EmailAddress> _emailAddresses;
		Dictionary<string, List<Experience>> _experiences;
		private List<Experience> _workExperience;
		private List<Experience> _educationExperience;
		private List<Experience> _otherExperience;
		private string _other;
				
		#endregion

		#region Properties

		#endregion
	}
}
