using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector]
    public float currentHealth;
    public float dieForce;
    SkinnedMeshRenderer skinnedMeshRenderer;
    UiHealthBar healthBar;
    RagDoll ragDoll;

    public float blinkIntensity;
    public float blinkDuration;
    float blinkTimer;

    void Start()
    {
        ragDoll = GetComponent<RagDoll>();
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<UiHealthBar>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBodie in rigidBodies)
        {
            HitBox hitBox = rigidBodie.gameObject.AddComponent<HitBox>();
            hitBox.health = this;
        }
    }

    public void TakeDamage(float damageAmount, Vector3 direction)
    {
        currentHealth -= damageAmount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0f){
            Die(direction);
        }
        blinkTimer = blinkDuration;
    }

    private void Die(Vector3 direction)
    {
        ragDoll.ActivateRagdoll();
        direction.y = 0.5f;
        ragDoll.ApplyForce(direction * dieForce);
        healthBar.gameObject.SetActive(false);

    }

    private void Update()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1.0f;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}

