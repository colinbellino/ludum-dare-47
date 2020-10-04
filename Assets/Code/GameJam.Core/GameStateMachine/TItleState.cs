
using UnityEngine.InputSystem;
using Zenject;

public class TitleState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameConfig _gameConfig;

	public TitleState(GameStateMachine machine, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public void Enter(object[] parameters)
	{
		GameEvents.QuitGame += QuitGame;
		GameEvents.StartGame += StartGame;
	}

	public void Tick()
	{
		if (Keyboard.current.enterKey.wasPressedThisFrame)
		{
			StartGame();
		}
	}

	public void Exit()
	{
		GameEvents.QuitGame -= QuitGame;
		GameEvents.StartGame -= QuitGame;
	}

	private void StartGame()
	{
		_machine.Initialize(_gameConfig.MainSceneName);
	}

	private void QuitGame()
	{
		_machine.Quit();
	}

	public class Factory : PlaceholderFactory<GameStateMachine, TitleState> { }
}
