using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWeaponController : MonoBehaviour
{
    public WeaponController wc { get; private set; }
    NPCWeaponSwitcher ws;

    private void Start()
    {
        ws = GetComponent<NPCWeaponSwitcher>();
    }

    public void shoot()
    {
        wc.Empezar();
    }

    public void stop()
    {
        wc.Parar();
    }

    public void reload()
    {
        wc.Recargar();
    }

    public void changeWeapon()
    {
        ws.changeWeapon();
    }

    public void changeWeapon(string weapon)
    {
        if(wc.gameObject.name != weapon)
            ws.changeWeapon();
    }

    public void setWeapon(WeaponController wc_)
    {
        if(wc) 
            wc.Parar();
        wc = wc_;
    }
}