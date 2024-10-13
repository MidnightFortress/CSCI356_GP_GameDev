using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>(); // List of picked-up weapons
    private int selectedWeapon = 0; // Index of the currently selected weapon

    void Update()
    {
       /* Debug.Log("Weapon count" + weapons.Count);*/

        if (weapons.Count == 0) return; // No weapons to switch if none have been picked up

        // Handle mouse scroll for weapon switching
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f) 
        {
            selectedWeapon = (selectedWeapon + 1) % weapons.Count;
        }
        else if (scroll < 0f) 
        {
            selectedWeapon = (selectedWeapon - 1 + weapons.Count) % weapons.Count;
        }
        // if statment to scroll the index, uses the mouse scroll

      
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count > 0)
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
        {
            selectedWeapon = 1;
        }
        // if statement can swap weapons via 1, 2

        SelectWeapon();
    }

    void SelectWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            /*Debug.Log(weapons[i].name);*/
            if (i == selectedWeapon)
            {
                weapons[i].SetActive(true);
                EnableWeaponScript(weapons[i]); 
                // enable the script from the selected index
            }
            else
            {
                weapons[i].SetActive(false);
                DisableWeaponScript(weapons[i]); 
                //Debug.Log("Disabled");
                // disable teh script from the selected index

            }
        }
    }

    void EnableWeaponScript(GameObject weapon)
    {
            Camera mainCamera = Camera.main; 
        if (weapon.CompareTag("HandGun")) 
        {
            HandGun handGunScript = mainCamera.GetComponent<HandGun>(); 
            // getting teh selected script

            if (handGunScript != null)
            {
                handGunScript.enabled = true;
                //Debug.Log("Hand Enabled");
            }
            // enabling and disabling

        }
        else if (weapon.CompareTag("MachineGun")) 
        {
            
            MachineGun machineGunScript = mainCamera.GetComponent<MachineGun>();
            // getting teh selected script

            if (machineGunScript != null)
            {
                machineGunScript.enabled = true;
                //Debug.Log("Mach Enabled");
            }
            // enabling the selected script
        }
        // had to use a tag to find the selected gun
    }

    void DisableWeaponScript(GameObject weapon)
    {

        Camera mainCamera = Camera.main; 
        if (weapon.CompareTag("HandGun")) 
        {
           
            HandGun handGunScript = mainCamera.GetComponent<HandGun>();
            // same as above

            if (handGunScript != null)
            {
                handGunScript.enabled = false;
                //Debug.Log("Hand Dis");
            }
            // disabling script
        }
        else if (weapon.CompareTag("MachineGun")) 
        {
            
            MachineGun machineGunScript = mainCamera.GetComponent<MachineGun>();

            if (machineGunScript != null)
            {
                machineGunScript.enabled = false;
                //Debug.Log("mac Dis");
            }

            // read above
        }
    }

    public void AddWeapon(GameObject newWeapon)
    {
        if (!weapons.Contains(newWeapon))
        {
            weapons.Add(newWeapon);
            //Debug.Log("Weapon count" + weapons.Count);
            if (weapons.Count == 1)
            {
                selectedWeapon = 0;
                SelectWeapon();
            }
            // this script adds the passed game object to the scripts
        }
    }
}

