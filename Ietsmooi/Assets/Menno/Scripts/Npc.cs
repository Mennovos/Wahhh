using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    private int uploc;
    private NavMeshAgent agent;
   [SerializeField] private List<Transform> Npclocations;
    private void Start()
    {
       agent = GetComponent<NavMeshAgent>();

        //if (agent.SetDestination(Npclocations[uploc].position))
        //{
        //    Debug.Log("Destination set to: " + Npclocations[uploc].position);
        //}
    }
    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            uploc = (uploc + 1) % Npclocations.Count;
            agent.SetDestination(Npclocations[uploc].position);
        }
    }
}
   
