using System;
using UnityEngine;

public static class GameEvents
{
	public static Action DayEnded;

	public static Action<Vector3> LayoutChanged;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		DayEnded = null;
		LayoutChanged = null;
	}
}
