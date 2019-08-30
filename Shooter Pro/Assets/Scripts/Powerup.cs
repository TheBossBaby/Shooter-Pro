using UnityEngine;

public class Powerup : MonoBehaviour
{
    #region Private Attributes
        [SerializeField] private int _powerId = -1;
    #endregion

    #region Monobehaviours Callbacks
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.down*5*Time.deltaTime);
            if(transform.position.y < -4.5)
            {
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D other) {
            if(other.gameObject.tag == "Player")
            {
                Player player = other.transform.GetComponent<Player>();
                if(player != null)
                {
                    switch(_powerId)
                    {
                        case 0:
                            player.EnableTripleShot();
                            break;
                        case 1:
                            player.EnableSpeedBoost();
                            break;
                        case 2:
                            player.EnableShield();
                            break;
                        default:
                            Debug.LogError("Invalid powerup Id");
                            break;
                    }
                    Destroy(gameObject);
                }
            }    
        }        
    #endregion
}