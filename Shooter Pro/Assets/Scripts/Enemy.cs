using UnityEngine;
public class Enemy : MonoBehaviour
{
    #region Scriptable Object
        [SerializeField] private MovementConfigurations _movementConfigurations;
    #endregion

    #region Monobehaviour Callback
        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down * _movementConfigurations.enemyMovementSpeed * Time.deltaTime);
            if(transform.position.y < -4f)
            {
                float randomX = Random.Range(-9.54f, 9.27f);
                transform.position = new Vector3(randomX,8f,0f);
            }        
        }

        /// <summary>
        /// OnTriggerEnter is called when the Collider other enters the trigger.
        /// </summary>
        /// <param name="other">The other Collider involved in this collision.</param>
        void OnTriggerEnter(Collider other)
        {
           if(other.tag == "Player")
           {
               Player player = other.transform.GetComponent<Player>();
               if(player != null)
               {
                   player.Damage();
               }
               Destroy(gameObject);
           }

           if(other.tag == "Laser")
           {
               Destroy(other.gameObject);
               Destroy(gameObject);
           } 
        }        
    #endregion
}