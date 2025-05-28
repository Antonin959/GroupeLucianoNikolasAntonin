using Unity.VisualScripting;
using UnityEngine;

public class ScreamEnvironement : MonoBehaviour
{
    Transform player;

    public AudioSource audiosource;
    public AudioClip[] clips;

    [Range(0, 30)] public float TimerMin, TimerMax;
    [Range(0, 1)] public float VolumeMin, VolumeMax;

    float timer = 0;

    void Start()
    {
        SetTimer();
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (!PlayerScript.gameStart)
            return;

        //Debug.Log(timer);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            audiosource.clip = clips[Random.Range(0, clips.Length)];

            audiosource.loop = false;

            audiosource.volume = Random.Range(VolumeMin, VolumeMax);
            audiosource.panStereo = Random.Range(-1f, 1f);
            audiosource.pitch = Random.Range(-0.2f, 0.2f);

            audiosource.transform.position = player.position; //+ new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));

            audiosource.Play();
            audiosource.Pause();
            audiosource.UnPause();

            SetTimer();
            timer += audiosource.clip.length*2;
        }
    }


    void SetTimer() => timer = Random.Range(TimerMin, TimerMax);

    [Header("preview parameters")]
    public int indexPreview;
    [ContextMenu("ClipSoundPreview")]
    void ClipPreview()
    {
        if (indexPreview < 0 || indexPreview >= clips.Length)
        {
            Debug.LogError("Clip index " + indexPreview + " is out of array range!\n" + "<b>Index : 0 >= <color=red>" + indexPreview + "</color> < " + clips.Length + "</b>");
            return;
        }

        audiosource.clip = clips[indexPreview];
        audiosource.Play();
        Debug.Log("Played " + audiosource.clip.name);
    }
}
