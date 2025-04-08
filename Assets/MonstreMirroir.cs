using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MonstreMirroir : MonoBehaviour
{
    public AudioSource hideinsound;

    public GameObject FreezeEffect;

    float freezeTimer = 0;

    public RenderTexture camRender;
    const float randomSphereSize = 20;

    float waittimer;

    bool onhide = false, viewhide = false, hidedeath = false;
    bool onesound = true;

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
        Debug.Log(waittimer);

        if (freezeTimer <= 0)
        {
            if (!PlayerScript.isHiding())
            {
                agent.destination = player.position;
                randomTarget = transform.position;
                onhide = false;
                viewhide = false;
                hidedeath = false;
                onesound = true;
            }
            else
            {
                Debug.DrawRay(transform.position, (player.position - transform.position) * 1000, Color.cyan);
                if (!onhide)
                {
                    viewhide = Physics.Raycast(transform.position + new Vector3(0, transform.localScale.y * 0.5f, 0), player.position - transform.position, out RaycastHit hit)
                        && hit.collider.tag == "hidespot" && Vector3.Angle(transform.forward, player.position - transform.position) < 90;

                    if (Vector3.Distance(transform.position, player.position) < 10)
                    {
                        hidedeath = true;
                    }


                    Debug.LogWarning(hit.collider.tag);
                    Debug.LogWarning(Vector3.Angle(transform.forward, player.position - transform.position) < 90);

                    waittimer = Random.Range(4f, 8f);
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
                        if (hidedeath)
                            Debug.LogError("player is dead !");

                        if (onesound)
                        {
                            onesound = false;
                            hideinsound.volume = 1;
                            hideinsound.Play();
                        }

                        agent.isStopped = true;
                        waittimer -= Time.deltaTime;
                        if (waittimer <= 0)
                            viewhide = false;

                        if (waittimer < 1)
                            hideinsound.volume *= 0.95f;
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
