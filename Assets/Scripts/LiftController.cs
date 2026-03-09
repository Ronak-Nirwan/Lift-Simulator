using System.Collections;
using UnityEngine;

/// <summary>
/// Controlls the lift movement, speed & Direction
/// </summary>

public class LiftController : MonoBehaviour
{
    public float Speed = 3f;
    public float FloorWaitTime = 2f;

    public FloorSystem FloorSystem;
    public int LiftIndex;
    public int TotalFloors = 4;

    public int CurrentFloor { get; private set; }

    public LiftDirection Direction;

    bool _isMoving;

    int _targetFloor;
    float _targetY;

    bool _waitingForDestination = false;

    /// <summary>
    /// Gets the current floor Number for each lift,sets the direction of lift 
    /// </summary>

    void Start()
    {
        CurrentFloor = FloorSystem.GetClosestFloor(transform.position.y);

        Direction = (LiftIndex % 2 == 0) ? LiftDirection.Up : LiftDirection.Down;

        StartPatrol();
    }

    void Update()
    {
        MoveLift();
    }

    void StartPatrol()
    {
        SetNextPatrolFloor();
    }

    /// <summary>
    /// called by lift manager to call lift to a specific floor
    /// </summary>
    /// <param name="floorIndex"></param>
    public void CallLift(int floorIndex)
    {
        MoveToFloor(floorIndex);
    }

    // Called by panel button inside lift
    public void SelectDestination(int floorIndex)
    {
        if (!_waitingForDestination) return;

        _waitingForDestination = false;

        MoveToFloor(floorIndex);
    }

    /// <summary>
    /// Sets the next waypoint for the lift
    /// </summary>

    void SetNextPatrolFloor()
    {
        int nextFloor = CurrentFloor;

        if (Direction == LiftDirection.Up)
        {
            if (CurrentFloor >= TotalFloors - 1)
            {
                Direction = LiftDirection.Down;
                nextFloor = CurrentFloor - 1;
            }
            else
            {
                nextFloor = CurrentFloor + 1;
            }
        }
        else
        {
            if (CurrentFloor <= 0)
            {
                Direction = LiftDirection.Up;
                nextFloor = CurrentFloor + 1;
            }
            else
            {
                nextFloor = CurrentFloor - 1;
            }
        }

        MoveToFloor(nextFloor);
    }

    /// <summary>
    /// Moves the lift to the specified floor, based on the floor position
    /// </summary>
    /// <param name="floorIndex"></param>

    void MoveToFloor(int floorIndex)
    {
        _targetFloor = Mathf.Clamp(floorIndex, 0, TotalFloors - 1);
        _targetY = FloorSystem.GetFloorHeight(_targetFloor);

        _isMoving = true;
    }

    /// <summary>
    /// core lift transition
    /// </summary>

    void MoveLift()
    {
        if (!_isMoving) return;

        Vector3 target = new Vector3(transform.position.x, _targetY, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Speed * Time.deltaTime
        );

        if (Mathf.Abs(transform.position.y - _targetY) < 0.01f)
        {
            transform.position = target;

            _isMoving = false;

            CurrentFloor = _targetFloor;

            StartCoroutine(FloorStopRoutine());
        }
    }

    /// <summary>
    /// to stop the lift at each floor
    /// </summary>
    /// <returns></returns>

    IEnumerator FloorStopRoutine()
    {
        DoorController door = FloorSystem.GetDoor(CurrentFloor, LiftIndex);

        if (door != null)
            door.Open();

        yield return new WaitForSeconds(FloorWaitTime);

        // If this stop was due to a call, wait for destination
        if (!_waitingForDestination)
        {
            _waitingForDestination = true;
            yield break;
        }

        if (door != null)
            door.Close();

        StartPatrol();
    }

    // to be extended for future players
    public void ResumePatrol()
    {
        _waitingForDestination = false;

        DoorController door = FloorSystem.GetDoor(CurrentFloor, LiftIndex);

        if (door != null)
            door.Close();

        StartPatrol();
    }
}