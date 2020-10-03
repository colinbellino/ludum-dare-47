using UnityEngine;
using Zenject;

public class TimeLord : MonoBehaviour
{
	[SerializeField] private float _duration = 10f;

	private bool _isDayOver;
	private GameState _state;

	[Inject]
	public void Construct(GameState state)
	{
		_state = state;
	}

	protected void Awake()
	{
		_state.DayDuration = _duration;
		_state.TimeStart = Time.time;
		_state.TimeEnd = _state.TimeStart + _state.DayDuration;
	}

	protected void Update()
	{
		if (_isDayOver)
		{
			return;
		}

		_state.TimeCurrent = Time.time - _state.TimeStart;

		if (_state.TimeCurrent >= _state.TimeEnd)
		{
			_isDayOver = true;
			UnityEngine.Debug.Log("Day over !");
		}
	}
}
