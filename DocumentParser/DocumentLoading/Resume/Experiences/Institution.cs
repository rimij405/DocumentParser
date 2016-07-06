using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume.Experiences
{
	/// <summary>
	/// <para>Institution.cs represents
	/// organizations on the high
	/// level. It makes no distinction
	/// between companies or universities
	/// at this level.</para>
	/// 
	/// <para>Every institution has a title,
	/// list of opportunities, and a location.</para>
	/// 
	/// <para>This allows us to build "up"
	/// when we develop the following classes
	/// for Opportunity and Location in general.</para>
	/// 
	/// <para>We will define the specifity
	/// of the Institution implementation
	/// classes by uniquely customizing them
	/// for other purposes.</para>
	/// 
	/// <para>The advantage here is that we
	/// can work with BOTH education
	/// and work experiences on the high
	/// level while maintaining their
	/// functions on the low level.</para>
	/// </summary>
	public class Institution : IEnumerable
	{
		#region Attributes

		// Fields
		private string _title;
		private Location _loc;
		private List<Opportunity> _opp;

		// Flags
		private bool _empty = true;
		private bool _initialized = false;

		#endregion
		
		#region Properties

		/// <summary>
		/// Name returns the title
		/// of this particular institution.
		/// </summary>
		public string Name
		{
			get { return this._title; }
		}

		/// <summary>
		/// Location returns the location
		/// of this organization
		/// to the closest specifity
		/// known. <para>(City, State, Country),
		/// (City, State), (City), or
		/// just, (State, Country),
		/// are all possible depending
		/// on what information is known.</para>
		/// </summary>
		public string Location
		{
			get { return this._loc.ToString(); }
		}

		/// <summary>
		/// Opportunities is a collection
		/// of "experiences" whether they
		/// be Education degrees or 
		/// occupational duties.
		/// </summary>
		public List<Opportunity> Opportunities
		{
			get { return this._opp; }
		}

		#endregion
		
		#region Constructor / Initialization

		/// <summary>
		/// Institution() is an empty
		/// constructor that creates
		/// the basic groundwork
		/// by calling _init().
		/// </summary>
		public Institution()
		{
			_init();
		}

		/// <summary>
		/// Institution(string) is a
		/// constructor that assigns
		/// the name to the correct
		/// field on creation.
		/// </summary>
		/// <param name="name">Institution title.</param>
		public Institution(string name)
		{
			_init();
			this._title = name;
		}
		
		/// <summary>
		/// Institution(Location) is a
		/// constructor that assigns
		/// the location to the correct
		/// field on creation.
		/// </summary>
		/// <param name="location">Institution location.</param>
		public Institution(Location location)
		{
			_init();
			this._loc = location;
		}

		/// <summary>
		/// Institution(string, Location) is a
		/// constructor that assigns the
		/// name and the location to the correct
		/// field on creation.
		/// </summary>
		/// <param name="location">Institution location.</param>
		public Institution(string name, Location location)
		{
			_init();
			this._title = name;
			this._loc = location;
		}

		/// <summary>
		/// Institution(string, Location, List<Opportunity>) is a
		/// constructor that assigns the
		/// name and the location to the correct
		/// field on creation.
		/// </summary>
		/// <param name="location">Institution location.</param>
		public Institution(string name, Location location, List<Opportunity> opportunities)
		{
			_init();
			this._title = name;
			this._loc = location;
			AssignUnlessEmpty(opportunities);
		}

		protected virtual void _init()
		{
			this._title = "";
			this._loc = new Location();
			this._opp = new List<Opportunity>();
			this._empty = true;
			this._initialized = true;
		}

		#endregion

		#region Methods

		// TODO: Define some of the accessory methods.

		#region Service Methods

		/// <summary>
		/// Sorts Opportunities by 
		/// their date.
		/// </summary>
		public virtual void SortOpportunities()
		{
			// TODO: Implement SortOpportunities(). Description below.

			// This method should sort the List of opportunities by their date.
			// The earliest dates should be at the front of the list.

			throw new NotImplementedException("This method still needs to be implemented.");
		}

		#endregion


		#region IEnumerable Methods
			
		/// <summary>
		/// GetEnumerator() via
		/// the IEnumerable interface.
		/// </summary>
		/// <returns>Returns Opportunities.GetEnumerator()</returns>
		public IEnumerator<Opportunity> GetEnumerator()
		{
			return Opportunities.GetEnumerator();
		}
		
		/// <summary>
		/// GetEnumerator() via
		/// the IEnumerable interface.
		/// </summary>
		/// <returns>Returns Opportunities.GetEnumerator()</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion


		#region Accessor Methods

		/// <summary>
		/// CalculateTimeFrame() uses the existing list of opportunities
		/// to return a TimeFrame that can be used
		/// </summary>
		/// <returns>Returns a time frame object with the appropriate values.</returns>
		public TimeFrame CalculateTimeFrame()
		{
			DateTime earliest;
			DateTime latest;

			// TODO: Implement CalculateTimeFrame(); Description below.

			// TimeFrame needs to be created.
			// The time span needs to be calculated on the Institutional level here.
			// This means it takes the earliest time from the earliest Opportunity,
			// and the latest time from the latest opportunity.
			// Creating a range from that series of data.
			// This information also needs to be initialized so this method
			// need not be called multiple times on object creation: only when the first
			// opportunity or the last opportunity in the list change, does this need
			// to be recalculated.

			throw new NotImplementedException("TimeFrame has not been implemented as of yet, so that we can.");
		}

		#endregion


		#region Mutator Methods

		/// <summary>
		/// Overwrite the existing list of opportunities,
		/// unless, this input parameter is empty.
		/// </summary>
		/// <param name="opportunities">The opportunities to be added.</param>
		public void AssignUnlessEmpty(List<Opportunity> opportunities)
		{
			if(opportunities.Count > 0) { this._opp = opportunities; }
			// Sort

			if(this._opp.Count > 0) { this._empty = false; }
		}

		/// <summary>
		/// Append the existing opportunity to the list
		/// of opportunities.
		/// </summary>
		/// <param name="opportunity">The opportunity to be added.</param>
		public void AssignUnlessEmpty(Opportunity opportunity)
		{
			if (!opportunity.Empty) { this._opp.Add(opportunity); }
			// Sort

			if(this._opp.Count > 0) { this._empty = false; }
		}
		
		#endregion

		#endregion
	}
}
