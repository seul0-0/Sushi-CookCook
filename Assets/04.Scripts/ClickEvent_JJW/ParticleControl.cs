using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    AutoAttack autoAttack;

    [Header("파티클 프리팹 or 컴포넌트")]
    [SerializeField] private ParticleSystem attackParticlePrefab;
    [SerializeField] private ParticleSystem criticalParticlePrefab;

    public void PlayNormalParticle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        ParticleSystem normalParticle = Instantiate(attackParticlePrefab, mousePosition, Quaternion.identity);
        normalParticle.Play();

        Destroy(normalParticle.gameObject, normalParticle.main.duration);
    }
    public void PlayCriticalParticle()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        ParticleSystem criticalParticle = Instantiate(criticalParticlePrefab, mousePosition, Quaternion.identity);
        criticalParticle.Play();

        Destroy(criticalParticle.gameObject, criticalParticle.main.duration);
    }
}
