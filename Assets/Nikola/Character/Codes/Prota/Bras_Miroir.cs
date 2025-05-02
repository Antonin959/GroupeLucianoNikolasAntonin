using UnityEngine;

public class Bras_Miroir : MonoBehaviour
{
    public GameObject BrasBas;
    public GameObject BrasHaut;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Show");
            BrasHaut.SetActive(true);
            BrasBas.SetActive(false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Hide");
            BrasHaut.SetActive(false);
            BrasBas.SetActive(true);
        }
    }
}



