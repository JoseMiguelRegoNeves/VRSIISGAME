using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Health health;
    public void OnHit(DamageDealer weapon, Vector3 direction)
    {
        health.TakeDamage(weapon.damageAmount, direction);
    }
}
