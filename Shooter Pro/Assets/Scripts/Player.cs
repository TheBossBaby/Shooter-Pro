using UnityEngine;

public class Player : MonoBehaviour
{
  #region Scriptable Object
    [SerializeField] private MovementConfigurations _movementConfiguataion;
    [SerializeField] private Transform _laserPrefab;
  #endregion

  #region Private Field
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;
  #endregion

  #region Public

  #endregion

  #region MonoBehaviour Callback
    // Start is called before the first frame update
    void Start()
    {
      // Take position of player starting = new Position(0,0,0)
      transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
      CalculatePlayer();

      if(Input.GetButtonUp("Jump") && Time.time >_canFire)
      {
        // _laserPrefab.gameObject.SetActive(true);
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
      }
    }
  #endregion

  #region Private Methods

    void CalculatePlayer()
    {
      float horizontalInputAxis = Input.GetAxis("Horizontal");
      float verticalInputAxis = Input.GetAxis("Vertical");

      // Basic way
      // transform.Translate(Vector3.right * horizontalInputAxis * _movementConfiguataion.playerMovementSpeed * Time.deltaTime);
      // transform.Translate(Vector3.up * verticalInputAxis * _movementConfiguataion.playerMovementSpeed * Time.deltaTime);

      // Optimal way
      transform.Translate(new Vector3(horizontalInputAxis, verticalInputAxis, 0) * _movementConfiguataion.playerMovementSpeed * Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f , 5.5f), 0);
      
      if(transform.position.x <= -10f)
        transform.position = new Vector3(10f,transform.position.y,0);
      else if(transform.position.x >= 10f)
        transform.position = new Vector3(-10f,transform.position.y,0);
    }
  #endregion
}