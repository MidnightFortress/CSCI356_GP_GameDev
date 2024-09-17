using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moster1 : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float shootDelay = 2f;
    public GameObject laserPrefab;
    public Transform shootPoint;
    public float laserSpeed = 10f;
    public float returnRotationSpeed = 2f;

    private bool isPlayerInRange = false;
    private bool hasShot = false;
    private Quaternion oririnalRotatuion;



    void Start(){
        oririnalRotatuion = transform.rotation;
    }
    void Update()
    {
        DetectPlayer();

        if (isPlayerInRange && !hasShot)
        {
            StartCoroutine(ShootLaser());
        }
        else if (!isPlayerInRange && !hasShot){
            ReturnToOriginalRoation();
        }

    }

    private void ReturnToOriginalRoation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, oririnalRotatuion,Time.deltaTime  * returnRotationSpeed);
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
            LookAtPlayer();
        }
        else
        {
            isPlayerInRange = false;
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Quaternion roation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, roation, Time.deltaTime * 20f);
    }

    IEnumerator ShootLaser()
    {
        hasShot = true;
        yield return new WaitForSeconds(shootDelay);

        GameObject laser = Instantiate(laserPrefab, shootPoint.position, shootPoint.rotation);
        laser.GetComponent<Laser>().setDirection(shootPoint.forward, laserSpeed); 
        hasShot = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }



}
