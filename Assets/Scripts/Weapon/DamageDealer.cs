using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float damageAmount = 0;
    public ParticleSystem particlePrefab;

    /*private void OnCollisionEnter(Collision collision)
    {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);

        if (collision.gameObject.TryGetComponent(out CharacterHealth health)){
            health.TakeDamage(damageAmount);

        }
        if (collision.gameObject.TryGetComponent(out DamageBodyPart hp)){
            hp.TakeDamage(damageAmount);
        }

        Destroy(this.gameObject);
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        var heading = collision.gameObject.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance; 

        var hitBox = collision.gameObject.GetComponent<HitBox>();
        if (hitBox)
        {
            hitBox.OnHit(this, direction);
        }
        Destroy(this.gameObject);
    }

}
