using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int hitPoints = 10;
    [SerializeField] GameObject deathFX;
    [SerializeField] Text hpText;
    [SerializeField] AudioClip reachPlayerBase;

    private void Start()
    {
        hpText.text = hitPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(GetComponent<AudioSource>().isPlaying)
        {

        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(reachPlayerBase);
        }

        if (hitPoints <= 10 && hitPoints > 1)
        {
            print("point loss");
            hitPoints = hitPoints - 1;
            hpText.text = hitPoints.ToString();
        }

        if (hitPoints == 1)
        {
            print("dead");
            hitPoints--;
            hpText.text = hitPoints.ToString();
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            float fxDieTime = fx.GetComponent<ParticleSystem>().main.duration;
            Destroy(fx, fxDieTime);
            Destroy(gameObject);
        }
    }



}
