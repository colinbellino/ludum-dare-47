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
		GameEvents.BackToTitle += BackToTitle;

		await SceneManager.LoadSceneAsync(_gameConfig.WinSceneName);
	}

	public void Tick() { }

	public void Exit()
	{
		GameEvents.BackToTitle -= BackToTitle;
	}

	private void BackToTitle()
	{
		_machine.Initialize(_gameConfig.TitleSceneName);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, WinState> { }
}
