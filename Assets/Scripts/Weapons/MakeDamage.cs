using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    private struct DeltaParticle
    {
        public Color min, max;
        public int maxParticles;

        public DeltaParticle(Color min_, Color max_, int maxParticles_) {
            min = min_;
            max = max_;
            maxParticles = maxParticles_;
        }
    }

    Transform  particlePool;

    public ParticleSystem ParticleExplosion;

    [SerializeField] float damageBullet;

    DeltaParticle hittingWall, hittingEnemy, killingEnemy;

    // Start is called before the first frame update
    void Start()
    {
        particlePool = GameObject.Find("ParticlesPool").transform;

        hittingWall = new DeltaParticle(new Color(0, 0, 0), new Color(13f / 255, 13f / 255, 13f / 255), 20);
        hittingEnemy = new DeltaParticle(new Color(60f / 255, 13f / 255, 13f / 255), new Color(150f / 255, 13f / 255, 13f / 255), 30);
        killingEnemy = new DeltaParticle(new Color(235f / 255, 13f / 255, 13f / 255), new Color(255f / 255, 13f / 255, 13f / 255), 50);
    }

    public void damage(in RaycastHit hit)
    {
        EnemyLife eLife = hit.transform.gameObject.GetComponent<EnemyLife>();
        if (eLife)
        {
            if (eLife.damageEnemy(damageBullet))  //Si muere el enemigo
                changeParticleExplosion(killingEnemy);
            else
                changeParticleExplosion(hittingEnemy);
        }
        else
            changeParticleExplosion(hittingWall);

        if (ParticleExplosion) Destroy(Instantiate(ParticleExplosion, hit.point, transform.rotation, particlePool), 0.5f);
        //FrenaBala f = hit.collider.gameObject.GetComponent<FrenaBala>();
        //if (f != null) damage -= f.GetDamageReduction();
    }

    private void changeParticleExplosion(in DeltaParticle delta)
    {
        if (!ParticleExplosion) return;
        var main = ParticleExplosion.main;
            var startColor = main.startColor;
                startColor.colorMin = delta.min;
                startColor.colorMax = delta.max;
            main.startColor = startColor;
        var emission = ParticleExplosion.emission;
            var burst = emission.GetBurst(0);
                burst.count = delta.maxParticles;
            emission.SetBurst(0, burst);
    }
}
