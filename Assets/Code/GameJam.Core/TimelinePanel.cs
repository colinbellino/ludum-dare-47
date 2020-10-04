using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private TextMeshProUGUI text;

	private GameState _state;

	[Inject]
	public void Construct(GameState state)
	{
		_state = state;
	}

	protected void Update()
	{
		_slider.value = 1f - (_state.TimeEnd - Time.time) / _state.DayDuration;
		text.text = (100 - _state.LoopCount).ToString();
	}
}
