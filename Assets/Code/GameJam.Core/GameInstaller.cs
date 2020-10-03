using Zenject;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<GameState>().AsSingle();
		Container.Bind<GameConfig>().AsSingle();

		Container.BindFactory<GameStateMachine, TitleState, TitleState.Factory>();
		Container.BindFactory<GameStateMachine, PlayState, PlayState.Factory>();
		Container.BindFactory<GameStateMachine, GameOverState, GameOverState.Factory>();
		Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();

		Container.BindInterfacesAndSelfTo<TimeLord>().AsSingle().NonLazy();

		InstallDebugTools();
	}

	private void InstallDebugTools()
	{

	}
}
