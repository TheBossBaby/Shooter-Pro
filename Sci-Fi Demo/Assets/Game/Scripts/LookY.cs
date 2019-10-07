using UnityEngine;

public class LookY : MonoBehaviour
{
    #region Public Fields
    #endregion

    #region Private Fields
        float _ySensitivity = 5f;
    #endregion

    #region SerializeField Private Fields
    #endregion

    #region Unity Methods
        void Start()
        {
            
        }

        void Update()
        {
            float _mouseY = Input.GetAxis("Mouse Y");
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.x -= _mouseY;
            transform.localEulerAngles = newRotation;
        }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion    
}
