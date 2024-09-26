using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]   // force RB dependency

public class DropLadderBehaviour : MonoBehaviour
{
    Rigidbody objRigigBody;
    Collider objCollider;

    [SerializeField] PlaySFX playSound;

    private void Awake()
    {
        objRigigBody = gameObject.GetComponent<Rigidbody>();
        objCollider = gameObject.GetComponent<Collider>();

        // progrmatically setup the rigid body component
        objRigigBody.mass = 1.5f;
        objRigigBody.useGravity = false;
        objRigigBody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    public void Activate()
    {
        objRigigBody.useGravity = true;     // set ladder to use gravity
        objCollider.enabled = true;         // enable box collider

        if (playSound.GetAudioClip() != null)
        {
            StartCoroutine(DelayAudio());     // play drop ladder sfx after 0.8 sec delay
        }

        Invoke(nameof(SetLadderKinematic), 3);    // wait 3 seconds then call kinematic function 
    }

    public IEnumerator DelayAudio()
    {
        yield return new WaitForSeconds(0.5f);  // delay playing audio by 0.5 sec

        playSound.PlaySoundOnce();
    }


    public void SetLadderKinematic()
    {
        objRigigBody.isKinematic = true;
    }
}
