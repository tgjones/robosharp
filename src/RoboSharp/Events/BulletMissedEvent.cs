using System;

namespace RoboSharp.Events
{
	/// <summary>
	/// Summary description for BulletMissedEvent.
	/// </summary>
	public class BulletMissedEvent : Event
	{
		private Bullet m_pBullet;

		public Bullet Bullet
		{
			get {return m_pBullet;}
		}

		public BulletMissedEvent(Bullet pBullet)
		{
			m_pBullet = pBullet;
		}
	}
}
