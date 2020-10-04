using UnityEngine;

[CreateAssetMenu(menuName = "LD47/Game Config")]
public class GameConfig : ScriptableObject
{
	public string TitleSceneName = "Title";
	public string MainSceneName = "Main";
	public string GroundTag = "Ground";
	public LayerMask GroundLayer;
	public LayerMask InteractiveLayer;
	public int MaximumLoop;
}
