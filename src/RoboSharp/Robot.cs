using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using RoboSharp.Events;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Robot.
	/// </summary>
	public abstract class Robot
	{
		#region Variables

		private bool   m_bAdjustGunForRobotTurn;
		private bool   m_bAdjustRadarForGunTurn;
		private bool   m_bAdjustRadarForRobotTurn;
		private Battle m_pBattle;
		private Bitmap m_pBaseBitmap;
		private Bitmap m_pGunBitmap;
		private double m_dEnergy;
		private int m_nGunHeading;
		private int m_nHeading;
		private string m_sName;
		private PointD m_tPosition;
		private int m_nRadarHeading;
		private SizeD  m_tSize;
		private double m_dVelocity;

		private bool m_bInterrupted;
		private bool m_bMoveComplete;
		private bool m_bTurnComplete;
		private double m_dDistance;
		private PointD m_tMovement;
		private int m_nRotation;
		private int m_nTurnSpeed;

		private ArrayList m_pBullets;
		private ArrayList m_pEvents;
		private long m_lLastFireTime;

		private Graphics m_pGraphics;

		private bool m_bIsAlive = true;
		private bool m_bColourChanged;

		#endregion

		#region Properties

		public bool IsAlive
		{
			get {return m_bIsAlive;}
			set {m_bIsAlive = value;}
		}

		public bool AdjustGunForRobotTurn
		{
			set {m_bAdjustGunForRobotTurn = value;}
		}

		public bool AdjustRadarForGunTurn
		{
			set {m_bAdjustRadarForGunTurn = value;}
		}

		public bool AdjustRadarForRobotTurn
		{
			set {m_bAdjustRadarForRobotTurn = value;}
		}

		public Size BattleFieldSize
		{
			get {return m_pBattle.BattleFieldSize;}
		}

		public Rectangle Bounds
		{
			get
			{
				return new Rectangle((int) m_tPosition.X, (int) m_tPosition.Y,
					(int) Size.Width, (int) Size.Height);
			}
		}

		internal Bullet[] Bullets
		{
			get
			{
				Bullet[] pBullets = new Bullet[m_pBullets.Count];
				m_pBullets.CopyTo(pBullets);
				return pBullets;
			}
		}

		[DescriptionAttribute("The robot's current energy")]
		public double Energy
		{
			get {return m_dEnergy;}
			set {m_dEnergy = value;}
		}

		[DescriptionAttribute("The direction the robot is facing, in degrees")]
		public int Heading
		{
			get {return m_nHeading;}
		}

		[DescriptionAttribute("The direction the robot is facing, in radians")]
		public double HeadingRadians
		{
			get {return (double) m_nHeading * Math.PI / 180.0;}
		}

		public SizeD Size
		{
			get {return m_tSize;}
		}

		public string Name
		{
			get {return m_sName;}
		}

		[DescriptionAttribute("The position of the robot")]
		public PointD Position
		{
			get {return m_tPosition;}
		}

		internal Bitmap BaseBitmap
		{
			get {return m_pBaseBitmap;}
		}

		internal Bitmap GunBitmap
		{
			get {return m_pGunBitmap;}
		}

		public int GunHeading
		{
			get {return m_nGunHeading;}
		}

		public int NumRounds
		{
			get {return m_pBattle.NumRounds;}
		}

		public int Others
		{
			get {return m_pBattle.Others;}
		}

		public int RadarHeading
		{
			get {return m_nRadarHeading;}
		}

		public int RoundNum
		{
			get {return m_pBattle.RoundNum;}
		}

		public long Time
		{
			get {return m_pBattle.Time;}
		}

		public double Velocity
		{
			get {return m_dVelocity;}
		}

		#endregion

		#region Constructor
		
		public Robot()
		{
			m_dEnergy = 100;
			m_tSize = new SizeD(50.0, 50.0);
			m_dVelocity = 5.0;
			m_nTurnSpeed = 4;
			m_pBullets = new ArrayList();
			m_pEvents = new ArrayList();
		}

		#endregion

		#region Methods

		internal void Initialize(Battle pBattle, PointD tPosition, int nHeading, string sName, Graphics pGraphics)
		{
			m_pGraphics = pGraphics;
			m_sName = sName;

			m_pBaseBitmap = new Bitmap(@"images\tankbase.bmp");
			m_pGunBitmap = new Bitmap(@"images\tankgun.bmp");
			m_pGunBitmap.MakeTransparent(Color.White);

			m_pBattle = pBattle;
			m_tPosition = tPosition;
			m_nHeading = m_nGunHeading = m_nRadarHeading = nHeading;
		}

		private void ChangeColour(Bitmap pBitmap, Color tColour)
		{
			if (tColour != Color.Empty)
			{
				int nWidth = pBitmap.Width;
				int nHeight = pBitmap.Height;
				for (int x = 10; x < 39; x++)
				{
					for (int y = 0; y < nHeight; y++)
					{
						Color tCurrentColour = pBitmap.GetPixel(x, y);
						if (!(tCurrentColour.A == 0 && tCurrentColour.R == 0 && tCurrentColour.G == 0 && tCurrentColour.B == 0))
						{
							Color tNewColour = Color.FromArgb(
								Math.Min(MakeColour(tCurrentColour.R, tColour.R), 255),
								Math.Min(MakeColour(tCurrentColour.G, tColour.G), 255),
								Math.Min(MakeColour(tCurrentColour.B, tColour.B), 255));
							pBitmap.SetPixel(x, y, tNewColour);
						}
					}
				}
			}
		}

		private int MakeColour(byte tOld, byte tNew)
		{
			// scale to 0..1
			float fOld = (float) tOld / 255.0f;
			float fNew = (float) tNew / 255.0f;

			// multiply and scale back to 0..255
			int nReturn = (int) (fOld * fNew * 255.0f);
			if (nReturn != 0) nReturn += 100;
			return nReturn;
		}

		private void ProcessEvents()
		{
			m_bMoveComplete = false;
			m_bTurnComplete = false;
			if (m_pEvents.Count > 0)
			{
				if (m_pEvents[0] is HitWallEvent)
				{
					HitWallEvent pEvent = (HitWallEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnHitWall(pEvent);
					m_bMoveComplete = true;
					return;
				}

				if (m_pEvents[0] is HitRobotEvent)
				{
					HitRobotEvent pEvent = (HitRobotEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnHitRobot(pEvent);
					m_bMoveComplete = true;
					return;
				}

				if (m_pEvents[0] is ScannedRobotEvent)
				{
					ScannedRobotEvent pEvent = (ScannedRobotEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnScannedRobot(pEvent);
					return;
				}

				if (m_pEvents[0] is BulletMissedEvent)
				{
					BulletMissedEvent pEvent = (BulletMissedEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnBulletMissed(pEvent);
					return;
				}

				if (m_pEvents[0] is BulletHitEvent)
				{
					BulletHitEvent pEvent = (BulletHitEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnBulletHit(pEvent);
					return;
				}

				if (m_pEvents[0] is HitByBulletEvent)
				{
					HitByBulletEvent pEvent = (HitByBulletEvent) m_pEvents[0];
					m_pEvents.Clear();
					OnHitByBullet(pEvent);
					return;
				}
			}
		}

		internal void ProcessBullets()
		{
			if (m_pBullets.Count > 0)
			{
				for (int i = 0; i < m_pBullets.Count; i++)
				{
					// update bullet position
					Bullet pBullet = (Bullet) m_pBullets[i];
					PointD tNewPosition = PointD.Empty;
					tNewPosition.X = pBullet.Position.X + Math.Sin(pBullet.HeadingRadians) * pBullet.Velocity;
					tNewPosition.Y = pBullet.Position.Y + Math.Cos(pBullet.HeadingRadians) * pBullet.Velocity;
					pBullet.Position = tNewPosition;

					// check if new position hits wall
					if (tNewPosition.X < 0 || tNewPosition.X > BattleFieldSize.Width ||
						tNewPosition.Y < 0 || tNewPosition.Y > BattleFieldSize.Height)
					{
						m_pEvents.Add(new BulletMissedEvent(pBullet));
						m_pBullets.RemoveAt(i);
						i--;
						continue;
					}

					// check if new position hits robot (excluding ourselves)
					for (int j = 0; j < m_pBattle.Robots.Length; j++)
					{
						Robot pRobot = m_pBattle.Robots[j];
						if (pRobot != this)
						{
							if (pRobot.Bounds.IntersectsWith(new Rectangle((int) pBullet.Position.X,
								(int) pBullet.Position.Y, 5, 5)))
							{
								// inform this robot that one of our bullets hit
								m_pEvents.Add(new BulletHitEvent(pRobot.Name, pRobot.Energy, pBullet));

								// add to our energy
								m_dEnergy += 3.0 * pBullet.Power;

								// remove from other robot's energy
								pRobot.Energy -= 4.0 * pBullet.Power;
								if (pBullet.Power > 1.0)
								{
									pRobot.Energy -= 2.0 * (pBullet.Power - 1.0);
								}

								// inform the robot that was hit about it
								pRobot.m_pEvents.Add(new HitByBulletEvent(pBullet.Heading + 180 - Heading, pBullet));
								m_pBullets.RemoveAt(i);
								i--;
								break;
							}
						}
					}
				}
			}
		}

		internal void ProcessMovement()
		{
			// update robot position
			PointD tOldPosition = m_tPosition;
			m_tPosition += m_tMovement;
			m_tMovement = PointD.Empty;

			// check if we have hit a wall
			bool bHitWall = false;
			if (m_tPosition.X - Size.Width / 2.0 < 0)
			{
				m_tPosition.X = Size.Width / 2.0;
				bHitWall = true;
			}
			else if (m_tPosition.X + Size.Width / 2.0 > m_pBattle.BattleFieldSize.Width)
			{
				m_tPosition.X = m_pBattle.BattleFieldSize.Width - Size.Width / 2.0;
				bHitWall = true;
			}
			if (m_tPosition.Y - Size.Height / 2.0 < 0)
			{
				m_tPosition.Y = Size.Height / 2.0;
				bHitWall = true;
			}
			else if (m_tPosition.Y + Size.Height / 2.0 > m_pBattle.BattleFieldSize.Height)
			{
				m_tPosition.Y = m_pBattle.BattleFieldSize.Height - Size.Height / 2.0;
				bHitWall = true;
			}
			if (bHitWall)
			{
				m_pEvents.Add(new HitWallEvent());
				m_dEnergy -= 3.0;
			}

			// rotate robot
			m_nHeading += m_nRotation;
			if (m_nHeading < 0) m_nHeading = 360 + m_nHeading;
			if (m_nHeading > 359) m_nHeading -= 360;
			m_nRotation = 0;

			PointF tEndPoint = new PointF((float) (Position.X + Math.Sin(HeadingRadians) * BattleFieldSize.Width),
				(float) (Position.Y + Math.Cos(HeadingRadians) * BattleFieldSize.Width));

			// check if we can see another robot
			for (int i = 0; i < m_pBattle.Robots.Length; i++)
			{
				Robot pRobot = m_pBattle.Robots[i];

				// make sure we're not testing against ourselves
				if (pRobot != this)
				{
					GraphicsPath pPath = new GraphicsPath();
					pPath.AddLine(Position, tEndPoint);
					pPath.CloseFigure();
					pPath.Widen(new Pen(Color.Black, 1));
					Region pRegion = new Region(pPath);
					pRegion.Intersect(pRobot.Bounds);
					if (!pRegion.IsEmpty(m_pGraphics))
					{
						// we can see this robot
						PointD tDistance = pRobot.Position - Position;
						double dDistance = Math.Sqrt(tDistance.X * tDistance.X + tDistance.Y * tDistance.Y);
						ScannedRobotEvent pEvent = new ScannedRobotEvent(
							pRobot.Name, pRobot.Energy, pRobot.Heading - Heading,
							dDistance, pRobot.Heading, pRobot.Velocity);
						m_pEvents.Add(pEvent);
					}
				}
			}

			// check if we have collided with another robot
			for (int i = 0; i < m_pBattle.Robots.Length; i++)
			{
				Robot pRobot = m_pBattle.Robots[i];

				// make sure we're not testing against ourselves
				if (pRobot != this)
				{
					if (pRobot.Bounds.IntersectsWith(Bounds))
					{
						// send message to ourselves
						bool bAtFault = true;
						HitRobotEvent pEvent = new HitRobotEvent(pRobot.Name,
							Math.Abs(pRobot.Heading - Heading),
							pRobot.Energy, bAtFault);
						m_pEvents.Add(pEvent);
						m_tPosition = tOldPosition;
						m_dEnergy -= 3.0;
					}
				}
			}
		}

		private void Move(double dDistance, double dX, double dY)
		{
			// reset flags
			m_bInterrupted = false;
			m_dDistance = 0.0;
			m_bMoveComplete = false;

			// loop until we have moved the correct distance,
			// or other movement interrupted this movement
			while (Math.Abs(m_dDistance) < Math.Abs(dDistance) && !m_bInterrupted)
			{
				// move robot forwards along heading with current velocity
				m_tMovement.X = dX;
				m_tMovement.Y = dY;
				m_dDistance += Math.Sign(dDistance) * Velocity;

				// suspend this thread
				Thread.CurrentThread.Suspend();

				// check whether we have queued events waiting
				ProcessEvents();

				// check if an event was fired that ended this move
				if (m_bMoveComplete)
				{
					break;
				}
			}

			// set interrupted flag
			m_bInterrupted = true;
			m_dDistance = 0.0;
		}

		public void Ahead(double dDistance)
		{
			Move(dDistance, Velocity * Math.Sin(HeadingRadians), Velocity * Math.Cos(HeadingRadians));
		}

		public void Back(double dDistance)
		{
			Move(-dDistance, -Velocity * Math.Sin(HeadingRadians), -Velocity * Math.Cos(HeadingRadians));
		}

		private void Turn(int nDegrees)
		{
			// reset flags
			m_bInterrupted = false;
			m_nRotation = 0;
			int nDestHeading = Heading + nDegrees;
			m_bMoveComplete = false;
			int nCounter = Math.Abs(nDegrees);

			// loop until we have moved the correct distance,
			// or other movement interrupted this movement
			while (nCounter > 0 && !m_bInterrupted)
			{
				// rotate robot current velocity
				if (Math.Abs(nDestHeading - Heading) < m_nTurnSpeed)
				{
					m_nRotation = nDestHeading - Heading;
				}
				else
				{
					m_nRotation = Math.Sign(nDegrees) * m_nTurnSpeed;
				}

				// suspend this thread
				Thread.CurrentThread.Suspend();

				// check whether we have queued events waiting
				ProcessEvents();

				// check if an event was fired that ended this move
				if (m_bTurnComplete)
				{
					break;
				}

				nCounter -= m_nTurnSpeed;
			}
			
			// set interrupted flag
			m_bInterrupted = true;
		}
		
		public void TurnLeft(int nDegrees)
		{
			Turn(-nDegrees);
		}

		public void TurnRight(int nDegrees)
		{
			Turn(nDegrees);
		}

		public void DoNothing()
		{

		}

		public void Fire(double dPower)
		{
			FireBullet(dPower);
		}

		public Bullet FireBullet(double dPower)
		{
			if (dPower < 0.1) dPower = 0.1;
			if (dPower > 3.0) dPower = 3.0;

			if (m_lLastFireTime + 30 < Time)
			{
				m_lLastFireTime = Time;
				Bullet pBullet = new Bullet(this, dPower, 6.0);
				m_pBullets.Add(pBullet);
				return pBullet;
			}
			return null;
		}

		public void Resume()
		{

		}

		public void Scan()
		{

		}

		public void SetColours(
			Color tRobotColour,
			Color tGunColour)
		{
			if (!m_bColourChanged)
			{
				ChangeColour(m_pBaseBitmap, tRobotColour);
				ChangeColour(m_pGunBitmap, tGunColour);
				m_bColourChanged = true;
			}
		}

		public void Stop()
		{
			Stop(false);
		}

		public void Stop(bool bOverwrite)
		{

		}

		public void TurnGunLeft(double dDegrees)
		{

		}

		public void TurnGunRight(double dDegrees)
		{
			
		}

		public void TurnRadarLeft(double dDegrees)
		{

		}

		public void TurnRadarRight(double dDegrees)
		{

		}

		public virtual void Run() {}
		protected virtual void OnBulletHit(BulletHitEvent e) {}
		protected virtual void OnBulletHitBullet(BulletHitEvent e) {}
		protected virtual void OnBulletMissed(BulletMissedEvent e) {}
		protected virtual void OnDeath(DeathEvent e) {}
		protected virtual void OnHitByBullet(HitByBulletEvent e) {}
		protected virtual void OnHitRobot(HitRobotEvent e) {}
		protected virtual void OnHitWall(HitWallEvent e) {}
		protected virtual void OnRobotDeath(RobotDeathEvent e) {}
		protected virtual void OnScannedRobot(ScannedRobotEvent e) {}
		protected virtual void OnWin(WinEvent e) {}

		#endregion
	}

	enum RobotEvent
	{
		BulletHitBullet,
		BulletHit,
		BulletMissed,
		Death,
		HitByBullet,
		HitRobot,
		HitWall,
		RobotDeath,
		ScannedRobot,
		Win
	}
}