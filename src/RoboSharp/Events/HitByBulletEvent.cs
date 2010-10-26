using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for HitByBulletEvent.
	/// </summary>
	public class HitByBulletEvent : Event
	{
		#region Variables

		private double m_dBearing;
		private Bullet m_pBullet;

		#endregion

		#region Properties

		public double Bearing
		{
			get {return m_dBearing;}
		}

		public Bullet Bullet
		{
			get {return m_pBullet;}
		}

		public double Heading
		{
			get {return m_pBullet.Heading;}
		}

		public string Name
		{
			get {return m_pBullet.Name;}
		}

		public double Power
		{
			get {return m_pBullet.Power;}
		}

		public double Velocity
		{
			get {return m_pBullet.Velocity;}
		}

		#endregion

		#region Constructor

		public HitByBulletEvent(double dBearing, Bullet pBullet)
		{
			m_dBearing = dBearing;
			m_pBullet = pBullet;
		}

		#endregion
	}
}
