using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformMovePlayer : MonoBehaviour
{
    private Transform _player;
    private Vector3 _lastPosition;

    private void Update()
    {
        if (_player != null)
        { 
            Vector3 displacementDuringLastFrame = transform.position - _lastPosition;
            _player.position += displacementDuringLastFrame;
            /*Vector2 velocityDuringLastFrame = displacementDuringLastFrame / Time.deltaTime;
            _player.GetComponent<Rigidbody2D>().velocity += velocityDuringLastFrame;*/
        }

        _lastPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovementRB>())
        {
            //collision.transform.parent = transform;

            _player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovementRB>())
        {
            //collision.transform.parent = null;
            
            _player = null;
        }
    }
}