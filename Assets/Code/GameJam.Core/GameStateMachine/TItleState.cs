
using UnityEngine.InputSystem;
using Zenject;

public class TitleState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;
	private readonly GameConfig _gameConfig;

	public TitleState(GameStateMachine machine, GameState gameState, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public void Enter(object[] parameters) { }

	public void Tick()
	{
		if (Keyboard.current.enterKey.wasPressedThisFrame)
		{
			_machine.Initialize(_gameConfig.MainSceneName);
		}
	}

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, TitleState> { }
}
