using UnityEngine;

public class Key : BaseInteractive
{
	[SerializeField] private GameObject _door;

	protected void Update()
	{
		BaseUpdate();
	}

	protected override void OnInteractDone()
	{
		_door.SetActive(false);
		gameObject.SetActive(false);

		GameEvents.LayoutChanged?.Invoke(new Vector3Int((int)_door.transform.position.x, (int)_door.transform.position.y, 0));
		GameEvents.LayoutChanged?.Invoke(_gridPosition);
	}
}
