using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
		GameEvents.BackToTitle += BackToTitle;

		await SceneManager.LoadSceneAsync(_gameConfig.GameOverSceneName);

		await UniTask.Delay(10000);

		_machine.Initialize(_gameConfig.TitleSceneName);
	}

	public void Tick()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			_machine.Initialize(_gameConfig.TitleSceneName);
		}
		else if (Keyboard.current.anyKey.wasPressedThisFrame)
		{
			_machine.Initialize(_gameConfig.TitleSceneName);
		}
	}

	public void Exit()
	{
		GameEvents.BackToTitle -= BackToTitle;
	}

	private void BackToTitle()
	{
		_machine.Initialize(_gameConfig.TitleSceneName);
	}

	public class Factory : PlaceholderFactory<GameStateMachine, GameOverState> { }
}
