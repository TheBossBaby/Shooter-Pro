using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
  #region Scriptable Object
    [SerializeField] private MovementConfigurations _movementConfiguataion;
  #endregion

  #region SerializeField Private Field
    [SerializeField] private Transform _laserPrefab;
    [SerializeField] private Transform _tripleShotPrefab;
    [SerializeField] private Transform _speedBoostPrefab;
    [SerializeField] private float _fireRate = 0.5f;      
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _shield;      
  #endregion

  #region Private Field
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    private int _score;
    private UIManager _uiManager;
    // private int _laserIndex = 0; 
  #endregion

  #region MonoBehaviour Callback
    void Start()
    {
      transform.position = new Vector3(0,0,0);

      _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
      if (_spawnManager == null)
      {
        Debug.LogError("Spawn Manager not refrenced properly");
      }

      _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
      if(_uiManager == null)
      {
        Debug.LogError("UI Manager is NULL");
      }
    }

    // Update is called once per frame
    void Update()
    {
      CalculatePlayer();

      if(Input.GetButtonUp("Jump") && Time.time >_canFire)
      {
        FireLaser();
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

    void FireLaser()
    {
      // _laserPrefab.gameObject.SetActive(true);
      _canFire = Time.time + _fireRate;
      if(_isTripleShotActive)
        Instantiate(_tripleShotPrefab, transform.position + Vector3.up, Quaternion.identity);
      else
        // laserArray[_laserIndex].gameObject.SetActive(true);
        Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
    }

    IEnumerator TriplePowerShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _movementConfiguataion.playerMovementSpeed /= 2;

    }
  #endregion

  #region Public Methods
    public void Damage(int damageValue = 1)
    {
      if(_isShieldActive)
      {
        _isShieldActive = false;
        _shield.SetActive(false);
        return;
      }
      _lives -= damageValue;
      _uiManager.UpdateLife(_lives);
      if(_lives <= 0)
      {
        _spawnManager.OnPalyerDeath();
        Destroy(this.gameObject);
      }
    }
    public void EnableTripleShot()
    {
      _isTripleShotActive = true;
      StartCoroutine("TriplePowerShotPowerDownRoutine");
    }
    public void EnableSpeedBoost()
    {
      _isSpeedBoostActive = true;
      _movementConfiguataion.playerMovementSpeed *= 2;
      StartCoroutine("SpeedBoostPowerDownRoutine");
    }
    public void EnableShield()
    {
      _isShieldActive = true;
      _shield.SetActive(true);
    }
    public void KillEnemyAndAddScore(int reward = 10)
    {
      _score += reward;
      _uiManager.UpdateScore(_score);
    }
  #endregion
}