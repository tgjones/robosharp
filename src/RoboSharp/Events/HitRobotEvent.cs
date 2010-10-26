using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for HitRobotEvent.
	/// </summary>
	public class HitRobotEvent
	{
		#region Variables

		private string m_sName;
		private double m_dBearing;
		private double m_dEnergy;
		private bool m_bAtFault;

		#endregion

		#region Properties

		public string Name
		{
			get {return m_sName;}
		}

		public double Bearing
		{
			get {return m_dBearing;}
		}

		public double Energy
		{
			get {return m_dEnergy;}
		}

		public bool AtFault
		{
			get {return m_bAtFault;}
		}

		#endregion

		#region Constructor

		public HitRobotEvent(string sName, double dBearing, double dEnergy, bool bAtFault)
		{
			m_sName = sName;
			m_dBearing = dBearing;
			m_dEnergy = dEnergy;
			m_bAtFault = bAtFault;
		}

		#endregion
	}
}
