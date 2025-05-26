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
        //Permet de v�rifier si l'objet est pr�sent dans l'inventaire
        if (InventoryScript.HasItemInventoryIndex(3) && Input.GetKeyDown(KeyCode.F)) 
        {
            FlashlightActive = !FlashlightActive;
            FlashlightLight.SetActive(FlashlightActive);
        }
    }
}
