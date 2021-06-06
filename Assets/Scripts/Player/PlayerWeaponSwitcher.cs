using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSwitcher : MonoBehaviour
{
    private WeaponController[] weapons;

    PlayerWeaponController pShoot;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        pShoot = GetComponent<PlayerWeaponController>();
        index = 0;
        weapons = GetComponentsInChildren<WeaponController>(true);
        if(pShoot != null)
            pShoot.setWeapon(weapons[index]);
    }

    public void changeWeapon()
    {
        weapons[index].gameObject.SetActive(false);
        index = (index + 1) % weapons.Length;
        activateWeapon();
    }

    void activateWeapon()
    {
        weapons[index].gameObject.SetActive(true);
        if (pShoot != null) 
            pShoot.setWeapon(weapons[index]);   
    }
}
