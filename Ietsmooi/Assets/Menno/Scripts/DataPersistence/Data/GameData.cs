using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;

    public Vector3 playerPosition;
    // can use this also for checkpoint and other collectibles
    public Dictionary<string, bool> coinsCollected;

    //starting values for initializing a new game
    public GameData()
    {
        this.deathCount = 0;
        playerPosition = new Vector3(-2569.9f, 38.04f, -3384.1f);
        coinsCollected = new Dictionary<string, bool>();
    }
}
