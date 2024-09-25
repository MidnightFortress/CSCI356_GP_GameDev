using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float waitTime = 2.0f;
    public float respawnTime = 5.0f;
    
    private Vector3 startPostion;
    private Quaternion startRotation;

    public GameObject Player;
    private GameObject Platform;

    // Start is called before the first frame update
    void Start()
    {
        // obtain game object of child - platform
        Platform = transform.GetChild(0).gameObject;

        // store the start/default values of the platform
        startPostion = Platform.transform.position;
        startRotation = Platform.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if collider is attached to player
        if (other.gameObject.CompareTag("Player"))
        {
            // start cooroutine for platform drop and reset
            StartCoroutine(DropPlatform());
        }
    }

    // cooroutine for platfrom drop and then reset
    private IEnumerator DropPlatform()
    {
        // drop the platform
        yield return new WaitForSeconds(waitTime);

        Platform.GetComponent<Rigidbody>().isKinematic = false; // enable physics on platform (gravity)

        //reset the platform
        yield return new WaitForSeconds(respawnTime);

        Platform.GetComponent<Rigidbody>().isKinematic = true;
        Platform.transform.SetPositionAndRotation(startPostion, startRotation);
    }
}
