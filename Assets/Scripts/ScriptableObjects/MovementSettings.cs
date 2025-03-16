using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "ScriptableObjects/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    public float fallDuration;
    public float collapseDuration;
}
