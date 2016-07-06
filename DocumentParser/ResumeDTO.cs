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

		private ContactSheet _contactSheet;
		private ExperienceList _experienceList;
		private bool _empty;

		#endregion

		#region Properties

		public ContactSheet ContactInformation
		{
			get { return this._contactSheet; }
		}

		public ExperienceList Experiences
		{
			get { return this._experienceList; }
		}
		
		public bool Empty
		{
			get { return this._empty; }
		}

		#endregion

		#region Constructor

		public ResumeDTO()
		{
			this._contactSheet = new ContactSheet();
			this._experienceList = new ExperienceList();
			this._empty = true;
		}

		public ResumeDTO(ContactSheet cs, ExperienceList el)
		{
			this._contactSheet = cs;
			this._experienceList = el;
			this._empty = false;
		}
		
		#endregion
	}
}
