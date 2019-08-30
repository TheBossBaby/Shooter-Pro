﻿using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Private Serialized Field
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemyContainer;
        [SerializeField] private GameObject[] powerUps;

    #endregion

    #region Private Field
        private bool _stopSpawn = false;
    #endregion
    // Start is called beafore the first frame updategne
    void Start()
    {
        StartCoroutine("SpawnRoutine");
        StartCoroutine("SpawnPowerupRoutine");        
    }

    // Update is called once per frame
    void Update()
    {
    }

    #region Private Coroutines
        IEnumerator SpawnRoutine()
        {
            while(_stopSpawn == false)
            {
                float randomX = Random.Range(-9.54f, 9.27f);
                Vector3 posToSpawn = new Vector3(randomX,7f,0f);
                GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(5);
            }
        }
        IEnumerator SpawnPowerupRoutine()
        {
            while(_stopSpawn == false)
            {
                float randomX = Random.Range(-9.54f, 9.27f);
                Vector3 posToSpawn = new Vector3(randomX,7f,0f);
                GameObject powerUp = powerUps[Random.Range(0,powerUps.Length)];
                GameObject newEnemy = Instantiate(powerUp, posToSpawn, Quaternion.identity);
                newEnemy.transform.parent = _enemyContainer.transform;
                yield return new WaitForSeconds(Random.Range(3,8));                
            }
        }
    #endregion
    
    #region Public Methods
        public void OnPalyerDeath()
        {
            _stopSpawn = true;
        }
    #endregion
}