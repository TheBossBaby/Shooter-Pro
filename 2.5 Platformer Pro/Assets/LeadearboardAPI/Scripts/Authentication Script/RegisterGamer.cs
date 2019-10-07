using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Networking;

public class RegisterGamer : MonoBehaviour
{
    [SerializeField] private CredentialsConfig credentialsConfig;
    [SerializeField] private  string PORT = "5000";
    [SerializeField] private  string ServerLink = "http://ec2-18-222-25-184.us-east-2.compute.amazonaws.com";

    [SerializeField] private InputField gamerNameInputField;
    [SerializeField] private InputField gamerIdInputField;    
    [SerializeField] private InputField gamerPassword1InputField;
    [SerializeField] private InputField gamerPassword2InputField;     
    private  string gamerName = "";
    private  string gamerId = "";
    private string password1 = "";
    private string password2 = "";
    private IEnumerator coroutine;

    private void Start() {
        PORT = credentialsConfig.PORT;
        ServerLink = credentialsConfig.URL;
    }
    
    public void CloseRegistrationPanel()
    {
        this.gameObject.SetActive(false);
    }
    public void TakeNameInput()
    {
        gamerName = gamerNameInputField.text;
    }

    public void TakeIdInput()
    {
        gamerId = gamerIdInputField.text;
    }

    public void TakePassword1Input()
    {
        password1 = gamerPassword1InputField.text;
    }

    public void TakePassword2Input()
    {
        password2 = gamerPassword2InputField.text;
    }

    public void HitRegisterGamerApi()
    {
        if(gamerName.Length == 0 || gamerId.Length == 0 || password1.Length == 0)
            return;
        if(password1 != password2)
            return;
        if(password1.Length < 6)
            return;
        coroutine = Register();
        StartCoroutine(coroutine);
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("gamerid", gamerId);
        form.AddField("gamername", gamerName);
        form.AddField("password", password1);
        form.AddField("projectId", credentialsConfig.projectId);

        using (UnityWebRequest www = UnityWebRequest.Post(ServerLink +":" + PORT + "/unity/register", form))
        {
            www.SetRequestHeader("Accept", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                if(www.responseCode == 200)
                {
                    this.gameObject.SetActive(false);
                }
                var c = www.downloadHandler.text;
                Debug.Log("Response-> " + c);
            }
        }
    }    
}