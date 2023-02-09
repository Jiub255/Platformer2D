using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
	public float MoveX { get; set; }
	public float LastX { get; set; }
	public bool IsJumping { get; set; }

	[SerializeField]
	private float _timePerFrame = 0.16f; 

	private SpriteRenderer _spriteRenderer; 

	// States 
	private SpriteAnimation _walkAnimation; 
	private SpriteAnimation _jumpAnimation; 
	private SpriteAnimation _idleAnimation;

	private SpriteAnimation _currentAnimation; 

	// State Sprites 
	[SerializeField]
	private List<Sprite> _walkSprites;
	[SerializeField]
	private List<Sprite> _jumpSprites;
	[SerializeField]
	private List<Sprite> _idleSprites;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

		_walkAnimation = new SpriteAnimation(_walkSprites, _spriteRenderer, _timePerFrame);
		_jumpAnimation = new SpriteAnimation(_jumpSprites, _spriteRenderer, _timePerFrame);
		_idleAnimation = new SpriteAnimation(_idleSprites, _spriteRenderer, _timePerFrame);

		_currentAnimation = _walkAnimation;
    }

    private void Update()
    {
		// Flips sprite if facing left. 
		if (LastX < 0f)
        {
			transform.localScale = new Vector3(-1f, 1f, 1f);
        }
		else
        {
			transform.localScale = new Vector3(1f, 1f, 1f);
        }

		// Update current animation. 
		SpriteAnimation previousAnimation = _currentAnimation;

		// Jumping 
		if (IsJumping)
        {
			_currentAnimation = _jumpAnimation; 
		}
        else
        {
			// Idle 
			if (Mathf.Abs(MoveX) < 0.5f)
            {
				_currentAnimation = _idleAnimation;
            }
            // Moving 
            else
            {
				_currentAnimation = _walkAnimation; 
            }
        }

		// Run current animation's Start method if changed this frame. 
		if (_currentAnimation != previousAnimation)
        {
			_currentAnimation.Start();
        }

		// Run current animation's Update method. 
		_currentAnimation.HandleUpdate();
    }
}