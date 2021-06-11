using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class WeaponController : MonoBehaviour
{
    [SerializeField] int balasPorDisparo, balasPorMinuto, cargador;
    [SerializeField] float tEmpezarDisparar, dispersion;
    [SerializeField] float reloadTime;
    [SerializeField] bool automaticFire;

    Disparar disp;
    float t;

    void Start()
    {
        disp = GetComponent<Disparar>();
        disp.SetAtributosArma(balasPorDisparo, cargador, tEmpezarDisparar, balasPorMinuto, dispersion, automaticFire, reloadTime);
        t = Time.time;
    }

    public void Empezar()
    {
        if (disp && Time.time > t + 60f / balasPorMinuto)
        {
            t = Time.time;
            disp.Empezar();
        }
    }

    public void Parar()
    {
        if (automaticFire) disp.Parar();
    }

    public void Recargar()
    {
        disp.Recargar();
    }

    public float getWeaponPercentage()
    {
        return (disp) ? disp.balasEnCargador / (float)cargador : 1f;
    }

    public bool reloading()
    {
        return disp.reloading;
    }
}
