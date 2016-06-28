/*****************************************************************************
   * 
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
   **************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading
{
	public class PDFDocumentParser : IDocumentParser
	{
		// What does the PDF parser do?
		// First we need to support loading of a .pdf file.
		// Then, we need to read through the pdf file.
		// Then, we need to store the proper information inside of a "PDFObject" of our making.

		/* Let's take the PDFObject and make it a Data Transfer Object,
		   taking notes from the idea of APO development.
		   For now, instead of naming it "PDFObject", we don't want to waste time:
		   Let's go straight to the problem and create the
		   ResumeDTO.*/

		
		protected override bool LoadDocument(string path)
		{
			throw new NotImplementedException();
		}
	}
}
