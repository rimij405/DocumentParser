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
	/// Location stores an
	/// Institution's location
	/// in terms of a:
	/// 
	/// City, State, and Country.
	/// 
	/// <para>It can print a variation of
	/// (City), (City, State),
	/// (City, State, Country),
	/// (State, Country),
	/// (State), or (Country).</para>
	/// </summary>
	public class Location
	{
		#region Constants

		public const string CITY = "City";
		public const string STATE = "State";
		public const string COUNTRY = "Country";

		public static LocationGroup CITIES;
		public static LocationGroup STATES;
		public static LocationGroup COUNTRIES;
		private static bool _Initialized = false;

		#endregion

		#region Attributes

		// Fields
		private LocationGroup _cities;
		private LocationGroup _states;
		private LocationGroup _countries;
		private string _city;
		private string _state;
		private string _country;
		private bool _empty;

		#endregion
		
		#region Properties

		public LocationGroup Cities
		{
			get { return this._cities; }
		}

		public LocationGroup States
		{
			get { return this._states; }
		}

		public LocationGroup Countries
		{
			get { return this._countries; }
		}

		public bool Empty
		{
			get { return this._empty; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Location() is the basic,
		/// empty constructor.
		/// </summary>
		public Location()
		{
			_init();
		}

		/// <summary>
		/// Location(string, string, string)
		/// is the basic constructor.
		/// </summary>
		/// <param name="city">City to set.</param>
		/// <param name="state">State to set.</param>
		/// <param name="country">Country to set.</param>
		public Location(string city, string state, string country)
		{
			_init();
			SetCity(city);
			SetState(state);
			SetCountry(country);
		}

		/// <summary>
		/// _init() is called by
		/// the above constructor(s)
		/// in order to initialize the
		/// program and add the appropriate
		/// values to the LocationGroup
		/// default.
		/// </summary>
		public void _init()
		{
			_empty = true;
			_cities = new LocationGroup();
			_states = new LocationGroup();
			_countries = new LocationGroup();
			_city = "";
			_state = "";
			_country = "";

			if (!_Initialized)
			{
				// Build the defaults if they haven't been
				// built already.
				Location.BuildDefaults();

				_Initialized = true;
			}

			_cities.SetDefault(CITIES);
			_states.SetDefault(STATES);
			_countries.SetDefault(COUNTRIES);
		}

		/// <summary>
		/// BuildDefaults() builds the
		/// defualt location groups
		/// for reassignment later on.
		/// </summary>
		private static void BuildDefaults()
		{
			#region Build Countries

			// Countries.
			COUNTRIES = new LocationGroup();
			COUNTRIES.Add("AF", "Afghanistan");
			COUNTRIES.Add("AL", "Albania");
			COUNTRIES.Add("DZ", "Algeria");
			COUNTRIES.Add("AS", "American Samoa");
			COUNTRIES.Add("AD", "Andorra");
			COUNTRIES.Add("AO", "Angola");
			COUNTRIES.Add("AI", "Anguilla");
			COUNTRIES.Add("AQ", "Antarctica");
			COUNTRIES.Add("AG", "Antigua and Barbuda");
			COUNTRIES.Add("AR", "Argentina");
			COUNTRIES.Add("AM", "Armenia");
			COUNTRIES.Add("AW", "Aruba");
			COUNTRIES.Add("AU", "Australia");
			COUNTRIES.Add("AT", "Austria");
			COUNTRIES.Add("AZ", "Azerbaijan");
			COUNTRIES.Add("BS", "Bahamas");
			COUNTRIES.Add("BH", "Bahrain");
			COUNTRIES.Add("BD", "Bangladesh");
			COUNTRIES.Add("BB", "Barbados");
			COUNTRIES.Add("BY", "Belarus");
			COUNTRIES.Add("BE", "Belgium");
			COUNTRIES.Add("BZ", "Belize");
			COUNTRIES.Add("BJ", "Benin");
			COUNTRIES.Add("BM", "Bermuda");
			COUNTRIES.Add("BT", "Bhutan");
			COUNTRIES.Add("BO", "Bolivia");
			COUNTRIES.Add("BA", "Bosnia and Herzegovina");
			COUNTRIES.Add("BW", "Botswana");
			COUNTRIES.Add("BV", "Bouvet Island");
			COUNTRIES.Add("BR", "Brazil");
			COUNTRIES.Add("IO", "British Indian Ocean Territory");
			COUNTRIES.Add("BN", "Brunei Darussalam");
			COUNTRIES.Add("BG", "Bulgaria");
			COUNTRIES.Add("BF", "Burkina Faso");
			COUNTRIES.Add("BI", "Burundi");
			COUNTRIES.Add("KH", "Cambodia");
			COUNTRIES.Add("CM", "Cameroon");
			COUNTRIES.Add("CA", "Canada");
			COUNTRIES.Add("CV", "Cape Verde");
			COUNTRIES.Add("KY", "Cayman Islands");
			COUNTRIES.Add("CF", "Central African Republic");
			COUNTRIES.Add("TD", "Chad");
			COUNTRIES.Add("CL", "Chile");
			COUNTRIES.Add("CN", "China");
			COUNTRIES.Add("CX", "Christmas Island");
			COUNTRIES.Add("CC", "Cocos (Keeling) Islands");
			COUNTRIES.Add("CO", "Colombia");
			COUNTRIES.Add("KM", "Comoros");
			COUNTRIES.Add("CG", "Congo");
			COUNTRIES.Add("CD", "Congo, the Democratic Republic of the");
			COUNTRIES.Add("CK", "Cook Islands");
			COUNTRIES.Add("CR", "Costa Rica");
			COUNTRIES.Add("CI", "Cote D'Ivoire");
			COUNTRIES.Add("HR", "Croatia");
			COUNTRIES.Add("CU", "Cuba");
			COUNTRIES.Add("CY", "Cyprus");
			COUNTRIES.Add("CZ", "Czech Republic");
			COUNTRIES.Add("DK", "Denmark");
			COUNTRIES.Add("DJ", "Djibouti");
			COUNTRIES.Add("DM", "Dominica");
			COUNTRIES.Add("DO", "Dominican Republic");
			COUNTRIES.Add("EC", "Ecuador");
			COUNTRIES.Add("EG", "Egypt");
			COUNTRIES.Add("SV", "El Salvador");
			COUNTRIES.Add("GQ", "Equatorial Guinea");
			COUNTRIES.Add("ER", "Eritrea");
			COUNTRIES.Add("EE", "Estonia");
			COUNTRIES.Add("ET", "Ethiopia");
			COUNTRIES.Add("FK", "Falkland Islands");
			COUNTRIES.Add("FO", "Faroe Islands");
			COUNTRIES.Add("FJ", "Fiji");
			COUNTRIES.Add("FI", "Finland");
			COUNTRIES.Add("FR", "France");
			COUNTRIES.Add("GF", "French Guiana");
			COUNTRIES.Add("PF", "French Polynesia");
			COUNTRIES.Add("TF", "French Southern Territories");
			COUNTRIES.Add("GA", "Gabon");
			COUNTRIES.Add("GM", "Gambia");
			COUNTRIES.Add("GE", "Georgia");
			COUNTRIES.Add("DE", "Germany");
			COUNTRIES.Add("GH", "Ghana");
			COUNTRIES.Add("GI", "Gibraltar");
			COUNTRIES.Add("GR", "Greece");
			COUNTRIES.Add("GL", "Greenland");
			COUNTRIES.Add("GD", "Grenada");
			COUNTRIES.Add("GP", "Guadeloupe");
			COUNTRIES.Add("GU", "Guam");
			COUNTRIES.Add("GT", "Guatemala");
			COUNTRIES.Add("GN", "Guinea");
			COUNTRIES.Add("GW", "Guinea-Bissau");
			COUNTRIES.Add("GY", "Guyana");
			COUNTRIES.Add("HT", "Haiti");
			COUNTRIES.Add("HM", "Honduras");
			COUNTRIES.Add("VA", "Vatican City State");
			COUNTRIES.Add("HN", "Honduras");
			COUNTRIES.Add("HK", "Hong Kong");
			COUNTRIES.Add("HU", "Hungary");
			COUNTRIES.Add("IS", "Iceland");
			COUNTRIES.Add("IN", "India");
			COUNTRIES.Add("ID", "Indonesia");
			COUNTRIES.Add("IR", "Iran");
			COUNTRIES.Add("IQ", "Iraq");
			COUNTRIES.Add("IE", "Ireland");
			COUNTRIES.Add("IL", "Isreal");
			COUNTRIES.Add("IT", "Italy");
			COUNTRIES.Add("JM", "Jamaica");
			COUNTRIES.Add("JP", "Japan");
			COUNTRIES.Add("JO", "Jordan");
			COUNTRIES.Add("KZ", "Kazakhstan");
			COUNTRIES.Add("KE", "Kenya");
			COUNTRIES.Add("KI", "Kiribati");
			COUNTRIES.Add("KP", "North Korea");
			COUNTRIES.Add("KR", "South Korea");
			COUNTRIES.Add("KW", "Kuwait");
			COUNTRIES.Add("KG", "Kyrgyzstan");
			COUNTRIES.Add("LA", "Laos");
			COUNTRIES.Add("LV", "Latvia");
			COUNTRIES.Add("LB", "Lebanon");
			COUNTRIES.Add("LS", "Lesotho");
			COUNTRIES.Add("LR", "Liberia");
			COUNTRIES.Add("LY", "Libya");
			COUNTRIES.Add("LI", "Liechtenstein");
			COUNTRIES.Add("LT", "Lithuania");
			COUNTRIES.Add("LU", "Luxembourg");
			COUNTRIES.Add("MO", "Macao");
			COUNTRIES.Add("MK", "Macedonia");
			COUNTRIES.Add("MG", "Madagascar");
			COUNTRIES.Add("MW", "Malawi");
			COUNTRIES.Add("MY", "Malaysia");
			COUNTRIES.Add("MV", "Maldives");
			COUNTRIES.Add("ML", "Mali");
			COUNTRIES.Add("MT", "Malta");
			COUNTRIES.Add("MH", "Marshall Islands");
			COUNTRIES.Add("MQ", "Martinique");
			COUNTRIES.Add("MR", "Mauritania");
			COUNTRIES.Add("MU", "Mauritius");
			COUNTRIES.Add("YT", "Mayotte");
			COUNTRIES.Add("MX", "Mexico");
			COUNTRIES.Add("FM", "Micronesia");
			COUNTRIES.Add("MD", "Moldava");
			COUNTRIES.Add("MC", "Monaco");
			COUNTRIES.Add("MN", "Mongolia");
			COUNTRIES.Add("MS", "Montserrat");
			COUNTRIES.Add("MA", "Morocco");
			COUNTRIES.Add("MZ", "Mozambique");
			COUNTRIES.Add("MM", "Myanmar");
			COUNTRIES.Add("NA", "Nambia");
			COUNTRIES.Add("NR", "Nauru");
			COUNTRIES.Add("NP", "Nepal");
			COUNTRIES.Add("NL", "Netherlands");
			COUNTRIES.Add("AN", "Netherlands Antilles");
			COUNTRIES.Add("NC", "New Caledonia");
			COUNTRIES.Add("NZ", "New Zealand");
			COUNTRIES.Add("NI", "Nicaragua");
			COUNTRIES.Add("NE", "Niger");
			COUNTRIES.Add("NG", "Nigeria");
			COUNTRIES.Add("NU", "Niue");
			COUNTRIES.Add("NF", "Norfolk Island");
			COUNTRIES.Add("MP", "Northern Mariana Islands");
			COUNTRIES.Add("NO", "Norway");
			COUNTRIES.Add("OM", "Oman");
			COUNTRIES.Add("PK", "Pakistan");
			COUNTRIES.Add("PW", "Palau");
			COUNTRIES.Add("PS", "Palestine");
			COUNTRIES.Add("PA", "Panama");
			COUNTRIES.Add("PG", "Papua New Guinea");
			COUNTRIES.Add("PY", "Paraguay");
			COUNTRIES.Add("PE", "Peru");
			COUNTRIES.Add("PH", "Philippines");
			COUNTRIES.Add("PN", "Pitcairn");
			COUNTRIES.Add("PL", "Poland");
			COUNTRIES.Add("PT", "Portugal");
			COUNTRIES.Add("PR", "Puerto Rico");
			COUNTRIES.Add("QA", "Qatar");
			COUNTRIES.Add("RE", "Reunion");
			COUNTRIES.Add("RO", "Romania");
			COUNTRIES.Add("RU", "Russian Federation");
			COUNTRIES.Add("RW", "Rwanda");
			COUNTRIES.Add("SH", "Saint Helena");
			COUNTRIES.Add("KN", "Saint Kitts and Nevis");
			COUNTRIES.Add("LC", "Saint Lucia");
			COUNTRIES.Add("PM", "Saint Pierre and Miquelon");
			COUNTRIES.Add("VC", "Saint Vincent & The Grenadines");
			COUNTRIES.Add("WS", "Samoa");
			COUNTRIES.Add("SM", "San Marino");
			COUNTRIES.Add("ST", "Sao Tome / Principe");
			COUNTRIES.Add("SA", "Saudi Arabia");
			COUNTRIES.Add("SN", "Senegal");
			COUNTRIES.Add("SC", "Seychelles");
			COUNTRIES.Add("SL", "Sierra Leone");
			COUNTRIES.Add("SG", "Singapore");
			COUNTRIES.Add("SK", "Slovakia");
			COUNTRIES.Add("SI", "Slovenia");
			COUNTRIES.Add("SB", "Solomon Islands");
			COUNTRIES.Add("SO", "Somalia");
			COUNTRIES.Add("ZA", "South Africa");
			COUNTRIES.Add("GS", "South Georgia");
			COUNTRIES.Add("ES", "Spain");
			COUNTRIES.Add("LK", "Sri Lanka");
			COUNTRIES.Add("SD", "Sudan");
			COUNTRIES.Add("SR", "Suriname");
			COUNTRIES.Add("SJ", "Svalbard and Jan Mayen");
			COUNTRIES.Add("SZ", "Swaziland");
			COUNTRIES.Add("SE", "Sweden");
			COUNTRIES.Add("CH", "Switzerland");
			COUNTRIES.Add("SY", "Syria");
			COUNTRIES.Add("TW", "Taiwan");
			COUNTRIES.Add("TJ", "Tajikistan");
			COUNTRIES.Add("TZ", "Tanzania");
			COUNTRIES.Add("TH", "Thailand");
			COUNTRIES.Add("TL", "Timor-Leste");
			COUNTRIES.Add("TG", "Togo");
			COUNTRIES.Add("TK", "Tokelau");
			COUNTRIES.Add("TO", "Tonga");
			COUNTRIES.Add("TT", "Trinidad and Tobago");
			COUNTRIES.Add("TN", "Tunisia");
			COUNTRIES.Add("TR", "Turkey");
			COUNTRIES.Add("TM", "Turkmenistan");
			COUNTRIES.Add("TC", "Turks and Caicos Islands");
			COUNTRIES.Add("TV", "Tuvalu");
			COUNTRIES.Add("UG", "Uganda");
			COUNTRIES.Add("UA", "Ukraine");
			COUNTRIES.Add("AE", "United Arab Emirates");
			COUNTRIES.Add("GB", "United Kingdom");
			COUNTRIES.Add("US", "United States");
			COUNTRIES.Add("UM", "United States Minor Outlying Minor Islands");
			COUNTRIES.Add("UY", "Uruguay");
			COUNTRIES.Add("UZ", "Uzbekistan");
			COUNTRIES.Add("VU", "Vanuatu");
			COUNTRIES.Add("VE", "Venezuela");
			COUNTRIES.Add("VN", "Viet Nam");
			COUNTRIES.Add("VG", "Virgin Islands (British)");
			COUNTRIES.Add("VI", "Virgin Islands (USA)");
			COUNTRIES.Add("WF", "Wallis and Futuna Islands");
			COUNTRIES.Add("EH", "Western Sahara");
			COUNTRIES.Add("YE", "Yemen");
			COUNTRIES.Add("ZM", "Zambia");
			COUNTRIES.Add("ZW", "Zimbabwe");
			
			#endregion

			#region Build States

			// States
			STATES = new LocationGroup();
			STATES.Add("AL", "Alabama");
			STATES.Add("AK", "Alaska");
			STATES.Add("AZ", "Arizona");
			STATES.Add("AR", "Arkansas");
			STATES.Add("CA", "California");
			STATES.Add("CO", "Colorado");
			STATES.Add("CT", "Connecticut");
			STATES.Add("DE", "Delaware");
			STATES.Add("DC", "District of Columbia");
			STATES.Add("FL", "Florida");
			STATES.Add("GA", "Georgia");
			STATES.Add("HI", "Hawaii");
			STATES.Add("ID", "Idaho");
			STATES.Add("IL", "Illinois");
			STATES.Add("IN", "Indiana");
			STATES.Add("IA", "Iowa");
			STATES.Add("KS", "Kansas");
			STATES.Add("KY", "Kentucky");
			STATES.Add("LA", "Louisiana");
			STATES.Add("ME", "Maine");
			STATES.Add("MD", "Maryland");
			STATES.Add("MA", "Massachusetts");
			STATES.Add("MI", "Michigan");
			STATES.Add("MN", "Minnesota");
			STATES.Add("MS", "Mississippi");
			STATES.Add("MO", "Missouri");
			STATES.Add("MT", "Montana");
			STATES.Add("NE", "Nebraska");
			STATES.Add("NV", "Nevada");
			STATES.Add("NH", "New Hampshire");
			STATES.Add("NJ", "New Jersey");
			STATES.Add("NM", "New Mexico");
			STATES.Add("NY", "New York");
			STATES.Add("NC", "North Carolina");
			STATES.Add("ND", "North Dakota");
			STATES.Add("OH", "Ohio");
			STATES.Add("OK", "Oklahoma");
			STATES.Add("OR", "Oregon");
			STATES.Add("PA", "Pennsylvania");
			STATES.Add("RI", "Rhode Island");
			STATES.Add("SC", "South Carolina");
			STATES.Add("SD", "South Dakota");
			STATES.Add("TN", "Tennessee");
			STATES.Add("TX", "Texas");
			STATES.Add("UT", "Utah");
			STATES.Add("VT", "Vermont");
			STATES.Add("VA", "Virginia");
			STATES.Add("WA", "Washington");
			STATES.Add("WV", "West Virginia");
			STATES.Add("WI", "Wisconsin");
			STATES.Add("WY", "Wyoming");

			#endregion

			#region Build Cities

			// Countries
			CITIES = new LocationGroup();
			CITIES.Add("AAR", "Aarhus");
			CITIES.Add("ABZ", "Aberdeen");
			CITIES.Add("ABJ", "Abidjan");
			CITIES.Add("AUH", "Abu Dhabi");
			CITIES.Add("ACC", "Accra");
			CITIES.Add("ACA", "Acapulco");
			CITIES.Add("ADD", "Addis Ababa");
			CITIES.Add("ADL", "Adelaide");
			CITIES.Add("ADE", "Aden");
			CITIES.Add("AGA", "Agadir");
			CITIES.Add("AJA", "Ajaccio");
			CITIES.Add("CAK", "Akron Canton");
			CITIES.Add("ALB", "Albany");
			CITIES.Add("ABQ", "Albuquerque");
			CITIES.Add("AHO", "Alghero");
			CITIES.Add("ALG", "Algiers");
			CITIES.Add("AHU", "Al Hoceima");
			CITIES.Add("ALC", "Alicante");
			CITIES.Add("ASP", "Alice Springs");
			CITIES.Add("LEI", "Almeria");
			CITIES.Add("AMM", "Amman");
			CITIES.Add("AMS", "Amritsa");
			CITIES.Add("ANC", "Anchorage");
			CITIES.Add("ANK", "Ankara");
			CITIES.Add("ESB", "Esenboga");
			CITIES.Add("AAE", "Annaba");
			CITIES.Add("ANU", "Antigua");
			CITIES.Add("ANR", "Antwerp");
			CITIES.Add("ARI", "Arica");
			CITIES.Add("AUA", "Aruba");
			CITIES.Add("ASM", "Asmara");
			CITIES.Add("ASU", "Asuncion");
			CITIES.Add("ATH", "Athens");
			CITIES.Add("ATL", "Atlanta");
			CITIES.Add("AKL", "Auckland");
			CITIES.Add("BWN", "Bandar Seri Begawan");
			CITIES.Add("BGW", "Baghdad");
			CITIES.Add("BAG", "Baguio");
			CITIES.Add("BAH", "Bahrain");
			CITIES.Add("BWI", "Baltimore");
			CITIES.Add("BKO", "Bamako");
			CITIES.Add("BLR", "Bangalore");
			CITIES.Add("BKK", "Bangkok");
			CITIES.Add("BGF", "Bangui");
			CITIES.Add("BDJ", "Banjarmasin-Borneo");
			CITIES.Add("BJL", "Banjul");
			CITIES.Add("BGI", "Barbados");
			CITIES.Add("BCN", "Barcelona");
			CITIES.Add("BRI", "Bari");
			CITIES.Add("BRR", "Barra");
			CITIES.Add("BAQ", "Barranquilla");
			CITIES.Add("BSL", "Basel");
			CITIES.Add("BIA", "Bastia");
			CITIES.Add("BVA", "Beauvais");
			CITIES.Add("BEW", "Beira");
			CITIES.Add("BEY", "Beirut");
			CITIES.Add("BEL", "Belem");
			CITIES.Add("BFS", "Belfast");
			CITIES.Add("BEG", "Belgrade");
			CITIES.Add("BHZ", "Belo Horizonte");
			CITIES.Add("BEB", "Benbecula");
			CITIES.Add("BEN", "Benghazi");
			CITIES.Add("BGO", "Bergen");
			CITIES.Add("BER", "Berlin");
			CITIES.Add("TXL", "Tegel");
			CITIES.Add("THF", "Templehof");
			CITIES.Add("BDA", "Bermuda");
			CITIES.Add("BRN", "Berne");
			CITIES.Add("BIQ", "Biarritz");
			CITIES.Add("BIO", "Bilbao");
			CITIES.Add("BHX", "Birmingham UK");
			CITIES.Add("BHM", "Birmingham USA");
			CITIES.Add("BXO", "Bissau");
			CITIES.Add("BLZ", "Blantyre");
			CITIES.Add("BOO", "Bodo");
			CITIES.Add("BOG", "Bogota");
			CITIES.Add("BLQ", "Bologna");
			CITIES.Add("BOD", "Bordeaux");
			CITIES.Add("BOS", "Boston");
			CITIES.Add("BOH", "Bournemouth");
			// http://globalinkllc.com/useful-info/weblinks/codes-of-abbreviations-to-airportscities-codes/ //
			// TODO: Finish adding the 3-Letter codes for all global cities.
			// TODO: Implement methods that allow us to go from City to Country, easily. LocationGroups?

			#endregion
		}

		#endregion

		#region Methods

		#region Mutator Methods

		/// <summary>
		/// SetCity(string) sets the location
		/// group to the appropriate values.
		/// </summary>
		/// <param name="entry">Entry to test input.</param>
		/// <returns>Returns true if successfully input.</returns>
		public bool SetCity(string entry)
		{
			if (String.IsNullOrEmpty(entry))
			{
				return false;
			}
			else
			{
				// Set the attribute.
				_city = entry;

				// Get the appropriate abbreviation and location from the location group.
				string testAb = _cities.GetAbbreviation(entry);
				string testLoc = _cities.GetLocation(testAb);

				// If it's a new one.
				if (testLoc.ToLower() != entry.ToLower())
				{
					string newAb = testAb;

					do
					{
						int count = 0;
						newAb = entry.Substring(0, testAb.Length + count);
						count++;
					} while (_cities.ContainsAbbreviation(newAb));

					_cities.Add(newAb, entry);
					_empty = false;
					return true;
				}
				else
				{
					// If it's an old entry.
					return false; // Entry already set.
				}
			}
		}

		/// <summary>
		/// SetState(string) sets the location
		/// group to the appropriate values.
		/// </summary>
		/// <param name="entry">Entry to test input.</param>
		/// <returns>Returns true if successfully input.</returns>
		public bool SetState(string entry)
		{
			if (String.IsNullOrEmpty(entry))
			{
				return false;
			}
			else
			{
				// Set the attribute.
				_state = entry;

				// Get the appropriate abbreviation and location from the location group.
				string testAb = _states.GetAbbreviation(entry);
				string testLoc = _states.GetLocation(testAb);

				// If it's a new one.
				if (testLoc.ToLower() != entry.ToLower())
				{
					string newAb = testAb;

					do
					{
						int count = 0;
						newAb = entry.Substring(0, testAb.Length + count);
						count++;
					} while (_states.ContainsAbbreviation(newAb));

					_states.Add(newAb, entry);
					_empty = false;
					return true;
				}
				else
				{
					// If it's an old entry.
					return false; // Entry already set.
				}
			}
		}

		/// <summary>
		/// SetCountry(string) sets the location
		/// group to the appropriate values.
		/// </summary>
		/// <param name="entry">Entry to test input.</param>
		/// <returns>Returns true if successfully input.</returns>
		public bool SetCountry(string entry)
		{
			if (String.IsNullOrEmpty(entry))
			{
				return false;
			}
			else
			{
				// Set the attribute.
				_country = entry;

				// Get the appropriate abbreviation and location from the location group.
				string testAb = _countries.GetAbbreviation(entry);
				string testLoc = _countries.GetLocation(testAb);

				// If it's a new one.
				if (testLoc.ToLower() != entry.ToLower())
				{
					string newAb = testAb;

					do
					{
						int count = 0;
						newAb = entry.Substring(0, testAb.Length + count);
						count++;
					} while (_countries.ContainsAbbreviation(newAb));

					_countries.Add(newAb, entry);
					_empty = false;
					return true;
				}
				else
				{
					// If it's an old entry.
					return false; // Entry already set.
				}
			}
		}

		#endregion

		#region Accessor Methods

		/// <summary>
		/// City returns the city
		/// as a string.
		/// </summary>
		/// <returns>Returns the appropriate string.</returns>
		public string City(bool abbreviation)
		{
			if (abbreviation)
			{
				return _cities.Abbreviation(_city);
			}
			else
			{
				return _cities.Location(_cities.Abbreviation(_city));
			}
		}

		/// <summary>
		/// State returns the state
		/// as a string.
		/// </summary>
		/// <returns>Returns the appropriate string.</returns>
		public string State(bool abbreviation)
		{
			if (abbreviation)
			{
				return _states.Abbreviation(_state);
			}
			else
			{
				return _states.Location(_states.Abbreviation(_state));
			}
		}

		/// <summary>
		/// Country returns the country
		/// as a string.
		/// </summary>
		/// <returns>Returns the appropriate string.</returns>
		public string Country(bool abbreviation)
		{
			if (abbreviation)
			{
				return _countries.Abbreviation(_country);
			}
			else
			{
				return _countries.Location(_countries.Abbreviation(_country));
			}
		}


		/// <summary>
		/// ToString() override method.
		/// </summary>
		/// <returns>Returns the city, state, country string.</returns>
		public override string ToString()
		{
			string response = "";
			bool comma = false;

			if (!String.IsNullOrEmpty(_city.Trim()))
			{
				response += City(false);
				comma = true;
			}
			
			if (!String.IsNullOrEmpty(_state.Trim()))
			{
				if (comma) { response += ", "; }
				response += State(false) + ", ";
				comma = true;
			}


			if (!String.IsNullOrEmpty(_country.Trim()))
			{
				if (comma) { response += ", "; }
				response += Country(false);
				comma = false;
			}

			return response;
		}

		#endregion

		#region Capitalize

		/// <summary>
		/// Capitalize takes a string
		/// and returns an array
		/// of the entire string
		/// being capitalized.
		/// with Title case.
		/// </summary>
		/// <param name="input">Input to be capitalized.</param>
		/// <returns>Returns capitalized string array.</returns>
		public static string[] Capitalize(string input)
		{
			string[] words = input.Split(' ');
			string[] response = new string[words.Length];

			int i = 0;
			foreach (string word in words)
			{
				if (!String.IsNullOrEmpty(word)) {
					response[i] = CapitalizeWord(word);
				}
				else
				{
					response[i] = "";
				}

				i++;
			}

			return response;
		}

		/// <summary>
		/// CapitalizeWord(string) returns a title capitalized
		/// string in response.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>Returns capitalized word.</returns>
		public static string CapitalizeWord(string input)
		{
			if (!String.IsNullOrEmpty(input))
			{
				string firstCharacter = input.Substring(0, 1).ToUpper();

				if (input.Length > 1)
				{
					string concate = input.Substring(1, (input.Length - 1)).ToLower();
					firstCharacter += concate;
				}

				return firstCharacter;
			}
			else
			{
				return "";
			}
		}

		#endregion

		#endregion



	}
	
	public class LocationGroup
	{
		#region Constants

		

		#endregion

		#region Attributes

		private SortedDictionary<string, string> _abbreviation;
		private SortedDictionary<string, string> _location;
		private bool _initialized = false;
		private bool _empty = true;

		#endregion

		#region Properties

		/// <summary>
		///  Returns the abbreviation dictionary.
		/// </summary>
		public SortedDictionary<string, string> AbbreviationDirectory
		{
			get { return this._abbreviation; }
		}

		/// <summary>
		/// Returns the location dictionary.
		/// </summary>
		public SortedDictionary<string, string> LocationDirectory
		{
			get { return this._location; }
		}

		/// <summary>
		/// Returns the _emtpy flag value.
		/// </summary>
		public bool Empty
		{
			get { return this._empty; }
		}

		#endregion

		#region Constructor / Initialization

		/// <summary>
		/// LocationGroup's empty constructor.
		/// </summary>
		public LocationGroup()
		{
			_init();
		}

		/// <summary>
		/// LocationGroup's base constructor takes
		/// an abbreviation and a location.
		/// </summary>
		/// <param name="abbreviation">The abbreviation of a given location.</param>
		/// <param name="location">Location assigned to abberviation.</param>
		public LocationGroup(string abbreviation, string loc)
		{
			_init();
			Add(abbreviation, loc);
		}

		public void _init()
		{
			if (!_initialized)
			{
				_abbreviation = new SortedDictionary<string, string>();
				_location = new SortedDictionary<string, string>();
				_empty = true;
				_initialized = true;
			}
		}

		/// <summary>
		/// Sets the default dictionaries
		/// for both abbreviations and
		/// locations. Useful for big
		/// libraries of data.
		/// </summary>
		/// <param name="defaultLG">The default to take on.</param>
		public void SetDefault(LocationGroup defaultLG)
		{
			this._abbreviation = defaultLG.AbbreviationDirectory;
			this._location = defaultLG.LocationDirectory;
		}

		#endregion

		#region Methods

		#region Mutator Methods

		/// <summary>
		/// Add a new entry to the
		/// bi-directional dictionary.
		/// Sorts boht by acronym.
		/// 
		/// Does not overwrite.
		/// </summary>
		/// <param name="ab">Abbreviation to add.</param>
		/// <param name="location">Location assigned to abberviation.</param>
		/// <returns></returns>
		public bool Add(string abbreviation, string location)
		{
			_init();

			if (_abbreviation.ContainsKey(location) || _location.ContainsKey(abbreviation))
			{
				// Does not overwrite. Return false.
				return false;
			}
			else
			{
				_abbreviation.Add(location, abbreviation);
				_location.Add(abbreviation, location);
				_abbreviation.OrderByDescending(p => p.Value); // The value will match the key of _location.
				_location.OrderByDescending(p => p.Key); // The key will match the value of _abbreviation.
				_empty = false;
				return true;
			}
		}

		#endregion

		#region Accessor Methods
		/// <summary>
		/// Returns whether or not
		/// it contains the string.
		/// </summary>
		/// <param name="abbreviation">String to search.</param>
		/// <returns>Returns true if match, returns false if none.</returns>
		public bool ContainsAbbreviation(string abbreviation)
		{
			return _location.ContainsKey(abbreviation);
		}

		/// <summary>
		/// Returns whether or not
		/// it contains the string.
		/// </summary>
		/// <param name="location">String to search.</param>
		/// <returns>Returns true if match, returns false if none.</returns>
		public bool ContainsLocation(string location)
		{
			return _abbreviation.ContainsKey(location);
		}

		/// <summary>
		/// Abbreviation(string) returns the abbreviation,
		/// formatted, or returns "", if it doesn't exist.
		/// </summary>
		/// <param name="location">Location assigned to abberviation.</param>
		/// <returns>Returns abbreviation, formatted, from dictionary.</returns>
		public string Abbreviation(string location)
		{
			string response = GetAbbreviation(location).Trim();

			if (String.IsNullOrEmpty(response))
			{
				return "";
			}
			else
			{
				return response.ToUpper();
			}
		}

		/// <summary>
		/// GetAbbreviation(string) returns the abbreviation,
		/// or returns "", if it doesn't exist.
		/// </summary>
		/// <param name="location">Location assigned to abberviation.</param>
		/// <returns>Returns abbreviation as-is from dictionary.</returns>
		public string GetAbbreviation(string location)
		{
			_init();

			if (_empty || !_abbreviation.ContainsKey(location))
			{
				return "";
			}
			else
			{
				return _abbreviation[location];
			}
		}

		/// <summary>
		/// GetLocation(string) returns the location,
		/// formatted, or returns "", if it doesn't exist.
		/// </summary>
		/// <param name="abbreviation">The abbreviation of a given location.</param>
		/// <returns>Returns location, formatted, from dictionary.</returns>
		public string Location(string abbreviation)
		{
			string response = GetLocation(abbreviation).Trim();

			if (String.IsNullOrEmpty(response))
			{
				return "";
			}
			else
			{
				string[] reply = Capitalize(response);
				string answer = "";

				if (reply.Length > 0)
				{
					foreach (string word in reply)
					{
						if (!String.IsNullOrEmpty(word))
						{
							answer += word + " ";	
						}
					}
				}

				return answer.Trim();
			}
		}
		
		/// <summary>
		/// GetLocation(string) returns the location,
		/// or returns "", if it doesn't exist.
		/// </summary>
		/// <param name="abbreviation">The abbreviation of a given location.</param>
		/// <returns>Returns location as-is from dictionary.</returns>
		public string GetLocation(string abbreviation)
		{
			_init();

			if (_empty || !_abbreviation.ContainsKey(abbreviation))
			{
				return "";
			}
			else
			{
				return _abbreviation[abbreviation];
			}
		}

		#endregion

		#region Capitalize

		/// <summary>
		/// Capitalize takes a string
		/// and returns an array
		/// of the entire string
		/// being capitalized.
		/// with Title case.
		/// </summary>
		/// <param name="input">Input to be capitalized.</param>
		/// <returns>Returns capitalized string array.</returns>
		public static string[] Capitalize(string input)
		{
			string[] words = input.Split(' ');
			string[] response = new string[words.Length];

			int i = 0;
			foreach (string word in words)
			{
				if (!String.IsNullOrEmpty(word))
				{
					response[i] = CapitalizeWord(word);
				}
				else
				{
					response[i] = "";
				}

				i++;
			}

			return response;
		}

		/// <summary>
		/// CapitalizeWord(string) returns a title capitalized
		/// string in response.
		/// </summary>
		/// <param name="input"></param>
		/// <returns>Returns capitalized word.</returns>
		public static string CapitalizeWord(string input)
		{
			if (!String.IsNullOrEmpty(input))
			{
				string firstCharacter = input.Substring(0, 1).ToUpper();

				if (input.Length > 1)
				{
					string concate = input.Substring(1, (input.Length - 1)).ToLower();
					firstCharacter += concate;
				}

				return firstCharacter;
			}
			else
			{
				return "";
			}
		}

		#endregion

		#endregion
	}






}
