using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IInteractive
{
	[SerializeField] private GameObject _door;
	[SerializeField] private float _duration = 1f;
	[SerializeField] private Slider _progressSlider;

	private bool _inProgress;
	private bool _done;
	private float _startTime;
	public Transform Transform { get; private set; }

	protected void Awake()
	{
		Transform = transform;
	}

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
				var initialPosition = _door.transform.position;
				var origin = new Vector3Int((int)initialPosition.x, (int)initialPosition.y, 0);
				_door.SetActive(false);
				gameObject.SetActive(false);

				GameEvents.OnKeyCollected?.Invoke(origin);
				GameEvents.InterationFinished?.Invoke(this);
				// _door.GetComponent<Collider2D>().enabled = false;
			}
		}
	}

	public void Interact()
	{
		_inProgress = true;
		_startTime = Time.time;
	}

	public void CancelInteract()
	{
		_inProgress = false;
	}
}
