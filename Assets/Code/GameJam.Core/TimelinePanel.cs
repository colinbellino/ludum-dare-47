using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private Image[] _liveImages;
	[SerializeField] private Image[] _segmentsImages;

	private GameState _state;
	private GameConfig _gameConfig;

	[Inject]
	public void Construct(GameState state, GameConfig gameConfig)
	{
		_state = state;
		_gameConfig = gameConfig;
	}

	protected void OnEnable()
	{
		UpdateLoops();

		GameEvents.DayEnded += UpdateLoops;
	}

	protected void OnDisable()
	{
		GameEvents.DayEnded -= UpdateLoops;
	}

	protected void Update()
	{
		_slider.value = 1f - (_state.TimeEnd - Time.time) / _state.DayDuration;
	}

	private void UpdateLoops()
	{
		var remainingLoops = (_gameConfig.MaximumLoop - _state.LoopCount) - 1;

		for (int i = 0; i < _liveImages.Length; i++)
		{
			if (i > remainingLoops)
			{
				_liveImages[i].color = Color.clear;
			}
			else if (i == remainingLoops)
			{
				_liveImages[i].color = _gameConfig.Color2;
			}
		}
	}
}
