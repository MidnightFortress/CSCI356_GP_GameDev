using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : MonoBehaviour
{
    public GameObject dropBlock;            // store ref to block to drop
    [SerializeField] PlaySFX playSound;     // get ref to PlaySFX script component

    bool audioPlayed = false;
    private Vector3 startPos;

    private void Awake()
    {
        // get block start position
        startPos = dropBlock.transform.position;
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

    public void ResetBlock()
    {
        if (dropBlock.transform.position != startPos)
        {
            // disable gravity
            dropBlock.GetComponent<Rigidbody>().useGravity = false;

            // reset position
            dropBlock.transform.position = startPos;

            // reset sound played
            audioPlayed = false;
        }
    }
}
