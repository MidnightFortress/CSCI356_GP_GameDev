using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : MonoBehaviour
{
    public GameObject dropBlock;    // store ref to block to drop
    private PlaySFX playSound;

    bool audioPlayed = false;

    private void Awake()
    {
        // get ref to sfx script
        playSound = dropBlock.GetComponent<PlaySFX>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // check play enters trigger
        if (other.gameObject.CompareTag("Player"))
        {
            // enable gravity on drop block
            dropBlock.GetComponent<Rigidbody>().useGravity = true;

            if (!audioPlayed && playSound != null)   // play sfx once only
            {
                // play stone sfx
                playSound.PlaySoundOnce();
                audioPlayed = true;
            }
            else
            {
                Debug.Log("No audio component present!");
            }
        }
    }
}
