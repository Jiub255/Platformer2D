using UnityEngine;

public class PlatformCarry : MonoBehaviour
{
    private Rigidbody2D _playerRB;
	private Vector3 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (_playerRB != null)
        {
            Vector2 displacement = transform.position - _lastPosition;

            Debug.Log(displacement/* / _playerRB.mass*/);
            // F = ma, so F/m = a. (Mass is 1 here so it doesn't matter). 
            // Vector2 acceleration = displacement / _playerRB.mass;
            // Why isn't this working? Not doing anything. 
            Debug.Log(_playerRB.gameObject.name);
            _playerRB.transform.Translate(displacement, Space.World);
           // _playerRB.AddForce(displacement, ForceMode2D.Force);
        }

        _lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerJump>())
        {
            _playerRB = collision.GetComponentInParent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerJump>())
        {
            _playerRB = null;
        }
    }
}