using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hits = 10;
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] AudioClip takeDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnParticleCollision(GameObject other)
    {
        print("hit");
        hits--;

        if (hits <= 0)
        {
            EnemyDeath();
        }
    }

    private void EnemyDeath()
    {
        DeathEffects();
        Destroy(gameObject);
    }

    private void DeathEffects()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        float fxDieTime = fx.GetComponent<ParticleSystem>().main.duration;
        Destroy(fx, fxDieTime);
    }
}
