using UnityEngine;
using Zenject;

public class TimeLord : IInitializable, ITickable
{
	private readonly GameState _state;
	private readonly GameConfig _gameConfig;

	public TimeLord(GameState state, GameConfig gameConfig)
	{
		_state = state;
		_gameConfig = gameConfig;
	}

	public void Initialize()
	{
		Reset();

		GameEvents.GameStarted += Reset;
		GameEvents.DayEnded += Reset;
	}

	public void Tick()
	{
		if (Time.time > _state.TimeEnd)
		{
			_state.LoopCount += 1;

			Reset();
			GameEvents.DayEnded?.Invoke();
			UnityEngine.Debug.Log("Day over !");
		}
	}

	private void Reset()
	{
		_state.DayDuration = _gameConfig.DayDuration;
		_state.TimeStart = Time.time;
		_state.TimeEnd = _state.TimeStart + _state.DayDuration;
	}
}
