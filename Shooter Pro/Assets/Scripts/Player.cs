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
    [SerializeField] private float _fireRate = 0.5f;      
    [SerializeField] private int _lives = 3;
    [SerializeField] private GameObject _shield;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;
  #endregion

  #region Private Field
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    private int _score;
    private UIManager _uiManager;
    AudioSource _laserAudio;
    AudioManager _audioManager;
    GameManager _gameManager;
    int _numberOfPowerUpCollected = 0;
  #endregion

  #region Public Attribute
    public enum PlayerNumber
      {
          playerOne,
          playertwo
      }
    public PlayerNumber playerNumber;
  #endregion
  
  #region MonoBehaviour Callback
    void Start()
    {
      _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
      if (_gameManager == null)
      {
          Debug.LogError("Game Manager is null");
      }
      if(_gameManager.isCoOpMode == false)
      {
        transform.position = new Vector3(0,0,0);
      }
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

      _laserAudio = GameObject.Find("Laser_Audio").GetComponent<AudioSource>();
      if(_laserAudio == null)
        Debug.LogError("Laser Audio is NULL");

      _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
      if(_audioManager == null)
        Debug.LogError(message: "Audio Manager is NULL");        
    }

    void Update()
    {
      if(playerNumber.Equals(PlayerNumber.playerOne))
        MovePlayer1();
      if(playerNumber.Equals(PlayerNumber.playertwo))
        MovePlayer2();        
      if(Input.GetButtonUp("Jump") && Time.time >_canFire && playerNumber.Equals(PlayerNumber.playerOne))
        FireLaser();

      if(Input.GetButtonDown("Fire3") && Time.time > _canFire && playerNumber.Equals(PlayerNumber.playertwo))
        FireLaser();
    }
  #endregion

  #region Private Methods
    void MovePlayer1()
    {
      float horizontalInputAxis = Input.GetAxis("Horizontal");
      float verticalInputAxis = Input.GetAxis("Vertical");

      transform.Translate(new Vector3(horizontalInputAxis, verticalInputAxis, 0) * _movementConfiguataion.playerMovementSpeed * Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f , 5.5f), 0);
      
      if(transform.position.x <= -10f)
        transform.position = new Vector3(10f,transform.position.y,0);
      else if(transform.position.x >= 10f)
        transform.position = new Vector3(-10f,transform.position.y,0);
    }

    void MovePlayer2()
    {
      float horizontalInputAxis = Input.GetAxis("HorizontalPlayer2");
      float verticalInputAxis = Input.GetAxis("VerticalPlayer2");

      transform.Translate(new Vector3(horizontalInputAxis, verticalInputAxis, 0) * _movementConfiguataion.playerMovementSpeed * Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f , 5.5f), 0);
      
      if(transform.position.x <= -10f)
        transform.position = new Vector3(10f,transform.position.y,0);
      else if(transform.position.x >= 10f)
        transform.position = new Vector3(-10f,transform.position.y,0);
    }

    void FireLaser()
    {
      _canFire = Time.time + _fireRate;
      if(_isTripleShotActive)
        Instantiate(_tripleShotPrefab, transform.position + Vector3.up, Quaternion.identity);
      else
        Instantiate(_laserPrefab, transform.position + Vector3.up, Quaternion.identity);
      _laserAudio.Play();
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
      if(_lives == 2)
        _leftEngine.SetActive(true);
      else if(_lives == 1)
        _rightEngine.SetActive(true);
      _uiManager.UpdateLife(_lives);
      if(_lives <= 0)
      {
        _spawnManager.OnPalyerDeath();
        int[] f = {_numberOfPowerUpCollected,_score/10,_score};
        Destroy(this.gameObject);
        _audioManager.ExplosionSound();
      }
    }
    public void EnableTripleShot()
    {
      _isTripleShotActive = true;
      _numberOfPowerUpCollected++;
      StartCoroutine("TriplePowerShotPowerDownRoutine");
    }
    public void EnableSpeedBoost()
    {
      _isSpeedBoostActive = true;
      _movementConfiguataion.playerMovementSpeed *= 2;
      _numberOfPowerUpCollected++;
      StartCoroutine("SpeedBoostPowerDownRoutine");
    }
    public void EnableShield()
    {
      _isShieldActive = true;
      _numberOfPowerUpCollected++;
      _shield.SetActive(true);
    }
    public void KillEnemyAndAddScore(int reward = 10)
    {
      _score += reward;
      _uiManager.UpdateScore(_score);
    }
  #endregion
}