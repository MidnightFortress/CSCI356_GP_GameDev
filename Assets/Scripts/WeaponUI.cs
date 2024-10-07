using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public string currentWeapon = "None";
    public int currentAmmo = 0;

    private Text weaponText;
    private Text ammoText;

    // Start is called before the first frame update
    void Start()
    {
        weaponText = GameObject.Find("WeaponName").GetComponent<Text>();
        ammoText = GameObject.Find("AmmoCounter").GetComponent<Text>();
    }

    void OnGUI()
    {
        weaponText.text = currentWeapon;
        ammoText.text = currentAmmo.ToString();
    }

    void changeWeapon(string weapon)
    {
        currentWeapon = weapon;
    }

    void changeAmmo(int ammoCount)
    {
        currentAmmo = ammoCount;
    }
}
