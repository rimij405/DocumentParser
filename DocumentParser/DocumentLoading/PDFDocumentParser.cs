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
