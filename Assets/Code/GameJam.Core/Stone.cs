using System;
using UnityEngine;
using UnityEngine.UI;

public class Stone : MonoBehaviour, IInteractive
{
	[SerializeField] private float _duration = 1f;
	[SerializeField] private Vector2Int _direction = Vector2Int.up;
	[SerializeField] private Collider2D _blockingCollider;
	[SerializeField] private Slider _progressSlider;
	[SerializeField] private AudioClip _actionStartAudioClip;
	[SerializeField] private AudioClip _actionDoneAudioClip;
	[SerializeField] private GameObject _onDoneParticlePrefab;

	private AudioSource _audioSource;
	private SpriteRenderer _renderer;

	public Transform Transform => transform;

	private bool _done;
	private bool _inProgress;
	private float _startTime;

	protected void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
		_renderer = GetComponentInChildren<SpriteRenderer>();
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
				var initialPosition = transform.position;

				_blockingCollider.enabled = false;
				_renderer.gameObject.SetActive(false);

				var origin = new Vector3Int((int)initialPosition.x, (int)initialPosition.y, 0);
				GameEvents.StonePushed?.Invoke(origin, origin);
				GameEvents.InterationFinished?.Invoke(this);
				PlayActionDoneSoundEffect();
				InstantiateParticlePrefab();
				_done = true;
			}
		}
	}

	public void Interact()
	{
		UnityEngine.Debug.Log("Interact started: " + name);

		_inProgress = true;
		_startTime = Time.time;
		PlayActionSoundEffect();
	}

	public void CancelInteract()
	{
		_inProgress = false;

		UnityEngine.Debug.Log("Interact cancelled: " + name);
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

	private void InstantiateParticlePrefab()
	{
		var instance = Instantiate(_onDoneParticlePrefab);
		instance.transform.position = transform.position;
	}
}
