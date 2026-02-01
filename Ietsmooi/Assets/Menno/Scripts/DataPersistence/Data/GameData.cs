using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deathCount;


    //starting values for initializing a new game
    public GameData()
    {
        this.deathCount = 0;
    }
}
