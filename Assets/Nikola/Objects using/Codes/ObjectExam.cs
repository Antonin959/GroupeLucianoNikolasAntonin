using UnityEngine;

public class ObjectExam : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private PlayerMovement player;

    public Transform objectToInspect;

    public float rotationSpeed = 100f;

    public Raycast _raycast;

    bool pick;

    //private Vector3 previousMousePosition;

    void Update()
    {
       

        /*
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) 
        { 
            Vector3 deltaMousePosition = Input.mousePosition - previousMousePosition;
            float rotationX = deltaMousePosition.y * rotationSpeed * Time.deltaTime;
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
            objectToInspect.rotation = rotation * objectToInspect.rotation;

            previousMousePosition = Input.mousePosition;
        }
        */
    }

    public void Exam()
    {
        if (Input.GetKey(KeyCode.E) && pick == true)
        {
            /*this.transform.parent = target.transform;
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
            player.LockControl = true;
            */
        }
    }

    public void UnExam()
    {
        if (Input.GetKeyUp(KeyCode.E) && pick == false)
        {
            /*this.transform.parent = null;
            this.GetComponent<Rigidbody>().isKinematic = false;
            player.LockControl = false;
            */
        }
    }
}
