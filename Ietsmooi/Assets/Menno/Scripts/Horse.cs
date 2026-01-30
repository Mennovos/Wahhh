using UnityEngine;

public class Horse : MonoBehaviour
{
    private PlayerScript playerScript;
    private GameObject player;
    private bool Inrange;
    private void Start()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Inrange = true;
        if (playerScript.isInteracting == true && Inrange == true)
        {
            player.SetActive(false);
        }
    }
}
