using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
public class MongoDbSetup : MonoBehaviour
{
    [SerializeField]
    private  string PORT;
    [SerializeField]
    private  string ServerLink;    
    
    [SerializeField] private CredentialsConfig credentialsConfig;
    private  string developerId;
    private  string projectId;
    [SerializeField]
    private  string leaderboardId;

    [SerializeField]
    private  string gamerId;
    [SerializeField]
    private int[] scores;
    [SerializeField]
    private string[] values;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        developerId = credentialsConfig.developerId;
        projectId = credentialsConfig.projectId;
        PORT = credentialsConfig.PORT;
        ServerLink = credentialsConfig.URL;        
        gamerId = GamerObject.instance.activeGamerId;
    }

    IEnumerator Upload(int[] scores)
    {
        WWWForm form = new WWWForm();
        form.AddField("developerId", developerId);
        form.AddField("projectId", projectId);
        form.AddField("leaderboardId", leaderboardId);
        form.AddField("gamerId", gamerId);

        foreach (var item in scores)
        {
            form.AddField("scores[]", item);            
        }

        foreach (var item in values)
        {
            form.AddField("values[]", item);            
        }
        using (UnityWebRequest www = UnityWebRequest.Post(ServerLink +":" + PORT + "/unity/createOrUpdateUser", form))
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
                var c = www.downloadHandler.text;
                Debug.Log("Response-> " + c);
            }
        }
    }

    public void UpdateClientScore()
    {
        coroutine = Upload(scores);
        StartCoroutine(coroutine);
    }

    public void UpdateClientScore(int[] localScores)
    {
        coroutine = Upload(localScores);
        StartCoroutine(coroutine);
    }
}