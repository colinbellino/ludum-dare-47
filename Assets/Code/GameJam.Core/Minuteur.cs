using System;
using Cysharp.Threading.Tasks;

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

	protected async override void OnInteractDone()
	{
		GameEvents.LayoutChanged?.Invoke(_gridPosition);

		_blockingCollider.enabled = false;
		_renderer.gameObject.SetActive(false);

		await UniTask.Delay(1000);

		gameObject.SetActive(false);
	}
}
