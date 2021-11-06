using UnityEngine;
using System.Collections.Generic;

public class EnemyDistanceChecker : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private Ball _ball;
    [SerializeField] private float _distance;

    private void CheckDistance()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (GetDistance(_enemies[i]) < _distance)
            {
                _enemies[i].StartMove(_ball.transform);
                _enemies.RemoveAt(i);
            }
        }
    }

    private float GetDistance(Enemy enemy)
    {
        return Vector3.Distance(_ball.transform.position, enemy.transform.position);
    }

    private void LateUpdate()
    {
        if (_enemies.Count > 0)
        {
            CheckDistance();
        }
    }
}
