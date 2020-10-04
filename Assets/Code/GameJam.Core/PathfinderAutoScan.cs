using UnityEngine;

public class PathfinderAutoScan : MonoBehaviour
{
	private AstarPath _pathfinder;

	protected void Awake()
	{
		_pathfinder = FindObjectOfType<AstarPath>();

		GameEvents.LayoutChanged += UpdateGraph;
	}

	private void UpdateGraph(Vector3 origin)
	{
		UnityEngine.Debug.Log("UpdateGraph at " + origin);
		_pathfinder.UpdateGraphs(new Bounds(origin, new Vector3(10f, 10f, 1f)));
	}

	// [ContextMenu("Pouet")]
	// private void Pouet()
	// {
	// 	UpdateGraph(new Vector3(11f, 13f, 0f));
	// }
}
