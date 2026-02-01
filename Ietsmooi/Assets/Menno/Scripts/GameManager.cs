using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistence
{
    [SerializeField] private int Amountcoins = 0;
    [SerializeField] private TextMeshProUGUI Coins;

    private int deathCount = 0;
    [SerializeField] private TextMeshProUGUI Deathcount;

    private PlayerScript playerScript;
    private void Awake()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
        Coins.text = "Amount of Coins: " + Amountcoins;
    }


    private void Update()
    {
        //temporary health and death ui update
        if (playerScript.health <= 0)
        {
            deathCount++;
            Deathcount.text = "Death Amount: " + deathCount;
        }
    }


    public void AddCoins()
    {
        Amountcoins++;
        Coins.text = "Amount of Coins: " + Amountcoins;
    }
    public void LoadData(GameData data)
    {
        this.deathCount = data.deathCount;
        foreach (KeyValuePair<string, bool> coin in data.coinsCollected)
        {
            if (coin.Value == true)
            {
                AddCoins();
            }
        }
    }
    public void SaveData(ref GameData data)
    {
        data.deathCount = this.deathCount;
        //nothing to save here
    }
}
