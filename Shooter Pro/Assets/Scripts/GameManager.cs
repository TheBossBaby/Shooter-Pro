using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Private Attributes
        private bool _isGameOver = false;
    #endregion

    #region Public Attributes
        public bool isCoOpMode = false;
    #endregion
    #region MonoBehaviour Callback
        private void Awake() 
        {
            if(SceneManager.GetActiveScene().buildIndex == 2) // if loaded scene is co op mode
            {
                isCoOpMode = true;
            }
        }
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R)
               && _isGameOver)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load Game Scene
            }

            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }     
    #endregion

    #region Public Methods
        public void GameOver()
        {
            _isGameOver = true;
        }
    #endregion
}