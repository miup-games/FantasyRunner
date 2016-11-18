using UnityEngine;
using System.Collections;

public class CoinUIController : MonoBehaviour
{
    [SerializeField] private TextMesh _coinText;

    private void Awake()
    {
        PlayerRepository.OnCoinChange += this.SetCoins;
        this.SetCoins(PlayerRepository.GetCoins());
    }

    private void OnDestroy()
    {
        PlayerRepository.OnCoinChange -= this.SetCoins;
    }

    private void SetCoins(int coins)
    {
        this._coinText.text = "" + coins;
    }
}
