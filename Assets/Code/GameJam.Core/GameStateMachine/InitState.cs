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
		var sceneName = SceneManager.GetActiveScene().name;

		if (parameters?.Length > 0)
		{
			sceneName = (string)parameters[0];
		}

		if (sceneName == _gameConfig.TitleSceneName)
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
