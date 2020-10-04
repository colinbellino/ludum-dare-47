using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

public class WinState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;
	private readonly GameConfig _gameConfig;

	public WinState(GameStateMachine machine, GameState gameState, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public async void Enter(object[] parameters)
	{
		UnityEngine.Debug.Log("win !");
		await SceneManager.LoadSceneAsync(_gameConfig.WinSceneName);
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, WinState> { }
}
