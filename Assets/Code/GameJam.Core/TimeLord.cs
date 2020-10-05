using UnityEngine;
using Zenject;

public class TimeLord : IInitializable, ITickable
{
	private readonly GameState _state;
	private readonly GameConfig _gameConfig;
	private bool _isRunning;

	public TimeLord(GameState state, GameConfig gameConfig)
	{
		_state = state;
		_gameConfig = gameConfig;
	}

	public void Initialize()
	{
		GameEvents.FirstLoopAction += StartLoop;
	}

	public void Tick()
	{
		if (_isRunning)
		{
			if (Time.time > _state.TimeEnd)
			{
				_state.LoopCount += 1;

				GameEvents.DayEnded?.Invoke();
				_isRunning = false;
				UnityEngine.Debug.Log("Day over !");
			}
		}
	}

	public void StartLoop()
	{
		Reset();
		_isRunning = true;
	}

	private void Reset()
	{
		_state.DayDuration = _gameConfig.DayDuration;
		_state.TimeStart = Time.time;
		_state.TimeEnd = _state.TimeStart + _state.DayDuration;
		_isRunning = false;
	}

	private void Exit()
	{
		GameEvents.FirstLoopAction -= StartLoop;
		GameEvents.DayEnded -= Reset;
	}
}
