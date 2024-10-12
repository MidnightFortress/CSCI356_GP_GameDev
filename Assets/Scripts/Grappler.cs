using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] private Transform grappleRopeTransform;

    private GameObject grappleHookText;
    
    public GameObject player;
    public GameObject point;
    
    public float maxRange = 10f;
    private FPSInput fpsInput;
    private Vector3 grappleMeasure;
    //public GameObject mainCamera;
    private Camera cam;
    public float stopDistance = 1.0f;
    private float stopPoint;
    private float grappleRopeLength;
    private Vector3 grappleLocation;
    private GameObject hitObject;
    private bool inrange = false;


    void Start()
    {
        GameObject grappleShooter = GameObject.FindWithTag("Grapple-rope");
        grappleRopeTransform = grappleShooter.transform;

        player = GameObject.FindWithTag("Player");
        cam = GetComponent<Camera>();
        Debug.Log(cam);

        Debug.Log(player);
        fpsInput = player.GetComponent<FPSInput>();
        Debug.Log(fpsInput);

        grappleHookText = GameObject.Find("GrappleHookText");
        Debug.Log(grappleHookText + "found");
        grappleHookText.SetActive(false);

    }

    private void OnGUI()
    {
        if (inrange)
        {
            //Debug.Log(grappleHookText);
            grappleHookText.SetActive(true);
        }
        else
            grappleHookText?.SetActive(false);
    }

    public void GrappleStarter()
    {
        grappleRopeTransform.LookAt(grappleLocation);

        grappleRopeLength += 30f * Time.deltaTime;
        grappleRopeTransform.localScale = new Vector3(1, 1, grappleRopeLength);

        if(grappleRopeLength >= Vector3.Distance(transform.position, grappleLocation))
        {

            fpsInput.state = FPSInput.State.Grappling;
            //Debug.Log(hitObject);
            StartCoroutine(Grapple(hitObject.transform));
        }
        

    }

    public void GrappleReady()
    {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);

            // create a ray from the point in the direction of the camera
            Ray ray = cam.ScreenPointToRay(point);

            RaycastHit hit; // stores ray intersection information

            if (Physics.Raycast(ray, out hit, maxRange))
            {
                
                // get the GameObject that was hit
                hitObject = hit.transform.gameObject;

                //Debug.Log("Thing hit");

                if (hitObject.CompareTag("Grapple-able"))
                {
                    inrange = true;
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {

                        //Debug.Log("Correct thing hit");
                        grappleLocation = hitObject.transform.position;
                        grappleRopeLength = 0f;
                        fpsInput.StateChange(FPSInput.State.GrappleStart);
                        inrange = false;
                        //Debug.Log("Start reeling");
                    }
                    
                }
                else
                    inrange = false;
                
            }
            else
                inrange = false;
    }

    private IEnumerator Grapple(Transform specOrb)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = specOrb.position;
        
        grappleMeasure = (endPosition - startPosition).normalized;
        
        Vector3 stopPoint = endPosition - grappleMeasure * stopDistance;

        while (Vector3.Distance(player.transform.position, stopPoint) > 0.01f){
            grappleRopeTransform.LookAt(grappleLocation);
            grappleRopeLength -= 10f * Time.deltaTime;
            grappleRopeTransform.localScale = new Vector3(1, 1, grappleRopeLength);

            player.transform.position = Vector3.MoveTowards(player.transform.position, stopPoint, 10 * Time.deltaTime);
            //Debug.Log(player.transform.position + "player loc");
            //Debug.Log(stopPoint + "end location");
            yield return null;
            
        }
        Debug.Log("coroutine ending");
        grappleRopeTransform.localScale = new Vector3(1, 1, 0);
        fpsInput.StateChange(FPSInput.State.Normal);
        Debug.Log(fpsInput.state);
        yield return null;
    }

}
