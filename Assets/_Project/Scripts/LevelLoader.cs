using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _nextLevelButton;

    private void RestartLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);
    }

    private void LoadNextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _nextLevelButton.onClick.AddListener(LoadNextLevel);
    }
}
