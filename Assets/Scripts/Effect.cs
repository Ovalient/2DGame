using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public Animator animator;

    public Transform slidePoint;
    public GameObject particleObject;
    public ParticleSystem particle;
    public ParticleSystem clone;

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("IsRunning") == true && animator.GetBool("IsSliding") == true && clone == null)
        {
            Slide();
        }
    }

    void Slide()
    {
        clone = Instantiate(particle, slidePoint.position, slidePoint.rotation);
        particle.Play();
        DestroyParticleSystem(clone);
    }

    void DestroyParticleSystem(ParticleSystem particleInstance)
    {
        float start = particle.main.startLifetime.constantMax;
        float duration = particle.main.duration;
        float totalDuration = start + duration;

        Destroy(particleInstance.gameObject, totalDuration);
    }
}
