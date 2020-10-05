
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;
using Cysharp.Threading.Tasks;

public class StaticKillDOTween
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void Init()
	{
		DOTween.KillAll();
	}
}

public class TitleState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameConfig _gameConfig;

	public TitleState(GameStateMachine machine, GameConfig gameConfig)
	{
		_machine = machine;
		_gameConfig = gameConfig;
	}

	public async void Enter(object[] parameters)
	{
		GameEvents.QuitGame += QuitGame;
		GameEvents.StartGame += StartGame;

		var startImage = GameObject.Find("Press Start").GetComponent<Image>();
		startImage.color = Color.clear;

		await UniTask.Delay(1000);

		await startImage.DOColor(Color.white, duration: 1f).AsyncWaitForCompletion();

		await UniTask.Delay(4000);

		startImage.DOColor(_gameConfig.Color4, duration: 1f).SetLoops(50, LoopType.Yoyo);
	}

	public void Tick()
	{
		if (Keyboard.current.escapeKey.wasPressedThisFrame)
		{
			QuitGame();
		}
		else if (Mouse.current.leftButton.wasPressedThisFrame)
		{
			StartGame();
		}
		else if (Keyboard.current.anyKey.wasPressedThisFrame)
		{
			StartGame();
		}
	}

	public void Exit()
	{
		GameEvents.QuitGame -= QuitGame;
		GameEvents.StartGame -= QuitGame;
	}

	private void StartGame()
	{
		_machine.Initialize(_gameConfig.MainSceneName);
	}

	private void QuitGame()
	{
		_machine.Quit();
	}

	public class Factory : PlaceholderFactory<GameStateMachine, TitleState> { }
}
