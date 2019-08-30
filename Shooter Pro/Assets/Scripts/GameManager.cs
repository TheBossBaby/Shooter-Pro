using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Private Attributes
        private bool _isGameOver = false;
    #endregion

    #region MonoBehaviour Callback
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.R)
               && _isGameOver)
            {
                SceneManager.LoadScene(0); // Load Game Scene
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