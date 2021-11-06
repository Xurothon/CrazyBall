using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _minSpeedIncrement;
    [SerializeField] private float _maxSpeedIncrement;
    private float _speedIncrement;
    [SerializeField] private float _maxDistance;
    private Vector3 _offset;
    private Vector3 _initialOffset;

    public void SetTarget(Transform target)
    {
        _target = target;
    }


    private void Start()
    {
        _initialOffset = transform.position - _target.position;
        _offset = _initialOffset;
        _speedIncrement = _minSpeedIncrement;
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position + _offset, Time.deltaTime * _speedIncrement);
            CheckDistance();
        }
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance > _maxDistance) _speedIncrement = _maxSpeedIncrement * distance / _maxDistance;
        else _speedIncrement = _minSpeedIncrement;
    }
}
