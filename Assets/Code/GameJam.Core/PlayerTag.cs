using Pathfinding;
using UnityEngine;

public class PlayerTag : MonoBehaviour
{
	public AIDestinationSetter AiDestination { get; private set; }

	protected void Awake()
	{
		AiDestination = GetComponent<AIDestinationSetter>();
	}
}
