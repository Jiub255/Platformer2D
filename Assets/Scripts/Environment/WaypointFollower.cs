using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f; 

	[SerializeField]
	private List<Transform> _waypoints;
	private int _currentWaypointIndex;

    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentWaypointIndex = 0;
    }

/*    private void Start()
    {
        LaunchTowardNextWaypoint();
    }

    private void LaunchTowardNextWaypoint()
    {
        Debug.Log("Launch");
        _rb.velocity = Vector3.zero;
        Vector3 direction = _waypoints[_currentWaypointIndex].transform.position - transform.position;
        direction.Normalize();
        direction *= _speed;
        _rb.AddForce(direction, ForceMode2D.Impulse);
    }*/

    private void FixedUpdate()
    {
        // Using Rigidbody 
        Vector3 direction = _waypoints[_currentWaypointIndex].position - transform.position;
        direction.Normalize();
        Vector3 movement = direction * (_speed * Time.deltaTime);
        Vector3 nextPosition = transform.position + movement;
        _rb.MovePosition(nextPosition);
       
        if (Vector2.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }

            //LaunchTowardNextWaypoint();
        }


        // Using Transform 
        /*        float step = Time.deltaTime * _speed;

                transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, step);
                if (Vector2.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
                {
                    _currentWaypointIndex++;
                    if (_currentWaypointIndex >= _waypoints.Count)
                    {
                        _currentWaypointIndex = 0;
                    }
                }*/
    }

    // Will this get called before or after update runs? 
/*    public Vector3 GetDisplacement()
    {
        float step = Time.deltaTime * _speed;

        Vector3 nextPosition = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, step);

        return (transform.position - nextPosition);
    }*/
}