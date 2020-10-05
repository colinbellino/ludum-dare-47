using UnityEngine;
using UnityEngine.UI;

public class Exit : BaseInteractive
{
	protected void Update()
	{
		BaseUpdate();
	}

	protected override void OnInteractDone()
	{
		GameEvents.ExitReached?.Invoke();
	}
}
