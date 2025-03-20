using UnityEngine;
using UnityEngine.AI;

public class MonstreMirroir : MonoBehaviour
{
    public RenderTexture camRender;
    const float randomSphereSize = 20;

    NavMeshAgent agent;
    Transform player;
    Vector3 randomTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

        agent.stoppingDistance = 1.5f; // Outil de debug
        agent.speed = 1.0f;
    }
    void Update()
    {
        if (!PlayerScript.isHiding())
        {
            agent.destination = player.position;
            randomTarget = transform.position;
        }
        else
        {
            if (Vector3.Distance(transform.position, randomTarget) < 3)
            {
                GetRandomPos();
            }
            agent.destination = randomTarget;
        }

        float d = Mathf.Clamp(1 - Vector3.Distance(transform.position, player.position) / 30, 0.1f, 1.0f);

        camRender.Release();
        camRender.width = (int)(d * 130);
        camRender.height = (int)(d * 130);
    }

    void GetRandomPos()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomSphereSize;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomSphereSize, NavMesh.AllAreas))
        {
            randomTarget = hit.position;
        }
    }
}
