using UnityEngine;

public class Asteroid : MonoBehaviour
{
    #region Private Attribute
        private float _zAngleRotationSpeed = 15.0f;
        [SerializeField] GameObject _explosionPrefab;
        [SerializeField] SpawnManager _spawnManager;
        AudioManager _audioManager;
    #endregion

    #region MonoBehaviour Callback
        void Start()
        {
            _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            if(_spawnManager == null)
            {
                Debug.LogError("Spawn Manager is NULL");
            }
 
            _audioManager = GameObject.Find("Audio_Manager").GetComponent<AudioManager>();
            if(_audioManager == null)
                Debug.LogError(message: "Audio Manager is NULL");        
        }

        void Update()
        {
            transform.Rotate(Vector3.forward * _zAngleRotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if (other.tag == "Laser")
            {
                Destroy(other.gameObject);
                Instantiate(_explosionPrefab,transform.position, Quaternion.identity);
                _spawnManager.StartSpawning();
                Destroy(this.gameObject, 0.25f);
                _audioManager.ExplosionSound();
            }
        }        
    #endregion
}