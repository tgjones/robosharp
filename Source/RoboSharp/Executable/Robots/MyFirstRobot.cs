using System;
using System.Drawing;
using RoboSharp;
using RoboSharp.Events;

	/// <summary>
	/// Summary description for MyFirstRobot.
	/// </summary>
	public class MyFirstRobot : Robot
	{
		public override void Run()
		{
			SetColours(Color.Pink, Color.Pink);
			TurnLeft(Heading);
			while (true)
			{
				Ahead(1000);
				TurnRight(90);
			}
		}

		protected override void OnScannedRobot(ScannedRobotEvent e)
		{
			Fire(1);
		}

		protected override void OnHitByBullet(HitByBulletEvent e)
		{
			TurnLeft(180);
		}
	}