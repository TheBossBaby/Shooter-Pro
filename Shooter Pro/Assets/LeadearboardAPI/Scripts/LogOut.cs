using UnityEngine;
using UnityEngine.SceneManagement;

public class LogOut : MonoBehaviour
{
    public string loginScreenScene;
    public void LogOutButton()
    {
        GamerObject.instance.activeGamerId = null;
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(loginScreenScene);
    }
}
