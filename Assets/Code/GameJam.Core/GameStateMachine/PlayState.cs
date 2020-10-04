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
	private IInteractive _elementSelected;

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
		if (_gameState.LoopCount >= _gameConfig.MaximumLoop)
		{
			_machine.GameOver();
			return;
		}

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

			GetClosestInteractiveElement(ray);
		}

		if (_gameState.PlayerDestination != null)
		{
			_cursor.position = _gameState.PlayerDestination.Value;
			_player.Follow(_cursor);

			InteractWithElement();
		}
	}

	public void Exit()
	{
		GameEvents.DayEnded -= OnDayEnded;
	}

	private void OnDayEnded()
	{
		_player.transform.position = _gameState.PlayerStartPosition;
		_gameState.PlayerActionStartTime = 0;
		ClearPlayerDestination();
	}

	private void ClearPlayerDestination()
	{
		_gameState.PlayerDestination = null;
		_player.Stop();
		_cursor.position = new Vector3(999, 999, 0);
	}

	private void GetClosestInteractiveElement(Ray ray)
	{
		var interactiveHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _gameConfig.InteractiveLayer);
		if (interactiveHit.collider != null)
		{
			var itemInterface = interactiveHit.collider.GetComponent<IInteractive>();
			if (itemInterface != null)
			{
				_elementSelected = itemInterface;
			}
		}
	}

	private void InteractWithElement()
	{
		if (_elementSelected == null)
		{
			_gameState.PlayerActionStartTime = 0;
			return;
		}

		var interactiveHit = Physics2D.Raycast(_player.transform.position, _elementSelected.position, Mathf.Infinity, _gameConfig.InteractiveLayer);
		if (interactiveHit.collider == null)
		{
			_gameState.PlayerActionStartTime = 0;
			return;
		}

		if (interactiveHit.distance <= _interactRange)
		{
			if (_gameState.PlayerActionStartTime != 0)
			{
				UnityEngine.Debug.Log(((Time.time - _gameState.PlayerActionStartTime) / _elementSelected.DurationLength) * 100 + "%");
				if (_elementSelected.DurationLength >= Time.time - _gameState.PlayerActionStartTime)
				{
					return;
				}

				_elementSelected.Interact();
				_elementSelected = null;
				_gameState.PlayerActionStartTime = 0;
				return;
			}
			_gameState.PlayerActionStartTime = Time.time;

			return;
		}

		_gameState.PlayerActionStartTime = 0;

	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
