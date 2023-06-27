using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public float MoveSpeed { get; set; }
    public WayPoint WayPoint;
    public Vector3 CurrentPointPosition => WayPoint.GetWaypointPosition(_currentWaypointIndex);

    private int _currentWaypointIndex;
    private Vector3 _lastPointPosition;

    // Start is called before the first frame update
    void Start()
    {
        _currentWaypointIndex = 0;
        MoveSpeed = moveSpeed;
        _lastPointPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (CurrentPointPositionReached())
        {
            UpdateWayPointIndex();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            CurrentPointPosition, MoveSpeed * Time.deltaTime);
    }

    //check xem da di den waypoint hien tai chua de di chuyen den waypoint ke tiep 
    private bool CurrentPointPositionReached()
    {
        float distanceToNextPointPosition = (transform.position - CurrentPointPosition).magnitude;
        if (distanceToNextPointPosition < 0.1f)
        {
            _lastPointPosition = transform.position;
            return true;
        }
        return false;
    }

    //Update waypoint index 
    private void UpdateWayPointIndex()
    {
        int lastWaypointIndex = WayPoint.Points.Length - 1;
        if (_currentWaypointIndex < lastWaypointIndex)
        {
            _currentWaypointIndex++;
        }
    }

    //reset current waypoint index ve 0 
    public void ResetEnemy()
    {
        _currentWaypointIndex = 0;
    }

}
