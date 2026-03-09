using UnityEngine;

/// <summary>
/// To call the lift to the specific floor by pressing a button
/// </summary>

public class FloorCallButton : MonoBehaviour
{
    public int FloorIndex;
    public LiftManager LiftManager;

    public void PressButton()
    {
        LiftManager.RequestLift(FloorIndex);
    }
}