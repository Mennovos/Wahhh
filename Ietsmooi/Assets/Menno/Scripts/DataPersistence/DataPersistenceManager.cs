using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;
    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
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
        // Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No data found. Initializing data to defaults.");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }   
    }

    public void SaveGame()
    {
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(data: ref gameData);
        }
        //save data to a file using the data handler
        dataHandler.Save(gameData);
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
        .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}

