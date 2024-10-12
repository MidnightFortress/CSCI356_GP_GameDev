using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Droids : MonoBehaviour
{

    [SerializeField] private float detectionRange = 40f;
    [SerializeField] private float shootDelay = .5f;
    [SerializeField] private GameObject critObject;
    [SerializeField] private GameObject roationObject;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float laserSpeed = 30f;
    [SerializeField] private float returnRotationSpeed = 2f;
    [SerializeField] private float helth = 100f;
    [SerializeField] private int DroidDamage = 10;
    [SerializeField] private float critModifier = 2f;
    [SerializeField] private ParticleSystem explosionEffect;

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
        if (distoryed)  // don't run update code if droid is destroyed
            return;

        if (helth <= 0)     // destory droid if health depleted
        {
            distoryed = true;
            StartCoroutine(Explode());
        }
        else
        {
            DetectPlayer();     // target and attack player

            if (isPlayerInRange && !hasShot)
            {
                StartCoroutine(ShootLaser());
            }
            else if (!isPlayerInRange && !hasShot)
            {
                ReturnToOriginalRoation();
            }
        }


        //if (helth <= 0) 
        //{
        //    if (!distoryed)
        //    {
        //        distoryed = true;
        //        explosionEffect.Play();
        //        roationObject.SetActive(false);
        //        Destroy(this);
        //    }
        //}
        //else
        //{
        //    DetectPlayer();

        //    if (isPlayerInRange && !hasShot)
        //    {
        //        StartCoroutine(ShootLaser());
        //    }
        //    else if (!isPlayerInRange && !hasShot)
        //    {
        //        ReturnToOriginalRoation();
        //    }
        //}


    }

    // cooroutine to run explosion effect on droid destroy
    public IEnumerator Explode()
    {
        explosionEffect.Play();
        
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
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
            helth -= damage * critModifier;
        }
        else
        {
            helth -= damage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(roationObject.transform.position, detectionRange);
        Gizmos.DrawWireSphere(roationObject.transform.position, detectionRange);
    }
}
