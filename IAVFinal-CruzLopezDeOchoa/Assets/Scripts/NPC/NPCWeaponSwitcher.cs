using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class NPCWeaponSwitcher : MonoBehaviour
{
    private WeaponController[] weapons;

    NPCWeaponController npcShoot;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        npcShoot = GetComponent<NPCWeaponController>();
        index = 0;
        weapons = GetComponentsInChildren<WeaponController>(true);
        if(npcShoot != null)
            npcShoot.setWeapon(weapons[index]);
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
        if (npcShoot != null)
            npcShoot.setWeapon(weapons[index]);   
    }
}
