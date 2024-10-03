using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertKey : MonoBehaviour
{
    [SerializeField] float castOffset = 1f;
    [SerializeField] float castRadius = 2f;

    private KeyInventory keyInventory;
    public GameObject keyObject;
    public GameObject keyHole;

    private void Start()
    {
        keyInventory = GetComponent<KeyInventory>();    // get ref to key inventory
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && keyInventory.GetKey() != "None") // if player has key
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, castOffset, 0), castRadius, Vector3.forward);

            // check all ray cast hits 
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("KeyHole")) // look for keyhole tag
                {
                    Vector3 pos = keyHole.transform.position;
                    Quaternion rot = keyHole.transform.rotation;

                    // if keyhole found insert the key
                    Instantiate(keyObject, pos, rot);                // create key prefab
                    //keyInventory.SetKey("None");
                    //hit.collider.gameObject.GetComponent<DoorTrigger>().Interact();
                }
            }
        }
    }
}
