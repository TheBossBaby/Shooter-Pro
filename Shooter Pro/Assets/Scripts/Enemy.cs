using UnityEngine;
public class Enemy : MonoBehaviour
{
    #region Scriptable Object
        [SerializeField] private MovementConfigurations _movementConfigurations;
    #endregion

    #region Private Attributes
        private Player _player;
    #endregion

    #region Monobehaviour Callback
        private void Start() {
            _player = GameObject.Find("Player").GetComponent<Player>();
        }
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
        void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.transform.GetComponent<Player>();
           if(other.tag == "Player")
           {
               if(player != null)
               {
                   player.Damage();
               }
               Destroy(gameObject);
           }

           if(other.tag == "Laser")
           {
               if(_player != null)
               {
                    _player.KillEnemyAndAddScore();
               }
               Destroy(other.gameObject);
               Destroy(gameObject);
           } 
        }        
    #endregion
}