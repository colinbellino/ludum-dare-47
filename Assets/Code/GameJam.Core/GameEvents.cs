using System;
using UnityEngine;

public static class GameEvents
{
	public static Action DayEnded;
	public static Action ExitReached;
	public static Action<Vector3Int> LayoutChanged;
	public static Action<Vector3Int, Vector3Int> StonePushed;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		DayEnded = null;
		ExitReached = null;
		LayoutChanged = null;
		StonePushed = null;
	}
}
