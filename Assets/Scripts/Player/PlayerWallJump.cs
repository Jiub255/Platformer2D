using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerWallJump : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayers;

    [SerializeField]
    private float _wallSlideGravity = 0.3f; 
    private float _normalGravity; 

    private bool _inWallRange = false;

    private InputAction _moveAction;

    private Rigidbody2D _rb;

    private void Start()
    {
        _moveAction = S.I.IM.PC.Gameplay.Move;
        _rb = GetComponentInParent<Rigidbody2D>();
        _normalGravity = _rb.gravityScale;
    }

    private void Update()
    {
        if (_inWallRange && _moveAction.ReadValue<float>() < -0.5f)
        {
            _rb.gravityScale = _wallSlideGravity;
        }
        else
        {
            _rb.gravityScale = _normalGravity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _inWallRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_groundLayers.Contains(collision.gameObject.layer))
        {
            _inWallRange = false;
        }
    }
}