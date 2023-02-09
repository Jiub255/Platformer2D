using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CharacterAnimator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _acceleration = 10f;

    private Rigidbody2D _rb;
    private CharacterAnimator _animationStateMachine;

    private InputAction _moveAction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animationStateMachine = GetComponent<CharacterAnimator>();

        // Needs to be in start instead of Awake/OnEnable so IM can have the PC reference ready. 
        _moveAction = S.I.IM.PC.Gameplay.Move;
    }

    private void FixedUpdate()
    {
        float movement = _moveAction.ReadValue<float>();

        // Set animation parameters. 
        _animationStateMachine.MoveX = movement;
        if (Mathf.Abs(movement) > 0.5f)
        {
            _animationStateMachine.LastX = movement;
        }

        float desiredSpeed = movement * _speed;

        // If desired speed is 0, apply force in direction opposite to movement to slow down faster. 
        if (Mathf.Abs(desiredSpeed) < 0.1f)
        {
            _rb.AddForce(new Vector2(-_rb.velocity.x * _acceleration, 0f), ForceMode2D.Force);
        }
        // If not at desired speed, add force in that direction until you are. 
        else if (Mathf.Abs(desiredSpeed - _rb.velocity.x) > 0.1f)
        {
            _rb.AddForce(new Vector2((desiredSpeed - _rb.velocity.x) * _acceleration, 0f), ForceMode2D.Force);
        }
    }
}