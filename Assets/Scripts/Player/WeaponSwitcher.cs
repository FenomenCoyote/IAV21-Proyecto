using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] KeyCode switchKey;

    private WeaponController[] weapons;

    PlayerShoot pShoot;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        pShoot = GetComponent<PlayerShoot>();
        index = 0;
        weapons = GetComponentsInChildren<WeaponController>(true);
        pShoot.setWeapon(weapons[index]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(switchKey)) {
            changeWeapon();
        }
    }

    void changeWeapon()
    {
        weapons[index].gameObject.SetActive(false);
        index = (index + 1) % weapons.Length;
        activateWeapon();
    }

    void activateWeapon()
    {
        weapons[index].gameObject.SetActive(true);
        pShoot.setWeapon(weapons[index]);   
    }
}
