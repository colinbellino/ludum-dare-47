using UnityEngine;

public interface InteractiveElementInterface
{
	void Interact();
	Vector3 position { get; }
}

public class Stone: MonoBehaviour, InteractiveElementInterface
{
	private bool _isPushed;
	public Vector3 position { get; private set; }

	void Awake()
	{
		_isPushed = false;
		position = transform.position;
	}

	public void Interact()
	{
		_isPushed = true;
		// gameObject.GetComponent<Collider2D>().enabled = false;
		transform.position = new Vector3(8,14.5f);
	}
}
