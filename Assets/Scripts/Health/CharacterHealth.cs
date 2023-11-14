using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth;
    public HealthBar healthBar;
    private float curHealth;
    public Transform teleportTarget;
    public GameObject player;
    // Start is called before the first frame update
    void Start(){
        curHealth = maxHealth;
    }

    public void AddHealth(float amount)
    {
        curHealth += amount;
    }
    public void TakeDamage(float damage) {
        curHealth -= damage;
        healthBar.UpdateHealth(curHealth / maxHealth);
        if (curHealth <= 0)
        {
            curHealth = 0;
            player.transform.position = teleportTarget.transform.position;
            curHealth = maxHealth;
            healthBar.UpdateHealth(maxHealth);
        }
    }
    
}
