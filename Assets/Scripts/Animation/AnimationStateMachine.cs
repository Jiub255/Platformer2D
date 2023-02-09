using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateMachine : MonoBehaviour
{
	public float MoveX { get; set; }
	public float LastX { get; set; }
	public bool IsJumping { get; set; }

	private SpriteRenderer _spriteRenderer; 

	// States 
	private SpriteAnimator _walkAnimation; 
	private SpriteAnimator _jumpAnimation; 
	private SpriteAnimator _idleAnimation;

	private SpriteAnimator _currentAnimation; 

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

		_walkAnimation = new SpriteAnimator(_walkSprites, _spriteRenderer);
		_jumpAnimation = new SpriteAnimator(_jumpSprites, _spriteRenderer);
		_idleAnimation = new SpriteAnimator(_idleSprites, _spriteRenderer);

		_currentAnimation = _walkAnimation;
    }

    private void Update()
    {
		// Flips sprite if facing left. 
		if (LastX < 0f)
        {
			transform.localScale = new Vector3(-1f, 0f, 0f);
        }
		else
        {
			transform.localScale = new Vector3(1f, 0f, 0f);
        }

		// Update current animation. 
		SpriteAnimator previousAnimation = _currentAnimation;

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