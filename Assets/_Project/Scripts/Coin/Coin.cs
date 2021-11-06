using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinCounter _coinCounter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Ball ball))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _coinCounter.AddCoin();
    }
}
