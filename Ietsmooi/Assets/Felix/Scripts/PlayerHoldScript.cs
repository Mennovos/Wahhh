using UnityEngine;

public class PlayerHoldScript : MonoBehaviour
{
    [SerializeField] private Transform hand;
    public InventoryItem[] allItems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(InventoryItem i in allItems)
        {
            AddToInventory(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddToInventory(InventoryItem i)
    {
        GameObject a = Instantiate(i.obj, Vector3.zero, Quaternion.Euler(0f, 0f, 0f));
        a.transform.SetParent(hand);

        a.transform.localPosition = Vector3.zero;
        Debug.Log(a);
    }
}

