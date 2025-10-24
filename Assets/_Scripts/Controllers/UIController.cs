using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private EnemySpawner spawnReference;
    [SerializeField] private Objective objectiveReference;

    void OnEnable()
    {
        objectiveReference.OnObjectiveDestroyed += ShowGameOverMenu;
    }

    void OnDisable()
    {
        objectiveReference.OnObjectiveDestroyed -= ShowGameOverMenu;
    }

    public void ShowEndWaveMenu() { }

    public void HideEndWaveMenu() { }

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
}
