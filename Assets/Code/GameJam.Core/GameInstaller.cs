using Zenject;

public class GameInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<GameState>().AsSingle();

		InstallDebugTools();
	}

	private void InstallDebugTools()
	{

	}
}
