using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject player;
    public GameObject activateObject;
    public Camera objectCam;
    bool canPress = true;

    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip ladderDropSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        //  Debug.Log("Near Button!");

        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && canPress)
            {
                Debug.Log("button pressed");
                StartCoroutine(activate());
            }
            //else if (Input.GetKeyDown(KeyCode.Q))
            //{
            //    transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            //}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void activateButton()
    {
        // change button to green color to represent activation
        transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        canPress = false;   // can not press button while button activated
    }

    IEnumerator activate()
    {
        activateButton();
        //transform.position = transform.position + new Vector3(0,0,0.03f);

        if (objectCam != null)
        {
            objectCam.GetComponent<Camera>().enabled = true;    // enable ladder cam
        }
        
        activateObject.GetComponent<Rigidbody>().useGravity = true;     // set ladder to use gravity
        activateObject.GetComponent<BoxCollider>().enabled = true;      // enable box collider

        Invoke("PlaySound", 0.8f);     // play drop ladder sfx after 2 sec delay
        
        yield return new WaitForSeconds(3);

        //transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        //transform.position = transform.position - new Vector3(0, 0, 0.03f);
        //ladder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;      // enable box collider
        activateObject.GetComponent<Rigidbody>().isKinematic = true;    // ladder not affected by forces
        canPress = true;

        objectCam.GetComponent<Camera>().enabled = false;    // disable ladder cam -> return to player
    }

    void PlaySound()
    {
        soundSource.PlayOneShot(ladderDropSound);   // play ladder drop sound
    }

}
