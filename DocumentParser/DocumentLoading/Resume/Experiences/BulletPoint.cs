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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentParser.DocumentLoading.Resume.Experiences
{
	/// <summary>
	/// BulletPoint is an object that
	/// stores a message, refers to
	/// the first embedded bullet point,
	/// and refers to the next bullet
	/// point in the level.
	/// 
	/// Reading is completed once all
	/// embedded bullets are read.
	/// </summary>
	public class BulletPoint
	{

		// TODO: Create an IEnumerator<BulletPoint> combo for BulletPoints
		// TODO: Implement system where parent BulletPoint is read in to a BulletPoint to allow returning ability.
		// TODO: Implement system where BulletPoint creation allows for new, embedded BulletPoints to be created.
		// TODO: Implement system where BulletPoint points to the next BulletPoint in the same level.
		// TODO: Implement system where BulletPoint points to the first BulletPoint in the embedded system.

		#region Attributes

		// Fields'n'Flags
		private int _level;
		private string _msg;
		private BulletPoint _parent;
		private BulletPoint _prev;
		private BulletPoint _next;
		private BulletPoint _nextChunk;
		private bool _empty;
		private bool _initialized;

		// References
		private BulletPoint self;

		#endregion

		#region Properties

		// TODO: Complete the Properties section.

		// Properties

		/// <summary>
		/// Level is the indentation
		/// level of a given bullet
		/// point. A level of 0 indicates
		/// there is no bullet point.
		/// </summary>
		public int Level
		{
			get { return this._level; }
		}

		/// <summary>
		/// Message returns the actual
		/// string content of the
		/// BulletPoint, to be printed.
		/// </summary>
		public string Message
		{
			get { return this._msg; }
		}

		// Links
		/// <summary>
		/// The parent bullet point.
		/// Will return self, if _parent is null
		/// or empty.
		/// </summary>
		public BulletPoint Parent
		{
			get {
				// TODO: Return self.
				// TODO: Create IsNullOrEmpty method.
				return this._parent;
			}
		}

		/// <summary>
		/// The previous (embedded) bullet point.
		/// Will return self, if _parent is null.
		/// </summary>
		public BulletPoint Previous
		{
			get {
				// TODO: Set up the PreviousChunk (Parent) option.
				return this._prev;
			}
		}

		/// <summary>
		/// The next (embedded) bullet point.
		/// Will return null, NextChunk if null.
		/// </summary>
		public BulletPoint Next
		{
			// TODO: Return NextChunk option.
			get { return this._next; }
		}

		/// <summary>
		/// The next (same-level) bullet point.
		/// Will return null, if null.
		/// </summary>
		public BulletPoint NextChunk
		{
			// TODO: Return null case handle.
			get { return this._nextChunk; }
		}
		
		// Flags

		/// <summary>
		/// HasParent returns the
		/// null/empty status of
		/// _parent.
		/// </summary>
		public bool HasParent
		{
			get
			{
				// If the parent node is equal to this, or null, return false.
				if (IsNullOrEmpty(this._parent) || this._parent == this)
				{
					return false;
				}

				// If _parent is NULL or EMPTY, return true.
				return true;
			}
		}

		/// <summary>
		/// HasPrevious returns the
		/// null/empty status of
		/// _prev.
		/// </summary>
		public bool HasPrevious
		{
			get
			{
				// If the previous (same-level) node is null, return false.
				if (IsNullOrEmpty(this._prev))
				{
					return false;
				}

				// If _prev is NULL or EMPTY, return true.
				return true;
			}
		}

		/// <summary>
		/// HasChildren returns the
		/// null/empty status of
		/// _next.
		/// </summary>
		public bool HasChildren
		{
			get
			{
				// If the next (embed-level) node is null, return false.
				if (IsNullOrEmpty(this._next))
				{
					return false;
				}

				// If _next is NULL or EMPTY, return true.
				return true;
			}
		}

		/// <summary>
		/// HasNeighbor returns the
		/// null/empty status of
		/// _nextChunk.
		/// </summary>
		public bool HasNeighbor
		{
			get
			{
				// If the next (same-level) node is null, return false.
				if (IsNullOrEmpty(this._nextChunk))
				{
					return false;
				}

				// If _nextChunk is NULL or EMPTY, return true.
				return true;
			}
		}

		/// <summary>
		/// HasMessage returns the
		/// null/empty status of
		/// _msg.
		/// </summary>
		public bool HasMessage
		{
			get
			{
				// If the nmessage is null, return false.
				if (String.IsNullOrEmpty(this._msg))
				{
					return false;
				}

				// If, pass, return true.
				return true;
			}
		}

		/// <summary>
		/// IsEnd returns true
		/// if this has no parent
		/// AND has no previous neighbors.
		/// </summary>
		public bool IsStart
		{
			get
			{
				return (!HasParent && !HasPrevious);
			}
		}

		/// <summary>
		/// IsEnd returns true
		/// if this has no children
		/// AND has no neighbors.
		/// </summary>
		public bool IsEnd
		{
			get
			{
				return (!HasChildren && !HasNeighbor);
			}
		}
		
		/// <summary>
		/// Returns <see cref="_initialized"/> status.
		/// </summary>
		public bool Initialized
		{
			get { return this._initialized; }
		}

		/// <summary>
		/// Returns <see cref="_empty"/> status.
		/// </summary>
		public bool Empty
		{
			get
			{
				if (this._empty) { return this._empty; }
				this._empty = IsNullOrEmpty(this);
				return this._empty;
			}
		}

		#endregion

		#region Constructor / Initialization
		
		/// <summary>
		/// BulletPoint() is the empty
		/// constructor that just runs
		/// _init().
		/// 
		/// The level defaults to 0,
		/// And this BulletPoint itself,
		/// remains the head.
		/// </summary>
		public BulletPoint()
		{
			_init();
		}

		// TODO: Complete the overloaded Constructors.
		/// <summary>
		/// BulletPoint(string) creates
		/// a new BulletPoint with the
		/// correct string.
		/// </summary>
		/// <param name="message">Message to add.</param>
		public BulletPoint(string message)
		{
			_init();

			SetMessage(message);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		public BulletPoint(string message, int level, BulletPoint head)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, BulletPoint) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		public BulletPoint(string message, int level, BulletPoint head, BulletPoint embed)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, string) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		public BulletPoint(string message, int level, BulletPoint head, string embed)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<BulletPoint> embed)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<string> embed)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, BulletPoint, BulletPoint) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, BulletPoint embed, BulletPoint chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, string, string) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, string embed, string chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, BulletPoint) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<BulletPoint> embed, BulletPoint chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, string) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<BulletPoint> embed, string chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, BulletPoint) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<string> embed, BulletPoint chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, string) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<string> embed, string chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<BulletPoint> embed, ICollection<BulletPoint> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<string> embed, ICollection<BulletPoint> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<BulletPoint> embed, ICollection<string> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, ICollection, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Collection of Embeds to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, ICollection<string> embed, ICollection<string> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, BulletPoint, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, BulletPoint embed, ICollection<BulletPoint> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, string, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, string embed, ICollection<BulletPoint> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, BulletPoint, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, BulletPoint embed, ICollection<string> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}

		/// <summary>
		/// BulletPoint(string, int, BulletPoint, string, ICollection) creates
		/// a new BulletPoint with proper parameters.
		/// 
		/// <para>If head is null, it will keep this BulletPoint
		/// as the head.</para>
		/// </summary>
		/// <param name="message">Message to add.</param>
		/// <param name="level">Indentation level.</param>
		/// <param name="head">Head is the parent BulletPoint</param>
		/// <param name="embed">Embed to embed in current BulletPoint.</param>
		/// <param name="chunkNext">Collection of Next same level chunk in the list.</param>
		public BulletPoint(string message, int level, BulletPoint head, string embed, ICollection<string> chunkNext)
		{
			_init();

			SetMessage(message);
			SetParent(head);
			SetLevel(level);
			Embed(embed);
			Add(chunkNext);
		}


		/// <summary>
		/// _init() is an initializer
		/// run by the above constructors.
		/// </summary>
		private void _init()
		{
			// Initialization of fields.
			_level = 0;
			_msg = "";
			_next = null;
			_nextChunk = null;
			_parent = this;

			// Flags
			_empty = true;
			_initialized = true;

			// References
			self = this;
		}

		#endregion

		#region Methods

		// TODO: Complete the Mutator Methods section.
		#region Mutator Methods

		/// <summary>
		/// SetMessage(string) takes a string
		/// and adds it.
		/// </summary>
		/// <param name="message">Message to set.</param>
		public void SetMessage(string message)
		{
			if (!String.IsNullOrEmpty(message))
			{
				this._msg = message;
			}
		}

		/// <summary>
		/// SetParent(BulletPoint) takes
		/// a BulletPoint and assigns it as
		/// the head to this one.
		/// </summary>
		/// <param name="head">Parent to set, if not empty.</param>
		public void SetParent(BulletPoint head)
		{
			// If the head is not null and not empty.
			if (!IsNullOrEmpty(head))
			{
				// Set parent to this input head.
				this._parent = head;
			}
			// If the head IS null or empty, AND the current parent value is null.
			else if(IsNullOrEmpty(this._parent))
			{
				// Set parent to self. (Do we need this if this is done in the _init() method?)
				this._parent = self;
			}
		}

		/// <summary>
		/// SetPrevious(BulletPoint) takes
		/// a BulletPoint and assigns it as
		/// the _prev.
		/// </summary>
		/// <param name="previous">Previous BulletPoint</param>
		public void SetPrevious(BulletPoint previous)
		{
			// Set _prev to previous, even if previous is null.
			if (previous != null)
			{
				this._prev = previous;
			}
			else
			{
				this._prev = null;
			}
		}

		/// <summary>
		/// SetLevel(int) takes the indentation
		/// level and sets it. If the value is
		/// negative, it's okay, because it
		/// is sent to its absolute value.
		/// </summary>
		/// <param name="level">Indentation level.</param>
		public void SetLevel(int level)
		{
			this._level = Math.Abs(level);
		}

		/// <summary>
		/// SetNext(BulletPoint) takes
		/// a BulletPoint and assigns it as
		/// the _next.
		/// </summary>
		/// <param name="next">Next to set.</param>
		public void SetNext(BulletPoint next)
		{
			// Set _next to next, even if next is null.
			if (next != null)
			{
				this._next = next;
			}
			else
			{
				this._next = null;
			}
		}

		/// <summary>
		/// SetNextChunk(BulletPoint) takes
		/// a BulletPoint and assigns it as
		/// the head to this one.
		/// </summary>
		/// <param name="chunkNext">Chunk to set, if not empty</param>
		public void SetNextChunk(BulletPoint chunkNext)
		{
			// If the chunkNext is not null and not empty.
			if (!IsNullOrEmpty(chunkNext))
			{
				// Set chunk to this input head.
				this._nextChunk = chunkNext;
			}
			// If the chunkNext IS null or empty, AND the current chunkNext value is null.
			else if (IsNullOrEmpty(this._nextChunk))
			{
				// Set parent to null.
				this._nextChunk = null;
			}
		}

		/// <summary>
		/// Embed(BulletPoint) takes an existing bulletpoint
		/// and adds it to the very end.
		/// </summary>
		/// <param name="item"></param>
		public void Embed(BulletPoint item)
		{
			// TODO: Get Parent, Previous, Next (in embed).

			// Qualities to modify in this and in item:
			// // Parent
			// // // This - Parent does not need to be modified.
			// // // Item - Parent should be "this".
			item.SetParent(this);

			if (this.HasChildren)
			{
				// // Previous
				// // // This - Previous should not need to be modified.
				// // // Item - Previous should either be the last embed in the list, or "this", if the list is empty.
				BulletPoint temp = this.Next;
				// TODO: #CONTINUE THIS NEXT


			}
			else
			{


			}


			// // Next
			// // // This - Next should be item, if the embed list doesn't exist. Else, it should remain unchanged.

			// // // Item - Next should be null.

			// // NextChunk
			// // // This - NextChunk should not need to be modified.
			// // // Item - NextChunk of the last embedded item should be affected. If the list didn't exist, nothing needs to be changed.




			// Determine if the embed list of current node currently exists.
			if (this.HasChildren)
			{

				// Seek through list to find last embed.

				// Store last embed as the "previous" node in the embedded item.

				// Set the "next" of the "previous" to the embedded item.

				// Get level to the embed.
			}
			else
			{
				// If this has no children - set previous to null.
				item.SetPrevious(null);

				// This is the new parent for the embed.
				item.SetParent(this);

				// Chekc

				// The next chunk does not exist


			}



		}

		/// <summary>
		/// Embed(string) creates an bulletpoint
		/// and adds it to the very end.
		/// </summary>
		/// <param name="item"></param>
		public void Embed(string item)
		{
			// TODO: Get Parent, Previous, Next (in embed).

			// Determine if the embed list of current node currently exists.

			// Seek through list to find last embed.

			// Store last embed as the "previous" node in the embedded item.

			// Set the "next" of the "previous" to the embedded item.

			// Add a level to the embed.



		}




		#endregion

		// TODO: Complete the Accessor Methods section.

		// TODO: Complete the Service Methods section.
		#region Service Methods

		/// <summary>
		/// ToString() overrided to return
		/// the message content held by a
		/// bullet point.
		/// </summary>
		/// <returns>Returns the message.</returns>
		public override string ToString()
		{
			return this._msg;
		}

		#endregion


		#region Static Methods

		/// <summary>
		/// IsNullOrEmpty(BulletPoint) returns whether
		/// or not the item is null or empty.
		/// </summary>
		/// <param name="item">BulletPoint to check.</param>
		/// <returns>Returns true if null or empty. Returns false if otherwise.</returns>
		public static bool IsNullOrEmpty(BulletPoint item)
		{
			if (item == null || !item.Initialized)
			{
				return true; // Is Null.
			}
			else
			{
				// Is it currently empty?
				if (String.IsNullOrEmpty(item.Message) || !item.HasChildren)
				{
					return true;
				}
				
				return false;
			}
		}

		#endregion


		#endregion
	}
}
