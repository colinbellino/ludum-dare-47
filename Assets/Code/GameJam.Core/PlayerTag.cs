using Pathfinding;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
	private AIDestinationSetter _aiDestination;
	private AILerp _ai;
	private IInteractive _interactingWith;
	private IInteractive _target;
	private AudioSource _audioSource;
	[SerializeField] private AudioClip _clickAudioClip;
	[SerializeField] private AudioClip _deathAudioClip;

	private const float _interactRange = 1.00001f;

	protected void Awake()
	{
		_aiDestination = GetComponent<AIDestinationSetter>();
		_ai = GetComponent<AILerp>();
		_audioSource = GetComponent<AudioSource>();
		GameEvents.StonePushed += OnActionDone;
	}

	public void Follow(Transform target)
	{
		_ai.isStopped = false;
		_aiDestination.target = target;
		_ai.SearchPath();
	}

	public void StopFollowing()
	{
		_ai.isStopped = true;
		_aiDestination.target = null;
	}

	public void SetTarget(IInteractive target)
	{
		UnityEngine.Debug.Log("Target -> " + target?.Transform.name);
		_target = target;
	}

	public bool IsInteracting => _interactingWith != null;

	public bool CanInteractWithTarget()
	{
		return _target != null && Vector3.Distance(_target.Transform.position, transform.position) <= _interactRange;
	}

	public void Interact()
	{
		if (_interactingWith == _target)
		{
			return;
		}

		CancelInteract();

		_interactingWith = _target;
		_interactingWith.Interact();
	}

	public void CancelInteract()
	{
		if (_interactingWith != null)
		{
			_interactingWith.CancelInteract();
		}
	}

	public void Reset(Vector3 position)
	{
		CancelInteract();
		_interactingWith = null;
		_target = null;
		_ai.Teleport(position, true);
		PlayDeathSoundEffect();
	}

	public void PlayClickSoundEffect()
	{
		_audioSource.clip = _clickAudioClip;
		_audioSource.Play();
	}

	private void PlayDeathSoundEffect()
	{
		_audioSource.clip = _deathAudioClip;
		_audioSource.Play();
	}

	private void OnActionDone(Vector3Int origin, Vector3Int destination)
	{
		SetTarget(null);
	}

	protected void OnDestroy()
	{
		GameEvents.StonePushed -= OnActionDone;
	}
}
