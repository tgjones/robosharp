using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for ScannedRobotEvent.
	/// </summary>
	public class ScannedRobotEvent : Event
	{
		#region Variables

		private string m_sName;
		private double m_dEnergy;
		private double m_dBearing;
		private double m_dDistance;
		private double m_dHeading;
		private double m_dVelocity;

		#endregion

		#region Properties

		public string Name
		{
			get {return m_sName;}
		}

		public double Energy
		{
			get {return m_dEnergy;}
		}

		public double Bearing
		{
			get {return m_dBearing;}
		}

		public double Distance
		{
			get {return m_dDistance;}
		}

		public double Heading
		{
			get {return m_dHeading;}
		}

		public double Velocity
		{
			get {return m_dVelocity;}
		}

		#endregion

		#region Constructor

		public ScannedRobotEvent(string sName, double dEnergy,
			double dBearing, double dDistance,
			double dHeading, double dVelocity)
		{
			m_sName = sName;
			m_dEnergy = dEnergy;
			m_dBearing = dBearing;
			m_dDistance = dDistance;
			m_dHeading = dHeading;
			m_dVelocity = dVelocity;
		}

		#endregion
	}
}
