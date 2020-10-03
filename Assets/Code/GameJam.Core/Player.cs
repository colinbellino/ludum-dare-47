using Pathfinding;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private Transform _cursor;
	[SerializeField] private float _speed = 1f;

	private AIPath _aiPath;
	private Camera _camera;
	private GameActions _actions;
	private Vector3? _destination;

	protected void Awake()
	{
		_actions = new GameActions();
		_actions.Enable();

		_camera = Camera.main;
		_aiPath = GetComponent<AIPath>();

		_aiPath.maxSpeed = _speed;
	}

	protected void Update()
	{
		var mousePosition = _actions.Gameplay.MousePosition.ReadValue<Vector2>();

		if (_actions.Gameplay.Action1.ReadValue<float>() > 0f)
		{
			var ray = _camera.ScreenPointToRay(mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 999f, Color.red);
			if (Physics.Raycast(ray.origin, ray.direction, out var hit, Mathf.Infinity, _groundMask))
			{
				if (hit.collider.CompareTag(Settings.GroundTag))
				{
					_destination = hit.point;
				}
				else
				{
					UnityEngine.Debug.Log("Can't move here (" + hit.collider.name + ").");
				}
			}
		}

		if (_destination != null)
		{
			_cursor.transform.position = _destination.Value;
		}
	}
}
