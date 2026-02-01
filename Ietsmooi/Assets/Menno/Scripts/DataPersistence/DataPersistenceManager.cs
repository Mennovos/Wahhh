using UnityEngine;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        LoadGame();
    }
    private void Awake()
    {
         if (instance != null)
         {
              Debug.LogWarning("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
         }
    
         instance = this;

    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if(this.gameData == null)
        {
            Debug.Log("No data found. Initializing data to defaults.");
            NewGame();
        }
    }

    public void SaveGame()
    {

    }   
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

