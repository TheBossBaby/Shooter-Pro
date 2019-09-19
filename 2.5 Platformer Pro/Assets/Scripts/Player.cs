using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    #region Private Attribute
        private CharacterController _characterController;
        private float _speed = 5.0f;
        private float _gravity = 2.0f;
        private float _jumpHeight = 30.0f;
        private float _yVelocity;
        private bool _canDoubleJump;
        private int _coin;
        UIManager _uiManager;        

    #endregion

    #region Serialized Private Attribute
    #endregion

    #region MonoBehaviour Callbacks
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            if(_characterController == null)
                Debug.LogError("Character Contoller is NULL");
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            if(_uiManager == null)
                Debug.LogError("UI Manager is NULL");
        }

        void Update()
        {
            var horizontalInput = Input.GetAxis("Horizontal");
            var direction = new Vector3(horizontalInput,0,0);
            var velocity = direction * _speed;

            if(!_characterController.isGrounded)
            {
                if(Input.GetButtonDown("Jump") && _canDoubleJump)
                {
                    _yVelocity = _jumpHeight;                
                    _canDoubleJump = false;
                }
                else
                    _yVelocity -= _gravity;
            }
            else
            {
                if(Input.GetButtonDown("Jump"))
                {
                    _yVelocity = _jumpHeight;                
                    _canDoubleJump = true;
                }
            }

            velocity.y = _yVelocity;
            _characterController.Move(velocity * Time.deltaTime);
        }        
    #endregion

    #region Public Methods
        public void AddCoinNumber()
        {
            _coin++;
            _uiManager.UpdateCoinText(_coin);
        }
    #endregion
}
