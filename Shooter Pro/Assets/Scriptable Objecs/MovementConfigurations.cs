 using UnityEngine;

[CreateAssetMenu(menuName = "Create Movement Configuration")]
public class MovementConfigurations : ScriptableObject
{
  public float playerMovementSpeed = 1f;
  public float laserMovementSpeed = 12f;
  public float enemyMovementSpeed = 0.8f;
}
