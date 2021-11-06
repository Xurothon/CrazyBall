using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    private int _coins;

    public void AddCoin()
    {
        _coins++;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        _coinText.text = _coins.ToString();
    }

    private void Start()
    {
        UpdateCoinText();
    }
}
