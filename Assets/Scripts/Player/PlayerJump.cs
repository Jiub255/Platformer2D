using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerJump : MonoBehaviour
{
    public static event Action<float> OnLanded;

	[SerializeField]
	private float _jumpForce = 13f;
    [SerializeField]
    private LayerMask _groundLayer;

    private Rigidbody2D _rb;
    private CharacterAnimator _animationStateMachine;

    private bool _playerIsOnGround = true;

    [SerializeField]
    private AudioClip _jumpClip;
    [SerializeField]
    private GEAudioClip _onPlayJumpSound; 

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animationStateMachine = GetComponent<CharacterAnimator>();

        S.I.IM.PC.Gameplay.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        S.I.IM.PC.Gameplay.Jump.performed -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_playerIsOnGround)
        {
            // TODO: Try using ForceMode2D.Force and add smaller forces while button held (for a short time). 
            // Make for higher jump if you hold down jump for like a half second or a small jump if you quickly tap jump. 
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _animationStateMachine.IsJumping = true;
            _onPlayJumpSound.Invoke(_jumpClip);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_groundLayer.Contains(collision.gameObject.layer))
        {
            _playerIsOnGround = true;
            _animationStateMachine.IsJumping = false; 

            // CameraFollow listens for this, SmoothDamps y-position to new ground level + offset. 
            OnLanded?.Invoke(transform.position.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_groundLayer.Contains(collision.gameObject.layer))
        {
            _playerIsOnGround = false;
        }
    }
}