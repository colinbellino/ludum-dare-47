using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
	[SerializeField] private Tilemap _waterTilemap;
	[SerializeField] private Tilemap _groundTilemap;

	protected void Awake()
	{
		GameEvents.StonePushed += OnStonePushed;
	}

	private async void OnStonePushed(Vector3Int origin, Vector3Int destination)
	{
		_waterTilemap.SetTile(destination, null);
		UnityEngine.Debug.Log(" ??");

		await UniTask.NextFrame();

		GameEvents.LayoutChanged?.Invoke(origin);
	}
}