using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    #region Public Methods
    public void LoadSinglePlayerGame() => SceneManager.LoadScene(1); // Load Game Scene

    public void LoadCoOpGame() => SceneManager.LoadScene(2); // Load Co Op mode game
    #endregion
}