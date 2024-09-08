using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject player;
    public GameObject ladder;
    bool canPress = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Near Button!");
        if (other.gameObject == player)
        {
            if (Input.GetKeyDown(KeyCode.E) && canPress)
            {
                Debug.Log("button pressed");
                StartCoroutine(activate());
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
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
    }

    IEnumerator activate()
    {
        activateButton();
        transform.position = transform.position + new Vector3(0,0,0.03f);
        canPress = false;

        ladder.GetComponent<BoxCollider>().enabled = true;      // enable box collider
        ladder.GetComponent<Rigidbody>().useGravity = true;     // set ladder to use gravity

        yield return new WaitForSeconds(2);

        transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        transform.position = transform.position - new Vector3(0, 0, 0.03f);
        ladder.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;      // enable box collider
        ladder.GetComponent<Rigidbody>().isKinematic = true;    // ladder not affected by forces
        canPress = true;
    }

}
