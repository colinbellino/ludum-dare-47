using UnityEngine;

public class GameState
{
	public float TimeStart;
	public float TimeEnd;
	public float DayDuration;
	public int LoopCount;
	public float PlayerActionStartTime = 0;

	public Vector3 PlayerStartPosition;
	public Vector3? PlayerDestination;
}
