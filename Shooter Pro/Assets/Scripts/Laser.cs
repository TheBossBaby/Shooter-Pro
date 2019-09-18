using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Serialized Private Field
        [SerializeField] private MovementConfigurations _movementConfigurations;
    #endregion
    
    void Update()
    {
        transform.Translate(Vector3.up * _movementConfigurations.laserMovementSpeed * Time.deltaTime);

        if(transform.position.y >= 8.0f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);                
            }
        }
    }
}