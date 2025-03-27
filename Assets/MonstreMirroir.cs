using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonstreMirroir : MonoBehaviour
{
    public GameObject FreezeEffect;

    float freezeTimer = 0;

    public RenderTexture camRender;
    const float randomSphereSize = 20;

    float waittimer;

    bool onhide = false, viewhide = false;

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
        if (freezeTimer <= 0)
        {
            if (!PlayerScript.isHiding())
            {
                agent.destination = player.position;
                randomTarget = transform.position;
                onhide = false;
                viewhide = false;
            }
            else
            {
                Debug.DrawRay(transform.position, (player.position - transform.position) * 1000, Color.cyan);
                if (!onhide)
                {
                    viewhide = Physics.Raycast(transform.position + new Vector3(0, transform.localScale.y * 0.5f, 0), player.position - transform.position, out RaycastHit hit)
                        && hit.collider.tag == "hidespot" && Vector3.Angle(transform.forward, player.position - transform.position) < 90;


                    Debug.LogWarning(hit.collider.tag);
                    Debug.LogWarning(Vector3.Angle(transform.forward, player.position - transform.position) < 90);

                    waittimer = Random.Range(2f, 4f);
                }
                onhide = true;


                if (!viewhide)
                {
                    if (Vector3.Distance(transform.position, randomTarget) < 3)
                    {
                        GetRandomPos();
                    }
                    agent.destination = randomTarget;
                }
                else
                {
                    agent.destination = player.position;
                    randomTarget = transform.position;

                    if (Vector3.Distance(transform.position, player.position) < 5)
                    {
                        agent.isStopped = true;
                        waittimer -= Time.deltaTime;
                        if (waittimer <= 0)
                            viewhide = false;
                    }
                }
            }
            agent.isStopped = false;
        }
        else
        {
            freezeTimer -= Time.deltaTime;
            agent.isStopped = true;
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

    public void Freeze(Vector3 pos)
    {
        freezeTimer = 5.0f;

        AutoDestroy(Instantiate(FreezeEffect, pos, Quaternion.identity), 6.0f);
    }

    IEnumerator AutoDestroy(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }
}
