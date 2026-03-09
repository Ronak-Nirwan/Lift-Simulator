using UnityEngine;

/// <summary>
/// For future to add the floor choosing mechanic
/// </summary>

public class LiftPanelButton : MonoBehaviour
{
    public int floorIndex;
    public LiftController lift;

    public void PressButton()
    {
        lift.SelectDestination(floorIndex);
    }
}