using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1f, 30f)]
    private float _maxSpeed = 10f;
    [SerializeField, Range(1f, 20f)]
    private float _acceleration = 7f;
    [SerializeField, Range(1f, 20f)]
    private float _deceleration = 7f;
    [SerializeField, Range(0f, 2f)]
    private float _velocityPower = 0.9f;
    [SerializeField, Range(0f, 1f)]
    private float _frictionAmount = 0.2f;

    [SerializeField]
    private BoolSO _isGroundedSO;

    private Rigidbody2D _rb;
    private CharacterAnimator _characterAnimator;

    private InputAction _moveAction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<CharacterAnimator>();

        // Needs to be in start instead of Awake/OnEnable so IM can have the PC reference ready. 
        _moveAction = S.I.IM.PC.Gameplay.Move;
    }

    private void FixedUpdate()
    {
        // Set animation parameters 
        float moveInput = _moveAction.ReadValue<float>();
        _characterAnimator.MoveX = moveInput;
        if (Mathf.Abs(moveInput) > 0.5f)
        {
            _characterAnimator.LastX = moveInput;
        }

        // Run 
        float targetSpeed = moveInput * _maxSpeed;
        float speedDifference = targetSpeed - _rb.velocity.x;
        float accelerationRate = (Mathf.Abs(speedDifference) > 0.01f) ? _acceleration : _deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, _velocityPower) * Mathf.Sign(speedDifference);

        _rb.AddForce(movement * Vector2.right);

        // Friction 
        if (_isGroundedSO.Value == true && Mathf.Abs(_moveAction.ReadValue<float>()) < 0.1f)
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rb.velocity.x);
            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }
}