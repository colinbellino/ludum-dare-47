using Cysharp.Threading.Tasks;
using UnityEngine;

public interface InteractiveElementInterface
{
	void Interact();
	Vector3 position { get; }
}

public class Stone : MonoBehaviour, InteractiveElementInterface
{
	private bool _isPushed;
	public Vector3 position { get; private set; }

	void Awake()
	{
		_isPushed = false;
		position = transform.position;
	}

	public async void Interact()
	{
		_isPushed = true;
		// gameObject.GetComponent<Collider2D>().enabled = false;
		transform.position += Vector3.up;

		await UniTask.NextFrame();

		GameEvents.LayoutChanged?.Invoke(position);
	}
}
