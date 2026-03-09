using UnityEngine;

/// <summary>
/// The Script is used for controlling the floor doors, to open and close the door according to the arrival of lift
/// </summary>
public class DoorController : MonoBehaviour
{
    // Sliding Door opening left and right  
    public Transform LeftDoor;
    public Transform RightDoor;

    // To control the speed and distance of the door
    public float SlideDistance = 1.2f;
    public float DoorSpeed = 2f;

    // open and closed door position
    Vector3 _leftClosed;
    Vector3 _rightClosed;

    Vector3 _leftOpen;
    Vector3 _rightOpen;

    bool _opening;
    bool _closing;

    /// <summary>
    /// captures the initial position of the doors, and calculates the open position
    /// </summary>
    void Start()
    {
        _leftClosed = LeftDoor.localPosition;
        _rightClosed = RightDoor.localPosition;

        _leftOpen = _leftClosed + Vector3.left * SlideDistance;
        _rightOpen = _rightClosed + Vector3.right * SlideDistance;
    }

    /// <summary>
    /// switching door from open to close and close to open
    /// </summary>

    void Update()
    {
        if (_opening)
        {
            LeftDoor.localPosition = Vector3.MoveTowards(LeftDoor.localPosition, _leftOpen, DoorSpeed * Time.deltaTime);
            RightDoor.localPosition = Vector3.MoveTowards(RightDoor.localPosition, _rightOpen, DoorSpeed * Time.deltaTime);
        }

        if (_closing)
        {
            LeftDoor.localPosition = Vector3.MoveTowards(LeftDoor.localPosition, _leftClosed, DoorSpeed * Time.deltaTime);
            RightDoor.localPosition = Vector3.MoveTowards(RightDoor.localPosition, _rightClosed, DoorSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// setting the doors to be opened
    /// </summary>

    public void Open()
    {
        _opening = true;
        _closing = false;
    }

    /// <summary>
    /// closing the door
    /// </summary>
    public void Close()
    {
        _closing = true;
        _opening = false;
    }
}