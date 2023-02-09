using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f; 

	[SerializeField]
	private List<Transform> _waypoints = new List<Transform>();

	private int _currentWaypointIndex = 0;

    private void Update()
    {
        float step = Time.deltaTime * _speed;

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, step);
        if (Vector2.Distance(transform.position, _waypoints[_currentWaypointIndex].position) < 0.1f)
        {
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }
        }
    }
}