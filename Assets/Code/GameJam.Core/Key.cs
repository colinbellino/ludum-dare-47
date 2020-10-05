using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IInteractive
{
	[SerializeField] private GameObject _door;
	[SerializeField] private float _duration = 1f;
	[SerializeField] private Slider _progressSlider;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _actionStartAudioClip;
	[SerializeField] private AudioClip _actionDoneAudioClip;

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
				PlayActionDoneSoundEffect();
				GameEvents.OnKeyCollected?.Invoke(origin);
				GameEvents.InterationFinished?.Invoke(this);
			}
		}
	}

	private void PlayActionSoundEffect()
	{
		_audioSource.clip = _actionStartAudioClip;
		_audioSource.Play();
	}

	private void PlayActionDoneSoundEffect()
	{
		_audioSource.clip = _actionDoneAudioClip;
		_audioSource.Play();
	}

	public void Interact()
	{
		_inProgress = true;
		_startTime = Time.time;
		PlayActionSoundEffect();
	}

	public void CancelInteract()
	{
		_inProgress = false;
	}
}
