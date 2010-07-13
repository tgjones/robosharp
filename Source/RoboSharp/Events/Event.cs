using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for Event.
	/// </summary>
	public class Event
	{
		#region Variables

		private DateTime m_tTime;

		#endregion

		#region Properties

		public DateTime Time
		{
			get {return m_tTime;}
			set {m_tTime = value;}
		}

		#endregion
	}
}
