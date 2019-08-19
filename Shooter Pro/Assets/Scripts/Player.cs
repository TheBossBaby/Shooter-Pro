using UnityEngine;

public class Player : MonoBehaviour
{
  #region Scriptable Object
    [SerializeField] private MovementConfigurations _movementConfiguataion;
  #endregion

  #region SerializeField Private Field
    [SerializeField] private Transform _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;      
    [SerializeField] private int _lives = 3;      
  #endregion

  #region Private Field
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    // private int _laserIndex = 0; 
  #endregion

  #region Public
    // public Transform[] laserArray;
  #endregion

  #region MonoBehaviour Callback
    // Start is called before the first frame update
    void Start()
    {
      // Take position of player starting = new Position(0,0,0)
      transform.position = new Vector3(0,0,0);
      _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

      if (_spawnManager == null)
      {
        Debug.LogError("Spawn Manager not refrenced properly");
      }
    }

    // Update is called once per frame
    void Update()
    {
      CalculatePlayer();

      if(Input.GetButtonUp("Jump") && Time.time >_canFire)
      {
        // _laserPrefab.gameObject.SetActive(true);
        _canFire = Time.time + _fireRate;
        // laserArray[_laserIndex].gameObject.SetActive(true);
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

  #region Public Methods
    public void Damage(int damageValue = 1)
    {
      _lives -= damageValue;

      if(_lives <= 0)
      {
        _spawnManager.OnPalyerDeath();
        Destroy(this.gameObject);
      }
    }
  #endregion
}