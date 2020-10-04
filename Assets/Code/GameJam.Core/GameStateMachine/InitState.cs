using UnityEngine.SceneManagement;
using Zenject;
using Cysharp.Threading.Tasks;

public class InitState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameConfig _gameConfig;

	public InitState(GameStateMachine machine, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public async void Enter(object[] parameters)
	{
		if (parameters?.Length > 0)
		{
			await SceneManager.LoadSceneAsync((string)parameters[0]);
			_machine.Play();

			return;
		}

		if (SceneManager.GetActiveScene().name == _gameConfig.TitleSceneName)
		{
			await SceneManager.LoadSceneAsync(_gameConfig.TitleSceneName);
			_machine.TitleScreen();
		}
		else
		{
			await SceneManager.LoadSceneAsync(_gameConfig.MainSceneName);
			_machine.Play();
		}
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, InitState> { }
}
