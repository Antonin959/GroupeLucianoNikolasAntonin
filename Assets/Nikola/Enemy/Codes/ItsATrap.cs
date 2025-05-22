using UnityEngine;

public class ItsATrap : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Destroy(transform.GetComponent<LifeSpirit>());
        if (other.tag == "Esprit") //A changer pour l'esprit du jeu
        {
            other.GetComponent<LifeSpirit>().TakeDamage(1);
        }

    }

            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
