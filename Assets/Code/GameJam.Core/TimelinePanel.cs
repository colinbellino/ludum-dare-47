using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private TextMeshProUGUI text;

	private GameState _state;
	private GameConfig _gameConfig;

	[Inject]
	public void Construct(GameState state, GameConfig gameConfig)
	{
		_state = state;
		_gameConfig = gameConfig;
	}

	protected void Update()
	{
		_slider.value = 1f - (_state.TimeEnd - Time.time) / _state.DayDuration;
		text.text = (_gameConfig.MaximumLoop - _state.LoopCount).ToString();
	}
}
