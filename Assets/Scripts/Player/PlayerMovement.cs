using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
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
        Vector2 movement = new Vector2(
            _moveAction.ReadValue<float>() * _speed, 
            _rb.velocity.y);

        _rb.velocity = movement;
    }
}