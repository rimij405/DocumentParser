﻿/*****************************************************************************
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
using PdfSharp.Pdf;
using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading
{
	public static class PdfSharpExtensions
	{
		public static IEnumerable<string> ExtractText(this PdfPage page)
		{
			var content = ContentReader.ReadContent(page);
			var text = ExtractText(content);
			return text;
		}

		public static IEnumerable<string> ExtractText(this CObject cObject)
		{
			if (cObject is COperator)
			{
				var cOperator = cObject as COperator;
				if (cOperator.OpCode.Name == OpCodeName.Tj.ToString() ||
					cOperator.OpCode.Name == OpCodeName.TJ.ToString())
				{
					foreach (var cOperand in cOperator.Operands)
					{
						foreach (var txt in ExtractText(cOperand))
						{
							yield return txt;
						}
					}
				}
			}
			else if (cObject is CSequence)
			{
				var cSequence = cObject as CSequence;
				foreach (var element in cSequence)
				{
					foreach (var txt in ExtractText(element))
					{
						yield return txt;
					}
				}
			}
			else if (cObject is CString)
			{
				var cString = cObject as CString;
				yield return cString.Value;
			}
		}

	}
}
