 using UnityEngine;

[CreateAssetMenu(menuName = "Create Movement Configuration")]
public class MovementConfigurations : ScriptableObject
{
  public float playerMovementSpeed = 1f;
  public float laserMovementSpeed = 12f;
  public float left = -10f;
  public float right = 10f;
  public float top = 7.0f;
  public float botton = -3.8f;
}
