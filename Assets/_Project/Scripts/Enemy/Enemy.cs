using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider))]
[RequireComponent(typeof(EnemyMovement), typeof(RagdollControl))]
public class Enemy : MonoBehaviour
{
    public bool IsDie { get; private set; }
    [SerializeField] private Rigidbody _forceRigidbody;
    [SerializeField] private SkinnedMeshRenderer _mesh;
    [SerializeField] private Material _die;
    [SerializeField] private ParticleSystem _simpleDieParticle;
    [SerializeField] private ParticleSystem _dieParticle;
    [SerializeField] private Vector3 _particlesPffset;
    [SerializeField] private Vector3 _particlesAddRotation;
    [SerializeField] private EnemyCountChecker _enemyCountChecker;
    private EnemyMovement _enemyMove;
    private RagdollControl _ragdollControl;
    protected Animator _animator;
    private BoxCollider _boxCollider;

    public void TakeDamage(Ball ball, float addVelocity)
    {
        _enemyCountChecker.Remove(this);
        MakePhysical(ball);
        Helpers.Instance.TimeScale.Down();
        if (!ball.HasFullPower)
        {
            Instantiate(_simpleDieParticle, transform.position + _particlesPffset, Quaternion.identity);
            Vector3 direction = transform.position - ball.transform.position;
            direction.y = 0.3f;
            _forceRigidbody.AddForce(direction * addVelocity, ForceMode.Impulse);
            _mesh.sharedMaterial = _die;

        }
        else
        {
            transform.LookAt(ball.transform);
            _mesh.gameObject.SetActive(false);
            Instantiate(_dieParticle, transform.position + _particlesPffset, Quaternion.Euler(_particlesAddRotation + transform.eulerAngles));

        }
        this.Wait(2f, () => Destroy(gameObject));
    }

    public void StartMove(Transform ball)
    {
        _enemyMove.Move(ball);
        _animator.SetTrigger("Run");
    }

    public void MakePhysical(Ball ball)
    {
        IsDie = true;
        _animator.enabled = false;
        _boxCollider.enabled = false;
        if (!ball.HasFullPower)
        {
            _ragdollControl.MakePhysical();
        }
    }

    private void KickBall(Ball ball)
    {
        transform.LookAt(ball.transform.position);
        _animator.SetTrigger("Kick");

        Vector3 direction = ball.transform.position - transform.position;
        direction.y = 0.3f;
        ball.JumpBack(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsDie)
        {
            if (other.gameObject.TryGetComponent(out Ball ball))
            {
                if (ball.IsCanDied)
                {
                    KickBall(ball);
                }
                else
                {
                    if (!ball.IsDie)
                    {
                        if (ball.Velocity < 0.5f)
                        {
                            KickBall(ball);
                        }
                        else
                        {
                            _enemyMove.Stop();
                            TakeDamage(ball, 200);
                        }
                    }
                }
            }
        }
    }

    protected void Awake()
    {
        _enemyMove = GetComponent<EnemyMovement>();
        _ragdollControl = GetComponent<RagdollControl>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider>();
        _forceRigidbody.sleepThreshold = 0.0f;
    }
}
