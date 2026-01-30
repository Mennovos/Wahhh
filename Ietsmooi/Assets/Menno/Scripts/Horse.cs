using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject horse;
    private PlayerScript playerScript;
    private bool Inrange;
    private void Start()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
        horse.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Inrange = true;
        if (playerScript.isInteracting == true && Inrange == true)
        {
            horse.SetActive(true);
            player.SetActive(false);
            Destroy(gameObject);
        }
    }
}
