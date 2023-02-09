using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _follow;

    [SerializeField]
    private Vector3 _offset;

    [SerializeField, Range(0.1f, 1.0f)]
    private float _smoothTime = 0.3f;

/*    [SerializeField]
    private float _maxSpeed = 25f;*/

    private Vector3 _velocityX = Vector3.zero;
    private Vector3 _velocityY = Vector3.zero;
    private float _targetYPosition;

    private void OnEnable()
    {
        _targetYPosition = _follow.transform.position.y;

       PlayerJump.OnLanded += (y) => _targetYPosition = y;
    }

    private void OnDisable()
    {
        PlayerJump.OnLanded -= (y) => _targetYPosition = y;
    }

    private void Update()
    {
        // SmoothDamp toward player's x position. 
        float x = Vector3.SmoothDamp(
            transform.position,
            _follow.position + _offset,
            ref _velocityX,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
            Time.unscaledDeltaTime).x;

        // SmoothDamp toward target y position (y coordinate of last ground touched). 
        float y = Vector3.SmoothDamp(
            transform.position,
            new Vector3(transform.position.x, _targetYPosition, transform.position.z) + _offset,
            ref _velocityY,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
            Time.unscaledDeltaTime).y;

        transform.position = new Vector3(x, y, transform.position.z);

/*        transform.position = Vector3.SmoothDamp(
            transform.position,
            _follow.position + _offset,
            ref _velocity,
            _smoothTime,
            *//*_maxSpeed*//*Mathf.Infinity,
            Time.unscaledDeltaTime);*/
    }
}