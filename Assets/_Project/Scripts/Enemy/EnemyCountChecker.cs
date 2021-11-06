using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemyCountChecker : MonoBehaviour
{
    public event UnityAction AllEnemyDie;
    [SerializeField] private List<Enemy> _enemies;

    public void Remove(Enemy enemy)
    {
        _enemies.Remove(enemy);
        if (_enemies.Count == 0)
        {
            AllEnemyDie?.Invoke();
        }
    }
}
