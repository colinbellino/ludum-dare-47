using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
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

	public async void Enter(object[] parameters)
	{
		await SceneManager.LoadSceneAsync(_gameConfig.TitleSceneName);
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, TitleState> { }
}
