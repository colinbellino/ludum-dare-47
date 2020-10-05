using System;
using UnityEngine;

public static class GameEvents
{
	public static Action DayEnded;
	public static Action ExitReached;
	public static Action BackToTitle;
	public static Action GameStarted;
	public static Action StartGame;
	public static Action QuitGame;
	public static Action<IInteractive> TargetSelected;
	public static Action TargetUnSelected;
	public static Action<Vector3Int> LayoutChanged;
	public static Action<Vector3Int, Vector3Int> StonePushed;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		DayEnded = null;
		ExitReached = null;
		BackToTitle = null;
		GameStarted = null;
		StartGame = null;
		QuitGame = null;
		LayoutChanged = null;
		StonePushed = null;
	}
}
