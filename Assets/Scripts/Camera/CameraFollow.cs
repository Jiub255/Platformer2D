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

    private Camera _camera;

    public float LookOffsetMod { get; set; } = 0f;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        _targetYPosition = _follow.transform.position.y;

       PlayerJump.OnLanded += (y) => _targetYPosition = y;
    }

    private void OnDisable()
    {
        PlayerJump.OnLanded -= (y) => _targetYPosition = y;
    }

    private void Update()
    {
        // Chase player if they get too far down. 
        if ((transform.position.y - _follow.position.y) > _camera.orthographicSize * 0.5f)
        {
            _targetYPosition = _follow.position.y; 
        }

        // Chase player if they get too far up. 
        if ((transform.position.y - _follow.position.y) < -(_camera.orthographicSize) * 0.5f)
        {
            _targetYPosition = _follow.position.y;
        }

        // Change y-offset if holding down "Look". 
        Vector3 offset = _offset;
        if (LookOffsetMod > 0.5f)
        {
            offset = new Vector3(_offset.x, _camera.orthographicSize - 1, _offset.z);

        }
        else if (LookOffsetMod < -0.5f)
        {
            offset = new Vector3(_offset.x, -(_camera.orthographicSize) + 1, _offset.z);
        }

        // SmoothDamp toward player's x position. 
        float x = Vector3.SmoothDamp(
            transform.position,
            _follow.position + offset,
            ref _velocityX,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
            Time.unscaledDeltaTime).x;

        // SmoothDamp toward target y position (y coordinate of last ground touched). 
        float y = Vector3.SmoothDamp(
            transform.position,
            new Vector3(transform.position.x, _targetYPosition, transform.position.z) + offset,
            ref _velocityY,
            _smoothTime,
            /*_maxSpeed*/Mathf.Infinity,
            Time.unscaledDeltaTime).y;

        transform.position = new Vector3(x, y, transform.position.z);
    }
}