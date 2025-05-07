using UnityEngine;

public class PickUp : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private PlayerMovement player;

    bool canPick;
    //[SerializeField] GameObject target;

    void Start()
    {
        
        //takeObject = GameObject.FindGameObjectsWithTag("Item");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canPick == true)
        {
            //this.transform.parent = target.transform;
            this.transform.localEulerAngles = new Vector3 (0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
            player.LockControl = true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            this.transform.parent = null;
            this.GetComponent <Rigidbody>().isKinematic = false;
            player.LockControl = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canPick = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canPick = false;
    }
}
