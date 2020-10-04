using UnityEngine;
using Zenject;

public class TimeLord : IInitializable, ITickable
{
	private bool _isDayOver;
	private GameState _state;

	public TimeLord(GameState state)
	{
		_state = state;
	}

	public void Initialize()
	{
		Reset();
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
		_state.DayDuration = 10f;
		_state.TimeStart = Time.time;
		_state.TimeEnd = _state.TimeStart + _state.DayDuration;
	}
}
