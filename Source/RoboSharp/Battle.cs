using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Battle.
	/// </summary>
	public class Battle
	{
		private frmMain m_pParentForm;
		private Size m_tBattleFieldSize;
		private int m_nNumRobots;
		private Robot[] m_pRobots;
		private RobotListItem[] m_pRobotList;
		private Thread[] m_pThreads;
		private long m_lTime;
		private int m_nNumRounds;
		private int m_nRoundNum;
		private ArrayList m_pWinners;

		public Robot[] Winners
		{
			get
			{
				Robot[] pReturn = new Robot[m_pWinners.Count];
				m_pWinners.CopyTo(pReturn);
				return pReturn;
			}
		}

		public Size BattleFieldSize
		{
			get {return m_tBattleFieldSize;}
		}

		public int Others
		{
			get {return 1;}
		}

		public int NumRounds
		{
			get {return m_nNumRounds;}
		}

		public int RoundNum
		{
			get {return m_nRoundNum;}
		}

		public long Time
		{
			get {return m_lTime;}
		}

		public Robot[] Robots
		{
			get
			{
				ArrayList pTemp = new ArrayList();
				for (int i = 0; i < m_pRobots.Length; i++)
				{
					if (m_pRobots[i].IsAlive)
					{
						pTemp.Add(m_pRobots[i]);
					}
				}
				Robot[] pReturn = new Robot[pTemp.Count];
				pTemp.CopyTo(pReturn);
				return pReturn;
			}
		}

		public Robot[] AllRobots
		{
			get {return m_pRobots;}
		}

		public Battle(frmMain pParentForm, Size tBattleFieldSize, RobotListItem[] pRobots, int nNumRounds)
		{
			m_pParentForm = pParentForm;
			m_nNumRounds = nNumRounds;
			m_tBattleFieldSize = tBattleFieldSize;

			m_nNumRobots = pRobots.Length;
			m_pRobots = new Robot[m_nNumRobots];
			m_pThreads = new Thread[m_nNumRobots];

			m_pWinners = new ArrayList();

			m_pRobotList = pRobots;

			m_nRoundNum = 1;

			CreateRound();
		}

		private void CreateRound()
		{
			Random pRand = new Random(Environment.TickCount);
			for (int i = 0; i < m_nNumRobots; i++)
			{
				// attempt to load it and get the type name
				Assembly pAssembly = Assembly.LoadFile(m_pRobotList[i].Dll);
				Type pType = pAssembly.GetType(m_pRobotList[i].Name);
				Robot pRobot = (Robot) Activator.CreateInstance(pType);

				// add to array
				m_pRobots[i] = pRobot;

				// initialize
				int nX, nY; Rectangle tRect;
				do
				{
					nX = pRand.Next(50, m_tBattleFieldSize.Width  - 50);
					nY = pRand.Next(50, m_tBattleFieldSize.Height - 50);
					tRect = new Rectangle(nX, nY, 50, 50);
				}
				while (CheckIntersection(tRect, i));
				PointD tPosition = new PointD(tRect.X, tRect.Y);
				int nHeading = pRand.Next(0, 359);
				string sName = GetName(pType, i, m_pRobotList);
				pRobot.Initialize(this, tPosition, nHeading, sName, m_pParentForm.CreateGraphics());

				// create thread
				ThreadStart pThreadStart = new ThreadStart(pRobot.Run);
				m_pThreads[i] = new Thread(pThreadStart);
			}
		}

		private string GetName(Type pType, int nIndex, RobotListItem[] pRobots)
		{
			int nLength = pRobots.Length;
			int nCounter = 1;
			bool bMultiple = false;
			for (int i = 0; i < nLength; i++)
			{
				if (pRobots[i].Name == pType.Name)
				{
					bMultiple = true;
					if (i < nIndex) nCounter++;
				}
			}
			if (!bMultiple)
			{
				return pType.Name;
			}
			else
			{
				return pType.Name + " (" + nCounter +  ")";
			}
		}

		private bool CheckIntersection(Rectangle tRect, int nIndex)
		{
			for (int i = 0; i < nIndex; i++)
			{
				if (tRect.IntersectsWith(m_pRobots[i].Bounds))
				{
					return true;
				}
			}
			return false;
		}

		public bool ProcessFrame()
		{
			m_lTime++;

			for (int i = 0; i < m_nNumRobots; i++)
			{
				if (m_pRobots[i].IsAlive)
				{
					switch (m_pThreads[i].ThreadState)
					{
						case ThreadState.Unstarted :
							m_pThreads[i].Start();
							Thread.Sleep(3);
							m_pThreads[i].Suspend();
							break;
						case ThreadState.Suspended :
							m_pThreads[i].Resume();
							Thread.Sleep(3);
							m_pThreads[i].Suspend();
							break;
					}

					m_pRobots[i].ProcessBullets();
					m_pRobots[i].ProcessMovement();
				}
			}

			int nIndex;
			if (!RemoveDeadRobots(out nIndex))
			{
				// only one robot left, which has won
				m_pWinners.Add(m_pRobots[nIndex]);
				if (m_nRoundNum++ < m_nNumRounds)
				{
					CreateRound();
					m_lTime = 0;
				}
				else
				{
					return false;
				}
			}

			return true;
		}

		private bool RemoveDeadRobots(out int nIndex)
		{
			int nAlive = 0;
			nIndex = -1;
			for (int i = 0; i < m_nNumRobots; i++)
			{
				if (m_pRobots[i].Energy <= 0.0)
				{
					m_pRobots[i].IsAlive = false;
				}
				else
				{
					nIndex = i;
					nAlive++;
				}
			}
			return nAlive > 1;
		}
	}
}
