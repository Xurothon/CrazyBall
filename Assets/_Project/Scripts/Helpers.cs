using UnityEngine;

public class Helpers : MonoBehaviour
{
    public static Helpers Instance { get; private set; }
    public TimeScale TimeScale => _timeScale;
    [SerializeField] private TimeScale _timeScale;

    private void Awake()
    {
        Instance = this;
    }
}
