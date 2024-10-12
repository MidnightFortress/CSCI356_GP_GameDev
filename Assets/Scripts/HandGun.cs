using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGun : MonoBehaviour
{
    private Camera cam;     // stores camera component
    public int gDamage = 50;

    // Start is called before the first frame update
    void Start()
    {
        // gets the GameObject's camera component
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // on left mouse button click once
        if (Input.GetMouseButtonDown(0))
        {
            // get point in the middle of the screen
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // create a ray from the point in the direction of the camera
            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit; // stores ray intersection information

            // ray cast will obtain hit information if it intersects anything
            if (Physics.Raycast(ray, out hit))
            {
                // get the GameObject that was hit
                GameObject hitObject = hit.transform.gameObject;

                // check if droid
                if (hitObject.CompareTag("droid"))
                {
                    GameObject droid = hitObject.transform.root.gameObject;
                    droid.GetComponent<Droids>().TakeDamage(gDamage, false);

                    Debug.Log(droid.name);
                }

                //Shootable shootable = hitObject.GetComponent<Shootable>();

                //if (shootable != null)
                //{
                //    shootable.takeDamage(gDamage);      // reduce object damage from hit
                //    shootable.reduceScale(gDamage);            // reduce size from hit
                //}
            }

            // start projectile coroutine
            StartCoroutine(Projectile(hit.point));
        }
    }

    // instantiate bullet projectile
    public IEnumerator Projectile(Vector3 pos)
    {
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        bullet.transform.position = pos;

        yield return new WaitForSeconds(0.3f);

        Destroy(bullet);
    }
}
