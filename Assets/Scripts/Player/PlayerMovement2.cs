using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _acceleration = 5f;

    private Rigidbody2D _rb;
    private InputAction _moveAction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // Needs to be in start instead of Awake/OnEnable so IM can have the PC reference ready. 
        _moveAction = S.I.IM.PC.Gameplay.Move;
    }

    private void FixedUpdate()
    {
        float desiredSpeed = _moveAction.ReadValue<float>() * _speed;

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