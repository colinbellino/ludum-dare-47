using System;
using UnityEngine;

public static class GameEvents
{
	public static Action DayEnded;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		DayEnded = null;
	}
}
