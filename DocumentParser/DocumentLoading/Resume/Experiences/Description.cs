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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume.Experiences
{
	// TODO: Add a summary for the class.
	/// <summary>
	/// Description is a class that manages
	/// a collection of "bullet" points via
	/// a recursive system.
	/// </summary>
	public class Description
	{

		/*
		 * Visual representation of the bullet point system.
		 * 
		 Level = (x)

		 Document {
		
		 (0) * Points to a list of bullets.
			(1)
				(2)
				(2)
			(1)
			(1)
				(2)
					(3)
		(0)
		 }

		A bullet point's main content is a string.
		A bullet contains a "level".
		A bullet can contain another "bullet" via level + 1.
		A bullet's next should point to the next bullet,
		embedded.
		A bullet cannot point to the next bullet on the
		previous level.
		 */

		// TODO: Add content for the class.
	}
}
