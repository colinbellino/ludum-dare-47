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
		GameEvents.OnKeyCollected += OnKeyCollected;
	}

	protected void OnDestroy()
	{
		GameEvents.StonePushed -= OnStonePushed;
		GameEvents.OnKeyCollected -= OnKeyCollected;
	}

	private async void OnStonePushed(Vector3Int origin, Vector3Int destination)
	{
		_waterTilemap.SetTile(destination, null);

		await UniTask.NextFrame();

		GameEvents.LayoutChanged?.Invoke(origin);
	}

	private async void OnKeyCollected(Vector3Int origin)
	{
		await UniTask.NextFrame();

		GameEvents.LayoutChanged?.Invoke(origin);
	}
}
