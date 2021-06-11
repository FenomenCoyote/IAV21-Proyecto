using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] KeyCode switchKey = KeyCode.E;
    WeaponController wc;
    PlayerWeaponSwitcher ws;

    private void Start()
    {
        ws = GetComponent<PlayerWeaponSwitcher>();
    }

    void Update()
    {
        if (wc != null)
        {
            if (Input.GetButtonDown("Fire1")) wc.Empezar();
            else if (Input.GetButtonUp("Fire1")) wc.Parar();
            else if (Input.GetKeyDown(KeyCode.R)) wc.Recargar();
        }
        if(ws != null)
        {
            if (Input.GetKeyDown(switchKey)) ws.changeWeapon();
        }
    }

    public void setWeapon(WeaponController wc_)
    {
        if(wc) 
            wc.Parar();
        wc = wc_;
    }

    public bool reloading()
    {
        return wc.reloading();
    }
}