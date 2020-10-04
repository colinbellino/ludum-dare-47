using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour, IInteractive
{
	[SerializeField] private float _duration = 1f;
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
				GameEvents.ExitReached?.Invoke();

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
