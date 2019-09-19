using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    #region MonoBehaviour Callbacks
        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Player")
            {
                Player p = other.GetComponent<Player>();
                if(p != null)
                    p.AddCoinNumber();
                Destroy(gameObject);
            }
        }   
    #endregion
}