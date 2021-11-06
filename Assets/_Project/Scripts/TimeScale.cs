using UnityEngine;

public class TimeScale : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float _slowTime;
    [SerializeField] private float _timeToUp;
    private float _maxTimeSpeed = 1;
    private float _startFixedDeltaTime;

    public void Up()
    {
        Time.timeScale = _maxTimeSpeed;
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }

    public void Down()
    {
        StopAllCoroutines();
        Up();
        Time.timeScale = _slowTime;
        Time.fixedDeltaTime = Time.fixedDeltaTime * _slowTime;
        this.Wait(_timeToUp * _slowTime, Up);
    }

    private void Start()
    {
        _startFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        Up();
    }
}
