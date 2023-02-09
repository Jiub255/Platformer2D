using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public static event Action<float> OnLanded;

	[SerializeField]
	private float _jumpForce = 5f;
    [SerializeField]
    private LayerMask _groundLayer;

    private Rigidbody2D _rb;

    private bool _playerIsOnGround = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_groundLayer.Contains(collision.gameObject.layer))
        {
            _playerIsOnGround = true;

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