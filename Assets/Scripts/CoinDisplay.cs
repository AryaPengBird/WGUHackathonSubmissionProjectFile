using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
        
    }

    private void Update()
    {
        if (coinText != null)
        {
            coinText.text = "Orbs: " + GameManager.Instance.GetCoin();
        }
    }
}
