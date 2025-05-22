using UnityEngine;

public class LifeSpirit : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 5f; //Vie de l'esprit


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health < 0) 
        { 
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
