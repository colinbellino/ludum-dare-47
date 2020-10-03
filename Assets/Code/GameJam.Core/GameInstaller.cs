using Zenject;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<GameState>().AsSingle();

		Container.BindInterfacesAndSelfTo<TimeLord>().AsSingle().NonLazy();

		InstallDebugTools();
	}

	private void InstallDebugTools()
	{

	}
}
