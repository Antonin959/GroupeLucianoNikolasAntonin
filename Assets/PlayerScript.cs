using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    static HidingSpotScript hidespot = null;

    public Camera viewCam;
    public RectTransform Canvas;
    public Image HideKey;

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
        TriggerStay();

        if (!isMirrorVisible)
        {
            if (!Input.GetKey(KeyCode.LeftShift))
                speed = 2.5f;
            else
                speed = 4.0f;
        }
        else speed = 0.6f;

        rb.linearVelocity = (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right) * speed + new Vector3(0, rb.linearVelocity.y, 0);

        isWalking = Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0;

        rotX -= Input.GetAxis("Mouse Y") * sensi;
        rotX = Mathf.Clamp(rotX, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
        transform.localRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensi, 0);

        HideKey.gameObject.SetActive(false);

        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!isEntranceOpen && hit.transform.tag == "Entrancedoor")
                {
                    hit.transform.GetComponent<Animator>().SetBool("IsOpen", true);
                }

                if (hit.transform.tag == "hidespot" && hidespot == null)
                {
                    hidespot = hit.transform.GetComponent<HidingSpotScript>();
                    transform.position = hidespot.hidePos.position;
                    transform.rotation = hidespot.hidePos.rotation;
                    transform.localScale = new Vector3(transform.localScale.x, hidespot.isHidingCrouch ? 0.4f : 0.8f, transform.localScale.z);
                }
            }
            if (hit.transform.tag == "hidespot" && !isHiding())
            {
                Vector3 screenpos = viewCam.WorldToViewportPoint(hit.transform.GetComponent<HidingSpotScript>().hidePos.position);

                HideKey.rectTransform.position = new Vector3(screenpos.x * Screen.width, screenpos.y * Screen.height, 0);
                HideKey.gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && hidespot != null)
        {
            transform.position = hidespot.outHidePos.position;
            transform.rotation = hidespot.outHidePos.rotation;
            transform.localScale = new Vector3(transform.localScale.x, 0.8f, transform.localScale.z);
            hidespot = null;
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

    GameObject TriggerObj = null;

    void TriggerStay()
    {
        if (TriggerObj != null)
        {
            if (isMirrorVisible && TriggerObj.tag == "MirorLight")
            {
                RaycastHit hit;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
                {
                    Debug.DrawLine(cam.transform.position, hit.point, Color.blue);
                    if (hit.collider.tag == "MonstreMiroir")
                    {
                        hit.collider.transform.parent.GetComponent<MonstreMirroir>().Freeze(hit.point);
                        TriggerObj.transform.parent.GetComponent<MirrorLightTimer>().timer = 10;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        TriggerObj = other.gameObject;

        if (other.gameObject.name == "ManoirTeleporter")
            transform.position = spawnpos.position;
        
    }

    void OnTriggerExit(Collider other)
    {
        TriggerObj = null;
    }


    public static bool isHiding()
    {
        if (hidespot == null)
            return false;
        return true;
    }
}
