using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBlock : MonoBehaviour
{
    public GameObject dropBlock;    // store ref to block to drop

    private void OnTriggerEnter(Collider other)
    {
        // check play enters trigger
        if (other.gameObject.CompareTag("Player"))
        {
            // enable gravity on drop block
            dropBlock.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
