using Zenject;

public class PlayState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;

	public PlayState(GameStateMachine machine, GameState gameState)
	{
		_machine = machine;
	}

	public void Enter(object[] parameters)
	{
		UnityEngine.Debug.Log("PLAY");
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, PlayState> { }
}
