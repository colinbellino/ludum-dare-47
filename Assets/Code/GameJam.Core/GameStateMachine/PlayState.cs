using Cysharp.Threading.Tasks;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PlayState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;
	private readonly GameConfig _gameConfig;

	private Camera _camera;
	private GameActions _actions;
	private Transform _cursor;

	public PlayState(GameStateMachine machine, GameState gameState, GameConfig gameConfig)
	{
		_machine = machine;
		_gameState = gameState;
		_gameConfig = gameConfig;
	}

	public void Enter(object[] parameters)
	{
		_actions = new GameActions();
		_actions.Enable();

		_camera = Camera.main;
		_cursor = GameObject.Find("Cursor").transform; // TODO: Clean this

		GameEvents.DayEnded += OnDayEnded;
	}

	public void Tick()
	{
		var mousePosition = _actions.Gameplay.MousePosition.ReadValue<Vector2>();

		if (_actions.Gameplay.Action1.ReadValue<float>() > 0f)
		{
			var ray = _camera.ScreenPointToRay(mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 999f, Color.red);
			var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _gameConfig.GroundLayer);
			if (hit.collider != null)
			{
				_gameState.PlayerDestination = hit.point;
			}
		}

		if (_gameState.PlayerDestination != null)
		{
			_cursor.transform.position = _gameState.PlayerDestination.Value;
		}
	}

	public void Exit()
	{
		GameEvents.DayEnded -= OnDayEnded;
	}

	private async void OnDayEnded()
	{
		// await SceneManager.LoadSceneAsync(_gameConfig.MainSceneName);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
