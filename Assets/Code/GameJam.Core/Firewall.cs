public class Firewall : BaseInteractive
{
	protected void Update()
	{
		BaseUpdate();
	}

	protected override void OnInteractDone()
	{
		GameEvents.LayoutChanged?.Invoke(_gridPosition);
	}
}
