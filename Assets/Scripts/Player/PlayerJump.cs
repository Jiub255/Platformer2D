using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerJump : MonoBehaviour
{
    public static event Action<float> OnLanded;

	[SerializeField, Range(5f, 50f)]
	private float _jumpForce = 18f;

    [SerializeField, Range(0.25f, 5f)]
    private float _gravityScale = 1f;
    [SerializeField, Range(0.5f, 10f)]
    private float _fallingGravityScale = 2.5f;

    [SerializeField, Range(5f, 50f)]
    private float _maxFallSpeed = 18f;

    [SerializeField, Range(0f, 1f)]
    private float _jumpCutMultiplier = 0.5f;

    [SerializeField, Range(0f, 0.5f)]
    private float _jumpBufferTime = 0.2f; 
    [SerializeField, Range(0f, 0.5f)]
    private float _jumpCoyoteTime = 0.2f; 

    [SerializeField]
    private LayerMask _groundLayers;
    [SerializeField]
    private BoolSO _isGroundedSO;

    [SerializeField]
    private AudioClip _jumpClip;
    [SerializeField]
    private GEAudioClip _onPlayJumpSound; 

    private Rigidbody2D _rb;
    private CharacterAnimator _animationStateMachine;

    public float _lastJumpTime { get; private set; }
    public float _lastGroundedTime { get; private set; }
    private bool _isJumping = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animationStateMachine = GetComponent<CharacterAnimator>();

        S.I.IM.PC.Gameplay.Jump.started += OnJump;
        S.I.IM.PC.Gameplay.Jump.canceled += CutJump;
    }

    private void OnDisable()
    {
        S.I.IM.PC.Gameplay.Jump.started -= OnJump;
        S.I.IM.PC.Gameplay.Jump.canceled -= CutJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _lastJumpTime = _jumpBufferTime;
    }

    private void Jump(/*InputAction.CallbackContext context*/)
    {
        //if (_isGroundedSO.Value)
        {
            // TODO: Try using ForceMode2D.Force and add smaller forces while button held (for a short time). 
            // Make for higher jump if you hold down jump for like a half second or a small jump if you quickly tap jump. 

            _rb.velocity = new Vector2(_rb.velocity.x, 0f); 
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            //_animationStateMachine.IsJumping = true;
            _onPlayJumpSound.Invoke(_jumpClip);
            _isJumping = true;
        }
    }

    private void CutJump(InputAction.CallbackContext context)
    {
        if (_rb.velocity.y > 0 && !_isGroundedSO.Value)
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);
        }

        _lastJumpTime = 0f;
    }

    private void Update()
    {
        if (!_isGroundedSO.Value)
        {
            _lastGroundedTime -= Time.deltaTime;
        }

        _lastJumpTime -= Time.deltaTime;

        if (_lastGroundedTime > 0 && _lastJumpTime > 0 && !_isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.y > 0f)
        {
            _rb.gravityScale = _gravityScale;
        }
        else
        {
            _rb.gravityScale = _fallingGravityScale;

            if (_rb.velocity.y < -_maxFallSpeed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_maxFallSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _isGroundedSO.Value = true;

            _animationStateMachine.IsJumping = false; 

            // CameraFollow listens for this, SmoothDamps y-position to new ground level + offset. 
            OnLanded?.Invoke(transform.position.y);

            _isJumping = false;

            _lastGroundedTime = _jumpCoyoteTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _isGroundedSO.Value = false;

            _animationStateMachine.IsJumping = true;
        }
    }
}