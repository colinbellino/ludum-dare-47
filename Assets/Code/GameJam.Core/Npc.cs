using ModestTree;
using Pathfinding;
using UnityEngine;

public class Npc : MonoBehaviour
{
	[SerializeField] private Transform[] _targetsPosition;

	private AIDestinationSetter _aiDestinationSetter;
	private int index;

    // Start is called before the first frame update
    void Awake()
    {
	    index = 0;
	    _aiDestinationSetter = GetComponent<AIDestinationSetter>();
	    _aiDestinationSetter.target = _targetsPosition[index];
    }

    // Update is called once per frame
    void Update()
    {
	    var distance = Vector3.Distance(_targetsPosition[index].position, transform.position);

	    if (distance <= 1.0f)
	    {
		    if (index >= _targetsPosition.Length - 1)
		    {
			    index = 0;
		    }
		    else
		    {
			    index++;
		    }

		    _aiDestinationSetter.target = _targetsPosition[index];
	    }
    }
}
