using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField] private GameConfig _gameConfig;

	public override void InstallBindings()
	{
		Container.Bind<GameState>().AsSingle();
		Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();

		Container.BindFactory<GameStateMachine, InitState, InitState.Factory>();
		Container.BindFactory<GameStateMachine, TitleState, TitleState.Factory>();
		Container.BindFactory<GameStateMachine, PlayState, PlayState.Factory>();
		Container.BindFactory<GameStateMachine, GameOverState, GameOverState.Factory>();
		Container.BindFactory<GameStateMachine, WinState, WinState.Factory>();
		Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();

		Container.BindInterfacesAndSelfTo<TimeLord>().AsSingle().NonLazy();

		InstallDebugTools();
	}

	private void InstallDebugTools()
	{

	}
}
