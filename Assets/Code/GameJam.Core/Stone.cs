using UnityEngine;

public interface IInteractive
{
	float DurationLength { get; }
	void Interact();
	Vector3 position { get; }
}

public class Stone : MonoBehaviour, IInteractive
{
	[SerializeField] private float _durationLength;
	private bool _isPushed;

	public Vector3 position { get; private set; }
	public float DurationLength { get; private set; }

	protected void Awake()
	{
		DurationLength = _durationLength;
		_isPushed = false;
		position = transform.position;
	}

	public void Interact()
	{
		_isPushed = true;
		transform.position += Vector3.up;
		// TODO: Get this with [SerializeField]
		GetComponent<Collider2D>().enabled = false;

		var origin = new Vector3Int((int)position.x, (int)position.y, 0);
		var destination = origin + Vector3Int.up;
		GameEvents.StonePushed?.Invoke(origin, destination);
	}
}
