using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;
	private readonly GameConfig _gameConfig;

	public GameOverState(GameStateMachine machine, GameState gameState, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public async void Enter(object[] parameters)
	{
		UnityEngine.Debug.Log("game over");
		await SceneManager.LoadSceneAsync(_gameConfig.GameOverSceneName);
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, GameOverState> { }
}
