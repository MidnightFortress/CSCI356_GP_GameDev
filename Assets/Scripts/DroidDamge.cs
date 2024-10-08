using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//DroidDamge droid = hitObject.GetComponent<DroidDamge>();
//droid.TakeDamage(10, false); 
// fist is the base damge given to droid 2nd is if it is crit
//crit dame value is worked out by the droid script it just needs to know if it is crit and the base damage
public class DroidDamge : MonoBehaviour
{
    [SerializeField] private Droids parentDroid;

    public void TakeDamage(int damage, bool isCrit)
    {
        parentDroid.TakeDamage(damage, isCrit);
    }
}
