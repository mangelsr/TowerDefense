using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject wonWaveMenu;
    [SerializeField] private GameObject lastEnemyMessage;
    [SerializeField] private EnemySpawner spawnReference;
    [SerializeField] private Objective objectiveReference;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMPro.TMP_Text resourcesLabel;
    [SerializeField] private TMPro.TMP_Text waveLabel;
    [SerializeField] private TMPro.TMP_Text enemiesLabel;
    [SerializeField] private TMPro.TMP_Text bossesLabel;


    void OnEnable()
    {
        objectiveReference.OnObjectiveDestroyed += ShowGameOverMenu;
        spawnReference.OnWaveStarted += UpdateWave;
        spawnReference.OnWaveFinished += ShowLastEnemyUIMessage;
        spawnReference.OnWaveWon += ShowWonWaveMenu;
        gameManager.OnModifyResources += UpdateResources;
    }

    void OnDisable()
    {
        objectiveReference.OnObjectiveDestroyed -= ShowGameOverMenu;
        spawnReference.OnWaveStarted -= UpdateWave;
        spawnReference.OnWaveFinished -= ShowLastEnemyUIMessage;
        spawnReference.OnWaveWon -= ShowWonWaveMenu;
        gameManager.OnModifyResources -= UpdateResources;
    }

    public void ShowWonWaveMenu()
    {
        enemiesLabel.text = $"ENEMIES:\t{gameManager.defeatedEnemies}";
        bossesLabel.text = $"BOSSES :\t{gameManager.defeatedBosses}";
        wonWaveMenu.SetActive(true);
    }

    public void HideWonWaveMenu()
    {
        wonWaveMenu.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void HideGameOverMenu()
    {
        gameOverMenu.SetActive(false);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void UpdateResources()
    {
        resourcesLabel.text = $"Resources: {gameManager.resources}";
    }

    public void ShowLastEnemyUIMessage()
    {
        lastEnemyMessage.SetActive(true);
        Invoke("HideLastEnemyUIMessage", 3);
    }

    public void HideLastEnemyUIMessage()
    {
        lastEnemyMessage.SetActive(false);
    }

    public void UpdateWave()
    {
        waveLabel.text = $"Wave: {spawnReference.wave}";
    }
}
