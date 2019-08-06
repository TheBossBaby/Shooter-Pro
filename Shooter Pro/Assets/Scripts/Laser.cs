using UnityEngine;

public class Laser : MonoBehaviour
{
    #region Serialized Private Field
        [SerializeField] private MovementConfigurations _movementConfigurations;
        // [SerializeField] private Transform _playerTransform;
    #endregion
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _movementConfigurations.laserMovementSpeed * Time.deltaTime);

        if(transform.position.y >= 8.0f)
        {
            Destroy(this.gameObject);
            // gameObject.SetActive(false);
            // transform.Translate(_playerTransform.position);
        }
    }
}
