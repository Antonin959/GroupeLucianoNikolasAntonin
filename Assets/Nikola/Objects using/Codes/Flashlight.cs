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
        //Permet de vérifier si l'objet est présent dans l'inventaire

        InventoryScript.HasItemInventoryIndex(3, () =>
        {
            FlashlightActive = !FlashlightActive;
            FlashlightLight.SetActive(FlashlightActive);
        });
        
    }
}
