using Pathfinding;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
	private AIDestinationSetter _aiDestination;
	private AIPath _aiPath;
	private IInteractive _interactingWith;
	private IInteractive _target;
	private AudioSource _audioSource;
	[SerializeField] private AudioClip _clickAudioClip;
	[SerializeField] private AudioClip _deathAudioClip;

	private const float _interactRange = 1.5f;

	protected void Awake()
	{
		_aiDestination = GetComponent<AIDestinationSetter>();
		_aiPath = GetComponent<AIPath>();
		_audioSource = GetComponent<AudioSource>();
	}

	public void Follow(Transform target)
	{
		_aiPath.isStopped = false;
		_aiDestination.target = target;
	}

	public void StopFollowing()
	{
		_aiPath.isStopped = true;
		_aiDestination.target = null;
	}

	public void SetTarget(IInteractive target)
	{
		UnityEngine.Debug.Log("-> " + target.Transform.name);
		_target = target;
	}

	public bool IsInteracting => _interactingWith != null;

	public bool IsTargetInRange()
	{
		return _target != null && Vector3.Distance(_target.Transform.position, transform.position) <= _interactRange;
	}

	public void Interact()
	{
		if (_interactingWith == _target)
		{
			return;
		}

		if (_interactingWith != null)
		{
			CancelInteract();
		}

		_interactingWith = _target;
		_interactingWith.Interact();
	}

	public void CancelInteract()
	{
		_interactingWith.CancelInteract();
	}

	public void Reset()
	{
		CancelInteract();
		_interactingWith = null;
		_target = null;
		PlayDeathSoundEffect();
	}

	public void PlayClickSoundEffect()
	{
		_audioSource.clip = _clickAudioClip;
		_audioSource.Play();
	}

	public void PlayDeathSoundEffect()
	{
		_audioSource.clip = _deathAudioClip;
		_audioSource.Play();
	}
}
