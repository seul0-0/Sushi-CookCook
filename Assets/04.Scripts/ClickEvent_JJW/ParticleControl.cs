using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    [Header("파티클 프리팹 or 컴포넌트")]
    [SerializeField] private ParticleSystem attackParticle;
    private WaitForSeconds wait;
    private Coroutine particleCoroutine;

    private void Awake()
    {
        wait = new WaitForSeconds(1f);
    }
    public void PlayAttackParticle()
    {
        if (attackParticle == null) return;

        attackParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        attackParticle.Play();

        if (particleCoroutine != null)
        {
            StopCoroutine(particleCoroutine);
        }
        particleCoroutine = StartCoroutine(StopAfterDuration());
    }
    private IEnumerator StopAfterDuration()
    {
        yield return wait;
        attackParticle.Stop();
    }
}
