using Cysharp.Threading.Tasks;
using UnityEngine;

public interface InteractiveElementInterface
{
	float DurationLength { get; }
	void Interact();
	Vector3 position { get; }
}

public class Stone : MonoBehaviour, InteractiveElementInterface
{
	[SerializeField] private float _durationLength;
	private bool _isPushed;

	public Vector3 position { get; private set; }
	public float DurationLength { get; private set; }

	void Awake()
	{
		DurationLength = _durationLength;
		_isPushed = false;
		position = transform.position;
	}

	public async void Interact()
	{
		_isPushed = true;
		transform.position += Vector3.up;

		await UniTask.NextFrame();

		GameEvents.LayoutChanged?.Invoke(position);
	}
}
