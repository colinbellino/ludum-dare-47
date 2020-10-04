using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour, IInteractive
{
	[SerializeField] private float _duration = 1f;
	[SerializeField] private Vector2Int _direction = Vector2Int.up;
	[SerializeField] private Collider2D _blockingCollider;
	[SerializeField] private Slider _progressSlider;

	public Transform Transform => transform;

	private bool _done;
	private bool _inProgress;
	private float _startTime;

	protected void Update()
	{
		_progressSlider.gameObject.SetActive(_done == false && _inProgress);

		if (_done)
		{
			return;
		}

		if (_inProgress)
		{
			_progressSlider.value = (Time.time - _startTime) / _duration;

			if (Time.time > _startTime + _duration)
			{
				var initialPosition = transform.position;

				transform.position += (Vector3Int)_direction;
				_blockingCollider.enabled = false;

				var origin = new Vector3Int((int)initialPosition.x, (int)initialPosition.y, 0);
				var destination = origin + (Vector3Int)_direction;
				GameEvents.StonePushed?.Invoke(origin, destination);

				_done = true;
			}
		}
	}

	public void Interact()
	{
		UnityEngine.Debug.Log("Interact started: " + name);

		_inProgress = true;
		_startTime = Time.time;
	}

	public void CancelInteract()
	{
		_inProgress = false;

		UnityEngine.Debug.Log("Interact cancelled: " + name);
	}
}
