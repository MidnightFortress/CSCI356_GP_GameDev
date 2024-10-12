using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    private Camera cam;             // stores camera component
    public float fireRate = 0.2f;   // pause time between firing
    public float spread = 10.0f;    // radius offset from target centre
    private bool canShoot = true;   // disable/enable firing
    public int mgDamage = 1;        // machine gun damage inflicted

    // Start is called before the first frame update
    void Start()
    {
        // gets the GameObject's camera component
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // on left mouse button click held down
        if (Input.GetMouseButton(0))
        {
            if (canShoot) // avoids multiple coroutine calls
            {
                StartCoroutine(Shoot());
            }
        }
    }

    // coroutine to controll automatic firing
    public IEnumerator Shoot()
    {
        // get point in the middle of the screen
        Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

        // add randomness to target hit area
        point += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);

        // create a ray from the point in the direction of the camera
        Ray ray = cam.ScreenPointToRay(point);

        RaycastHit hit; // stores ray intersection information

        // ray cast will obtain hit information if it intersects anything
        if (Physics.Raycast(ray, out hit))
        {
            // get the GameObject that was hit
            GameObject hitObject = hit.transform.gameObject;

            // check if droid
            //Shootable shootable = hitObject.GetComponent<Shootable>();

            //if (shootable != null)
            //{
            //    shootable.takeDamage(mgDamage);      // reduce object health from hit damage
            //    shootable.reduceScale(mgDamage);            // reduce size from hit damage
            //}
        }

        // instantiate sphere projectile, scale and move to point of impact
        GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        bullet.transform.position = hit.point;

        canShoot = false;   // disables ability to shoot

        yield return new WaitForSeconds(fireRate);  // delays automatic firing

        // destroy projectile
        Destroy(bullet);
        // reset canShoot
        canShoot = true;    // enables ability to shoot
    }
}