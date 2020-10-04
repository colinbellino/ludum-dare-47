using UnityEngine;

[CreateAssetMenu(menuName = "LD47/Game Config")]
public class GameConfig : ScriptableObject
{
	public string TitleSceneName = "Title";
	public string MainSceneName = "Main";
	public string GameOverSceneName = "GameOver";
	public string WinSceneName = "Win";
	public string GroundTag = "Ground";
	public LayerMask GroundLayer;
	public LayerMask InteractiveLayer;
	public int MaximumLoop = 100;
	public float DayDuration = 10f;

	public Color Color1;
	public Color Color2;
	public Color Color3;
	public Color Color4;
	public Color Color5;
}
