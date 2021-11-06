using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public event UnityAction Die;
    public bool IsDie { get; private set; }
    public bool IsCanDied { get; private set; }
    public bool IsOnGround { get; private set; }
    public bool HasFullPower
    {
        get
        {
            return _rigidbody.velocity.magnitude > _fullVelocity;
        }
    }
    public float Velocity => _rigidbody.velocity.magnitude;
    [SerializeField] private float _speed;
    [SerializeField] private float _kickSpeed;
    [SerializeField] private float _fullVelocity;
    [SerializeField] private Toucher _toucher;
    private Rigidbody _rigidbody;

    public void Move(Vector3 direction)
    {
        if (IsOnGround)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
        }
    }

    public void Kick(Vector3 direction)
    {
        if (IsOnGround)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.AddForce(direction * _kickSpeed, ForceMode.Impulse);
        }
    }

    public void JumpBack(Vector3 direction)
    {
        IsDie = true;
        Die?.Invoke();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(direction * _speed, ForceMode.Impulse);
    }

    public void Stop()
    {
        if (IsOnGround)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void CanDie()
    {
        IsCanDied = true;
    }

    public void CanNotDie()
    {
        IsCanDied = false;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.TryGetComponent(out Ground ground))
        {
            IsOnGround = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!IsOnGround)
        {
            if (other.transform.TryGetComponent(out Ground ground))
            {
                IsOnGround = true;
            }
        }
    }

    private void OnEnable()
    {
        _toucher.TouchStart += CanDie;
        _toucher.TouchEnd += CanNotDie;
    }

    private void OnDisable()
    {
        _toucher.TouchStart -= CanDie;
        _toucher.TouchEnd -= CanNotDie;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        IsOnGround = true;
    }
}