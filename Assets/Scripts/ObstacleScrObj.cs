using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "Scriptable Objects/Obstacle")]
public class ObstacleScrObj : ScriptableObject
{
    public Sprite icon;
    public string obstacleName;
    public GameObject obstacle;
}
