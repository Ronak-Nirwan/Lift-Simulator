using UnityEngine;

/// <summary>
/// Have the data related to floors like position, have reference to door to open or close them directly
/// </summary>

public class FloorSystem : MonoBehaviour
{
    /// <summary>
    /// Floor and doors reference
    /// </summary>

    public Transform[] Floors;

    public DoorController[] Floor0Doors;
    public DoorController[] Floor1Doors;
    public DoorController[] Floor2Doors;
    public DoorController[] Floor3Doors;

    DoorController[][] doors;

    void Awake()
    {
        doors = new DoorController[4][];

        doors[0] = Floor0Doors;
        doors[1] = Floor1Doors;
        doors[2] = Floor2Doors;
        doors[3] = Floor3Doors;
    }

    /// <summary>
    /// gets the y coordinate of each floor for easy lift traversal
    /// </summary>
    /// <param name="floorIndex"></param>
    /// <returns></returns>

    public float GetFloorHeight(int floorIndex)
    {
        return Floors[floorIndex].position.y;
    }

    /// <summary>
    /// gets the DoorController component and returns the floor and lift location 
    /// </summary>
    /// <param name="floorIndex"></param>
    /// <param name="liftIndex"></param>
    /// <returns></returns>

    public DoorController GetDoor(int floorIndex, int liftIndex)
    {
        return doors[floorIndex][liftIndex];
    }

    /// <summary>
    /// Gives the nearest from a lift to help the calling function
    /// </summary>
    /// <param name="yPosition"></param>
    /// <returns></returns>
    public int GetClosestFloor(float yPosition)
    {
        int closest = 0;
        float minDist = Mathf.Abs(yPosition - Floors[0].position.y);

        for (int i = 1; i < Floors.Length; i++)
        {
            float dist = Mathf.Abs(yPosition - Floors[i].position.y);

            if (dist < minDist)
            {
                minDist = dist;
                closest = i;
            }
        }

        return closest;
    }
}