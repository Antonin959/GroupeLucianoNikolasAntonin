using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Transform spawnpos;

    bool isEntranceOpen = false;

    float rotX;
    public float speed = 2.0f;
    const float sensi = 20;
    Rigidbody rb;
    Transform cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = transform.GetChild(0);
    }
    void Update()
    {
        rb.linearVelocity = (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right) * speed + new Vector3(0, rb.linearVelocity.y, 0);

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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ManoirTeleporter")
            transform.position = spawnpos.position;
    }
}
