using UnityEngine;
using UnityEngine.AI;

public class MonstreMirroir : MonoBehaviour
{
    public RenderTexture camRender;

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

        float d = Mathf.Clamp(1 - Vector3.Distance(transform.position, player.position) / 30, 0.1f, 1.0f);

        camRender.Release();
        camRender.width = (int)(d * 130);
        camRender.height = (int)(d * 130);
    }
}
