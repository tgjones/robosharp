using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for BulletHitEvent.
	/// </summary>
	public class BulletHitEvent : Event
	{
		#region Variables

		private string m_sName;
		private double m_dEnergy;
		private Bullet m_pBullet;

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

		public Bullet Bullet
		{
			get {return m_pBullet;}
		}

		#endregion

		#region Constructor

		public BulletHitEvent(string sName, double dEnergy, Bullet pBullet)
		{
			m_sName = sName;
			m_dEnergy = dEnergy;
			m_pBullet = pBullet;
		}

		#endregion
	}
}
