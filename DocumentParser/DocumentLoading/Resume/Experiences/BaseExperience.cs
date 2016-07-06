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
	/// <summary>
	/// BaseExperience is used by all
	/// other experiences as a form of
	/// grounding.
	/// 
	/// <para>An experience is really
	/// just a collection of, "opportunities,"
	/// at a particular institution.</para>
	/// </summary>
	public class BaseExperience : IExperience
	{
		#region Attributes

		// // Private attributes
		/// <summary>
		/// _institute is an Institution.
		/// 
		/// Institution.cs represents
		/// organizations on the high
		/// level. It makes no distinction
		/// between companies or universities
		/// at this level.
		/// 
		/// Whereas Organizations will
		/// contain a list of jobs, 
		/// Universities will contain
		/// a series of degrees, and
		/// other academic related material.
		/// 
		/// Every institution has a title
		/// and a location.
		/// </summary>
		private Institution _institute;

		/// <summary>
		/// _timeFrame is the Time Frame.
		/// 
		/// TimeFrame is a particular
		/// length of time and can be
		/// printed out in a series of 
		/// ways, as the situation demands.
		/// 
		/// This in particular should be
		/// storage for the timeframe
		/// from the stored institution.
		/// </summary>
		private TimeFrame _timeFrame;

		// // Private flags
		private bool _empty = true;
		private bool _initialized = false;

		#endregion

		#region Properties

		/// <summary>
		/// Institute returns <seealso cref="_institute"/>, or, if it is null, throws a NullReferenceException. 
		/// </summary>
		public Institution Institute
		{
			get
			{
				if (this._institute == null)
				{
					throw new NullReferenceException("This experience is missing an institution.");
				}
				else
				{
					return this._institute;
				}
			}
		}

		/// <summary>
		/// <para>Returns <seealso cref="_timeFrame"/>: TimeFrame is a particular
		/// length of time and can be
		/// printed out in a series of 
		/// ways, as the situation demands.</para>
		/// 
		/// <para>If it is null, it returns a NullReferenceException.</para>
		/// </summary>
		public TimeFrame Time
		{
			get
			{
				if (TimeFrame.IsNullOrEmpty(_timeFrame))
				{
					throw new NullReferenceException("This experience doesn't have a timeframe assigned to it yet.");
				}
				else
				{
					return this._timeFrame;
				}
			}
		}

		/// <summary>
		/// Empty returns the _empty boolean flag.
		/// </summary>
		public bool Empty
		{
			get
			{
				return this._empty;
			}
		}

		#endregion

		#region Constructor / Initialization

		// // Empty constructor
		public BaseExperience()
		{
			_init();
		}

		// // Recommended constructor.
		public BaseExperience(Institution ins)
		{
			_init();
			this._institute = ins;
			this._timeFrame = ins.CalculateTimeFrame();
		}
		
		// // Initialization method.
		protected virtual void _init()
		{

			_empty = true;
			_institute = new Institution();
			_timeFrame = new TimeFrame();

			_initialized = true;
		}

		#endregion
	}
}
