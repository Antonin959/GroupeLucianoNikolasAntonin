using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ObjectInteracting : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Space(10)]
    [SerializeField] private PlayerMovement player;

    private Raycast _camera;

    //private Transform _inspectObjectTransform;

    public float deltaRotationX;
    public float deltaRotationY;

    public float rotateSpeed = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      //_player = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*RaycastHit rayHit;
            if (CameraToMouseRay(Input.mousePosition, out rayHit)) 
            {
            
            }*/
        }
    }

    public void ShowObject()
    {

    }

    public void DisableObject()
    {

    }

    public void ShowItem()
    {

    }

    public void DisableItem()
    {

    }
}
