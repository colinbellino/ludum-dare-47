using UnityEngine;

public interface IInteractive
{
	Transform Transform { get; }
	void Interact();
	void CancelInteract();
}
