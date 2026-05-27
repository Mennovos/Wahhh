using UnityEngine;
using UnityEngine.VFX;

public class Pickup : MonoBehaviour, IDataPersistence
{
    //use this script as a base class for all pickups/collectibles
    [SerializeField] private GameObject visual;
    [SerializeField] private string id;

    protected bool collected = false;
    
    //Makes sure each pickup has a unique ID
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private GameManager gameManger;

   // [SerializeField] private GameObject Soundeffect;

    protected virtual void Awake()
    {
        gameManger = FindFirstObjectByType<GameManager>();
    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!collected)
        {
            //Instantiate(Soundeffect, transform.position, Quaternion.identity);
            gameManger.AddCoins();
            Destroy(gameObject);
            collected = true;
        }
    }

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            visual.gameObject.SetActive(false);
        }
    }

    public void SaveData (ref GameData data)
    {
      if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }
}
