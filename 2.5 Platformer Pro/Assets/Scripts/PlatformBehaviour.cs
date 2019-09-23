using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    #region Private Attribute
        Animator _platformAnimator;
        float _movementSpeed = 0.35f;   
    #endregion

    #region MonoBehaviour Callbacks
        void Start()
        {
            _platformAnimator = GetComponent<Animator>();
            if(_platformAnimator == null)
                Debug.LogError("Platform Animator is NULL");
            _platformAnimator.speed =_movementSpeed;        

        }
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player")
            {
                other.transform.SetParent(gameObject.transform);       
            }
        }

        private void OnTriggerExit(Collider other) {
            if(other.tag == "Player")
            {
                other.transform.SetParent(null);       
            }        
        }     
    #endregion
}
