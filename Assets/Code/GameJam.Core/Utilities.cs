using UnityEngine;

public static class Utilities
{
	public static Vector3 MousePointerToWorldPosition(Camera camera, Vector2 mousePosition)
	{
		return camera.ScreenToWorldPoint(
			new Vector3(mousePosition.x, mousePosition.y, -camera.transform.position.z)
		);
	}
}
