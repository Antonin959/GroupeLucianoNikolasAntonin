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

        InventoryScript.HasItemInventoryIndex(3, () =>
        {
            FlashlightActive = !FlashlightActive;
            FlashlightLight.SetActive(FlashlightActive);
        });
        
    }
}
