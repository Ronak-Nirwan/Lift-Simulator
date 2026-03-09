using UnityEngine;

/// <summary>
/// manages the behaviour for each lift, and handles the lift call
/// </summary>

public class LiftManager : MonoBehaviour
{
    public LiftController[] Lifts;

    /// <summary>
    /// Handles the floor request for each lift, and sends the nearest lift in the same direction to the floor
    /// </summary>
    /// <param name="floorIndex"></param>

    public void RequestLift(int floorIndex)
    {
        LiftController bestLift = null;
        int bestScore = int.MaxValue;

        foreach (LiftController lift in Lifts)
        {
            int distance = Mathf.Abs(lift.CurrentFloor - floorIndex);

            bool movingTowardFloor =
                (lift.Direction == LiftDirection.Up && floorIndex >= lift.CurrentFloor) ||
                (lift.Direction == LiftDirection.Down && floorIndex <= lift.CurrentFloor);

            bool validLift =
                lift.Direction == LiftDirection.Idle || movingTowardFloor;

            if (!validLift)
                continue;

            int score = distance;

            if (score < bestScore)
            {
                bestScore = score;
                bestLift = lift;
            }
        }

        // if no lift is sent 
        if (bestLift == null)
        {
            foreach (LiftController lift in Lifts)
            {
                int distance = Mathf.Abs(lift.CurrentFloor - floorIndex);

                if (distance < bestScore)
                {
                    bestScore = distance;
                    bestLift = lift;
                }
            }
        }

        if (bestLift != null)
        {
            Debug.Log($"Lift {bestLift.LiftIndex} responding to floor {floorIndex}");

            bestLift.CallLift(floorIndex);
        }
    }
}