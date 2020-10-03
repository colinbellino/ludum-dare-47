using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;
using Zenject;

public class PlayState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;
	private readonly GameConfig _gameConfig;

	public PlayState(GameStateMachine machine, GameState gameState, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public void Enter(object[] parameters)
	{
		GameEvents.DayEnded += OnDayEnded;
	}

	public void Tick() { }

	public void Exit()
	{
		GameEvents.DayEnded -= OnDayEnded;
	}

	private async void OnDayEnded()
	{
		await SceneManager.LoadSceneAsync(_gameConfig.MainSceneName);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
