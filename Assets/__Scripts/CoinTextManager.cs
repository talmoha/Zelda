using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inv playerInventory;//ref coins in inv
    public TextMeshProUGUI coinDisplay;//coin displayed

    public void UpdateCoinCount()
    {
        coinDisplay.text=""+playerInventory.coins;//updating number of coins displayed in game
    }
}
