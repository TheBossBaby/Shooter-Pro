using UnityEngine;

public class LookX : MonoBehaviour
{
    #region Public Fields
    #endregion

    #region Private Fields
        float _xSensitivity = 5f;
    #endregion

    #region SerializeField Private Fields
    #endregion

    #region Unity Methods
        void Start()
        {
            
        }

        void Update()
        {
            float _mouseX = Input.GetAxis("Mouse X");
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += _mouseX * _xSensitivity;
            transform.localEulerAngles = newRotation;
        }
    #endregion

    #region Private Methods
    #endregion

    #region Public Methods
    #endregion    
}