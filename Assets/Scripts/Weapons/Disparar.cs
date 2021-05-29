using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour {

    [SerializeField] float maxDistance, muzzleLightIntensity = 1f;

    Transform shootingPoint;

    AudioSource source;
    Light muzzleLight;
    Light flashLight;
    float flashLightIntensity;

    MakeDamage mkdmg;
    public int balasEnCargador { get; private set; }

    int balasPorDisparo, balasPorMinuto, cargador;
    float tEmpezarDisparar, dispersion;
    bool automaticFire;

    float reloadTime;

    void Start()
    {
        shootingPoint = transform.GetChild(0);

        source = GetComponent<AudioSource>();

        muzzleLight = GetComponentsInChildren<Light>()[0];
        flashLight = GetComponentsInChildren<Light>()[1];
        flashLightIntensity = flashLight.intensity;

        mkdmg = GetComponent<MakeDamage>();
    }

    public void Empezar()
    {
        if (balasEnCargador <= 0)
            return;
        if (automaticFire) InvokeRepeating("InstantiateBullet", tEmpezarDisparar, 60f / balasPorMinuto);
        else Invoke("InstantiateBullet", tEmpezarDisparar);
    }

    public void Parar()
    {
        CancelInvoke("InstantiateBullet");
        stopMuzzleLight();
    }

    public void Recargar()
    {
        if (flashLight.intensity == 0 || balasEnCargador == cargador)
            return;
        flashLight.intensity = 0;
        balasEnCargador = 0;
        Parar();
        Invoke("Reload", reloadTime);
    }

    public void SetAtributosArma(int nBalas, int carg, float tCarga, int cadencia, float disp, bool atF, float tReload)
    {
        balasPorDisparo = nBalas;
        cargador = carg;
        balasEnCargador = carg;
        tEmpezarDisparar = tCarga;
        balasPorMinuto = cadencia;
        dispersion = disp;
        automaticFire = atF;
        reloadTime = tReload;
    }

    private void InstantiateBullet()
    {
        if (balasEnCargador > 0) {
            shoot();
            makeEffects();
        }
    }

    private void Reload()
    {
        balasEnCargador = cargador;
        flashLight.intensity = flashLightIntensity;
    }

    private void makeEffects()
    {
        source.Play();
        if (muzzleLight) {
            muzzleLight.intensity = muzzleLightIntensity;
            Invoke("stopMuzzleLight", 0.05f);
        }
    }

    private void shoot()
    {
        for (int i = 0; i < balasPorDisparo; i++) {
            RaycastHit hit;
            //float damage = damageBala;
            Vector3 dir = gameObject.transform.forward;
            dir += new Vector3(Random.Range(-dispersion, dispersion), Random.Range(-dispersion, dispersion), Random.Range(-dispersion, dispersion));

            if (Physics.Raycast(shootingPoint.position, dir, out hit, maxDistance)) {
                mkdmg.damage(hit);
            }

        }
        --balasEnCargador;
        if(balasEnCargador == 0) {
            Recargar();
        }
    }

 
    private void stopMuzzleLight()
    {
        if (muzzleLight) muzzleLight.intensity = 0;
    }
}