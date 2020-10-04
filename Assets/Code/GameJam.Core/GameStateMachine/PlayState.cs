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

	private PlayerTag _player;
	private Camera _camera;
	private GameActions _actions;
	private Transform _cursor;
	private InteractiveElementInterface _elementSelected;

	private const float _interactRange = 2.0f;

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

		_player = Object.FindObjectOfType<PlayerTag>();
		_gameState.PlayerStartPosition = _player.transform.position;

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

			var interactiveHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _gameConfig.InteractiveLayer);
			if (interactiveHit.collider != null)
			{
				var itemInterface = interactiveHit.collider.GetComponent<InteractiveElementInterface>();
				if (itemInterface != null)
				{
					_elementSelected = itemInterface;
				}
			}
		}

		if (_gameState.PlayerDestination != null)
		{
			_cursor.position = _gameState.PlayerDestination.Value;
			_player.Follow(_cursor);

			if (_elementSelected != null)
			{
				var interactiveHit = Physics2D.Raycast(_player.transform.position, _elementSelected.position, Mathf.Infinity, _gameConfig.InteractiveLayer);
				if (interactiveHit.collider != null && interactiveHit.distance <= _interactRange)
				{
					_elementSelected.Interact();
					_elementSelected = null;
					ClearPlayerDestination();
				}
			}
		}
	}

	public void Exit()
	{
		GameEvents.DayEnded -= OnDayEnded;
	}

	private void OnDayEnded()
	{
		_player.transform.position = _gameState.PlayerStartPosition;
		ClearPlayerDestination();
	}

	private void ClearPlayerDestination()
	{
		_gameState.PlayerDestination = null;
		_player.Stop();
		_cursor.position = new Vector3(999, 999, 0);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
