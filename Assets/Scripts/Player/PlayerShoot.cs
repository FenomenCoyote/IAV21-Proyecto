using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    WeaponController wc;

    void Update()
    {
        if (wc != null)
        {
            if (Input.GetButtonDown("Fire1")) wc.Empezar();
            else if (Input.GetButtonUp("Fire1")) wc.Parar();
            else if (Input.GetKeyDown(KeyCode.R)) wc.Recargar();
        }
    }

    public void setWeapon(WeaponController wc_)
    {
        if(wc) 
            wc.Parar();
        wc = wc_;
    }
}