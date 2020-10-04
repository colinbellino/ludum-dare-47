using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Slider _slider;
	[SerializeField] private Transform _loopsContainer;

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
		var remainingLoops = _gameConfig.MaximumLoop - _state.LoopCount;

		for (int i = 0; i < _loopsContainer.childCount; i++)
		{
			_loopsContainer.GetChild(i).gameObject.SetActive(i < remainingLoops);
		}
	}
}
