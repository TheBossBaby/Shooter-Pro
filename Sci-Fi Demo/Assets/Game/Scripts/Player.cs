using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Fields
    #endregion

    #region Private Fields
        private CharacterController _contoller;
        private const float _speed = 3.0f;
        private const float _gravity = 9.8f;        
    #endregion

    #region SerializeField Private Fields
    #endregion

    #region Unity Methods
        void Start()
        {
            _contoller = GetComponent<CharacterController>();
            if(_contoller == null)
                Debug.LogError("Character controller is NULL");
        }

        void Update()
        {
            CalculateMovement();
        }
    #endregion

    #region Private Methods
        void CalculateMovement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontalInput,0,verticalInput);
            Vector3 velocity = direction * _speed;
            velocity.y -= _gravity;
            velocity = transform.transform.TransformDirection(velocity); 
            _contoller.Move(velocity * Time.deltaTime);
        }
    #endregion

    #region Public Methods
    #endregion    
}