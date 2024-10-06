using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droids : MonoBehaviour
{

    public float detectionRange = 10f;
    public float shootDelay = 2f;
    public GameObject critObject;
    public GameObject roationObject;
    public GameObject laserPrefab;
    public Transform shootPoint;
    public float laserSpeed = 10f;
    public float returnRotationSpeed = 2f;
    public float helth = 100f;
    public float DroidDamage = 10f;
    public float genralDamage = 10f;
    public float spefifcDamage = 20f;
    public ParticleSystem explosionEffect;
    
    private bool distoryed = false;
    private Transform player;
    private bool isPlayerInRange = false;
    private bool hasShot = false;
    private Quaternion oririnalRotatuion;



    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        oririnalRotatuion = roationObject.transform.rotation;
    }

    void Update()
    {
        if (helth <= 0)
        {
            if (!distoryed)
            {
                distoryed = true;
                explosionEffect.Play();
                roationObject.SetActive(false);
                Destroy(this);
            }
        }
        else
        {
            DetectPlayer();

            if (isPlayerInRange && !hasShot)
            {
                StartCoroutine(ShootLaser());
            }
            else if (!isPlayerInRange && !hasShot)
            {
                ReturnToOriginalRoation();
            }
        }


    }

    private void ReturnToOriginalRoation()
    {
        roationObject.transform.rotation = Quaternion.Lerp(roationObject.transform.rotation, oririnalRotatuion, Time.deltaTime * returnRotationSpeed);
    }

    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(roationObject.transform.position, player.position);

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
        Vector3 direction = player.position - roationObject.transform.position;
        Quaternion roation = Quaternion.LookRotation(direction);
        roationObject.transform.rotation = Quaternion.Lerp(roationObject.transform.rotation, roation, Time.deltaTime * 20f);
    }

    IEnumerator ShootLaser()
    {
        hasShot = true;
        yield return new WaitForSeconds(shootDelay);

        GameObject laser = Instantiate(laserPrefab, shootPoint.position, shootPoint.rotation);
        laser.GetComponent<Laser>().setDirection(shootPoint.forward, laserSpeed, DroidDamage);
        hasShot = false;
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        if (isCrit)
        {
            helth -= damage * spefifcDamage;
        }
        else
        {
            helth -= damage * genralDamage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(roationObject.transform.position, detectionRange);
        Gizmos.DrawWireSphere(roationObject.transform.position, detectionRange);
    }
}
