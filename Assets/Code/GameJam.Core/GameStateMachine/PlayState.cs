using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
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
	private bool _clickSoundWasPlay;

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
		_gameState.LoopCount = 0;
		_gameState.PlayerDestination = null;
		_gameState.PlayerStartPosition = _player.transform.position;

		_camera = Camera.main;
		_cursor = GameObject.Find("Cursor").transform; // TODO: Clean this

		GameEvents.GameStarted?.Invoke();

		GameEvents.DayEnded += OnDayEnded;
		GameEvents.ExitReached += OnExitReached;
	}

	public void Tick()
	{
		if (_gameState.LoopCount >= _gameConfig.MaximumLoop)
		{
			_machine.GameOver();
			return;
		}

		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			_machine.Initialize(_gameConfig.TitleSceneName);
			return;
		}

#if UNITY_EDITOR
		if (Keyboard.current.rKey.wasPressedThisFrame)
		{
			UnityEngine.Debug.Log("Restarting...");
			_machine.Initialize();
			return;
		}
#endif

		var action1wasReleased = _actions.Gameplay.Action1.ReadValue<float>() > 0f;
		if (action1wasReleased)
		{
			if (!_clickSoundWasPlay)
			{
				_player.PlayClickSoundEffect();
				_clickSoundWasPlay = true;
			}

			var mousePosition = _actions.Gameplay.MousePosition.ReadValue<Vector2>();
			var ray = _camera.ScreenPointToRay(mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 999f, Color.red, 1f);
			var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, _gameConfig.GroundLayer | _gameConfig.InteractiveLayer);
			foreach (var hit in hits)
			{
				if (hit.collider != null)
				{
					_gameState.PlayerDestination = new Vector3(
						Mathf.Round(hit.point.x),
						Mathf.Round(hit.point.y)
					);
					_player.Follow(_cursor);

					var interactive = hit.collider.GetComponent<IInteractive>();
					if (interactive != null)
					{
						_player.SetTarget(interactive);
					}
				}
			}
		}
		else
		{
			_clickSoundWasPlay = false;
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
			}
		}
	}

	public void Exit()
	{
		GameEvents.DayEnded -= OnDayEnded;
		GameEvents.ExitReached -= OnExitReached;
	}

	private void OnDayEnded()
	{
		_player.Reset();
		ClearPlayerDestination();
	}

	private void OnExitReached()
	{
		_machine.Win();
	}

	private void ClearPlayerDestination()
	{
		_gameState.PlayerDestination = null;
		_player.StopFollowing();
		_cursor.position = new Vector3(999, 999, 0);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
