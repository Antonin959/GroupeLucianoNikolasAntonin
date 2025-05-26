using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] GameObject FlashlightLight;
    private bool FlashlightActive = false;

    void Start()
    {
        FlashlightLight.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            FlashlightActive = !FlashlightActive;
            FlashlightLight.SetActive(FlashlightActive);
        }
    }
}
