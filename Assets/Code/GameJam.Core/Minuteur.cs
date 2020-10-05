public class Minuteur : BaseInteractive
{
	protected void Update()
	{
		MinuteurUpdate();
	}

	protected override void OnInteractDone()
	{
		gameObject.SetActive(false);
		GameEvents.LayoutChanged?.Invoke(_gridPosition);
	}
}
