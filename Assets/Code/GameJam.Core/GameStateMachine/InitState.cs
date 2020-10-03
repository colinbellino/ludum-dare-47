using UnityEngine.SceneManagement;
using Zenject;

public class InitState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameConfig _gameConfig;

	public InitState(GameStateMachine machine, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public void Enter(object[] parameters)
	{
		if (SceneManager.GetActiveScene().name == _gameConfig.TitleSceneName)
		{
			_machine.TitleScreen();
		}
		else
		{
			_machine.Play();
		}
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, InitState> { }
}
