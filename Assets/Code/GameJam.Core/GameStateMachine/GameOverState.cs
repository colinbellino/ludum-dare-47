using System.Linq;
using UnityEngine;
using Zenject;

public class GameOverState : IState
{
	private readonly GameStateMachine _machine;
	private readonly GameState _gameState;

	public GameOverState(GameStateMachine machine, GameState gameState)
	{
		_machine = machine;
	}

	public void Enter(object[] parameters)
	{
		UnityEngine.Debug.Log("game over");
	}

	public void Tick() { }

	public void Exit() { }

	public class Factory : PlaceholderFactory<GameStateMachine, GameOverState> { }
}
