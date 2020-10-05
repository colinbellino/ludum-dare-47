using UnityEngine;

public class Key : BaseInteractive
{
	[SerializeField] private GameObject _door;
	[SerializeField] protected GameObject _onDoneParticlePrefabDoor;

	protected void Update()
	{
		BaseUpdate();
	}

	protected override void OnInteractDone()
	{
		_door.SetActive(false);
		gameObject.SetActive(false);

		if (_onDoneParticlePrefabDoor == null)
		{
			return;
		}

		var instance = Instantiate(_onDoneParticlePrefabDoor);
		instance.transform.position = _door.transform.position;

		GameEvents.LayoutChanged?.Invoke(new Vector3Int((int)_door.transform.position.x, (int)_door.transform.position.y, 0));
		GameEvents.LayoutChanged?.Invoke(_gridPosition);
	}
}
