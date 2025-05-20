using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ObjectInteracting : MonoBehaviour
{

    /*[Header("Input")]
    [SerializeField] private KeyCode closeKey;*/

    [Space(10)]
    [SerializeField] private PlayerMovement player;

    public Raycast _raycast;

    //private Transform _inspectObjectTransform;

    //public float deltaRotationX;
    //public float deltaRotationY;

    public float rotationSpeed = 100f;

    //public float rotateSpeed = 2;

    bool pick;

    private ObjectExam _objExam;

    private Transform examinedObject; //Stocker objet

    //bool add; Prendre ou pas item

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //_player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            RaycastHit rayHit;
            if (CameraToMouseRay(Input.mousePosition, out rayHit)) 
            {
            
            }
        }*/
    }

    public void ShowObject()
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

    public void DisableObject()
    {
        if (Input.GetKeyUp(KeyCode.E) && pick == false)
        {
            /*this.transform.parent = null;
            this.GetComponent<Rigidbody>().isKinematic = false;
            player.LockControl = false;
            */
        }
    }

    public void TakeItem()
    {
        if (Input.GetKey(KeyCode.E) && pick == true)
        {
            /*this.transform.parent = target.transform;
            this.transform.localEulerAngles = new Vector3(0, 0, 0);
            this.GetComponent<Rigidbody>().isKinematic = true;
            player.LockControl = true;
            */

            if (Input.GetKey(KeyCode.E))
            {
                
            }
        }
    }

    public void DisableItem()
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
