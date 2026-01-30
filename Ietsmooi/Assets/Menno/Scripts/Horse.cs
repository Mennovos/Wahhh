using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject horse;
    private PlayerScript playerScript;
    private bool Inrange;
    private float range = 3f;
    public bool hasHorse;
    private void Start()
    {
        playerScript = FindFirstObjectByType<PlayerScript>();
        horse.SetActive(false);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= range)
        {
            Inrange = true;
            // Player is in range
        }
        else
        {
            Inrange = false;
            // Player is out of range
        }

        if (playerScript.isInteracting == true && Inrange == true)
        {
            hasHorse = true;
            horse.SetActive(true);
            player.SetActive(false);
            Destroy(gameObject);
        }
    }
}
