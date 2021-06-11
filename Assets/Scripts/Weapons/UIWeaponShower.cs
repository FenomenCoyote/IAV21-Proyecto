using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Game related
/// </summary>
public class UIWeaponShower : MonoBehaviour
{
    [SerializeField] Text text;
    Disparar weapon;

    private void Start()
    {
        weapon = GetComponent<Disparar>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Ammo: " + weapon.balasEnCargador + "\nWeapon: " + weapon.name;
    }
}
