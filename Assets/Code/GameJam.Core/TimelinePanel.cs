using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Slider _slider;

	private GameState _state;

	[Inject]
	public void Construct(GameState state)
	{
		_state = state;
	}

	protected void Update()
	{
		_slider.value = 1 - (_state.TimeEnd - _state.TimeCurrent) / _state.DayDuration;
	}
}
