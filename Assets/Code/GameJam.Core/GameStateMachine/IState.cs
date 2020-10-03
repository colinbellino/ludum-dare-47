public interface IState
{
	void Enter(object[] parameters);
	void Tick();
	void Exit();
}
