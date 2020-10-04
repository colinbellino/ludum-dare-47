using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimelinePanel : MonoBehaviour
{
	[SerializeField] private Image[] _liveImages;
	[SerializeField] private Transform[] _segmentsImages;

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
		var value = 1f - (_state.TimeEnd - Time.time) / _state.DayDuration;

		// 0 -> N
		// 1 -> E
		// 2 -> S
		// 3 -> W
		if (value < 0.25f)
		{
			_segmentsImages[0].localScale = new Vector3(Mathf.Max(0f, (value - 0.00f) * 4), 1f, 1f);
			_segmentsImages[1].localScale = new Vector3(0f, 1f, 1f);
			_segmentsImages[2].localScale = new Vector3(0f, 1f, 1f);
			_segmentsImages[3].localScale = new Vector3(0f, 1f, 1f);
		}
		else if (value < 0.50f)
		{
			_segmentsImages[0].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[1].localScale = new Vector3(Mathf.Max(0f, (value - 0.25f) * 4), 1f, 1f);
			_segmentsImages[2].localScale = new Vector3(0f, 1f, 1f);
			_segmentsImages[3].localScale = new Vector3(0f, 1f, 1f);
		}
		else if (value < 0.75f)
		{
			_segmentsImages[0].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[1].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[2].localScale = new Vector3(Mathf.Max(0f, (value - 0.50f) * 4), 1f, 1f);
			_segmentsImages[3].localScale = new Vector3(0f, 1f, 1f);
		}
		else
		{
			_segmentsImages[0].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[1].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[2].localScale = new Vector3(1f, 1f, 1f);
			_segmentsImages[3].localScale = new Vector3(Mathf.Max(0f, (value - 0.75f) * 4), 1f, 1f);
		}
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

		// TODO: Update text
	}
}
