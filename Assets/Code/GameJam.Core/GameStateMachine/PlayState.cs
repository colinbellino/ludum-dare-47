using Cysharp.Threading.Tasks;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
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
		_gameState.TimeStart = 0;
		_gameState.LoopCount = 0;
		_gameState.PlayerDestination = null;
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
		var action1wasReleased = _actions.Gameplay.Action1.ReadValue<float>() > 0f;

		if (Keyboard.current.rKey.wasPressedThisFrame)
		{
			UnityEngine.Debug.Log("Restarting...");
			_machine.Initialize();
			return;
		}

		if (action1wasReleased)
		{
			var ray = _camera.ScreenPointToRay(mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 999f, Color.red, 1f);
			var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, _gameConfig.GroundLayer | _gameConfig.InteractiveLayer);
			foreach (var hit in hits)
			{
				if (hit.collider != null)
				{
					_gameState.PlayerDestination = hit.point;

					var interactive = hit.collider.GetComponent<IInteractive>();
					if (interactive != null)
					{
						_player.SetTarget(interactive);
					}
				}
			}
		}

		if (_player.IsTargetInRange())
		{
			ClearPlayerDestination();
			_player.Interact();
		}
		else
		{

			if (_gameState.PlayerDestination != null)
			{
				_cursor.position = _gameState.PlayerDestination.Value;
				_player.Follow(_cursor);
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
		_player.Reset();
		ClearPlayerDestination();
	}

	private void ClearPlayerDestination()
	{
		_gameState.PlayerDestination = null;
		_player.StopFollowing();
		_cursor.position = new Vector3(999, 999, 0);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
