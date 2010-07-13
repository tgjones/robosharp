using System;
using System.Drawing;
using RoboSharp;
using RoboSharp.Events;

	/// <summary>
	/// Summary description for Walls
	/// </summary>
	public class Walls : Robot
	{
		double moveAmount; // How much to move

		public override void Run()
		{
			SetColours(Color.Green, Color.LightGreen);
			// Initialize moveAmount to the maximum possible for this battlefield.
			moveAmount = Math.Max(BattleFieldSize.Width, BattleFieldSize.Height);
		
			// turnLeft to face a wall.
			// getHeading() % 90 means the remainder of 
			// getHeading() divided by 90.
			TurnLeft(Heading % 90);
			Ahead(moveAmount);

			// Turn the gun to turn right 90 degrees.
			TurnRight(90);
		
			while (true)
			{
				// Move up the wall
				Ahead(moveAmount);

				// Turn to the next wall
				TurnRight(90);
			}
		}
	
		protected override void OnHitRobot(HitRobotEvent e)
		{
			// If he's in front of us, set back up a bit.
			//if (e.Bearing > -90 && e.Bearing < 90)
				Back(100);
			// else he's in back of us, so set ahead a bit.
			//else
			//	Ahead(100);
		}

		/**
		 * onScannedRobot:  Fire!
		 */	
		protected override void OnScannedRobot(ScannedRobotEvent e)
		{
			Fire(2);
		}	
	}