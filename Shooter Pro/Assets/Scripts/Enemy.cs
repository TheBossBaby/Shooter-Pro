using UnityEngine;
public class Enemy : MonoBehaviour
{
    private const float MaxDistance = 100.0f;
    #region Scriptable Object
    [SerializeField] private MovementConfigurations _movementConfigurations;
    #endregion

    #region Private Attributes
        private Player _player;
        private Animator _enemyAnimator;
        private float _speed = 5.0f;
        AudioManager _audioManager;
    #endregion

    #region Monobehaviour Callback
        private void Start() {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _enemyAnimator = gameObject.GetComponent<Animator>();
            if(_enemyAnimator == null)
            {
                Debug.LogError("Enemy Animator is NULL");
            }

            _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
            if(_audioManager == null)
                Debug.LogError(message: "Audio Manager is NULL");        
        }
        void Update()
        {
            Shoot();
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
            if(transform.position.y < -4f)
            {
                float randomX = Random.Range(-9.54f, 9.27f);
                transform.position = new Vector3(randomX,8f,0f);
            }

        }
        void FixedUpdate()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
                print("Found an object - distance: " + hit.distance);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            Player player = other.transform.GetComponent<Player>();
           if(other.tag == "Player")
           {
               if(player != null)
               {
                   player.Damage();
               }
               _enemyAnimator.SetTrigger("OnEnemyDeath"); // OnEnemyDeath Trigger
               _speed = 0;
                _audioManager.ExplosionSound();
                Destroy(this.gameObject.GetComponent<Collider2D>());
               Destroy(gameObject, 2.40f);
           }

           if(other.tag == "Laser")
           {
               if(_player != null)
               {
                    _player.KillEnemyAndAddScore();
               }
               Destroy(other.gameObject);
               _enemyAnimator.SetTrigger("OnEnemyDeath"); // OnEnemyDeath Trigger               
               _speed = 0;
                _audioManager.ExplosionSound();
                Destroy(this.gameObject.GetComponent<Collider2D>());
               Destroy(gameObject, 2.40f);
           } 
        }        
    #endregion

    #region Private Function
        private void Shoot()
        {
            RaycastHit hit2D;
            Debug.DrawRay(transform.position,transform.forward,Color.blue);
            if (Physics.Raycast(transform.position,
                                Vector3.forward,
                                out hit2D,
                                MaxDistance))
            {
                Debug.Log(hit2D.transform.tag);
            }
        }
    #endregion
}