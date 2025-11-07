using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GPGSController : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private TMPro.TMP_Text gpgsText;

    void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuth);
    }

    void OnEnable()
    {
        spawner.OnWaveWon += UnlockAchievement;
    }

    void OnDisable()
    {
        spawner.OnWaveWon -= UnlockAchievement;
    }


    internal void ProcessAuth(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            var user = Social.localUser;
            gpgsText.text = $"Good Auth:\n{user.userName}\n{user.id}";
        }
        else
        {
            gpgsText.text = "Bad Auth";
        }
    }

    internal void UnlockAchievement()
    {
        string achievementStatus;
        Social.ReportProgress(
            GPGSIds.achievement_first_wave_completed,
            100,
            (bool success) =>
            {
                achievementStatus = success ? "Achievement unlocked" : "Fail in achievement unlock";
                gpgsText.text = achievementStatus;
            }
        );
    }
}
