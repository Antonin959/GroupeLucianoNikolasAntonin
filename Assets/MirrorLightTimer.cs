using UnityEngine;

public class MirrorLightTimer : MonoBehaviour
{
    GameObject lightparticlecoll;

    public float timer;

    void Start()
    {
        lightparticlecoll = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            lightparticlecoll.SetActive(false);
        }
        else
            lightparticlecoll.SetActive(true);
    }
}
