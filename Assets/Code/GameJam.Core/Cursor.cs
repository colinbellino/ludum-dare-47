using UnityEngine;

public class Cursor : MonoBehaviour
{
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private Sprite _spriteCursorGround;
	[SerializeField] private Sprite _spriteCursorItem;

	protected void Awake()
	{
		GameEvents.TargetSelected += ItemSelectedCursor;
		GameEvents.TargetUnSelected += GroundCursor;
	}

	private void ItemSelectedCursor(IInteractive interactive)
	{
		_spriteRenderer.sprite = _spriteCursorItem;
	}

	private void GroundCursor()
	{
		_spriteRenderer.sprite = _spriteCursorGround;
	}

	protected void OnDestroy()
	{
		GameEvents.TargetSelected -= ItemSelectedCursor;
		GameEvents.TargetUnSelected -= GroundCursor;
	}
}
