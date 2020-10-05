using System;

public class Minuteur : BaseInteractive
{
	protected new bool _timed = true;

	protected void OnEnable()
	{
		GameEvents.DayEnded += OnDayEnded;
		GameEvents.FirstLoopAction += OnFirstLoopAction;
	}

	private void OnDayEnded()
	{
		_paused = true;
	}

	private void OnFirstLoopAction()
	{
		_paused = false;
	}

	protected void Update()
	{
		MinuteurUpdate();
	}

	protected override void OnInteractDone()
	{
		gameObject.SetActive(false);
		GameEvents.LayoutChanged?.Invoke(_gridPosition);
	}
}
