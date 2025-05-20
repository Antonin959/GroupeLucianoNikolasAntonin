using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ObjectExam : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private PlayerMovement player;

    public bool isExamining = false;

    public Transform objectToInspect;

    public float rotationSpeed = 100f;

    public Raycast _raycast;

    bool pick;

    private Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    public Vector3 previousMousePosition;

    void Update()
    {

        if ( pick == true)
        {
            Examine();
        }
    }


    public void IsExam()
    {
        isExamining = !isExamining;
    }

    public void StartExam()
    {
        previousMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        pick = true;

        originalPositions[objectToInspect] = objectToInspect.position;
        originalRotations[objectToInspect] = objectToInspect.rotation;

        player.LockControl = true;

        

        Examine();   
    }

    public void FinishExam()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        pick = false;
        player.LockControl = false;

        NonExamine();
    }

    void Examine()
    {
        if (objectToInspect != null)
        {
            objectToInspect.position = Vector3.Lerp(objectToInspect.position, (_raycast.transform.position + 2f * player.transform.GetChild(0).forward.normalized), 0.2f);
            
            Vector3 deltaMouse = Input.mousePosition - previousMousePosition;
            float rotationSpeed = 1.0f;
            objectToInspect.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            objectToInspect.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            previousMousePosition = Input.mousePosition;
        }
    }

    void NonExamine()
    {
        //Reset la position de l'objet
        if (objectToInspect != null)
        {
           //if (originalPositions.(objectToInspect))

        }
    }
}
