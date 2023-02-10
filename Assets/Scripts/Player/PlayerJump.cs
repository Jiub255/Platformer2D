using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public static event Action<float> OnLanded;
    public static event Action OnLeftGround;

	[SerializeField, Range(5f, 50f)]
	private float _jumpForce = 19f;

    [SerializeField, Range(0.25f, 5f)]
    private float _gravityScale = 1f;
    [SerializeField, Range(0.5f, 10f)]
    private float _fallingGravityScale = 2.5f;

    [SerializeField, Range(5f, 50f)]
    private float _maxFallSpeed = 18f;

    [SerializeField, Range(0f, 1f)]
    private float _jumpCutMultiplier = 0.5f;

    [SerializeField, Range(0f, 0.5f)]
    private float _preLandGracePeriod = 0.2f; 
    [SerializeField, Range(0f, 0.5f)]
    private float _postFallGracePeriod = 0.2f; 

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

    private bool _isJumping = false;

    public float _jumpInputTimer { get; private set; }
    public float _leftGroundTimer { get; private set; }

    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _animationStateMachine = GetComponentInParent<CharacterAnimator>();

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
        _jumpInputTimer = _preLandGracePeriod;
    }

    private void Jump()
    {
        // TODO: Try using ForceMode2D.Force and add smaller forces while button held (for a short time). 
        // Make for higher jump if you hold down jump for like a half second or a small jump if you quickly tap jump. 

        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        //_animationStateMachine.IsJumping = true;
        _onPlayJumpSound.Invoke(_jumpClip);
        _isJumping = true;
    }

    private void CutJump(InputAction.CallbackContext context)
    {
        if (_rb.velocity.y > 0 && !_isGroundedSO.Value)
        {
            _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);
        }

        _jumpInputTimer = 0f;
    }

    private void Update()
    {
        if (!_isGroundedSO.Value)
        {
            _leftGroundTimer -= Time.deltaTime;
        }

        _jumpInputTimer -= Time.deltaTime;

        if (_leftGroundTimer > 0 && _jumpInputTimer > 0 && !_isJumping)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_rb.velocity.y >= 0f)
        {
            _rb.gravityScale = _gravityScale;
        }
        // Set higher gravity while falling. 
        else
        {
            _rb.gravityScale = _fallingGravityScale;

            // Clamp fall speed. 
            if (_rb.velocity.y < -_maxFallSpeed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_maxFallSpeed);
            }
        }
    }

    // Called when you land on ground. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _isGroundedSO.Value = true;

            _animationStateMachine.IsJumping = false; 

            // CameraFollow listens for this, SmoothDamps y-position to new ground level + offset. 
            OnLanded?.Invoke(transform.position.y);

            _isJumping = false;

            _leftGroundTimer = _postFallGracePeriod;
        }
    }

    // Called when starting falling or jumping. 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _isGroundedSO.Value = false;

            _animationStateMachine.IsJumping = true;
            
            OnLeftGround?.Invoke();
        }
    }
}