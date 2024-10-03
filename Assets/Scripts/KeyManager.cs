using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private string currentKey; // stores current key held

    private void Awake()
    {
        // default to no key on start
        currentKey = "None";
    }

    public void SetKey(string key)
    {
        this.currentKey = key;

        Debug.Log("Key has been set to " + key);
    }

    public string GetKey()
    {
        return currentKey;
    }
}