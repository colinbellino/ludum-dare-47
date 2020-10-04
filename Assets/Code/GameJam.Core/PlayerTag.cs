using System;
using Pathfinding;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
	private AIDestinationSetter _aiDestination;
	private AIPath _aiPath;

	protected void Awake()
	{
		_aiDestination = GetComponent<AIDestinationSetter>();
		_aiPath = GetComponent<AIPath>();
	}

	internal void Follow(Transform target)
	{
		_aiPath.isStopped = false;
		_aiDestination.target = target;
	}

	internal void Stop()
	{
		_aiPath.isStopped = true;
		_aiDestination.target = null;
	}
}
