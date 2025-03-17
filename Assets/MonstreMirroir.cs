using UnityEngine;
using UnityEngine.AI;

public class MonstreMirroir : MonoBehaviour
{

    NavMeshAgent agent;
    Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        agent.stoppingDistance = 1.5f; // Outil de debug
        agent.speed = 1.0f;
    }
    void Update()
    {
        agent.destination = player.position;
    }
}
