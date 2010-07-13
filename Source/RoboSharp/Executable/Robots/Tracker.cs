using RoboSharp;
using RoboSharp.Events;

public class Tracker : Robot
{
	int count = 0;			// Keeps track of how long we've
							// been searching for our target
	int gunTurnAmt;		// How much to turn our gun when searching
	string trackName;		// Name of the robot we're currently tracking

	/**
	 * run: Tracker's main run function
	 */	
	public override void Run()
	{
		trackName = null;					// Initialize to not tracking anyone
		while (true) {
			// turn the Gun (looks for enemy)
			TurnRight(gunTurnAmt);
			// Keep track of how long we've been looking
			count++;
			// If we've haven't seen our target for 2 turns, look left
			if (count > 2)					
			{
				gunTurnAmt = -10;
			}
			// If we still haven't seen our target for 5 turns, look right
			if (count > 5)
				gunTurnAmt = 10;
			// If we *still* haven't seen our target after 10 turns, find another target
			if (count > 11)
				trackName = null;
		}
	}
	
	/**
	 * onScannedRobot: Here's the good stuff
	 */	
	protected override void OnScannedRobot(ScannedRobotEvent e) {

		// If we have a target, and this isn't it, return immediately
		//  so we can get more ScannedRobotEvents.
		if (trackName != null && e.Name != trackName)
			return;

		// If we don't have a target, well, now we do!
    	if (trackName == null) {
			trackName = e.Name;
		}
		// This is our target.  Reset count (see the run method)
    	count = 0;
		// If our target is too far away, turn and move torward it.
		if (e.Distance > 150)
		{
			gunTurnAmt = normalRelativeAngle((int) e.Bearing + Heading);
			
			TurnRight(gunTurnAmt);  	// and see how much Tracker improves...
			Ahead(e.Distance - 140);
			return;
		}

		// Our target is close.
		gunTurnAmt = normalRelativeAngle((int) e.Bearing + Heading);
		TurnRight(gunTurnAmt);
		Fire(3);
		
		// Our target is too close!  Back up.
		if (e.Distance < 100)
		{
			if (e.Bearing > -90 && e.Bearing <= 90)
				Back(40);
			else
				Ahead(40);
		}
 
	}
	
	/**
	 * onHitRobot:  Set him as our new target
	 */	
	protected override void OnHitRobot(HitRobotEvent e) {
		// Set the target
		trackName = e.Name;
		// Back up a bit.
		// Note:  We won't get scan events while we're doing this!
		// An AdvancedRobot might use setBack(); execute();
		gunTurnAmt = normalRelativeAngle((int) e.Bearing + Heading);
		TurnRight(gunTurnAmt);
		Fire(3);
		Back(50);
	}

	/**
	 * onWin:  Do a victory dance
	 */	
	protected override void OnWin(WinEvent e) {
		for (int i = 0; i < 50; i++)
		{
			TurnRight(30);
			TurnLeft(30);
		}
	}
	
	// normalAbsoluteAngle is not used in this robot,
	// but is here for reference.
	/**
	 * normalAbsoluteAngle:  returns angle such that 0 <= angle < 360
	 */	
	public int normalAbsoluteAngle(int angle) {
		if (angle >= 0 && angle < 360)
			return angle;
		int fixedAngle = angle;
		while (fixedAngle < 0)
			fixedAngle += 360;
		while (fixedAngle >= 360)
			fixedAngle -= 360;
		return fixedAngle;
	}
	
	/**
	 * normalRelativeAngle:  returns angle such that -180<angle<=180
	 */	
	public int normalRelativeAngle(int angle) {
		if (angle > -180 && angle <= 180)
			return angle;
		int fixedAngle = angle;
		while (fixedAngle <= -180)
			fixedAngle += 360;
		while (fixedAngle > 180)
			fixedAngle -= 360;
		return fixedAngle;
	}

}