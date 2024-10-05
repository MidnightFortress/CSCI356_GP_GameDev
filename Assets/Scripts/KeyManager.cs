using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    //private string currentKey;

    private List<string> keys = new List<string>();  // store as strings instantiate as required

    private void Awake()
    {
        // default to no key on start
        //currentKey = "None";
    }

    public void AddKey(string key)
    {
        //this.currentKey = key;

        keys.Add(key);

        Debug.Log("Added the " + key + " to inventory");
    }

    public void SetKeyAtIndex(int keyIndex, string key)
    {
        keys[keyIndex] = key;
    }

    public string GetKey(int index)
    {
        return keys[index];
    }

    public int GetKeyIndex(string key)
    {
        return keys.IndexOf(key);
    }

    public bool HasKey(string key)
    {
        return keys.Contains(key);
    }
}