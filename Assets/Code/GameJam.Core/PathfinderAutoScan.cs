using Pathfinding;
using UnityEngine;

public class PathfinderAutoScan : MonoBehaviour
{
	private AstarPath _pathfinder;

	protected void Awake()
	{
		_pathfinder = FindObjectOfType<AstarPath>();
		_pathfinder.Scan();

		UpdateGraph(Vector3.zero);
	}

	private void UpdateGraph(Vector3 origin)
	{
		_pathfinder.UpdateGraphs(new Bounds(origin, new Vector3(10, 10, 1)));
	}
}
