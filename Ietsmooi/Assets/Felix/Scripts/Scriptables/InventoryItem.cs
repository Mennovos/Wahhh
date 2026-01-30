using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptable Objects/InventoryItem")]
public class InventoryItem : ScriptableObject
{
    [Header("general stuff")]
    public GameObject obj;
    public int index;
    public bool unlocked;

    [Header("offset stuff")]
    public Vector3 posOffset;
    public Quaternion rotOffset;
}
