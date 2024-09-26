using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseZipline : MonoBehaviour
{
    [SerializeField] float castOffset = 1f;
    [SerializeField] float castRadius = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, castOffset, 0), castRadius, Vector3.up);

            // check all ray cast hits 
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("Zipline")) // look for zipline tag
                {
                    // if zipline found call the start zipline function
                    hit.collider.GetComponent<Zipline>().StartZipline(gameObject);
                }
            }
        }
    }
}
