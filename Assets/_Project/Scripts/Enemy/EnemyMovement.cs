using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent _navMesh;
    private Transform _ball;
    private bool _isMove;

    public void Move(Transform ball)
    {
        _isMove = true;
        _ball = ball;
    }

    public void Stop()
    {
        _isMove = false;
        _navMesh.isStopped = true;
    }

    private void Update()
    {
        if (_isMove)
        {
            if (Vector3.Distance(_ball.position, transform.position) > 0.5f)
            {
                _navMesh.isStopped = false;
                _navMesh.SetDestination(_ball.position);
            }
            else
            {
                _navMesh.isStopped = true;
            }
        }
    }

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
    }
}
