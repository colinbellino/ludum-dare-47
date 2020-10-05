using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

		var startImage = GameObject.Find("Press Start").GetComponent<Image>();
		startImage.color = Color.clear;

		await UniTask.Delay(1000);

		await startImage.DOColor(Color.white, duration: 1f).AsyncWaitForCompletion();

		await UniTask.Delay(4000);

		startImage.DOColor(_gameConfig.Color4, duration: 1f).SetLoops(50, LoopType.Yoyo);
	}

	public void Tick()
	{
		if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			_machine.TitleScreen();
		}
		else if (Keyboard.current.anyKey.wasPressedThisFrame)
		{
			_machine.TitleScreen();
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

	public class Factory : PlaceholderFactory<GameStateMachine, WinState> { }
}
