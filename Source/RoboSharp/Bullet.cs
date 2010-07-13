using System;
using System.Drawing;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Bullet.
	/// </summary>
	public class Bullet
	{
		#region Variables

		private int m_nHeading;
		private Robot m_pAttacker;
		private double m_dPower;
		private double m_dVelocity;
		private Robot m_pVictim;
		private PointD m_tPosition;
		private bool m_bIsActive;

		#endregion

		#region Properties

		public int Heading
		{
			get {return m_nHeading;}
		}

		public double HeadingRadians
		{
			get {return (double) m_nHeading * Math.PI / 180.0;}
		}

		public string Name
		{
			get {return m_pAttacker.Name;}
		}

		public double Power
		{
			get {return m_dPower;}
		}

		public double Velocity
		{
			get {return m_dVelocity;}
		}

		public string Victim
		{
			get {return m_pVictim.Name;}
		}

		public PointD Position
		{
			get {return m_tPosition;}
			set {m_tPosition = value;}
		}

		public bool IsActive
		{
			get {return m_bIsActive;}
			set {m_bIsActive = value;}
		}

		#endregion
		
		public Bullet(Robot pAttacker, double dPower, double dVelocity)
		{
			m_nHeading = pAttacker.Heading;
			m_pAttacker = pAttacker;
			m_dPower = dPower;
			m_dVelocity = dVelocity;
			m_tPosition = pAttacker.Position;
			m_bIsActive = true;
			m_pVictim = null;
		}
	}
}
