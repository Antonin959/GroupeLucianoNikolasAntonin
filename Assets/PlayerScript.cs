using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Audio;
public class PlayerScript : MonoBehaviour
{
    public AudioClip[] MarcheHerbe, MarcheBois;
    AudioSource audioSource;

    public Transform spawnpos;
    public Animator mirroir;

    bool isEntranceOpen = false, isMirrorVisible = false, isWalking = false;

    float rotX;
    public float speed = 2.0f;
    const float sensi = 20;
    Rigidbody rb;
    Transform cam;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0);
        StartCoroutine(PlayRandomClips());

    }
    void Update()
    {
        if (!isMirrorVisible) speed = 2.0f;
        else speed = 0.6f;

        rb.linearVelocity = (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right) * speed + new Vector3(0, rb.linearVelocity.y, 0);

        isWalking = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;

        rotX -= Input.GetAxis("Mouse Y") * sensi;
        rotX = Mathf.Clamp(rotX, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        transform.localRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensi, 0);

        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(transform.position, cam.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name, hit.transform.gameObject);

            if (!isEntranceOpen && hit.transform.tag == "Entrancedoor")
            {
                hit.transform.GetComponent<Animator>().SetBool("IsOpen", true);//.Play("EntranceDoorOpen");
                //isEntranceOpen = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isMirrorVisible = !isMirrorVisible;
            mirroir.SetBool("is", isMirrorVisible);
        }
    }

    IEnumerator PlayRandomClips()
    {
        bool onHerbe = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) && hit.transform.tag == "GroundHerbe";

        AudioClip clip = onHerbe ?MarcheHerbe[Random.Range(0, MarcheHerbe.Length)] : MarcheBois[Random.Range(0, MarcheBois.Length)];
        audioSource.clip = clip;
        if (isWalking)
        {
            Debug.Log(isWalking);
            audioSource.Play();
        }

        yield return new WaitForSeconds(clip.length);

        StartCoroutine(PlayRandomClips());

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ManoirTeleporter")
            transform.position = spawnpos.position;
    }
}
