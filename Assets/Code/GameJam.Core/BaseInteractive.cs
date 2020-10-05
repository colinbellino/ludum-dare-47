using UnityEngine;
using UnityEngine.UI;

public abstract class BaseInteractive : MonoBehaviour, IInteractive
{
	public Transform Transform => transform;

	[SerializeField] protected float _duration = 1f;
	[SerializeField] protected Collider2D _blockingCollider;
	[SerializeField] protected Slider _progressSlider;
	[SerializeField] protected Image _progressSliderFill;
	[SerializeField] protected AudioClip _actionStartAudioClip;
	[SerializeField] protected AudioClip _actionDoneAudioClip;
	[SerializeField] protected GameObject _onDoneParticlePrefab;
	protected AudioSource _audioSource;
	protected SpriteRenderer _renderer;
	protected bool _done;
	protected bool _inProgress;
	protected float _remaining;
	protected bool _paused;
	protected Vector3Int _gridPosition;

	protected void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_renderer = GetComponentInChildren<SpriteRenderer>();

		_remaining = _duration;
		if (_progressSliderFill)
		{
			_progressSliderFill.color = _renderer.color;
		}

		_gridPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
	}

	public void Interact()
	{
		_inProgress = true;
		_paused = false;

		PlayActionSoundEffect();
	}

	public void CancelInteract()
	{
		_paused = true;
	}

	protected void BaseUpdate()
	{
		_progressSlider.gameObject.SetActive(_done == false && _inProgress);

		if (_done)
		{
			return;
		}

		if (_inProgress)
		{
			if (_paused)
			{
				return;
			}

			_remaining -= Time.deltaTime;
			_progressSlider.value = 1f - _remaining / _duration;

			if (_remaining <= 0f)
			{
				_blockingCollider.enabled = false;
				_renderer.gameObject.SetActive(false);

				GameEvents.InterationFinished?.Invoke(this);
				PlayActionDoneSoundEffect();
				InstantiateParticlePrefab();
				_done = true;

				OnInteractDone();
			}
		}
	}

	protected void MinuteurUpdate()
	{
		_progressSlider.gameObject.SetActive(_done == false && _inProgress);

		if (_done)
		{
			return;
		}

		if (_inProgress)
		{
			_remaining -= Time.deltaTime;
			_progressSlider.value = 1f - _remaining / _duration;

			if (!_paused)
			{
				GameEvents.InterationFinished?.Invoke(this);
				if (_remaining <= 0f)
				{
					_done = true;
					PlayActionDoneSoundEffect();
					InstantiateParticlePrefab();
					OnInteractDone();
				}
				_paused = true;
			}
		}
	}

	private void PlayActionSoundEffect()
	{
		if (_actionStartAudioClip == null)
		{
			return;
		}

		_audioSource.clip = _actionStartAudioClip;
		_audioSource.Play();
	}

	private void PlayActionDoneSoundEffect()
	{
		if (_actionDoneAudioClip == null)
		{
			return;
		}

		_audioSource.clip = _actionDoneAudioClip;
		_audioSource.Play();
	}

	private void InstantiateParticlePrefab()
	{
		if (_onDoneParticlePrefab == null)
		{
			return;
		}

		var instance = Instantiate(_onDoneParticlePrefab);
		instance.transform.position = transform.position;
	}

	protected virtual void OnInteractDone() { }
}
