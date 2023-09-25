using System.Collections;
using TowerDefence.Components;
using UnityEngine;

public class ProjectileParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    private TowerProjectile associatedProjectile;
    private Transform associatedProjectileVisual;

    private void Update()
    {
        if (associatedProjectileVisual)
            transform.position = associatedProjectileVisual.position;
    }

    public void SetAssociatedProjectile(TowerProjectile projectile)
    {
        associatedProjectile = projectile;
        associatedProjectile.TargetAchieved += OnProjectileTargetAchieved;
        associatedProjectileVisual = associatedProjectile.Visual;
    }

    private void OnProjectileTargetAchieved()
    {
        if (associatedProjectile)
            associatedProjectile.TargetAchieved -= OnProjectileTargetAchieved;

        associatedProjectile = null;
        associatedProjectileVisual = null;

        particles.Stop();
        StartCoroutine(WaitToDisable());
    }

    private IEnumerator WaitToDisable()
    {
        yield return new WaitForSecondsRealtime(particles.main.duration);

        gameObject.SetActive(false);
    }
}