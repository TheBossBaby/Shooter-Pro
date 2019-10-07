using UnityEngine;

[CreateAssetMenu(fileName = "leaderboardApiConfig", menuName = "Leaderboard API Config")]
public class CredentialsConfig : ScriptableObject
{
    public string developerId;
    public string projectId;
    public string PORT;
    public string URL;
}