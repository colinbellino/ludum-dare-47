using UnityEngine;
using System.Collections.Generic;
using Zenject;

public enum GameStates { Init, Title, Play, GameOver }

public class GameStateMachine : IInitializable, ITickable
{
	private readonly GameConfig _config;
	private readonly Dictionary<GameStates, IState> _states;
	private IState _currentState;

	public GameStateMachine(
		GameConfig config,
		InitState.Factory initStateFactory,
		TitleState.Factory titleStateFactory,
		PlayState.Factory playStateFactory,
		GameOverState.Factory gameOverStateFactory
	)
	{
		_config = config;
		_states = new Dictionary<GameStates, IState> {
			{ GameStates.Init, initStateFactory.Create(this) },
			{ GameStates.Title, titleStateFactory.Create(this) },
			{ GameStates.Play, playStateFactory.Create(this) },
			{ GameStates.GameOver, gameOverStateFactory.Create(this) },
		};
	}

	public void Initialize()
	{
		ChangeState(GameStates.Init);
	}

	public void Tick() => _currentState?.Tick();

	public void TitleScreen() => ChangeState(GameStates.Title);

	public void Play() => ChangeState(GameStates.Play);

	public void GameOver() => ChangeState(GameStates.GameOver);

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	private void ChangeState(GameStates state, object[] parameters = null)
	{
		if (_states.ContainsKey(state) == false)
		{
			Debug.LogError("Invalid state: " + state);
		}

		// Debug.Log("Enter state: " + state);
		_currentState?.Exit();

		_currentState = _states[state];
		_currentState?.Enter(parameters);
	}
}
