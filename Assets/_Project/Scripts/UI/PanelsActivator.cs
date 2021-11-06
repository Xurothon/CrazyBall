using UnityEngine;

public class PanelsActivator : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _levelCompletePanel;
    [SerializeField] private Ball _ball;
    [SerializeField] private EnemyCountChecker _enemyCountChecker;

    private void ActiveGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    private void ActiveLevelComplete()
    {
        _levelCompletePanel.SetActive(true);
    }

    private void OnBallDied()
    {
        ActiveGameOverPanel();
    }

    private void OnAllEnemyDied()
    {
        ActiveLevelComplete();
    }

    private void OnEnable()
    {
        _ball.Die += OnBallDied;
        _enemyCountChecker.AllEnemyDie += OnAllEnemyDied;
    }

    private void OnDisable()
    {
        _ball.Die -= OnBallDied;
        _enemyCountChecker.AllEnemyDie -= OnAllEnemyDied;
    }
}
