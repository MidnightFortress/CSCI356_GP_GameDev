using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : MonoBehaviour
{
    public GameObject dropBlock;    // store ref to block to drop
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip stoneSound;
    bool audioPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        // check play enters trigger
        if (other.gameObject.CompareTag("Player"))
        {
            // enable gravity on drop block
            dropBlock.GetComponent<Rigidbody>().useGravity = true;

            if (!audioPlayed)   // play sfx once only
            {
                // play stone sfx
                soundSource.PlayOneShot(stoneSound);
                audioPlayed = true;
            }
        }
    }
}
