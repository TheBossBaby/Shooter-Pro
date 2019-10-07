using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LogInGamer : MonoBehaviour
{
    [SerializeField] private CredentialsConfig credentialsConfig;
    [SerializeField] private  string PORT;
    [SerializeField] private  string ServerLink;
    [SerializeField] private string sucsessfulLoginScene;

    [SerializeField] private InputField gamerIdInputField;    
    [SerializeField] private InputField gamerPasswordInputField;
    private  string gamerId = "";
    private string password = "";
    private IEnumerator coroutine;

    private void Start() {
        PORT = credentialsConfig.PORT;
        ServerLink = credentialsConfig.URL;
    }
    [SerializeField] private GameObject registrationPanel;

    public void ActivateRegistationPanel()
    {
        registrationPanel.SetActive(true);
    }

    public void TakeIdInput()
    {
        gamerId = gamerIdInputField.text;
    }

    public void TakePasswordInput()
    {
        password = gamerPasswordInputField.text;
    }

    public void HitLogInApi()
    {
        coroutine = LogIn();
        StartCoroutine(coroutine);
    }

    IEnumerator LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("gamerid", gamerId);
        form.AddField("password", password);
        form.AddField("projectId", credentialsConfig.projectId);

        using (UnityWebRequest www = UnityWebRequest.Post(ServerLink +":" + PORT + "/unity/login", form))
        {
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error in connection: " + www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if(www.responseCode == 200)
                {
                    GamerObject.instance.activeGamerId = gamerId;
                    this.gameObject.SetActive(false);
                    SceneManager.LoadScene(sucsessfulLoginScene);
                }
                var c = www.downloadHandler.text;
                Debug.Log("Response-> " + c);
            }
        }
    }
}